using System;
using System.Collections.Generic;
using System.Linq;
using Project._Scripts.Library.Audio.Scriptable;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Project._Scripts.Library.Audio.Core
{
    public class AudioCore : MonoBehaviour
    {
        #region Components
        private static AudioSource _bgmSource;
        #endregion
        
        #region Fields
        public List<AudioContainer> AudioContainers;
        public static List<AudioContainer> SAudioContainers;
        [Space]
        [Header("PoolObjects")]
        

        private List<AudioData> _audioDatas;
        private static List<AudioData> _sAudioDatas;

        #endregion

        #region Unity Functions

        protected void Awake()
        {
            InitializeAudioContainers();
            InitializeAudioResources();
        }
        #endregion

        #region Initialization
        private void InitializeAudioContainers()
        {
            AudioContainers.ForEach(x =>
            {
                x.AudioSources = new List<AudioSource>();
                var container = new GameObject(
                    name = x.Name)
                {
                    transform =
                    {
                        parent = transform
                    }
                };

                for (int i = 0; i < x.Size; i++)
                {
                    var audioSourceContainer = new GameObject(
                        name = $"{x.Name} {i+1} Source")
                    {
                        transform =
                        {
                            parent = container.transform
                        }
                    };
                    

                    var audioSource = audioSourceContainer.AddComponent<AudioSource>();
                    x.AudioSources.Add(audioSource);
                    audioSource.playOnAwake = x.PlayOnAwake;
                    audioSource.outputAudioMixerGroup = x.Mixer;
                }
            });

            SAudioContainers = new List<AudioContainer>();
            SAudioContainers = AudioContainers;
        }

        private void InitializeAudioResources()
        {
            _audioDatas = Resources.LoadAll<AudioData>($"AudioDatas").ToList();
            _sAudioDatas = _audioDatas;
        }
        #endregion


        #region Audio Interactions
        public static void PlayAudio(string audioName)
        {
            var audioObject = GetAudioByName(audioName);
            
            if (audioObject.AudioClip == null) return;

            //Get the audio source
            AudioSource source = GetAvailableAudioSource(GetMixerGroup(audioName));

            //-----------------------------------AUDIO SETTINGS-----------------------------------------
            source.clip = audioObject.AudioClip;
            source.pitch = 1 + Random.Range(-audioObject.PitchVariation, audioObject.PitchVariation);
            source.volume = audioObject.Volume;
            //------------------------------------------------------------------------------------------
            
            //Play audio
            source.Play();
        }
  
        #endregion

        #region Audio Gathering
        public static AudioMixerGroup GetMixerGroup(string audioName)
        {
            var audioSource = GetAudioByName(audioName);

            AudioContainer container = SAudioContainers.Find(x => x.Name == audioSource.Type.ToString());
            
            return container.Mixer;
        }
        /// <summary>
        /// Returns the audio based on it's name from the given audio list
        /// </summary>
        /// <param name="audioName"></param>
        /// <returns></returns>
        public static AudioData GetAudioByName(string audioName) => _sAudioDatas.Find(x => x.name == audioName);

        /// <summary>
        /// Returns the available audio source channel
        /// </summary>
        /// <param name="mixerGroup"></param>
        /// <returns></returns>
        public static AudioSource GetAvailableAudioSource(AudioMixerGroup mixerGroup)
        {
            AudioContainer audioContainer = SAudioContainers.Find(x => x.Mixer == mixerGroup);

            if (audioContainer.AudioSources.Exists(x => x.isPlaying == false))
            {
                return audioContainer.AudioSources.Find(x => x.isPlaying == false);
            }
            var source = audioContainer.AudioSources.OrderBy(x => x.time).Last();

            source.Stop();
            
            return source;
        }

        #endregion

        #region AudioContainer
        [Serializable]
        public class AudioContainer
        {
            public string Name;
            public AudioMixerGroup Mixer;
            public int Size;
            public bool PlayOnAwake;
            public List<AudioSource> AudioSources { get; set; }
        }
        #endregion
    }
    #region Audio Class

    [Serializable]
    public class Audio
    {
        public string AudioName;
        public AudioData AudioData;
    }
    #endregion
}