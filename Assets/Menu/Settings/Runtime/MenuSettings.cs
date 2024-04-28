using Cysharp.Threading.Tasks;
using Global.Audio.Player.Abstract;
using Global.Audio.Player.Runtime;
using Global.Inputs.Constraints.Abstract;
using Global.Publisher.Abstract.DataStorages;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Menu.Common;
using Menu.Settings.Abstract;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.Settings.Runtime
{
    [DisallowMultipleComponent]
    public class MenuSettings : MonoBehaviour, IMenuSettings, IScopeLoadAsyncListener
    {
        private const float _sliderParts = 50f;
        
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;

        [SerializeField] private MenuStateTransition _transition;

        private IDataStorage _dataStorage;
        private IGlobalVolume _globalVolume;

        public IUIConstraints Constraints => new UIConstraints(InputConstraints.Game);

        public string Name => "Menu_Settings";

        [Inject]
        private void Construct(IDataStorage dataStorage, IGlobalVolume globalVolume)
        {
            _globalVolume = globalVolume;
            _dataStorage = dataStorage;
        }

        public async UniTask OnLoadedAsync()
        {
            var save = await _dataStorage.GetEntry<VolumeSave>();
            
            _musicSlider.value = save.MusicVolume;
            _soundSlider.value = save.SoundVolume;
            
            _globalVolume.SetVolume(save.MusicVolume / _sliderParts, save.SoundVolume / _sliderParts);
        }

        public void OnEntered(IStateHandle handle)
        {
            _transition.Transit();
            handle.VisibilityLifetime.ListenTerminate(_transition.Exit);

            _musicSlider.onValueChanged.Listen(handle.VisibilityLifetime, OnMusicChanged);
            _soundSlider.onValueChanged.Listen(handle.VisibilityLifetime, OnSoundChanged);
        }

        private void OnMusicChanged(float value)
        {
            _globalVolume.SetVolume(_musicSlider.value / _sliderParts, _soundSlider.value / _sliderParts);
            Save();
        }

        private void OnSoundChanged(float value)
        {
            _globalVolume.SetVolume(_musicSlider.value / _sliderParts, _soundSlider.value / _sliderParts);
            Save();
        }

        private void Save()
        {
            _dataStorage.Save(new VolumeSave()
            {
                MusicVolume = _musicSlider.value,
                SoundVolume = _soundSlider.value
            }).Forget();
        }
    }
}