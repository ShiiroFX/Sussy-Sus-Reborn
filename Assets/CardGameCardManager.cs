using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameCardManager : MonoBehaviour
{
    public string Answer { get; private set; }

    private Text _text;

    private void Start()
    {
        _text = transform.Find("Answer").GetComponent<Text>();
    }

    private void Update()
    {
        Answer = _text.text;
    }
}
