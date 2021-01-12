using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Action HealthChanged = delegate { }; //делегейт эта пустое дейстие чтобы не было ошибки


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
        ATTACK,
        RETURN
    }

    Player player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<ZombieMovement>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        ChangeState(ZombieState.STAND);
        player.OnDeath += PlayerDied;
    }

    void PlayerDied()
    {
        ChangeState(ZombieState.RETURN);
    }
    void Update()
    {
        if (lives < 0)
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

    public void LifeTaker(int lostLife)
    {
        lives -= lostLife;
        if (lives < 0)
        {
            animator.SetTrigger("Death");
            player.OnDeath -= PlayerDied;
            Destroy(this);
        }
        HealthChanged();
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
        if (distance > moveRadius)
        {
            return;
        }
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, Vector2.up * 50, Color.white);
        LayerMask layerMask = LayerMask.GetMask("Obstacles");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, layerMask);
        if (hit.collider != null)
        {
            print(hit.collider.name);
        }
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

    private void ChangeState(ZombieState newState)
    {
        switch (newState)
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

    void CheckMoveToPlayer()
    {
        float angle = Vector3.Angle(-transform.up, directionToPlayer);
        if (angle )
    }


}
