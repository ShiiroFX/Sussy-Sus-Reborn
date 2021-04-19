using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNumGen : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = Random.Range(10000, 100000).ToString();
    }
}
