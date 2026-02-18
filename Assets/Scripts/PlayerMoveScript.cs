using UnityEngine;
using System;

public class PlayerMoveScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientation;
    public Rigidbody rb;
    public float drag;

    private float _horizontal;
    private float _vertical;
    private Vector3 _moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    [Obsolete("Obsolete")]
    public void Update()
    {
        MyInput();
        SpeedControl();
        rb.linearDamping = drag;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        _moveDirection = orientation.forward * _vertical + orientation.right * _horizontal;
        rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
    }

    [Obsolete("Obsolete")]
    private void SpeedControl()
    {
        var flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (!(flatVel.magnitude > moveSpeed)) return;
        var limitedVelocity = flatVel.normalized * moveSpeed;
        rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
    }
}
