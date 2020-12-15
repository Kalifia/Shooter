using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public Text enemyLivesText;
    public Bullet bulletPrefab;
    public GameObject enemyShootPosition;

    int lives;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        Instantiate(bulletPrefab, enemyShootPosition.transform.position, transform.rotation);
    }

    public void LifeTaker()
    {
        lives--;
        enemyLivesText.text = lives.ToString();
        animator.SetInteger("Death", lives);
    }
}
