using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    private float scoreTime;
    public Text highscoreTime;
    public Text TimerText;
    private int timeLastPlayMin;
    private int currentTimerMin;
    private int timeLastPlaySec;
    private int currentTimerSec;

    // Start is called before the first frame update
    void Start()
    {
        highscoreTime.text = PlayerPrefs.GetString("Highscore");
        timeLastPlayMin = PlayerPrefs.GetInt("TimerCheckMin");
        timeLastPlaySec = PlayerPrefs.GetInt("TimerCheckSec");
    }

    // Update is called once per frame
    void Update()
    {
        scoreTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(scoreTime / 60F);
        int seconds = Mathf.FloorToInt(scoreTime % 60F);
        int milliseconds = Mathf.FloorToInt((scoreTime * 100F) % 100F);
        TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        currentTimerMin = minutes;
        currentTimerSec = seconds;
    }
    public void UpdateTimeScore()
    {
        PlayerPrefs.SetInt("TimerCheckMin", currentTimerMin);
        PlayerPrefs.SetInt("TimerCheckSec", currentTimerSec);
        if (currentTimerSec > timeLastPlaySec)
        {
            if (currentTimerMin >= timeLastPlayMin)
            {
                PlayerPrefs.SetString("Highscore", TimerText.text);
            }
        }
    }
}
