using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRunes : MonoBehaviour
{
    [SerializeField] UIRune _uiRune;

    public void UpdateUIRunes(RuneObject rune)
    {
        print(rune);
        _uiRune.UpdateRuneUI(rune);
    }

    public void ResetRune()
    {
        _uiRune.UpdateRuneUI(null);
    }

}

