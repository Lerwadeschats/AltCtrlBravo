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

        private List<MusicInstance> _currentMusicInstances;

        public bool IsPaused { get; private set; } = false;
        
        #endregion

        private void Awake()
        {
            MusicsGlobals.PlaylistManager = this;
            _currentMusicInstances = new List<MusicInstance>();
            _InitInstancesDict();
            _InitDatasDict();
            _LoadAllAudiosData();
        }

        private void Update()
        {
            if (IsPaused) return;
            //if (_currentMusicInstance == null) return;
            if (_currentMusicInstances.Count == 0) return;
            //TODO: Update Current Music Instance

            //Check Music Instance State
            for (int i = _currentMusicInstances.Count - 1; i >= 0; i--)
            {
                MusicInstance musicInstance = _currentMusicInstances[i];
                switch (musicInstance.CurrentState)
                {
                    case MusicInstance.State.Intro:
                        if (!musicInstance.AudioSource.isPlaying)
                        {
                            musicInstance.AudioSource.clip = _musicDataDict[musicInstance.Name].MainClip;
                            musicInstance.AudioSource.loop = _musicDataDict[musicInstance.Name].IsLooping;
                            musicInstance.AudioSource.Play();
                            musicInstance.CurrentState = MusicInstance.State.Loop;
                        }
                        break;
                    case MusicInstance.State.Loop:
                        //_currentMusicInstance.
                        if (!musicInstance.AudioSource.isPlaying &&
                            !_musicDataDict[musicInstance.Name].IsLooping &&
                            _musicDataDict[musicInstance.Name].HasOutro)
                        {
                            musicInstance.AudioSource.loop = false;
                            musicInstance.AudioSource.clip = _musicDataDict[musicInstance.Name].OutroClip;
                            musicInstance.AudioSource.Play();
                            musicInstance.CurrentState = MusicInstance.State.Outro;
                        }
                        break;
                    case MusicInstance.State.Outro:
                        if (!musicInstance.AudioSource.isPlaying)
                        {
                            musicInstance.AudioSource.loop = false;
                            musicInstance.AudioSource.Stop();
                            _currentMusicInstances.Remove(musicInstance);
                        }
                        break;
                }
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
            if (_musicInstancesDict.ContainsKey(name) && _musicInstancesDict[name] != null)
            {
                MusicInstance newMusicInstance = _musicInstancesDict[name];
                newMusicInstance.AudioSource.Play();

                _currentMusicInstances.Add(newMusicInstance);
            }
            //Stop _currentMusicInstance

            //Set _currentMusicInstance with musicInstanceFound

            //Play musicInstanceFound
        }

        public void PauseMusic()
        {
            if (_currentMusicInstances.Count == 0) return;
            //Do nothing if there is no _currentMusicInstance
            //if (_currentMusicInstance == null) return;
            IsPaused = true;
            //Pause _currentMusicInstance AudioSource
            //_currentMusicInstance.AudioSource.Pause();
            foreach (var musicInstance in _currentMusicInstances)
            {
                musicInstance.AudioSource.Pause();
            }
        }

        public void ResumeMusic()
        {
            if (_currentMusicInstances.Count == 0) return;
            //Do nothing if there is no _currentMusicInstance
            //if (_currentMusicInstance == null) return;
            IsPaused = false;
            //Play _currentMusicInstance AudioSource
            //_currentMusicInstance.AudioSource.Play();
            foreach (var musicInstance in _currentMusicInstances)
            {
                musicInstance.AudioSource.Play();
            }
        }

        public void StopMusic(string name)
        {
            ////Do nothing if there is no _currentMusicInstance
            //if (_currentMusicInstance == null) return;
            ////Stop _currentMusicInstance AudioSource
            //_currentMusicInstance.AudioSource.Stop();
            ////Don't forget to remove current reference to _currentMusicInstance
            //_currentMusicInstance = null;

            if (_currentMusicInstances.Count == 0) return;

            if (_musicInstancesDict.ContainsKey(name) && _musicInstancesDict[name] != null && _currentMusicInstances.Contains(_musicInstancesDict[name]))
            {
                int index = _currentMusicInstances.IndexOf(_musicInstancesDict[name]);
                if (index >= 0)
                {
                    MusicInstance musicInstance = _currentMusicInstances[index];
                    musicInstance.AudioSource.Stop();
                    _currentMusicInstances.Remove(musicInstance);
                }
            }
        }
        public void StopAllMusics() 
        {
            if (_currentMusicInstances.Count == 0) return;
            foreach(var musicInstance in _currentMusicInstances)
            {
                musicInstance.AudioSource.Stop();
            }
            _currentMusicInstances.Clear();
        }
    }
}