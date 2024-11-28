using System.Runtime.CompilerServices;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    [SerializeField] private GameObject tmGameObject;
    [SerializeField] private GameObject psmGameObject;
    [SerializeField] private GameObject cmGameObject;

    public void OnClick()
    {
        TurnManager tm = tmGameObject.GetComponent<TurnManager>();
        PlayerStatsManager psm = psmGameObject.GetComponent<PlayerStatsManager>();
        CardsManager cm = cmGameObject.GetComponent<CardsManager>();

        tm.playerTurn = false;
        tm.enemyTurn = true;

        cm.buffCardsCount = 0;

        if (psm.extraManaCount >= 2)
        {
            psm.extraManaCount = 0;
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        PlayerStatsManager psm = psmGameObject.GetComponent<PlayerStatsManager>();

        if (psm.extraManaCount == 0)
       {
            psm.maxMana = 5;
        }
    }
}
