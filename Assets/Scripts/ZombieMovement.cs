using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rb;
    Animator animator;
    Player player;
    Vector3 startPosition;
    Zombie zombie;
    Vector3 point1;
    Vector3 point2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        zombie = GetComponent<Zombie>();
    }

    private void Start()
    {
        startPosition = transform.position; //запомнить где был зрмби
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        if (zombie.lives >= 0)
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

    public void ZombieBackHome()
    {
        if (zombie.lives >= 0)
        {
            Vector3 zombiePosition = transform.position;
            Vector3 direction = startPosition - zombiePosition;
            if (direction.magnitude > 1)
            {
                direction = direction.normalized;
            }

            animator.SetFloat("Speed", direction.magnitude);
            rb.velocity = direction * speed;
        }
    }

    public void Patrol()
    {
            Vector3 zombiePosition = transform.position;
            Vector3 direction = point1 - zombiePosition;
        if (zombiePosition = =point1)
        {
            direction = point 2 - zombiePosition;
            return;
        }
        else if (zombiePosition = point2)
        {
            Vector3 direction = point1 - zombiePosition;
        }
            if (direction.magnitude > 1)
            {
                direction = direction.normalized;
            }

            animator.SetFloat("Speed", direction.magnitude);
            rb.velocity = direction * speed;
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
}
