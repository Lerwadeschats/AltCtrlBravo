using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _uiMenu;
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
        _uiMenu?.SetActive(true);
    }
}
