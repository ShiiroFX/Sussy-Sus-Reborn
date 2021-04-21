using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragManager : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Canvas _canvas;
    private Vector3 _originalPosition;

    void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _originalPosition = transform.position;
    }


    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(

            _canvas.transform as RectTransform,
            eventData.position,
            _canvas.worldCamera,
            out Vector2 pos
        );
        transform.position = _canvas.transform.TransformPoint(pos);
    }

    public void OnEndDrag(PointerEventData eventData) => transform.position = _originalPosition;
}
