using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{

    [SerializeField]
    private List<Color> Colors = new List<Color>();
    void Awake()
    {
        RawImage img = GetComponent<RawImage>();
        img.color = Colors[Random.Range(0, Colors.Count)];
    }
}
