using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    public bool isAblePause;
    
    private void Awake()
    {
        isAblePause = true;
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);    
        }
    }

    private void Update() {
        if (pauseScreen == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isAblePause)
        {
            if(pauseScreen.activeInHierarchy) {
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            } else {
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }  
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SwitchScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit() {
        Application.Quit();
    }
}

