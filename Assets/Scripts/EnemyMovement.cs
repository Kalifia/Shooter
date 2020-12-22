using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Player player;
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
  
    }

    // Update is called once per frame
    void Update()
    {
        
            Rotate();
        
    }

    private void Rotate()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 enemyPosition = transform.position;
        Vector3 direction = playerPosition - enemyPosition;
        direction.z = 0;
        transform.up = -direction;
    }
}
