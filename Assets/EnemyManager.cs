using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject tmGameObject;
    [SerializeField] private GameObject psmGameObject;
    [SerializeField] private GameObject cmGameObject;
    [SerializeField] private GameObject dmGameObject;

    [SerializeField] private GameObject endTurnButton;

    [SerializeField] private GameObject enemy1Sprite;
    [SerializeField] private GameObject enemy2Sprite;

    [SerializeField] private TMP_Text enemy1HP;
    [SerializeField] private TMP_Text enemy2HP;
    [SerializeField] private GameObject enemy1HPGO, enemy2HPGO;

    public bool enemy1Alive;
    public bool enemy2Alive;

    public int enemy1Health;
    public int enemy2Health;

    public int attackCount;

    public bool enemy1Targeted;
    public bool enemy2Targeted;

    private void Start()
    {
        attackCount = 0;

        enemy1Health = 15;
        enemy2Health = 15;

        enemy1Alive = true;
        enemy2Alive = true;

        enemy1Targeted = true;
        enemy2Targeted = false;
    }

    private void Update()
    {
        TextSetters();
        EnemyAttack();
        EnemyStatus();
        TurnOver();
    }

    private void EnemyStatus()
    {
        if (enemy1Health <= 0)
        {
            enemy1Alive = false;

            enemy1Sprite.SetActive(false);
            enemy1HPGO.SetActive(false);

            enemy1Targeted = false;
            enemy2Targeted = true;
        }

        if (enemy2Health <= 0)
        {
            enemy2Sprite.SetActive(false);
            enemy1HPGO.SetActive(false);

            enemy2Alive = false;
        }



        if (!enemy1Alive && !enemy2Alive)
        {
            SceneManager.LoadScene("End_Screen");
        }
    }

    private void TextSetters()
    {
        enemy1HP.text = ("HP: " + enemy1Health.ToString());
        enemy2HP.text = ("HP: " + enemy2Health.ToString());
    }

    private void EnemyAttack()
    {
        TurnManager tm = tmGameObject.GetComponent<TurnManager>();

        if (tm.enemyTurn && !tm.playerTurn)
        {
            gameObject.GetComponent<EnemyAttacks>().enabled = true;
        }
    }

    private void TurnOver()
    {
        TurnManager tm = tmGameObject.GetComponent<TurnManager>();
        PlayerStatsManager psm = psmGameObject.GetComponent<PlayerStatsManager>();
        CardsManager cm = cmGameObject.GetComponent<CardsManager>();
        DeckManager dm = dmGameObject.GetComponent<DeckManager>();

        if (attackCount >= 2)
        {
            dm.drawCardCount = 0;

            psm.playerMana = psm.maxMana;

            if (psm.extraManaCount == 1)
            {
                psm.extraManaCount = 2;
            }

            endTurnButton.SetActive(true);

            cm.protCardActive = false;

            gameObject.GetComponent<EnemyAttacks>().enabled = false;

            tm.playerTurn = true;
            attackCount = 0;
            tm.enemyTurn = false;
        }
    }
}
