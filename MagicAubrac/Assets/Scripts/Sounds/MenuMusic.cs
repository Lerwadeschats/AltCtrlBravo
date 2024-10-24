using IIMEngine.Music;
using IIMEngine.SFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] string _nameMenuMusic;

    private void Start()
    {
        MusicsGlobals.PlaylistManager.PlayMusic(_nameMenuMusic);
    }
}
