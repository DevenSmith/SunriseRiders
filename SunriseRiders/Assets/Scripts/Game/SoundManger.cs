using System;
using System.Collections.Generic;
using Devens;
using UnityEngine;

namespace Game
{
    public class SoundManger : PausableMonoBehavior
    {
        public static SoundManger Instance;

        private Stack<AudioSource> availableAudioSources;
        private List<AudioSource> loopingAudioSources;
        private List<AudioSource> playingAudioSources;

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            availableAudioSources = new Stack<AudioSource>();
            loopingAudioSources = new List<AudioSource>();
            playingAudioSources = new List<AudioSource>();
        }

        public AudioSource PlaySound(SoundClipSO sound)
        {
           var source = GetSource();

           if (sound.isLooping)
           {
               source.loop = true;
               loopingAudioSources.Add(source);
           }
           else
           {
               playingAudioSources.Add(source);
           }

           source.clip = sound.Value;
           source.Play();
           return source;
        }

        private AudioSource GetSource()
        {
            if (availableAudioSources.Count > 0)
            {
                return availableAudioSources.Pop();
            }

            var obj = new GameObject("audioSource");
            obj.transform.SetParent(transform);
            var source = obj.AddComponent<AudioSource>();
            return source;
        }

        private void Update()
        {
            for (int i = 0; i < playingAudioSources.Count; i++)
            {
                if (!playingAudioSources[i].isPlaying)
                {
                    availableAudioSources.Push(playingAudioSources[i]);
                    playingAudioSources.Remove(playingAudioSources[i]);
                    i--;
                }
            }
        }
    }
}