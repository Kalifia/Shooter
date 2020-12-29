using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieUI : MonoBehaviour
{
    public Slider healthSlider;
    public Zombie zombie;

    void Start()
    {
        healthSlider.maxValue = zombie.lives;
        healthSlider.value = zombie.lives;

        zombie.HealthChanged += UpdateHealthBar; 
    }
    public void UpdateHealthBar()
    {
        healthSlider.value = zombie.lives;
    }
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }


}
