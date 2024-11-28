using System.Collections;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    private int enemyDamage;
   
    [SerializeField] private GameObject psmGameObject;
    [SerializeField] private GameObject cmGameObject;
    [SerializeField] private GameObject tmGameObject;
    
    
    private void Start()
    {
        enemyDamage = 8;
        gameObject.GetComponent<EnemyAttacks>().enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(Attack(2f));
    }

    private IEnumerator Attack(float delay)
    {
        PlayerStatsManager psm = psmGameObject.GetComponent<PlayerStatsManager>();

        CardsManager cm = cmGameObject.GetComponent<CardsManager>();

        EnemyManager em = gameObject.GetComponent<EnemyManager>();

        TurnManager tm = tmGameObject.GetComponent<TurnManager>();

        if (cm.protCardActive)
        {
            enemyDamage = 6;

        }

        if (!cm.protCardActive)
        {
            enemyDamage = 8;
        }

        if (em.enemy1Alive && em.enemy2Alive && !cm.shieldCardActive && tm.enemyTurn && em.attackCount < 2)
        {
            yield return new WaitForSeconds(delay);

            psm.playerHealth = psm.playerHealth - enemyDamage;

            yield return new WaitForSeconds(delay);

            psm.playerHealth = psm.playerHealth - enemyDamage;

            em.attackCount = 2;
        }

        if (!em.enemy1Alive && em.enemy2Alive && !cm.shieldCardActive && tm.enemyTurn && em.attackCount < 2)
        {
            yield return new WaitForSeconds(delay);

            psm.playerHealth = psm.playerHealth - enemyDamage;

            em.attackCount = 2;
        }

        if (em.enemy1Alive && em.enemy2Alive && cm.shieldCardActive && tm.enemyTurn && em.attackCount < 2)
        {
            yield return new WaitForSeconds(delay);

            psm.playerHealth = psm.playerHealth - enemyDamage;

            cm.shieldCardActive = false;

            em.attackCount = 2;
        }

        if (!em.enemy1Alive && em.enemy2Alive && cm.shieldCardActive && tm.enemyTurn && em.attackCount < 2)
        {
            yield return new WaitForSeconds(delay);

            cm.shieldCardActive = false;

            em.attackCount = 2;
        }

        em.attackCount = 2;
    }
}
