using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IIMEngine.Music;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] string baseMusic;
    [SerializeField] string loopMusic;
    [SerializeField] AudioClip baseMusicClip;
    MusicsPlaylistManager playlistManager;
    private void Awake()
    {
        playlistManager = FindObjectOfType<MusicsPlaylistManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(musicPlay());
    }
    IEnumerator musicPlay()
    {
        playlistManager.PlayMusic(baseMusic);
        yield return new WaitForSeconds(baseMusicClip.length);
        playlistManager.PlayMusic(loopMusic);
        yield return null;
    }
}
