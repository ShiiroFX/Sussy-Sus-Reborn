using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardGameManager : MonoBehaviour
{
    private CardGameCardManager _card;
    private CardGameTaskManager _task;
    public event EventHandler OnSuccess;
    public event EventHandler OnFail;

    private void Awake()
    {
        _card = transform.Find("Card").GetComponent<CardGameCardManager>();
        _task = transform.Find("Task").GetComponent<CardGameTaskManager>();
    }

    private void Update()
    {
        string currInput = _task.CurrentInput;
        string answer = _card.Answer;

        if (_task.IsResetting || currInput == null || currInput.Length == 0 || answer == null || answer.Length == 0) return;

        if (currInput.Length >= answer.Length)
        {
            if (currInput.Equals(answer))
            {
                TaskSuccess();
            }
            else
            {
                TaskFail();
            }
        }
    }

    private void TaskSuccess()
    {
        _task.TaskSuccessful();
        OnSuccess?.Invoke();
    }

    private void TaskFail()
    {
        _task.TaskUnsuccessful();
        OnFail?.Invoke();
    }
}
