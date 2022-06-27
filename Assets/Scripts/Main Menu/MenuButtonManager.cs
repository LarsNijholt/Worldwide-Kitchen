using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _charSelection;
    [SerializeField] private GameObject _mainMenu;

    [SerializeField] private GameObject _muteBtn;
    [SerializeField] private GameObject _unmuteBtn;

    private MusicManager _musicManager;

    private void Start()
    {
        _musicManager = FindObjectOfType<MusicManager>();

        if (!_musicManager.Source.isPlaying) MuteAudio();
    }

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

    public void MuteAudio()
    {
        _muteBtn.SetActive(true);
        _unmuteBtn.SetActive(false);

        _musicManager.Source.Pause();
    }

    public void UnmuteAudio()
    {
        _muteBtn.SetActive(false);
        _unmuteBtn.SetActive(true);

        _musicManager.Source.UnPause();
    }
}
