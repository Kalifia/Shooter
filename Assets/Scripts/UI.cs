using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider playerHealth;
    public Image gameOverPanel;
    //private Player player;
    //todo player portret
    void Start()
    {
        Player.Instance.OnHealthChange += UpdateHealth;
        Player.Instance.OnDeath += ShowGameOverPanel;

        playerHealth.maxValue = Player.Instance.lives;
        playerHealth.value = Player.Instance.lives;
    }

    void UpdateHealth()
    {
        playerHealth.value = Player.Instance.lives;
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
