using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D rb;
    Enemy enemy;
    Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        rb.velocity = -speed * transform.up;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player"))
        {
            player.LifeTaker();
        }
        else if (CompareTag("Enemy"))
        {
            enemy.LifeTaker();
        }
    }
}
