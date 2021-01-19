using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieUI : MonoBehaviour
{
    public Image slider;
    public Zombie zombie;

    void Start()
    {
        zombie.HealthChanged += UpdateHealthBar;
    }
    public void UpdateHealthBar()
    {
        slider.fillAmount = zombie.lives / 50;
    }
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }


}
