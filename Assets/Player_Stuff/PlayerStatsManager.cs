using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsManager : MonoBehaviour
{
    public int playerHealth;
    public int playerMaxHealth;
    public int maxMana;
    public int playerMana;

    public int extraManaCount;

    public TMP_Text manaText;
    public TMP_Text playerText;

    private void Start()
    {
        playerMaxHealth = 50;
        playerHealth = playerMaxHealth;
        maxMana = 5;
        playerMana = maxMana;
    }

    private void Update()
    {
        playerText.text = playerHealth.ToString();
        manaText.text = playerMana.ToString();
    }

    private void PlayerDeath()
    {
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("Main_Scene");
        }
    }
}
