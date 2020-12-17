﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
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
        if (player.lives >= 0)
        {

            Move();
            Rotate();
        }
    }

    private void Move()
    {
        Vector3 zombiePosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - zombiePosition;
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }

        animator.SetFloat("Speed", direction.magnitude);
        rb.velocity = direction * speed;
    }

    private void Rotate()
    {
        Vector3 zombiePosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - zombiePosition;
        direction.z = 0;
        transform.up = -direction;
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
}
