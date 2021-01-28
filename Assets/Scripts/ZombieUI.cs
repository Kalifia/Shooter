using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieUI : MonoBehaviour
{
    public Image slider;
    public Zombie zombie;
    private float value;
    void Start()
    {
        zombie.HealthChanged += UpdateHealthBar;
        value = 1f / zombie.lives;
        //slider.fillAmount = 1f / zombie.lives;
    }
    public void UpdateHealthBar()
    {
        slider.fillAmount -= value ;
    }
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }


}
