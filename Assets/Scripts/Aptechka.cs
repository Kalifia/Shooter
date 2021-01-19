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
            player.LifeTaker(-25);
        }

        Invoke("Selfdestraction", 2.0f);
    }

    private void Selfdestraction()
    {
        Destroy(gameObject);
    }
}


