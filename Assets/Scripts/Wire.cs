using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Color CustomerColor;
    public event EventHandler OnSuccess;

    private Transform _mainWire;
    private RawImage _image;
    private LineRenderer _lineRenderer;
    private bool _isLeftWire;
    private WireGameManager _wireGameManager;
    private bool _isSuccess;

    private Canvas _canvas;

    private bool _isDragStarted;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "wireMain")
            {
                _mainWire = child;
                _image = child.GetComponent<RawImage>();
                break;
            }
        }

        _isLeftWire = _mainWire.position.x < 0;

        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _wireGameManager = GetComponentInParent<WireGameManager>();
    }

    private void Update()
    {
        if (_isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                _canvas.worldCamera,
                out movePos
            );

            _lineRenderer.SetPosition(0, _mainWire.position);
            _lineRenderer.SetPosition(1, _canvas.transform.TransformPoint(movePos));
        }
        else
        {
            if (!_isSuccess)
            {
                // Reset
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }
        }

        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(
                _mainWire.transform as RectTransform,
                Input.mousePosition,
                _canvas.worldCamera
            );

        if(isHovered)
        {
            _wireGameManager.CurrentHoveredWire = this;
        }
    }

    public void SetColor(Color color)
    {
        _image.color = color;
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
        CustomerColor = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isLeftWire || _isSuccess) return;
        _isDragStarted = true;
        _wireGameManager.CurrentDraggedWire = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if(_wireGameManager.CurrentDraggedWire != null && _wireGameManager.CurrentHoveredWire != this)
        {
            _isSuccess = _wireGameManager.CurrentHoveredWire.CustomerColor.Equals(CustomerColor);
            if (_isSuccess)
            {
                OnSuccess?.Invoke(this, EventArgs.Empty);
            }
 
        }

        _isDragStarted = false;
        _wireGameManager.CurrentDraggedWire = null;
    }

}
