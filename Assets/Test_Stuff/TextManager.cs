using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public TMP_Text HealthText;

    private void Update()
    {
        PlayerStatsManager pStats = new PlayerStatsManager();

        HealthText.text = pStats.playerHealth.ToString();
    }
}
