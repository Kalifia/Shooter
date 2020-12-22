using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveRadius = 10;
    public float attackRadius = 3;
    public float standbyRadius = 13;
    public int lives = 4;
    Animator animator;
    ZombieMovement movement;
    ZombieState activeState;
    float distance;

    enum ZombieState
    {
        STAND,
        MOVE, 
        ATTACK
    }

    Player player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent< ZombieMovement>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        if (lives<0)
        {
            return;
        }
        
       distance = Vector3.Distance(transform.position, player.transform.position);

        switch (activeState)
        {
            case ZombieState.STAND:
                DoStand();
                break;
            case ZombieState.MOVE:
                DoMove();
                break;
            case ZombieState.ATTACK:
                DoAttack();
                break;
        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, standbyRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void LifeTaker (int lostLife)
    {
        lives-=lostLife;
        if (lives<0)
        {
            animator.SetTrigger("Death");
            Destroy(this);
        }
    }


    IEnumerator DamageCoroutine(float delay)
    {
        while (true)
        {
            animator.SetTrigger("Shoot");
            yield return new WaitForSeconds(delay);
        }
    }

    private void DoStand()
    {
        if (distance < moveRadius)
        {
            activeState = ZombieState.MOVE;
            return;
        }
        movement.enabled = false;
        animator.SetFloat("Speed", 0);
    }

    private void DoMove()
    {
        if (distance < attackRadius)
        {
            activeState = ZombieState.ATTACK;
            StartCoroutine(DamageCoroutine(1f));
            return;
        }
        else if (distance > standbyRadius)
        {
            //activeState = ZombieState.STAND;
            movement.ZombieBackHome();
            return;
        }
        movement.enabled = true;
        animator.SetFloat("Speed", 1);
    }

    private void DoAttack()
    {
        if (distance > attackRadius)
        {
            activeState = ZombieState.MOVE;
            StopAllCoroutines();
            return;
        }

        animator.SetTrigger("Shoot");
    }

    public void DamageToPlayer()
    {
        if (distance > attackRadius)
        {
            player.LifeTaker(0.5f);
        }
    }
}
