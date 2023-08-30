using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBasedScore : MonoBehaviour
{
    public static TimeBasedScore instance;
    [SerializeField] private Text timeText;
    public float score {get; private set;}
    private int minutes;
    private float seconds;

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
        seconds += Time.deltaTime;
        score  += Time.deltaTime;
        timeText.text = String.Format("{0:00}:{1:00}", minutes, (int) seconds);

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
