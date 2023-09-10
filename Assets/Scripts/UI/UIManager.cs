using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject finishUI;

    public bool isAblePause;
    
    private void Awake()
    {
        isAblePause = true;
        if (pauseScreen && finishUI != null)
        {
            pauseScreen.SetActive(false);    
            finishUI.SetActive(false);
        }
    }

    private void Update() {
        if (pauseScreen == null)
        {
            Time.timeScale = 1;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(isAblePause);
        }

        if (pauseScreen.activeInHierarchy || finishUI.activeInHierarchy)
        {
            isAblePause = false;
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
            isAblePause = true;
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume() {
        pauseScreen.SetActive(false);
    }

    public void SwitchScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit() {
        Application.Quit();
    }
}

