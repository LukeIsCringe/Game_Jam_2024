using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    // Tank Stats
    private int eT_Health;
    private int eT_Damage;
    private int eT_Mana;
    private bool eT_TurnTaken;
    private bool eT_Alive;

    // Enemy ID is 1

    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject tankGO;

    // Mage Stats
    private int eMa_Health;
    private int eMa_Damage;
    private int eMa_Mana;
    private bool eMa_TurnTaken;
    private bool eMa_Alive;

    // Enemy ID is 2

    [SerializeField] private GameObject magePrefab;
    [SerializeField] private GameObject mageGO;

    // Stats
    private int eMe_Health;
    private int eMe_Damage;
    private int eMe_Mana;
    private bool eMe_TurnTaken;
    private bool eMe_Alive;

    // Enemy ID is 3

    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject meleeGO;

    // Positions
    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    [SerializeField] private GameObject pos3;

    [SerializeField] private int pos1Count;
    [SerializeField] private int pos2Count;
    [SerializeField] private int pos3Count;

    // Misc
    [SerializeField] private GameObject pathManagerGO;
    [SerializeField] private int spawnEnemyCount;

    private void Start()
    {
        eT_Health = 50;
        eT_Damage = 15;
        eT_Mana = 1;

        eMa_Health = 30;
        eMa_Damage = 5;
        eMa_Mana = 3;

        eMe_Health = 40;
        eMe_Damage = 20;
        eMa_Mana = 1;

        eT_TurnTaken = false;
        eMa_TurnTaken = false;
        eMe_TurnTaken = false;

        eT_Alive = true;
        eMa_Alive = true;
        eMe_Alive = true;

        spawnEnemyCount = 0;
    }

    public void Update()
    {
        PathManager pathManager = pathManagerGO.GetComponent<PathManager>();

        if (pathManager.loadLevel1Assets)
        {
            Invoke("spawnEnemies", 6.1f);
        }

        levelEnd();
        
    }

    private void spawnEnemies()
    {
        if (spawnEnemyCount == 0)
        {
            tankGO = Instantiate(tankPrefab, pos1.transform.position, Quaternion.identity);
            meleeGO = Instantiate(meleePrefab, pos2.transform.position, Quaternion.identity);
            mageGO = Instantiate(magePrefab, pos3.transform.position, Quaternion.identity);

            tankGO.transform.SetParent(pos1.transform);
            meleeGO.transform.SetParent(pos2.transform);
            mageGO.transform.SetParent(pos3.transform);
            
            spawnEnemyCount++;
        }
    }

    private void levelEnd()
    {
        if (!eT_Alive && !eMa_Alive && !eMe_Alive)
        {
            // End level 1
            // Load stat upgrade scene
        }
    }
}
