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

        player = FindObjectOfType<Player>();
        rb.velocity = -speed * transform.up;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.LifeTaker();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.GetComponent<Enemy>();
            enemy.LifeTaker();
        }
    }
}
