using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace IIMEngine.Music
{
    public class MusicsVolumeFader : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private const string AUDIOMIXER_PARAM_MUSICVOLUME = "MusicVolume";

        [SerializeField] private AudioMixer _audioMixer;
        
        [SerializeField] private float _minVolume = -80f;

        private float _startVolume = 0f;

        public bool IsFadingIn { get; private set; }
        public bool IsFadingOut { get; private set; }

#pragma warning restore 0414

        Coroutine _routineFade;
        #endregion

        private void Awake()
        {
            MusicsGlobals.VolumeFader = this;
            ResetStartVolumeFromAudioMixer();
        }

        public void ResetStartVolumeFromAudioMixer()
        {
            //TODO : Set _startVolume From AudioMixer
            _audioMixer.SetFloat(AUDIOMIXER_PARAM_MUSICVOLUME,_startVolume);
        }

        public void ResetToStartVolume()
        {
            //TODO: Interrupt FadeIn or FadeOut if running
            //TODO: Reset AudioMixer MusicVolume to _startVolume
            _audioMixer.SetFloat(AUDIOMIXER_PARAM_MUSICVOLUME, _startVolume);
        }

        public void FadeIn(float duration)
        {
            //TODO: Interrupt FadeIn or FadeOut if running
            if (IsFadingIn || IsFadingOut)
            {
                IsFadingIn = false;
                IsFadingOut = false;
                StopCoroutine(_routineFade);
                _routineFade = null;
            }
            //TODO: Lerp Value between Current Volume from AudioMixer to _startVolume
            //You can use coroutines if you want
            //Don't Forget to set IsFadingIn while transition is running
            _routineFade = StartCoroutine(RoutineFadeIn(duration));
        }

        IEnumerator RoutineFadeIn(float duration)
        {
            IsFadingIn = true;
            _audioMixer.GetFloat(AUDIOMIXER_PARAM_MUSICVOLUME, out float startVolume);
            float volume;
            float timer = 0f;
            while (timer < duration)
            {
                volume = Mathf.Lerp(startVolume, _startVolume, timer / duration);
                _audioMixer.SetFloat(AUDIOMIXER_PARAM_MUSICVOLUME, volume);
                yield return null;
                timer += Time.deltaTime;
            }
            IsFadingIn = false;
        }

        public void FadeOut(float duration)
        {
            //TODO: Interrupt FadeIn or FadeOut if running
            if (IsFadingIn || IsFadingOut)
            {
                IsFadingIn = false;
                IsFadingOut = false;
                StopCoroutine(_routineFade);
                _routineFade = null;
            }
            //TODO: Lerp Value between Current Volume from AudioMixer to _minVolume
            //You can use coroutines if you want
            //Don't Forget to set IsFadingIn while transition is running
            _routineFade = StartCoroutine(RoutineFadeOut(duration));
        }

        IEnumerator RoutineFadeOut(float duration)
        {
            IsFadingOut = true;
            _audioMixer.GetFloat(AUDIOMIXER_PARAM_MUSICVOLUME, out float startVolume);
            float volume;
            float timer = 0f;
            while (timer < duration)
            {
                volume = Mathf.Lerp(startVolume, _minVolume, timer / duration);
                _audioMixer.SetFloat(AUDIOMIXER_PARAM_MUSICVOLUME, volume);
                yield return null;
                timer += Time.deltaTime;
            }
            IsFadingOut = false;
        }
    }
}