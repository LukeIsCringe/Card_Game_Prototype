using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;

    public int handIndex;

    [SerializeField] private GameObject psmGameObject;
    [SerializeField] private GameObject cmGameObject;
    [SerializeField] private GameObject emGameObject;

    private DeckManager dm;
    [SerializeField] private GameObject deckManager;

    public bool firstEnable = false;

    // Card Setter Stuff

    private int cardNum;
    public bool isMana, isDamage, isAoE, isShield, isHeal, isBuff, isProt;

    [SerializeField] private int manaCost;

    private void Start()
    {
        dm = FindObjectOfType<DeckManager>();
        firstEnable = false;

        // Card Setter Bools

        isMana = false;
        isDamage = false;
        isAoE = false;
        isShield = false;
        isHeal = false;
        isBuff = false;
        isProt = false;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!firstEnable)
        {
            CardSetter();
            firstEnable = true;
        }
    }

    private void CardSetter()
    {
        CardRandom();

        if (cardNum == 1)
        {
            ManaCard();
        }

        if (cardNum == 2)
        {
            SingeDamageCard();
        }

        if (cardNum == 3)
        {
            AoEDamageCard();
        }

        if (cardNum == 4)
        {
            PlayerShieldCard();
        }

        if (cardNum == 5)
        {
            PlayerHealCard();
        }

        if (cardNum == 6)
        {
            PlayerBuffCard();
        }

        if (cardNum == 7)
        {
            PlayerProtCard();
        }
    }

    private void CardRandom()
    {
        cardNum = Random.Range(1, 8);
    }

    public void OnClick()
    {
        PlayerStatsManager pm = psmGameObject.GetComponent<PlayerStatsManager>();
        CardsManager cm = cmGameObject.GetComponent<CardsManager>();

        Debug.Log("Card Clicked");
        if (!hasBeenPlayed && pm.playerMana >= manaCost && !cm.shieldCardActive)
        {
            CardStats();
            hasBeenPlayed = true;
            dm.availableCardSlots[handIndex] = true;
            MoveToDiscardPile();
        }

        if (!hasBeenPlayed && pm.playerMana >= manaCost && isShield && !cm.shieldCardActive)
        {
            CardStats();
            hasBeenPlayed = true;
            dm.availableCardSlots[handIndex] = true;
            MoveToDiscardPile();
        }

        if (!hasBeenPlayed && pm.playerMana >= manaCost && isProt && !cm.protCardActive)
        {
            CardStats();
            hasBeenPlayed = true;
            dm.availableCardSlots[handIndex] = true;
            MoveToDiscardPile();
        }
    }

    private void MoveToDiscardPile()
    {
        dm.discardPile.Add(this);
        gameObject.SetActive(false);
    }

    //Card Actions

    private void CardStats()
    {
        PlayerStatsManager pm = psmGameObject.GetComponent<PlayerStatsManager>();

        EnemyManager em = emGameObject.GetComponent<EnemyManager>();

        CardsManager cm = cmGameObject.GetComponent<CardsManager>();

        if (isMana && pm.playerMana >= 0 && pm.extraManaCount == 0)
        {
            pm.extraManaCount++;
            pm.playerMana = pm.playerMana - manaCost;
            pm.maxMana = pm.maxMana + 2;
        }

        if (isDamage && pm.playerMana > 1)
        {
            if (cm.buffCardsCount > 0)
            {
                if (em.enemy1Targeted)
                {
                    em.enemy1Health = em.enemy1Health - (3 + cm.buffCardsCount);
                }

                if (em.enemy2Targeted)
                {
                    em.enemy2Health = em.enemy2Health - (3 + cm.buffCardsCount);
                }
            }

            else
            {
                if (em.enemy1Targeted)
                {
                    em.enemy1Health = em.enemy1Health - 3;
                }

                if (em.enemy2Targeted)
                {
                    em.enemy2Health = em.enemy2Health - 3;
                }
            }

            pm.playerMana = pm.playerMana - manaCost;
        }

        if (isAoE && pm.playerMana > 2)
        {
            if (cm.buffCardsCount > 0)
            {
                em.enemy1Health = em.enemy1Health - (2 + cm.buffCardsCount);
                em.enemy2Health = em.enemy2Health - (2 + cm.buffCardsCount);
            }

            else
            {
                em.enemy1Health = em.enemy1Health - 2;
                em.enemy2Health = em.enemy2Health - 2;
            }
            pm.playerMana = pm.playerMana - manaCost;
        }

        if (isShield && pm.playerMana > 0 && !cm.shieldCardActive)
        {
            cm.shieldCardActive = true;
            pm.playerMana = pm.playerMana - manaCost;
        }

        if (isHeal && pm.playerMana > 1)
        {
            pm.playerHealth = pm.playerHealth + 10;

            if (pm.playerHealth > pm.playerMaxHealth)
            {
                pm.playerHealth = pm.playerMaxHealth;
            }
            pm.playerMana = pm.playerMana - manaCost;
        }

        if (isBuff && pm.playerMana > 1)
        {
            cm.buffCardsCount++;
            pm.playerMana = pm.playerMana - manaCost;
        }

        if (isProt && pm.playerMana > 1)
        {
            cm.protCardActive = true;

            pm.playerMana = pm.playerMana - manaCost;
        }
    }


    // Setters

    private void ManaCard()
    {
        isMana = true;
        deckManager.GetComponent<DeckManager>().eManaCardCount--;

        Debug.Log("Mana Card Set");

        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Mana Boost";
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "2 Extra Mana Next Turn";
        gameObject.transform.GetChild(4).GetComponent<TMP_Text>().text = "1";

        manaCost = 1;
    }

    private void SingeDamageCard()
    {
        isDamage = true;
        deckManager.GetComponent<DeckManager>().sDamageCardCount--;

        Debug.Log("Single Damage Card Set");

        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Simple Attack";
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "3 Damage to the closest Enemy";
        gameObject.transform.GetChild(4).GetComponent<TMP_Text>().text = "2";

        manaCost = 2;
    }

    private void AoEDamageCard()
    {
        isAoE = true;
        deckManager.GetComponent<DeckManager>().aoeDamageCardCount--;

        Debug.Log("AoE Card Set");

        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Full Stage Blast";
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "2 Damage to each Enemy on the Field";
        gameObject.transform.GetChild(4).GetComponent<TMP_Text>().text = "3";

        manaCost = 3;
    }

    private void PlayerShieldCard()
    {
        isShield = true;
        deckManager.GetComponent<DeckManager>().pShieldCardCount--;

        Debug.Log("Player Shield Card Set");

        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Personal Shield";
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "1 Piece of Incoming Damage is Blocked";
        gameObject.transform.GetChild(4).GetComponent<TMP_Text>().text = "1";

        manaCost = 1;
    }

    private void PlayerHealCard()
    {
        isHeal = true;
        deckManager.GetComponent<DeckManager>().pHealCardCount--;

        Debug.Log("Player Heal Card Set");

        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Quick Heal";
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "Heal 4 Health to Player";
        gameObject.transform.GetChild(4).GetComponent<TMP_Text>().text = "2";

        manaCost = 2;
    }

    private void PlayerBuffCard()
    {
        isBuff = true;
        deckManager.GetComponent<DeckManager>().pBuffCardCount--;

        Debug.Log("Player Buff Card Set");

        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Player Buff";
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "All attacks for this turn do 1 Extra Damage";
        gameObject.transform.GetChild(4).GetComponent<TMP_Text>().text = "2";

        manaCost = 2;
    }

    private void PlayerProtCard()
    {
        isProt = true;
        deckManager.GetComponent<DeckManager>().pProtCardCount--;

        Debug.Log("Player Prot Card Set");

        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Player Protection";
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "All incoming attacks do -1 Damage";
        gameObject.transform.GetChild(4).GetComponent<TMP_Text>().text = "1";

        manaCost = 1;
    }
}
