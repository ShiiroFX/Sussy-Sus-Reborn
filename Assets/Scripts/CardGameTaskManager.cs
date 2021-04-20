using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameTaskManager : MonoBehaviour
{
    public string CurrentInput { get; private set; }
    public bool IsResetting { get; private set; }

    [SerializeField]
    private float WaitTimeInSeconds = 0.5f;

    private Text _text;

    private void Start()
    {
        _text = transform.Find("Input").Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        CurrentInput = _text.text;
    }

    private IEnumerator resetInput(string message=null)
    {
        IsResetting = true;
        if (message != null)
        {
            _text.text = message;
            yield return new WaitForSeconds(WaitTimeInSeconds);
        }

        _text.text = "";
        CurrentInput = "";
        IsResetting = false;
    }

    public void TaskSuccessful()
    {
        StartCoroutine(resetInput("Correct"));
    }

    public void TaskUnsuccessful()
    {
        StartCoroutine(resetInput("Incorrect"));
    }
}
