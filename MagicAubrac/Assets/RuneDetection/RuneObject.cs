using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rune")]
public class RuneObject : ScriptableObject
{
    [Header("Useful informations")]
    [SerializeField] Texture2D _runeDetectionMap;
    [SerializeField] Sprite _runeSprite;

    [Header("Semi garbage informations")]
    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] string _signification;
    [SerializeField] string _bravo = "Bravo";
}
