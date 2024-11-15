using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesManager : MonoBehaviour
{
    // Tank Stats
    public int eT_Health;
    private int eT_Damage;
    private int eT_Mana;
    [SerializeField] private bool eT_TurnTaken;
    [SerializeField] private bool eT_Alive;

    // Enemy ID is 1

    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject tankGO;

    // Mage Stats
    public int eMa_Health;
    private int eMa_Damage;
    [SerializeField] private int eMa_Mana;
    [SerializeField] private bool eMa_TurnTaken;
    [SerializeField] private bool eMa_Alive;
    public bool outOfMana;
    [SerializeField] private GameObject eMa_CoroutineHolder;

    // Enemy ID is 2

    [SerializeField] private GameObject magePrefab;
    [SerializeField] private GameObject mageGO;

    // Stats
    public int eMe_Health;
    private int eMe_Damage;
    private int eMe_Mana;
    [SerializeField] private bool eMe_TurnTaken;
    [SerializeField] private bool eMe_Alive;

    // Enemy ID is 3

    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject meleeGO;

    // Positions
    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    [SerializeField] private GameObject pos3;

    public int pos1Count;
    public int pos2Count;
    public int pos3Count;

    [SerializeField] private bool inPos1;
    [SerializeField] private bool inPos2;
    [SerializeField] private bool inPos3;

    // Error Deniers

    [SerializeField] private GameObject errorDenier1;
    [SerializeField] private GameObject errorDenier2;
    [SerializeField] private GameObject errorDenier3;

    // Misc
    [SerializeField] private GameObject pathManagerGO;
    [SerializeField] private GameObject playerCombatManager;
    [SerializeField] private int spawnEnemyCount;
    public bool enemyTurn;

    [SerializeField] private float testDelay;

    private void Start()
    {
        eT_Health = 45;
        eT_Damage = 15;
        eT_Mana = 1;

        eMa_Health = 20;
        eMa_Damage = 5;
        eMa_Mana = 3;

        eMe_Health = 30;
        eMe_Damage = 20;
        eMe_Mana = 1;

        eT_TurnTaken = false;
        eMa_TurnTaken = false;
        eMe_TurnTaken = false;

        eT_Alive = true;
        eMa_Alive = true;
        eMe_Alive = true;

        enemyTurn = false;

        spawnEnemyCount = 0;

        pos1Count = 1;
        pos2Count = 3;
        pos3Count = 2;

        inPos1 = false;
        inPos2 = false;
        inPos3 = false;

        outOfMana = false;


    }

    public void Update()
    {
        enemiesSpawn();

        enemyAI();

        deathSetter();

        enemyTurnEnd();       
        levelEnd();
        
        if (eMa_Mana == 0)
        {
            outOfMana = true;
        }
    }

    private void enemyAI()
    {
        if (eT_Alive && enemyTurn)
        {
            tankEnemy();
        }

        if (eMa_Alive && enemyTurn)
        {
            mageEnemy();
        }

        if (eMe_Alive && enemyTurn)
        {
            meleeEnemy();
        }
    }

    private void enemiesSpawn()
    {
        PathManager pathManager = pathManagerGO.GetComponent<PathManager>();

        if (pathManager.loadLevel1Assets)
        {
            Invoke("spawnEnemies", 6.1f);
        }
    }

    // Tank enemy AI

    private void tankEnemy()
    {
        if (pos1Count == 1)
        {
            //no damage to enemies behind
            Invoke("tankEnemyAttack", 2f);
            inPos1 = true;
        }

        if (pos2Count == 1 && inPos1)
        {
            //no damage for the person behind them
            Invoke("tankEnemyAttack", 8f);
            inPos2 = true;
        }

        if (pos3Count == 1 && inPos2)
        {
            Invoke("tankEnemyAttack", 14f);
            inPos3 = true;
        }
    }

    

    private void tankEnemyAttack()
    {
        if (pos1Count == 1 && !eT_TurnTaken && enemyTurn)
        {
            PlayerManager.p_Health = PlayerManager.p_Health - eT_Damage;

            eT_TurnTaken = true;
        }

        if (pos2Count == 1 && !eT_TurnTaken && enemyTurn)
        {
            eT_Damage = eT_Damage - 4;

            PlayerManager.p_Health = PlayerManager.p_Health - eT_Damage;

            eT_TurnTaken = true;
        }

        if (pos3Count == 1 && !eT_TurnTaken && enemyTurn)
        {
            eT_Damage = eT_Damage - 8;

            PlayerManager.p_Health = PlayerManager.p_Health - eT_Damage;

            eT_TurnTaken = true;
        }
    }

    // Beast/Melee enemy AI
    private void meleeEnemy()
    {
        if (pos1Count == 3)
        {
            //no damage to enemies behind
            Invoke("meleeEnemyAttack", 2f);
            inPos1 = true;
        }

        if (pos2Count == 3 && inPos1)
        {
            //no damage for the person behind them
            Invoke("meleeEnemyAttack", 8f);
            inPos2 = true;
        }

        if (pos3Count == 3 && inPos2)
        {
            Invoke("meleeEnemyAttack", 14f);
            inPos3 = true;
        }
    }



    private void meleeEnemyAttack()
    {
        if (pos1Count == 3 && !eMe_TurnTaken && enemyTurn)
        {
            PlayerManager.p_Health = PlayerManager.p_Health - eMe_Damage;

            eMe_TurnTaken = true;
        }

        if (pos2Count == 3 && !eMe_TurnTaken && enemyTurn)
        {
            eMe_Damage = eMe_Damage - 5;

            PlayerManager.p_Health = PlayerManager.p_Health - eT_Damage;

            eMe_TurnTaken = true;
        }

        if (pos3Count == 3 && !eMe_TurnTaken && enemyTurn)
        {
            eMe_Damage = eMe_Damage - 10;

            PlayerManager.p_Health = PlayerManager.p_Health - eT_Damage;

            eMe_TurnTaken = true;
        }
    }

    // Mage enemy AI

    private void mageEnemy()
    {
        if (pos1Count == 2)
        {
            Invoke("mageEnemyAttack", 2f);
            inPos1 = true;
        }

        if (pos2Count == 2 && inPos1)
        {
            Invoke("mageEnemyAttack", 8f);
            inPos2 = true;
        }

        if (pos3Count == 2 && inPos2) 
        {
            Invoke("mageEnemyAttack", 14f);
            inPos3 = true;
        }
    }
    
    private void mageEnemyAttack()
    {
        if (pos1Count == 2 && !eMa_TurnTaken && enemyTurn)
        {
            eMa_Damage = 2;
            eMa_CoroutineHolder.SetActive(true);
        }

        if (pos2Count == 2 && !eMa_TurnTaken && enemyTurn)
        {
            eMa_Damage = 3;
            eMa_CoroutineHolder.SetActive(true);
        }

        if (pos3Count == 2 && !eMa_TurnTaken && enemyTurn)
        {
            eMa_CoroutineHolder.SetActive(true);
        }

        if (eMa_Mana == 0)
        {
            eMa_TurnTaken = true;
        }
    }

    public void mageAttackLoop()
    {
        if (eMa_Mana > 0)
        {
            mageCountPlus();
        }
    }

    private void mageCountPlus()
    {
        PlayerManager.p_Health = PlayerManager.p_Health - eMa_Damage;
        eMa_Mana--;
        Debug.Log("Looped");
    }


    private void spawnEnemies()
    {
        if (spawnEnemyCount == 0)
        {
            Destroy(errorDenier1);
            Destroy(errorDenier2);
            Destroy(errorDenier3);

            tankGO = Instantiate(tankPrefab, pos1.transform.position, Quaternion.identity);
            meleeGO = Instantiate(meleePrefab, pos2.transform.position, Quaternion.identity);
            mageGO = Instantiate(magePrefab, pos3.transform.position, Quaternion.identity);

            tankGO.transform.SetParent(pos1.transform);
            meleeGO.transform.SetParent(pos2.transform);
            mageGO.transform.SetParent(pos3.transform);
            
            spawnEnemyCount++;
        }
    }

    private void deathSetter()
    {
        if (!eT_Alive)
        {
            eT_TurnTaken = true;
        }
        if (!eMa_Alive)
        {
            eMa_TurnTaken = true;
        }
        if (!eMe_Alive)
        {
            eMe_TurnTaken = true;
        }
    }

    private void enemyTurnEnd()
    {
        PlayerCombatManager pCombatManager = playerCombatManager.GetComponent<PlayerCombatManager>();

        if (eT_TurnTaken && eMe_TurnTaken && eMa_TurnTaken)
        {
            enemyTurn = false;
            pCombatManager.playerTurn = true;
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