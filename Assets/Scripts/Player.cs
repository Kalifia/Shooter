using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public GameObject shootPosition;
    public Action OnHealthChange = delegate { };
    public Action OnDeath = delegate { };
    Animator animator;

    public float fireRate = 0.15f;
    float nextFire; //время до след выстр


    public float lives = 5f;

    public Text playerLivesText;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }


    void Update()
    {
 
            CheckFire();
        
    }

    private void CheckFire()
    {
        if (Input.GetButton("Fire1") && nextFire <= 0)
        {
            Shoot();
        }
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        Instantiate(bulletPrefab, shootPosition.transform.position, transform.rotation);
        nextFire = fireRate;
    }

    public void LifeTaker(float lostLife)
    {
        lives-=lostLife;
        OnHealthChange();
        if (lives < 0)
        {
            animator.SetTrigger("Death");
            OnDeath();
            Destroy(this);
        }
    }
}
