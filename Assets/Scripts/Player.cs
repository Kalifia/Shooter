using Lean.Pool;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player instance;
    public Bullet bulletPrefab;
    public GameObject shootPosition;
    public Action OnHealthChange = delegate { };
    public Action OnDeath = delegate { };
    Animator animator;
    public float fireRate = 0.15f;
    float nextFire; //время до след выстр
    public float lives = 100f;
    public Text playerLivesText;
    public bool isDead = false;

    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
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
        LeanPool.Spawn(bulletPrefab, shootPosition.transform.position, transform.rotation);
        nextFire = fireRate;
    }

    public void LifeTaker(float lostLife)
    {
        lives-=lostLife;
        OnHealthChange();
        if (lives < 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
            OnDeath();
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (this==instance)
        {
            instance = null;
        }
    }
}
