﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rb;
    Animator animator;
    Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>(); 
    }
    void Update()
    {
        if (player.lives>=0)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(inputX, inputY);

        if (direction.magnitude>1)
        {
            direction = direction.normalized;
        }

        animator.SetFloat("Speed", direction.magnitude);
        rb.velocity = direction * speed;
    }

    private void Rotate()
    {
        Vector3 playerPosition = transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - playerPosition;
        direction.z = 0;

        transform.up = -direction;
    }
}
