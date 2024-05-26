using System;
using System.Collections.Generic;
using Devens;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class SoundHelper : MonoBehaviour
    {
        [Serializable]
        public enum PlayTypes
        {
            Random,
            Sequential
        }

        [Tooltip("Play sounds from the list randomly or in a sequence")]
        [SerializeField] public PlayTypes playType;
        public List<SoundClipSO> soundClips;

        private AudioSource loopingAudioSource;
        private int clipIndex = 0;

        [UsedImplicitly]
        public void PlaySound()
        {
            if (loopingAudioSource != null && loopingAudioSource.isPlaying)
            {
                SoundManger.Instance.StopAudio(loopingAudioSource);
            }

            SoundClipSO clipSo;
            if (playType == PlayTypes.Random)
            {
               clipSo = soundClips[Random.Range(0, soundClips.Count)];
            }
            else
            {
                clipSo = soundClips[clipIndex];
               clipIndex++;
               if (clipIndex >= soundClips.Count)
               {
                   clipIndex = 0;
               }
            }

            if (clipSo.isLooping)
            {
                loopingAudioSource = SoundManger.Instance.PlaySound(clipSo);
            }
            else
            {
                SoundManger.Instance.PlaySound(clipSo);
            }
        }

        [UsedImplicitly]
        public void PlaySoundDelayed(float delay)
        {
            Invoke(nameof(PlaySound), delay);
        }
        

    }
}
