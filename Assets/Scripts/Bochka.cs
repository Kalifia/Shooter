using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bochka : MonoBehaviour
{
    public float explosionRadius = 10;
    Animator animator;
    Enemy enemy;
    Player player;
    Zombie zombie;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        zombie = FindObjectOfType<Zombie>();
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int[] layerMask = { LayerMask.GetMask("Enemy"), LayerMask.GetMask("Player") };
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask[0], layerMask[1]);
        animator.SetTrigger("Explosion");
        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            Player player = collider.GetComponent<Player>();
            Zombie zombie = collider.GetComponent<Zombie>();

            if (collision.gameObject.CompareTag("Player"))
            {
                player.LifeTaker(2);
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                enemy.LifeTaker(2);
            }

            else if (collision.gameObject.CompareTag("Zombie"))
            {
                zombie.LifeTaker(2);
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
