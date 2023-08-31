using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public Image smoke;
    public int score {get; private set;}
    public int maxScore;
    private float initOpacity;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        initOpacity = smoke.color.a;
    }

    void Update() {
        text.text = score.ToString() + "/" + maxScore.ToString();
    }

    public void ChangeScore(int tokenValue)
    {
        score += tokenValue;
        float opactityModifier = initOpacity / maxScore;
        smoke.color = new Color(smoke.color.r, smoke.color.g, smoke.color.b, smoke.color.a - opactityModifier);
    }
}
