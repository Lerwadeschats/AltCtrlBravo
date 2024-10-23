using System.Collections.Generic;
using UnityEngine;

namespace IIMEngine.SFX
{
    public class SFXsManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        public static SFXsManager Instance { get; private set; }

        [Header("Bank")]
        [SerializeField] private SFXsBank _bank;

        [Header("Audio Source")]
        [SerializeField] private AudioSource _audioSourceTemplate = null;

        private Dictionary<string, List<SFXInstance>> _poolInstancesDict = new Dictionary<string, List<SFXInstance>>();
        private Dictionary<string, List<SFXInstance>> _playingInstancesDict = new Dictionary<string, List<SFXInstance>>();
        private Dictionary<string, SFXData> _datasDict = new Dictionary<string, SFXData>();
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            Instance = this;
            Init();
        }

        private void Update()
        {
            _CleanupNonPlayingInstances();
        }

        public void Init()
        {
            _InitDatasDict();
            _InitPoolDict();
            _InitPlayingInstancesDict();
            _LoadAllAudiosData();
        }

        private void _CleanupNonPlayingInstances()
        {
            //Loop over all playing instance
                //If SFXInstance audiosource is playing
                    //Destroy Instance if DestroyWhenComplete is true
                    //Reset Instance and move it to pool is DestroyWhenComplete is false
            List<SFXInstance> instanceToRemove = new List<SFXInstance>();
            foreach(var sfxInstances in _playingInstancesDict.Values)
            {
                foreach (SFXInstance sfxInstance in sfxInstances)
                {
                    if (!sfxInstance.AudioSource.isPlaying)
                    {
                        
                        if (sfxInstance.DestroyWhenComplete)
                        {
                            Destroy(sfxInstance.GameObject);
                        }
                        else
                        {
                            if (_poolInstancesDict.ContainsKey(sfxInstance.SFXName))
                            {
                                _poolInstancesDict[sfxInstance.SFXName].Add(sfxInstance);
                            }
                        }
                        instanceToRemove.Add(sfxInstance);
                    }
                }
            }
            for (int i = instanceToRemove.Count - 1; i >= 0; i--)
            {
                _playingInstancesDict[instanceToRemove[i].SFXName].Remove(instanceToRemove[i]);
            }
        }

        private void _InitDatasDict()
        {
            //Loop over all SFXsData inside bank and fill _datasDict dictionary
            foreach (SFXData sfx in _bank.SFXDatasList)
            {
                _datasDict[sfx.Name] = sfx;
            }
        }

        private void _InitPoolDict()
        {
            //Loop over all SFXsData inside bank
            //Create multiple SFXsInstance using SizeMax property inside SFXData
            //And store it into _poolInstancesDict
            foreach (var sfx in _bank.SFXDatasList)
            {
                var sfxInstances = new List<SFXInstance>(sfx.SizeMax);
                for (int i = 0; i < sfx.SizeMax; i++)
                {
                    var audioInstance = Instantiate(_audioSourceTemplate, _audioSourceTemplate.transform.parent.transform);
                    audioInstance.clip = sfx.Clip;
                    sfxInstances.Add(new SFXInstance() 
                    {  
                        SFXName = sfx.Name,
                        AudioSource = audioInstance,
                        Transform = audioInstance.transform,
                        GameObject = audioInstance.gameObject
                    }); 
                }
                _poolInstancesDict.Add(sfx.Name,sfxInstances);
            }
        }
        
        private void _InitPlayingInstancesDict()
        {
            //Loop over all SFXsData inside bank
            //Init PlayingInstances Dictionary using SizeMax property inside SFXData
            foreach (var sfx in _bank.SFXDatasList)
            {
                _playingInstancesDict.Add(sfx.Name, new List<SFXInstance>());
            }
        }

        public SFXInstance PlaySound(string name)
        {
            SFXInstance sfxInstance = _PikUpInstanceFromPool(name);
            if (sfxInstance == null) return null;
            sfxInstance.Transform.position = Vector2.zero;
            //Forcing SetActive for a gameobject containing an AudioSource replay the sound inside
            sfxInstance.GameObject.SetActive(false);
            sfxInstance.GameObject.SetActive(true);
            return sfxInstance;
        }

        private SFXInstance _PikUpInstanceFromPool(string name)
        {
            //If an Instance is available
            //Remove sfx instance from Pool Dictionary
            //Add sfx instance from PlayingSFX Dictionary
            //return sfx instance
            //Else
            //Check Overflow operation
            //If Overflow is cancel
            //Do nothing, cancel means we do not play sounds if there is no sounds available in the pool
            //If Overflow is ReuseOldest
            //Find sfx instance from PlayingSFX Dictionary
            //If Overflow is Create And Destroy
            //Create sfx instance using SFXData
            //Mark sfx instance as Destroyable (DestroyOnComplete = true)
            //Add Found sfx instance to PlayingSFX Dictionary
            //return Instance

            //Try to find an SFXInstance inside Pool Dictionary
            if (_poolInstancesDict.ContainsKey(name) && _poolInstancesDict[name].Count > 0)
            {
                SFXInstance instance = _poolInstancesDict[name][0];
                _poolInstancesDict[name].Remove(instance);
                if (_playingInstancesDict.ContainsKey(name))
                {
                    Debug.Log(name);
                    _playingInstancesDict[name].Add(instance);
                }
                return instance;
            } 
            else if (_datasDict.ContainsKey(name))
            {
                SFXInstance sfxInstance = null;
                switch (_datasDict[name].OverflowOperation)
                {
                    case SFXOverflowOperation.ReuseOldest:
                        if (_playingInstancesDict.ContainsKey(name) && _playingInstancesDict[name].Count > 0)
                        {
                            sfxInstance = _playingInstancesDict[name][0];
                            _playingInstancesDict[name].RemoveAt(0);
                        }
                        break;
                    case SFXOverflowOperation.CreateAndDestroy:
                        var audioInstance = Instantiate(_audioSourceTemplate, _audioSourceTemplate.transform.parent.transform);
                        audioInstance.clip = _datasDict[name].Clip;
                        sfxInstance = new SFXInstance()
                        {
                            SFXName = name,
                            AudioSource = audioInstance,
                            Transform = audioInstance.transform,
                            GameObject = audioInstance.gameObject,
                            DestroyWhenComplete = true
                        };
                        break;
                }
                if (sfxInstance != null)
                {
                    _playingInstancesDict[sfxInstance.SFXName].Add(sfxInstance);
                }
                return sfxInstance;
            }
            return null;
        }
        public void StopSound(string name)
        {
            if (_playingInstancesDict.ContainsKey(name))
            {
                _poolInstancesDict[name][0].AudioSource.Stop();
            }
        }
        private void _LoadAllAudiosData()
        {
            //AudioClips are not load by default
            //We need to load it using LoadAudioData
            //See : https://docs.unity3d.com/ScriptReference/AudioClip.LoadAudioData.html
            foreach (var sfxData in _bank.SFXDatasList)
            {
                sfxData.Clip.LoadAudioData();
            }
        }
    }
}