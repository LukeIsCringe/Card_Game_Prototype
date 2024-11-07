using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    private int testDamage = 10;
    public void DoDamage()
    {
        PlayerStatsManager pStats = new PlayerStatsManager();

        Debug.Log(pStats.playerHealth);

        if (pStats.playerArmor >= 2)
        {
            pStats.playerHealth = pStats.playerHealth - (testDamage / 2);
            pStats.playerArmor = pStats.playerArmor - (testDamage / 5);
        }

        else if (pStats.playerArmor < 2) 
        {
            pStats.playerHealth = pStats.playerHealth - testDamage;
        }

        
    }
}
