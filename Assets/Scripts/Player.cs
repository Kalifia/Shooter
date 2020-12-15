using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public GameObject shootPosition;
    Animator animator;

    public float fireRate = 0.15f;
    float nextFire; //время до след выстр
    int lives;
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

    public void LifeTaker()
    {
        lives--;
        playerLivesText.text = lives.ToString();
        animator.SetInteger("Death", lives);
    }
}
