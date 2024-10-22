using System.Collections.Generic;
using UnityEngine;

namespace IIMEngine.Music
{
    public class MusicsPlaylistManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        
        [SerializeField] private AudioSource _audioSourceTemplate;
        [SerializeField] private MusicsBank _bank;

        private Dictionary<string, MusicInstance> _musicInstancesDict = new Dictionary<string, MusicInstance>();
        private Dictionary<string, MusicData> _musicDataDict = new Dictionary<string, MusicData>();

        private MusicInstance _currentMusicInstance = null;

        public bool IsPaused { get; private set; } = false;
        
        #endregion

        private void Awake()
        {
            MusicsGlobals.PlaylistManager = this;
            
            _InitInstancesDict();
            _InitDatasDict();
            _LoadAllAudiosData();
        }

        private void Update()
        {
            if (IsPaused) return;
            if (_currentMusicInstance == null) return;
            
            //TODO: Update Current Music Instance
            
            //Check Music Instance State
            switch (_currentMusicInstance.CurrentState)
            {
                case MusicInstance.State.Intro:
                    if (!_currentMusicInstance.AudioSource.isPlaying)
                    {
                        _currentMusicInstance.AudioSource.clip = _musicDataDict[_currentMusicInstance.Name].MainClip;
                        _currentMusicInstance.AudioSource.Play();
                        _currentMusicInstance.CurrentState = MusicInstance.State.Loop;
                    }
                    break;
                case MusicInstance.State.Loop:
                    //_currentMusicInstance.
                    if (!_currentMusicInstance.AudioSource.isPlaying &&
                        !_musicDataDict[_currentMusicInstance.Name].IsLooping &&
                        _musicDataDict[_currentMusicInstance.Name].HasOutro)
                    {
                        _currentMusicInstance.AudioSource.clip = _musicDataDict[_currentMusicInstance.Name].OutroClip;
                        _currentMusicInstance.AudioSource.Play();
                        _currentMusicInstance.CurrentState = MusicInstance.State.Outro;
                    }
                    break;
                case MusicInstance.State.Outro:
                    if (!_currentMusicInstance.AudioSource.isPlaying)
                    {
                        _currentMusicInstance.AudioSource.Stop();
                        _currentMusicInstance = null;
                    }
                    break;
            }
            //If State is "Intro"
                //Wait if audio source is playing
                //If not playing
                    //Play Music Loop and set state to Loop
            
            //If State is "Loop"
                //if AudioSource not looping and not playing =>
                //If Music has Outro
                    //Play Music Outro and set state to "Outro"
                    
            //If State is "Outro"
                //if AudioSource not playing
                    //Reset MusicInstance
        }

        private void _InitInstancesDict()
        {
            //Loop over all Music Datas inside Bank and Fill Music Instances into _musicInstancesDict
            foreach (var musicData in _bank.MusicDatas)
            {
                AudioSource audioSource = Instantiate(_audioSourceTemplate, _audioSourceTemplate.transform.parent.transform);
                audioSource.clip =musicData.IntroClip;
                _musicInstancesDict[musicData.Name] = new MusicInstance()
                {
                    Name = musicData.Name,
                    AudioSource = audioSource
                };
            }
        }

        private void _InitDatasDict()
        {
            //Loop over all Music Datas inside Bank and Fill Music Data into _musicDataDict
            foreach (var musicData in _bank.MusicDatas)
            {
                _musicDataDict[musicData.Name] = musicData;
            }
        }

        private void _LoadAllAudiosData()
        {
            //AudioClips are not load by default
            //We need to load it using LoadAudioData
            //See : https://docs.unity3d.com/ScriptReference/AudioClip.LoadAudioData.html
            
            //Music Instances have until 3 audioClip
            //MainClip => always set
            //IntroClip => only set when HasIntro is true
            //OutroClip => only set when HasOutro is true
            foreach(var musicData in _bank.MusicDatas)
            {
                //Not sure if copy of object or not
                musicData.MainClip.LoadAudioData();
                if (musicData.HasIntro)
                {
                    musicData.IntroClip.LoadAudioData();
                }
                if (musicData.HasOutro)
                {
                    musicData.OutroClip.LoadAudioData();
                }
            }
        }

        public void PlayMusic(string name, bool forceReset = false)
        {
            //Find Music Instance From _musicInstancesDict
            //(Be careful to check if there is an instance is found)
            if (_musicInstancesDict[name] != null && _currentMusicInstance != _musicInstancesDict[name])
            {
                _currentMusicInstance?.AudioSource.Stop();
                _currentMusicInstance = _musicInstancesDict[name];
                _currentMusicInstance.AudioSource.Play();
            }
            //Do not replay MusicInstance if _currentMusicInstance is musicInstanceFound
            
            //Stop _currentMusicInstance
            
            //Set _currentMusicInstance with musicInstanceFound
            
            //Play musicInstanceFound
        }

        public void PauseMusic()
        {
            //Do nothing if there is no _currentMusicInstance
            if (_currentMusicInstance == null) return;
            IsPaused = true;
            //Pause _currentMusicInstance AudioSource
            _currentMusicInstance.AudioSource.Pause();
        }

        public void ResumeMusic()
        {
            //Do nothing if there is no _currentMusicInstance
            if (_currentMusicInstance == null) return;
            IsPaused = false;
            //Play _currentMusicInstance AudioSource
            _currentMusicInstance.AudioSource.Play();
        }

        public void StopMusic()
        {
            //Do nothing if there is no _currentMusicInstance
            if (_currentMusicInstance == null) return;
            //Stop _currentMusicInstance AudioSource
            _currentMusicInstance.AudioSource.Stop();
            //Don't forget to remove current reference to _currentMusicInstance
            _currentMusicInstance = null;
        }
    }
}