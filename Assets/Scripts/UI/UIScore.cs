using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    private float scoreTime;
    public Text highscoreTime;
    public Text TimerText;
    private float CurrentSecTime;
    private float lastSecTime;

    // Start is called before the first frame update
    void Start()
    {
        highscoreTime.text = PlayerPrefs.GetString("Highscore");
        lastSecTime = PlayerPrefs.GetFloat("TimerCheck");
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSecTime += Time.deltaTime;
        scoreTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(scoreTime / 60F);
        int seconds = Mathf.FloorToInt(scoreTime % 60F);
        int milliseconds = Mathf.FloorToInt((scoreTime * 100F) % 100F);
        TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");

        // begin of test button
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UpdateTimeScore();
        }
        // end of test button
    }
    public void UpdateTimeScore()
    {
        PlayerPrefs.SetFloat("TimerCheck", CurrentSecTime);

            if (CurrentSecTime >= lastSecTime)
            {
                PlayerPrefs.SetString("Highscore", TimerText.text);
            }
    }
}
