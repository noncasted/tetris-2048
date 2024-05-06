using Features.Loop.Sounds.Abstract;
using Global.Audio.Player.Runtime;

namespace Features.Loop.Sounds.Runtime
{
    public class GameSounds : IGameSounds
    {
        public GameSounds(IGlobalAudioPlayer player, GameSoundsConfig config)
        {
            _player = player;
            _config = config;
        }

        private readonly IGlobalAudioPlayer _player;
        private readonly GameSoundsConfig _config;

        public void StartBackgroundMusic()
        {
            _player.PlayLoopMusic(_config.Background);
        }

        public void PlayButtonClick()
        {
            _player.PlaySound(_config.ButtonClick);
        }

        public void PlayBlockCombine()
        {
            _player.PlaySound(_config.BlockCombine);
        }

        public void PlayBlockMove()
        {
            _player.PlaySound(_config.BlockMove);
        }
    }
}