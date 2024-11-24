using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;

    public int handIndex;

    private DeckManager dm;

    private void Start()
    {
        dm = FindObjectOfType<DeckManager>();
    }

    public void OnClick()
    {
        Debug.Log("Card Clicked");
        if (!hasBeenPlayed)
        {
            transform.position += Vector3.up * 2f;
            hasBeenPlayed = true;
            dm.availableCardSlots[handIndex] = true;
            Invoke("MoveToDiscardPile", 2f);
        }
    }

    private void MoveToDiscardPile()
    {
        dm.discardPile.Add(this);
        gameObject.SetActive(false);
    }
}
