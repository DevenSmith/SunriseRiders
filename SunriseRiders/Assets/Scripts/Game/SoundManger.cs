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

        private bool paused = false;
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
           source.gameObject.SetActive(true);
           source.loop = false;
           
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
           source.volume = sound.volumeLevel;
           source.Play();
           return source;
        }

        public void StopAudio(AudioSource source)
        {
            source.Stop();
            playingAudioSources.Remove(source);
            availableAudioSources.Push(source);
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
            if (Paused)
            {
                if (!paused)
                {
                    foreach (var audio in playingAudioSources)
                    {
                        audio.Pause();
                    }

                    paused = true;
                }

                return;
            }
            else
            {
                if (paused)
                {
                    paused = false;
                    
                    foreach (var audio in playingAudioSources)
                    {
                        audio.Play();
                    }
                }
            }
        
            
            for (int i = 0; i < playingAudioSources.Count; i++)
            {
                if (!playingAudioSources[i].isPlaying)
                {
                    playingAudioSources[i].gameObject.SetActive(false);
                    availableAudioSources.Push(playingAudioSources[i]);
                    playingAudioSources.Remove(playingAudioSources[i]);
                    i--;
                }
            }
        }
    }
}
