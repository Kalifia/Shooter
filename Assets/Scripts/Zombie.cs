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
        ChangeState(ZombieState.STAND);
    }


    void Update()
    {
        if (lives<0)
        {
            return;
        }

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
        distance = Vector3.Distance(transform.position, player.transform.position);
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
            ChangeState(ZombieState.MOVE);
            return;
        }
        animator.SetFloat("Speed", 0);
    }

    private void DoMove()
    {
        if (distance < attackRadius)
        {
            ChangeState(ZombieState.ATTACK);
            StartCoroutine(DamageCoroutine(1f));
            return;
        }
        else if (distance > standbyRadius)
        {
            activeState = ZombieState.STAND;
            //movement.ZombieBackHome();
            return;
        }
        animator.SetFloat("Speed", 1);
    }

    private void DoAttack()
    {
        if (distance > attackRadius)
        {
            ChangeState(ZombieState.MOVE);
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

    private void ChangeState (ZombieState newState)
    {
        switch(newState)
        {
            case ZombieState.STAND:
                movement.enabled = false;
                break;
            case ZombieState.MOVE:
                movement.enabled = true;
                break;
            case ZombieState.ATTACK:
                movement.enabled = false;
                break;
        }
    }
}
