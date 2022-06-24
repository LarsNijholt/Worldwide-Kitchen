using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _charSelection;
    [SerializeField] private GameObject _mainMenu;
    public void PlayButton()
    {
        _charSelection.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void SettingsButton()
    {
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
