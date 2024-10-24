using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRune : MonoBehaviour
{
    [SerializeField] Image _image;

    public void UpdateRuneUI(RuneObject rune)
    {
        
        if(rune == null)
        {
            _image.enabled = false;
        }
        else
        {
            _image.enabled = true;
            _image.sprite = rune._runeSprite;
        }
    }


}

