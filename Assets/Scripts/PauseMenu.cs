using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pause;
    public void Pause()
    {
        Time.timeScale = 0f;

        _pause.SetActive(true);
    }
    public void Play()
    {
        Time.timeScale = 1f;

        _pause.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
