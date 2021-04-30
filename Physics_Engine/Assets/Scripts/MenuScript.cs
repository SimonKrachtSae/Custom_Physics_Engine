using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
