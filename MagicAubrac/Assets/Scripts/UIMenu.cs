using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [Scene,SerializeField]
    private string _menuScene;
    [Scene, SerializeField]
    private string _mainScene;

    public void Restart()
    {
        SceneManager.LoadScene(_mainScene);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(_menuScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
