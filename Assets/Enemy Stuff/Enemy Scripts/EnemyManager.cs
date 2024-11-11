using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int e_Health;
    [SerializeField] private int e_Mana;
    [SerializeField] private bool e_TurnTaken = false;
    [SerializeField] private int e_OrderNumber;
    [SerializeField] private int e_Damage;

    [SerializeField] private string e_Type;

    public GameObject position1;
    public GameObject position2;
    public GameObject position3;

    public void Start()
    {
        
    }
}
