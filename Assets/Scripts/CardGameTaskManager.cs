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
    private GenerateButtons _btnGrid;

    private void Start()
    {
        _btnGrid = transform.Find("ButtonGrid").GetComponent<GenerateButtons>();
        _text = transform.Find("Input").Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        CurrentInput = _text.text;
    }

    private IEnumerator ResetInput(string message=null)
    {
        IsResetting = true;
        _btnGrid.CanEnter = false;

        if (message != null)
        {
            _text.text = message;
            yield return new WaitForSeconds(WaitTimeInSeconds);
        }

        _text.text = "";
        CurrentInput = "";

        _btnGrid.CanEnter = true;
        IsResetting = false;
        
    }

    public void TaskSuccessful()
    {
        StartCoroutine(ResetInput("Correct"));
    }

    public void TaskUnsuccessful()
    {
        StartCoroutine(ResetInput("Incorrect"));
    }
}
