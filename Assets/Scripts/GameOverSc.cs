using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSc : MonoBehaviour
{
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
