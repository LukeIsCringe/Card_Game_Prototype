using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DeckManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public TMP_Text deckSizeText;
    public TMP_Text discardPileText;
    
    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0,deck.Count)];

            for (int i = 0;i < availableCardSlots.Length;i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlots[i].position;
                    randCard.transform.rotation = cardSlots[i].rotation;
                    availableCardSlots[i] = false; 
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardPileText.text = discardPile.Count.ToString();
    }















    /*private int deckCardCount;
    private int maxCardCount;

    private int eManaCardCount, sDamageCardCount, aoeDamageCardCount, pShieldCardCount, pHealCardCount, pBuffCardCount, pProtCardCount;

    private void Start()
    {
        maxCardCount = 30;

        eManaCardCount = 5;
        sDamageCardCount = 7;
        aoeDamageCardCount = 3;
        pShieldCardCount = 3;
        pHealCardCount = 4;
        pBuffCardCount = 4;
        pProtCardCount = 4;

        deckCardCount = eManaCardCount + sDamageCardCount + aoeDamageCardCount + pShieldCardCount + pHealCardCount + pBuffCardCount + pProtCardCount;
    }

    public void Update()
    {
        deckFailsafe();
    }

    private void deckFailsafe()
    {
        if (deckCardCount > maxCardCount)
        {
            Debug.Log("Card count exceedes deck size");
        }
    } */
}
