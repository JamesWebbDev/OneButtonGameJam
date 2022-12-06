using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;

    [SerializeField] float Timer;
    private bool startTimer = false;

    public GameObject gameOverPanel;
    
    void Start()
    {
        gameOverPanel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        puzzleTimer();
    }

    public void SetTimer(bool result)
    {
        startTimer = result;
    }


    public void puzzleTimer()
    {
        if (!startTimer)
            return;

        Timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(Timer / 60f);
        int seconds = Mathf.FloorToInt(Timer % 60f);
        int milliseconds = Mathf.FloorToInt((Timer * 100f) % 100f);
        TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");

        if (Timer <=0)
        {
            Timer = 0;
            gameOverPanel.SetActive(true);            
        }
    }
}
