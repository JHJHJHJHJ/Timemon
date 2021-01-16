using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameObject questBubble = null;
    [SerializeField] Button createButton = null;
    [SerializeField] TMP_InputField titleInputField = null;
    [SerializeField] TMP_InputField minInputField = null;
    [SerializeField] TMP_InputField secInputField = null;

    Character character;
    Stopwatch stopwatch;

    void Start()
    {
        
    }

    void Update()
    {
        CheckQuestDone();
    }

    public void OpenQuestBubble()
    {
        createButton.gameObject.SetActive(false);

        titleInputField.gameObject.SetActive(true);
        minInputField.gameObject.SetActive(true);
        secInputField.gameObject.SetActive(true);

        questBubble.SetActive(true);
    }

    public void RunStopWatch() // button에서 실행
    {
        stopwatch = FindObjectOfType<Stopwatch>();
        stopwatch.RunStopwatch(titleInputField.text, GetInputTime());
        titleInputField.gameObject.SetActive(false);
        minInputField.gameObject.SetActive(false);
        secInputField.gameObject.SetActive(false);

        FindObjectOfType<Character>().GetComponent<Animator>().SetBool("isWorking", true);
    }

    float GetInputTime()
    {
        float min;
        float sec;

        if(minInputField.text == "") min = 0f;
        else min = Int32.Parse(minInputField.text);

        if(secInputField.text == "") sec = 0f;
        else sec = Int32.Parse(secInputField.text);

        float time = min * 60 + sec;
        return time;
    }

    void CheckQuestDone()
    {
        if(!stopwatch) return;

        if(stopwatch.isRunning && stopwatch.hasDone)
        {
            stopwatch.isRunning = false;
            stopwatch.hasDone = false;

            questBubble.GetComponent<Animator>().SetTrigger("hasDone");
            questBubble.GetComponent<Button>().enabled = true;
        }
    }

    public void CloseQuestBubble()
    {
        stopwatch.Initialize();
        questBubble.SetActive(false);
        createButton.gameObject.SetActive(true);

        titleInputField.text = "";
        minInputField.text = "";
        secInputField.text = "";

        questBubble.GetComponent<Button>().enabled = false;
    }
}
