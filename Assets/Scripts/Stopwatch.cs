using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI.ProceduralImage;
using System;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI leftTimeText;
    [SerializeField] ProceduralImage timerFill;
    [SerializeField] string textAnimationType = "bounce";
    [SerializeField] GameObject runButton = null;

    [SerializeField] float initialLeftTime = 10;
    float currentLeftTime;

    public bool isRunning = false;
    public bool hasDone = false;

    void Start()
    {
        currentLeftTime = initialLeftTime;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void RunStopwatch(string inputTitleText, float inputTime)
    {
        titleText.text = inputTitleText;
        initialLeftTime = inputTime;
        currentLeftTime = initialLeftTime;

        titleText.gameObject.SetActive(true);
        leftTimeText.gameObject.SetActive(true);
        runButton.SetActive(false);

        UpdateTimeText();
        AnimateTitleText();

        isRunning = true;
    }

    
    void UpdateTimer()
    {
        if(!isRunning) return;

        if(currentLeftTime > 0)
        {
            UpdateTimeText();
        }
        else
        {
            hasDone = true;
            FindObjectOfType<Character>().GetComponent<Animator>().SetBool("isWorking", false);
        }
    }

    void UpdateTimeText()
    {
        currentLeftTime = Mathf.Clamp(currentLeftTime - Time.deltaTime, 0f, 999999f);
        int leftTimeCeiled = (int)Math.Ceiling(currentLeftTime);

        float minutes = Mathf.FloorToInt(leftTimeCeiled / 60);
        float seconds = Mathf.FloorToInt(leftTimeCeiled % 60);

        leftTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerFill.fillAmount = (initialLeftTime - currentLeftTime) / initialLeftTime;
    }

    void AnimateTitleText()
    {
        titleText.text = "<" + textAnimationType + ">" + titleText.text + "</" + textAnimationType + ">";
    }

    public void Initialize()
    {
        titleText.text = "";
        leftTimeText.text = "";
        timerFill.fillAmount = 0;
        runButton.SetActive(true);
    }
}
