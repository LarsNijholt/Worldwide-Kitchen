using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    
    public void PlayButton()
    {
        SceneController.Instance.EnableScene();
    }
    public void SettingsButton()
    {
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
