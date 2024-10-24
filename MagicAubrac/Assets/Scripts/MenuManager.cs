using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _uiMenu;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private TextMeshProUGUI _scoreText;
    public bool IsInMenu { get; set; } = false;

    private void Awake()
    {
        _uiMenu?.SetActive(false);
        GameManager.MenuManager = this;
    }

    public void Start()
    {
        GameManager.LoseWinManager.OnLose += OnLose;
    }

    private void OnLose()
    {
        IsInMenu = true;
        if (_scoreText != null)
        {
            _scoreText.text = _shaker?.CompletedFull.ToString();
        }
        _uiMenu?.SetActive(true);
    }
}
