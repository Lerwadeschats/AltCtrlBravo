using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IIMEngine.Music;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] string baseMusic;
    [SerializeField] string loopMusic;
    [SerializeField] string croudMusic;
    [SerializeField] string windMusic;
    [SerializeField] AudioClip baseMusicClip;
    MusicsPlaylistManager playlistManager;

    // Start is called before the first frame update
    void Start()
    {
        playlistManager = MusicsGlobals.PlaylistManager;
        playlistManager.PlayMusic(croudMusic);
        playlistManager.PlayMusic(windMusic);
        StartCoroutine(musicPlay());
    }
    IEnumerator musicPlay()
    {
        playlistManager.PlayMusic(baseMusic);
        yield return new WaitForSeconds(baseMusicClip.length);
        playlistManager.StopMusic(baseMusic);
        playlistManager.PlayMusic(loopMusic);
        yield return null;
    }
}
