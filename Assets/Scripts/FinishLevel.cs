using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject finishUI;
    [SerializeField] private Image vidaHolder;
    [SerializeField] private Sprite[] vida;
    [SerializeField] private Image[] bigStars;
    [SerializeField] private Image[] smallStars;
    [SerializeField] private TextMeshProUGUI winningTextHolder;
    private string[] winningText = {"Do you even try?","GOOD JOB!","It was an amazing work!"};
    // Start is called before the first frame update
    void Start()
    {
        finishUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        LevelClear();          
    }

    private void LevelClear() { 
        Time.timeScale = 0;
        finishUI.SetActive(true);
        int counter = 1;
        Color maxOpacity = new Color(1,0.5581461f,0.5320755f,1);
        smallStars[0].color = maxOpacity;
       
        if (ScoreManager.instance.score >= ScoreManager.instance.maxScore)
        {   
            counter++;
            smallStars[1].color = maxOpacity;
        }
        if (TimeBasedScore.instance.score <= TimeBasedScore.instance.minimumTime)
        {
            counter++;
            smallStars[2].color = maxOpacity;
        }

        if (counter == 3)
        {
            winningTextHolder.transform.GetChild(0).gameObject.SetActive(true);
        }
        
        for (int i = 0; i < counter; i++)
        {
            bigStars[i].color = Color.white;
            winningTextHolder.text = winningText[i];
            vidaHolder.sprite = vida[i];
        } 
    }
}
