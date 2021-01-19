using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Zombie : MonoBehaviour
{
    public Action HealthChanged = delegate { }; //делегейт эта пустое дейстие чтобы не было ошибки


    public float moveRadius = 10;
    public float attackRadius = 3;
    public float standbyRadius = 13;
    public int viewAngle = 90;
    bool isDead = false;
    public int lives = 50;
    Animator animator;
    Vector3 startPosition;
    Player player;
    AIPath aiPath;
    AIDestinationSetter aiDestinationSetter;
    ZombieState activeState;
    float distanceToPlayer;

    enum ZombieState
    {
        STAND,
        MOVE,
        ATTACK,
        RETURN
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        ChangeState(ZombieState.STAND);
        startPosition = transform.position;
        player.OnDeath += PlayerDied;
    }

    void PlayerDied()
    {
        ChangeState(ZombieState.RETURN);
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }

        switch (activeState)
        {
            case ZombieState.STAND:
                DoStand();
                break;
            case ZombieState.RETURN:
                DoReturn();
                break;
            case ZombieState.MOVE:
                DoMove();
                break;
            case ZombieState.ATTACK:
                DoAttack();
                break;
        }
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, standbyRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.green;
        Vector3 lookDirection = -transform.up;
        Vector3 leftViewAngle = Quaternion.AngleAxis(viewAngle / 2, Vector3.forward) * lookDirection;
        Vector3 rightViewAngle = Quaternion.AngleAxis(-viewAngle / 2, Vector3.forward) * lookDirection;

        Gizmos.DrawRay(transform.position, lookDirection * moveRadius);
    }

    public void LifeTaker(int lostLife)
    {
        lives -= lostLife;
        if (lives <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
            player.OnDeath -= PlayerDied;
            Destroy(gameObject);
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
        if (!player.isDead)
        {
            CheckMoveToPlayer();
        }
    }

    private void DoMove()
    {
        if (distanceToPlayer < attackRadius)
        {
            ChangeState(ZombieState.ATTACK);
            StartCoroutine(DamageCoroutine(1f));
            animator.SetFloat("Speed", 0);
            return;
        }
        else if (distanceToPlayer > standbyRadius)
        {
            activeState = ZombieState.RETURN;
            animator.SetFloat("Speed", 0);
            return;
        }
        animator.SetFloat("Speed", 1);
    }

    private void DoReturn()
    {
        if (!player.isDead && CheckMoveToPlayer())
        {
            return;
        }
        float distanceToStart = Vector3.Distance(transform.position, startPosition);
        if (distanceToStart <= 0.05f)
        {
            ChangeState(ZombieState.STAND);
            return;
        }
    }

    private void DoAttack()
    {
        if (distanceToPlayer > attackRadius)
        {
            ChangeState(ZombieState.MOVE);
            StopAllCoroutines();
            return;
        }

        animator.SetTrigger("Shoot");
    }

    public void DamageToPlayer()
    {
        if (distanceToPlayer > attackRadius)
        {
            player.LifeTaker(5f);
        }
    }

    private void ChangeState(ZombieState newState)
    {
        switch (newState)
        {
            case ZombieState.STAND:
                aiPath.enabled = false;
                break;
            case ZombieState.MOVE:
                aiPath.enabled = true;
                aiDestinationSetter.target = player.transform;
                break;
            case ZombieState.RETURN:
                aiPath.enabled = true;
                break;
            case ZombieState.ATTACK:
                aiPath.enabled = false;
                break;
        }

        activeState = newState;
    }

    private bool CheckMoveToPlayer()
    {
        if (distanceToPlayer > moveRadius)
        {
            return false;
        }

        Vector3 directionToPlayer = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, directionToPlayer, Color.white);
        float angle = Vector3.Angle(-transform.up, directionToPlayer);
        if (angle > viewAngle / 2)
        {
            return false;
        }

        LayerMask layerMask = LayerMask.GetMask("Obstacles");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, layerMask);
        if (hit.collider != null)
        {
            return false;
        }

        ChangeState(ZombieState.MOVE);
        return true;
    }


}
