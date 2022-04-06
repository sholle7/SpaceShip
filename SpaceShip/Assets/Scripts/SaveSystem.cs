using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSystem : MonoBehaviour
{ 
    private bool flag;
    private void Awake()
    {
        flag = false;
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (!flag)
            {
                ShowHighScore();
                flag = true;
            }
        }
        else
        {
            if(flag)flag=false; 
        }
    }


    public static void ShowHighScore()
    {
        var nameFirst = GameObject.Find("NameFirst").GetComponent<TMP_Text>();
        var nameSecond = GameObject.Find("NameSecond").GetComponent<TMP_Text>();
        var nameThird = GameObject.Find("NameThird").GetComponent<TMP_Text>();
        var scoreFirst = GameObject.Find("ScoreFirst").GetComponent<TMP_Text>();
        var scoreSecond = GameObject.Find("ScoreSecond").GetComponent<TMP_Text>();
        var scoreThird = GameObject.Find("ScoreThird").GetComponent<TMP_Text>();
       

        nameFirst.text = PlayerPrefs.GetString("HighScore1Name");
        scoreFirst.text = PlayerPrefs.GetInt("HighScore1Score").ToString();

        nameSecond.text = PlayerPrefs.GetString("HighScore2Name");
        scoreSecond.text = PlayerPrefs.GetInt("HighScore2Score").ToString();

        nameThird.text = PlayerPrefs.GetString("HighScore3Name");
        scoreThird.text = PlayerPrefs.GetInt("HighScore3Score").ToString();
    }
}
