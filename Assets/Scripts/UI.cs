using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider playerHealth;
    Player player;
    public Image gameOverPanel;
    //todo player portret
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.OnHealthChange += UpdateHealth;
        player.OnDeath += ShowGameOverPanel;

        playerHealth.maxValue = player.lives;
        playerHealth.value = player.lives;
    }

    void UpdateHealth()
    {
        playerHealth.value = player.lives;
    }
    void ShowGameOverPanel()
    {
        StartCoroutine(ShowGameOverWithDelay(1.5f));
    }
    IEnumerator ShowGameOverWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameOverPanel.gameObject.SetActive(true);
    }

}
