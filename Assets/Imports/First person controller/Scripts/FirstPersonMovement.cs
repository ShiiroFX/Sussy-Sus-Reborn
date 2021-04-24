using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour {
    public float speed = 5;
    Vector3 velocity;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    // Custom Animator
    private CharacterController controller;
    private Animator anim;

    void Start() {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate() {
        // Move.
        IsRunning = canRun && Input.GetKey(runningKey);
        float movingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
            movingSpeed = speedOverrides[speedOverrides.Count - 1]();
        velocity.z = Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime;

        Vector3 move = transform.right * velocity.x + transform.forward * velocity.z;

        controller.Move(move);

        if (velocity.x == 0 && velocity.z == 0) {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
        else {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
        }
    }
}