using UnityEngine;

public class ShowHand : MonoBehaviour
{
    public bool showingHand;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject drawCard;

    private void Start()
    {
        showingHand = false;
        hand.SetActive(false);
        drawCard.SetActive(false);
    }

    public void OnShowHandClick()
    {
        if (!showingHand)
        {
            drawCard.SetActive(true);
            hand.SetActive(true);
            showingHand = true;
        }
        else if (showingHand)
        {
            drawCard.SetActive(false);
            hand.SetActive(false);
            showingHand = false;
        }
    }
}
