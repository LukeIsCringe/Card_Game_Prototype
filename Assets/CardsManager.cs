using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public int buffCardsCount;
    public bool protCardActive;
    public bool shieldCardActive;

    [SerializeField] private GameObject shieldUI;
    [SerializeField] private GameObject protUI;

    private void Start()
    {
        buffCardsCount = 0;
        protCardActive = false;
        shieldCardActive = false;

        shieldUI.SetActive(false);
        protUI.SetActive(false);
    }

    private void Update()
    {
        ShieldUI();
        ProtUI();
    }

    private void ShieldUI()
    {
        if (shieldCardActive)
        {
            shieldUI.SetActive(true);
        }

        if (!shieldCardActive)
        {
            shieldUI.SetActive(false);
        }
    }

    private void ProtUI()
    {
        if (protCardActive)
        {
            protUI.SetActive(true);
        }

        if (!protCardActive)
        {
            protUI.SetActive(false);
        }
    }
}
