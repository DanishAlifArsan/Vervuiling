using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    
    private void Awake()
    {
        pauseScreen.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy) {
                pauseScreen.SetActive(false);
            } else {
                pauseScreen.SetActive(true);
            }  
        }

        if (pauseScreen.activeInHierarchy)
        {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
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

