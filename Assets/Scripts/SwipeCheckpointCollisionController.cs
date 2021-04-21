using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCheckpointCollisionController : MonoBehaviour
{
    public EventHandler OnCollisionEnter;
    public EventHandler OnCollisionExit;

    private void OnCollisionEnter2D(Collision2D collision) => OnCollisionEnter?.Invoke(this, new SwipeCheckpointCollisionEventArgs { collision = collision });
    private void OnCollisionExit2D(Collision2D collision) => OnCollisionExit?.Invoke(this, new SwipeCheckpointCollisionEventArgs { collision = collision });
}
