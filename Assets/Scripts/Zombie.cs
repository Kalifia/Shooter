using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveRadius = 10;
    public float attackRadius = 3;
    Animator animator;
    ZombieMovement movement;
    ZombieState activeState;
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
        float distance = Vector3.Distance(transform.position, player.transform.position);

        switch (activeState)
        {
            case ZombieState.STAND:
                if (distance < moveRadius)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }
                movement.enabled=false;
                animator.SetFloat("Speed", 0);
                break;
            case ZombieState.MOVE:
                if (distance < attackRadius)
                {
                    activeState = ZombieState.ATTACK;
                    return;
                }
                movement.enabled = true;
                animator.SetFloat("Speed", 1);
                break;
            case ZombieState.ATTACK:
                if (distance>attackRadius)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }
                animator.SetTrigger("Shoot");
                break;
        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
