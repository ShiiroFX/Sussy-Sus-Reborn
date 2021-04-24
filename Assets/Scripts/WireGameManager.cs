using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireGameManager : MonoBehaviour
{

    public List<Color> _wireColors = new List<Color>();

    public List<Wire> _leftWires = new List<Wire>();
    public List<Wire> _rightWires = new List<Wire>();

    public Wire CurrentHoveredWire;
    public Wire CurrentDraggedWire;

    private List<Color> _availableColors;
    private List<int> _availableLeftWireIndex;
    private List<int> _availableRightWireIndex;
    private int _success;

    public event EventHandler OnSuccess;
    public event EventHandler OnFail;

    private void Start()
    {
        _availableColors = new List<Color>(_wireColors);
        _availableLeftWireIndex = new List<int>();
        _availableRightWireIndex = new List<int>();

        for(int i = 0; i < _leftWires.Count; i++)
        {
            _availableLeftWireIndex.Add(i);
        }

        for (int i = 0; i < _rightWires.Count; i++)
        {
            _availableRightWireIndex.Add(i);
        }

        while(_availableColors.Count > 0 && _availableLeftWireIndex.Count > 0 && _availableRightWireIndex.Count > 0)
        {
            Color pickedColor = _availableColors[UnityEngine.Random.Range(0, _availableColors.Count)];
            int pickedLeftWireIndex = UnityEngine.Random.Range(0, _availableLeftWireIndex.Count);
            int pickedRightWireIndex = UnityEngine.Random.Range(0, _availableRightWireIndex.Count);

            Wire leftWire = _leftWires[_availableLeftWireIndex[pickedLeftWireIndex]];
            Wire rightWire = _rightWires[_availableRightWireIndex[pickedRightWireIndex]];

            leftWire.SetColor(pickedColor);
            rightWire.SetColor(pickedColor);

            leftWire.OnSuccess += (_, __) =>
            {
                _success++;
                if (_success >= _leftWires.Count)
                {
                    TaskSuccess();
                }
            };

            _availableColors.Remove(pickedColor);
            _availableLeftWireIndex.RemoveAt(pickedLeftWireIndex);
            _availableRightWireIndex.RemoveAt(pickedRightWireIndex);
        }
    }

    private void TaskSuccess()
    {
        OnSuccess?.Invoke();
        Debug.Log("Wire task successful");
    }

    private void TaskFail()
    {
        OnFail?.Invoke();
        Debug.Log("Wire task failed");
    }
}
