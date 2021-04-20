using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNumGen : MonoBehaviour
{

    private Text num;
    private void Awake()
    {
        num = GetComponent<Text>();
        do
        {
            num.text = Random.Range(10000, 100000).ToString();
        }
        while (num.text.Contains("0"));
        
    }
}
