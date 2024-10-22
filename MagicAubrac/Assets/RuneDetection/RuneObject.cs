using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rune")]
public class RuneObject : ScriptableObject
{
    [Header("Useful informations")]
    public Texture2D _runeDetectionMap;
    public Sprite _runeSprite;

    [Header("Semi garbage informations")]
    public string _name;
    [SerializeField] string _description;
    [SerializeField] string _signification;
}
