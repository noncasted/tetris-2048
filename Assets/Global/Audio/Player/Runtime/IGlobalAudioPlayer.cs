using UnityEngine;

namespace Global.Audio.Player.Runtime
{
    public interface IGlobalAudioPlayer
    {
        void PlaySound(AudioClip clip);
        void PlayLoopMusic(AudioClip clip);
    }
}