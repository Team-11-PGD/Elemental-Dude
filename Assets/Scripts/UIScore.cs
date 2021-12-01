using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    private float scoreTime;
    public float highscoreTime;
    public Text TimerText;
    private bool playing = true;
    // Start is called before the first frame update
    void Start()
    {
        TimerText.text = PlayerPrefs.GetString("Highscore");
    }

    // Update is called once per frame
    void Update()
    {
        if (playing == true)
        {
            scoreTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(scoreTime / 60F);
            int seconds = Mathf.FloorToInt(scoreTime % 60F);
            int milliseconds = Mathf.FloorToInt((scoreTime * 100F) % 100F);
            TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }
        Debug.Log(TimerText.text);
        if (Input.GetKeyDown(KeyCode.F))
        {
            playing = false;
            PlayerPrefs.SetString("Highscore",TimerText.text);
        }
         if (Input.GetKeyDown(KeyCode.G))
        {
            playing = true;
        }
        
        
    }
}
