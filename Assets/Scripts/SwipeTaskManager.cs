using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTaskManager : MonoBehaviour
{
    private SwipeController _swipeController;
    private IndicatorController _indicatorController;

    void Awake()
    {
        var cardInput = transform.Find("CardInput");

        _swipeController = cardInput.Find("SwipeArea").GetComponent<SwipeController>();
        _indicatorController = cardInput.Find("Indicators").GetComponent<IndicatorController>();

        _swipeController.OnSuccess += (_, __) => _indicatorController.LightSuccess();
        _swipeController.OnFail += (_, __) => _indicatorController.LightFail();
    }
}
