using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    private CardGameCardManager _card;
    private CardGameTaskManager _task;

    private void Start()
    {
        _card = transform.Find("Card").GetComponent<CardGameCardManager>();
        _task = transform.Find("Task").GetComponent<CardGameTaskManager>();
    }

    private void Update()
    {
        string currInput = _task.CurrentInput;
        string answer = _card.Answer;
        
        if(!_task.IsResetting && currInput.Length >= answer.Length)
        {
            if (currInput.Equals(answer))
            {
                _task.TaskSuccessful();
            }
            else
            {
                _task.TaskUnsuccessful();
            }
        }


    }
}
