using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private float speed = 8f;
    private float jump = 16f;
    private bool isRight = true;

    [SerializedField] private Rigidbody2d rb;
    [SerializedField] private Transform groundCheck;
    [SerializedField] private LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.getAxisRaw("Horizontal");
    }
}
