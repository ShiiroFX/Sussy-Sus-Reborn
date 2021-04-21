using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    public EventHandler OnSuccess;
    public EventHandler OnFail;

    [SerializeField]
    private int Steps = 10;
    [SerializeField]
    private GameObject SwipeCheckpoint;
    [SerializeField]
    private float DesiredSpeed = 0.08f;
    [SerializeField]
    private float ErrorMargin = 0.03f;

    private float _startTime;
    private float _endTime;

    private List<int> _colliderOrder;

    void Start()
    {
        _colliderOrder = new List<int>();
        var start = transform.Find("Start");
        var end = transform.Find("End");
        
        var w = end.position.x - start.position.x;

        var startX = start.position.x;
        var space = w / Steps;


        for(int i = 0; i < Steps; i++)
        {
            var go = Instantiate(SwipeCheckpoint, new Vector3(startX + (space * i), 1, 0), Quaternion.identity, transform);
            var swipeController = go.GetComponent<SwipeCheckpointCollisionController>();
            var deref = i;

            swipeController.OnCollisionEnter += (a, b) => CheckPointEventHandler(a, b, deref);
        }
    }

    private (bool, SwipeCheckpointCollisionEventArgs) CollidedWithCard(EventArgs args)
    {
        SwipeCheckpointCollisionEventArgs cast = args as SwipeCheckpointCollisionEventArgs;
        Collision2D c = cast.collision;
        return ( c.gameObject.CompareTag("swipeCard"), cast );
    }

    private bool ValidateSpeed()
    {
        var avgSpeed = (_endTime - _startTime) / Steps;
        var (upperBound, lowerBound) = DesiredSpeed.PlusOrMinus(ErrorMargin);
        return avgSpeed.InRange(lowerBound, upperBound);
    }

    private bool ValidateOrder()
    {

        if (_colliderOrder.Count <= 1) return true;

        int last = _colliderOrder[0];
        foreach(var item in _colliderOrder)
        {
            if(item < last)
            {
                return false;
            }

            last = item;
        }

        return true;
    }

    private bool Validate() => _colliderOrder.Count == Steps && ValidateSpeed() && ValidateOrder();


    private void CheckPointEventHandler(object sender, EventArgs rawArgs, int position)
    {
        var (isGood, args) = CollidedWithCard(rawArgs);
        if (!isGood) return;

        _colliderOrder.Add(position);
        if (position == 0)
        {
            if(_colliderOrder.Count > 1)
            {
                _colliderOrder.Clear();
                _colliderOrder.Add(position);
            }
            
            _startTime = Time.time;
        }
        else if(position == Steps - 1)
        {
            _endTime = Time.time;
            if (Validate())
            {
                OnSuccess?.Invoke();
            }
            else
            {
                OnFail?.Invoke();
            }
            _colliderOrder.Clear();
        }
    }

}
