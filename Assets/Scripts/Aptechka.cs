using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aptechka : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.lives>=100)
            {
                return;
            }
            player.LifeTaker(-25);
        }

        Invoke("Selfdestraction", 0.5f);
    }

    private void Selfdestraction()
    {
        Destroy(gameObject);
    }
}


