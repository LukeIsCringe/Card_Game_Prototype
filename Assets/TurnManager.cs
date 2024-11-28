using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool playerTurn;
    public bool enemyTurn;

    [SerializeField]private GameObject psmGameObject;

    private void Start()
    {
        playerTurn = true;
        enemyTurn = false;
    }
}
