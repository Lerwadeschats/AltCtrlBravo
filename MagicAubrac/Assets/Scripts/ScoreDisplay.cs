using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text perfect;
    [SerializeField] TMP_Text badRune;
    [SerializeField] TMP_Text badDrink;

    private void Start()
    {
        changeScoreP(0);
        changeScoreR(0);
        changeScoreD(0);
    }
    public void changeScoreP(int x)
    {
        perfect.text = "   " + x;
    }
    public void changeScoreR(int x)
    {
        badRune.text = "   " + x;
    }
    public void changeScoreD(int x)
    {
        badDrink.text = "   " + x;
    }
}
