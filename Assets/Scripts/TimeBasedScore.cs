using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBasedScore : MonoBehaviour
{
    [SerializeField] private Text timeText;
    private float score;
    private int minutes;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        minutes = 0;
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        seconds  = (int) score;
        timeText.text = String.Format("{0:00}:{1:00}", minutes, seconds);

        if (seconds > 59)
        {
            minutes++;
            seconds = 0;
        }

        if (minutes > 59)
        {
            Time.timeScale = 0;
            Debug.Log("Times up");
        }
    }
}
