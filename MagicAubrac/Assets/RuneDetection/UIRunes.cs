using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRunes : MonoBehaviour
{
    [SerializeField] UIRune[] _uiRunes;

    public void UpdateUIRunes(List<RuneObject> runes)
    {
        for (int i = 0; i < _uiRunes.Length; i++)
        {
            if (i >= runes.Count)
            {
                _uiRunes[i].UpdateRuneUI(null); 
            }
            else
            {
                _uiRunes[i].UpdateRuneUI(runes[i]);
            }
        }
    }

    public void ResetRunes()
    {
        for (int i = 0; i < _uiRunes.Length; i++)
        {
            _uiRunes[i].UpdateRuneUI(null);
        }
    }

}

