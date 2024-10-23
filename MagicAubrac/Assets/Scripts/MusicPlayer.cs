using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IIMEngine.Music;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] AudioClip baseMusic;
    [SerializeField] AudioClip loopMusic;
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
        playlistManager.PlayMusic(baseMusic.name);
        yield return new WaitForSeconds(baseMusic.length);
        playlistManager.PlayMusic(loopMusic.name);
        yield return null;
    }
}
