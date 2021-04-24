using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour
{
    [SerializeField]
    private float WaitTimeInSeconds = 0.5f;

    [SerializeField]
    private Color SuccesLightUpColor;
    [SerializeField]
    private Color FailLightUpColor;

    private RawImage _success;
    private RawImage _fail;

    private void Start()
    {
        _success = transform.Find("Success").GetComponent<RawImage>();
        _fail = transform.Find("Fail").GetComponent<RawImage>();
    }

    private IEnumerator LightUp(RawImage indicator, Color color)
    {
        var defColor = indicator.color;
        indicator.color = color;

        yield return new WaitForSeconds(WaitTimeInSeconds);

        indicator.color = defColor;
    }

    public void LightSuccess() => StartCoroutine(LightUp(_success, SuccesLightUpColor));
    public void LightFail() => StartCoroutine(LightUp(_fail, FailLightUpColor));
}
