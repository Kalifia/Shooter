using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bochka : MonoBehaviour
{
    public float explosionRadius = 10;
    Animator animator;
    Enemy enemy;
    public LayerMask damageLayers;
    Player player;
    Zombie zombie;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayers);
        animator.SetTrigger("Explosion");
        Debug.Log(colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            collider.gameObject.SendMessage("LifeTaker", 1f);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
