using System.Collections.Generic;
using UnityEngine;

namespace Devens
{
    [CreateAssetMenu (menuName = "Devens/SoundClipSO")]
    public class SoundClipSO : ScriptableObject
    {
        [SerializeField] private List<AudioClip> clips;
        [SerializeField] private bool looping = false;
        public float volumeLevel = 1.0f;

        public bool isLooping
        {
            get => looping;
        }
        
        public SoundClipSO() { }

        public AudioClip Value
        {
            get
            {
                if (clips.Count > 0)
                {
                    return clips[Random.Range(0, clips.Count)];
                }

                return null;
            }
        }
    }
}
