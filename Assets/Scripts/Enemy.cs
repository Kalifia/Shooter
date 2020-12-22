using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public Text enemyLivesText;
    public Bullet bulletPrefab;
    public GameObject enemyShootPosition;


    public int lives = 3;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
       
            StartCoroutine(ShootCoroutine(2f));
        
    }
    IEnumerator ShootCoroutine(float delay)
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(delay);
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        Instantiate(bulletPrefab, enemyShootPosition.transform.position, transform.rotation);
    }

    public void LifeTaker(int lostLife)
    {
        lives-=lostLife;
        enemyLivesText.text = lives.ToString();
        
        if (lives<0)
        {
            StopAllCoroutines();
            animator.SetTrigger("Death");
            Destroy(this);
        }
    }
}
