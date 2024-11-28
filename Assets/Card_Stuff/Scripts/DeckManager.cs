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

    [SerializeField] private GameObject drawCardButton;

    // Card Types

    public int eManaCardCount, sDamageCardCount, aoeDamageCardCount, pShieldCardCount, pHealCardCount, pBuffCardCount, pProtCardCount;

    public int drawCardCount;
    private int drawLimit;

    private void Start()
    {
        eManaCardCount = 5; // 1
        sDamageCardCount = 7; // 2
        aoeDamageCardCount = 3; // 3
        pShieldCardCount = 3; // 4
        pHealCardCount = 4; // 5
        pBuffCardCount = 4; // 6
        pProtCardCount = 4; // 7

        drawCardCount = 0;
        drawLimit = 5;
    }

    public void DrawCard()
    {
        if (deck.Count >= 1 && drawCardCount < drawLimit)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];


            Debug.Log(randCard);

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);

                    randCard.handIndex = i;

                    randCard.transform.SetParent(cardSlots[i]);
                    randCard.transform.position = cardSlots[i].position;
                    randCard.transform.rotation = cardSlots[i].rotation;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    drawCardCount++;
                    return;
                }
            }
        }
    }

    private void Update()
    {
        deckSizeText.text = ("Deck: " + deck.Count.ToString());
        discardPileText.text = ("Discard: " + discardPile.Count.ToString());

        if (drawCardCount == 5)
        {
            drawCardCount = 0;
            drawLimit = 2;
        }
    }

}
