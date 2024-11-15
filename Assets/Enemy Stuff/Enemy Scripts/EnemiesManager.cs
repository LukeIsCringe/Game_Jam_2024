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
    public bool eT_Alive;

    // Enemy ID is 1

    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject tankGO;

    // Mage Stats
    public int eMa_Health;
    private int eMa_Damage;
    public int eMa_Mana;
    public bool eMa_TurnTaken;
    public bool eMa_Alive;
    public bool outOfMana;
    public GameObject eMa_CoroutineHolder;
    public bool eMa_Attacking;

    // Enemy ID is 2

    [SerializeField] private GameObject magePrefab;
    [SerializeField] private GameObject mageGO;

    // Stats
    public int eMe_Health;
    private int eMe_Damage;
    private int eMe_Mana;
    [SerializeField] private bool eMe_TurnTaken;
    public bool eMe_Alive;

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

    [SerializeField] private GameObject errDenierHolder;

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
        eMa_Mana = 0;
        eMa_Attacking = false;

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

        //outOfMana = false;

        //eMa_CoroutineHolder.SetActive(false);
    }

    public void Update()
    {
        enemiesSpawn();

        enemyAI();

        deathSetter();
        positionSetters();

        enemyTurnEnd();       
        levelEnd();
        
        //if (eMa_Mana == 0)
        //{
        //    outOfMana = true;
        //}
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
        if (pos1Count == 1 && eT_Alive)
        {
            //no damage to enemies behind
            Invoke("tankEnemyAttack", 2f);
            inPos1 = true;
        }

        if (pos2Count == 1 && inPos1 && eT_Alive)
        {
            //no damage for the person behind them
            Invoke("tankEnemyAttack", 8f);
            inPos2 = true;
        }

        if (pos3Count == 1 && inPos2 && eT_Alive)
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
        // Attack position definer

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
        if (pos1Count == 2 && !eMa_Attacking)
        {
            Invoke("mageEnemyAttack", 2f);
            inPos1 = true;
        }

        if (pos2Count == 2 && inPos1 && !eMa_Attacking)
        {
            Invoke("mageEnemyAttack", 8f);
            inPos2 = true;
        }

        if (pos3Count == 2 && inPos2 && !eMa_Attacking) 
        {
            Invoke("mageEnemyAttack", 14f);
            inPos3 = true;
        }
    }
    
    private void mageEnemyAttack()
    {
        if (pos1Count == 2 && !eMa_TurnTaken && enemyTurn && !eMa_Attacking)
        {
            eMa_Damage = 2;
            eMa_Mana = 3;
        }

        if (pos2Count == 2 && !eMa_TurnTaken && enemyTurn && !eMa_Attacking)
        {
            eMa_Damage = 3;
            eMa_Mana = 3;
        }

        if (pos3Count == 2 && !eMa_TurnTaken && enemyTurn && !eMa_Attacking)
        {
            eMa_Mana = 3;
        }

        if (eMa_Mana == 0)
        {
            eMa_TurnTaken = true;
        }

        if (eMa_Mana > 0)
        {
            eMa_TurnTaken = false;
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
        eMa_Attacking = true;
        PlayerManager.p_Health = PlayerManager.p_Health - eMa_Damage;
        eMa_Mana--;
        Debug.Log("Looped");
    }

    // Misc Stuff

    private void positionSetters()
    {
        //Misc 

        if (pos1Count == 3 && pos2Count == 3 && pos3Count == 2)
        {
            pos2Count = 2;
            mageGO.transform.SetParent(pos2.transform);
            mageGO.transform.position = pos2.transform.position;

            pos3Count = 0;
        }

        if (pos1Count == 2 && pos2Count == 2)
        {
            pos2Count = 0;
        }

        // Pos1

        if (pos1Count == 1 && eT_Alive && spawnEnemyCount == 1)
        {
            tankGO.transform.SetParent(pos1.transform);
            tankGO.transform.position = pos1.transform.position;
        }
        if (pos1Count == 2 && eMa_Alive && spawnEnemyCount == 1)
        {
            mageGO.transform.SetParent(pos1.transform);
            mageGO.transform.position = pos1.transform.position;
        }
        if (pos1Count == 3 && eMe_Alive && spawnEnemyCount == 1)
        {
            meleeGO.transform.SetParent(pos1.transform);
            meleeGO.transform.position = pos1.transform.position;
        }

        // Pos2

        if (pos2Count == 1 && eT_Alive && spawnEnemyCount == 1)
        {
            tankGO.transform.SetParent(pos2.transform);
            tankGO.transform.position = pos2.transform.position;
        }
        if (pos2Count == 2 && eMa_Alive && spawnEnemyCount == 1)
        {
            mageGO.transform.SetParent(pos2.transform);
            mageGO.transform.position = pos2.transform.position;
        }
        if (pos2Count == 3 && eMe_Alive && spawnEnemyCount == 1)
        {
            meleeGO.transform.SetParent(pos2.transform);
            meleeGO.transform.position = pos2.transform.position;
        }

        // Pos 3

        if (pos3Count == 1 && eT_Alive && spawnEnemyCount == 1)
        {
            tankGO.transform.SetParent(pos3.transform);
            tankGO.transform.position = pos3.transform.position;
        }
        if (pos3Count == 2 && eMa_Alive && spawnEnemyCount == 1)
        {
            mageGO.transform.SetParent(pos3.transform);
            mageGO.transform.position = pos3.transform.position;
        }
        if (pos3Count == 3 && eMe_Alive && spawnEnemyCount == 1)
        {
            meleeGO.transform.SetParent(pos3.transform);
            meleeGO.transform.position = pos3.transform.position;
        }

        // Position 2

        if (pos2Count == 1 && !eT_Alive && eMa_Alive && pos3Count == 2)
        {
            pos2Count = 2;
            mageGO.transform.SetParent(pos2.transform);
            mageGO.transform.position = pos2.transform.position;
            pos3Count = 0;
        }

        if (pos2Count == 1 && !eT_Alive && eMe_Alive && pos3Count == 3)
        {
            pos2Count = 3;
            meleeGO.transform.SetParent(pos2.transform);
            meleeGO.transform.position = pos2.transform.position;
            pos3Count = 0;
        }

        if (pos2Count == 2 && !eMa_Alive && pos3Count == 1)
        {
            pos2Count = 1;
            tankGO.transform.SetParent(pos2.transform);
            tankGO.transform.position = pos2.transform.position;
            pos3Count = 0;
        }

        if (pos2Count == 2 && !eMa_Alive && pos3Count == 3)
        {
            pos2Count = 3;
            meleeGO.transform.SetParent(pos2.transform);
            meleeGO.transform.position = pos2.transform.position;
            pos3Count = 0;
        }

        if (pos2Count == 3 && !eMe_Alive && pos3Count == 1)
        {
            pos2Count = 1;
            tankGO.transform.SetParent(pos2.transform);
            tankGO.transform.position = pos2.transform.position;
            pos3Count = 0;
        }

        if (pos2Count == 3 && !eMe_Alive && pos3Count == 2)
        {
            pos2Count = 2;
            mageGO.transform.SetParent(pos2.transform);
            mageGO.transform.position = pos2.transform.position;
            pos3Count = 0;
        }

        // Position 1

        if (pos1Count == 1 && !eT_Alive && eMe_Alive && (pos2Count == 3 || pos3Count == 3))
        {
            pos1Count = 3;
            meleeGO.transform.SetParent(pos1.transform);
            meleeGO.transform.position = pos1.transform.position;
        }

        if (pos1Count == 1 && !eT_Alive && eMa_Alive && (pos2Count == 2 || pos3Count == 2))
        {
            pos1Count = 2;
            mageGO.transform.SetParent(pos1.transform);
            mageGO.transform.position = pos1.transform.position;
        }

        if (pos1Count == 2 && !eMa_Alive && eT_Alive && (pos2Count == 1 || pos3Count == 1))
        {
            pos1Count = 1;
            tankGO.transform.SetParent(pos1.transform);
            tankGO.transform.position = pos1.transform.position;
        }

        if (pos1Count == 2 && !eMa_Alive && eMe_Alive && (pos2Count == 3 || pos3Count == 3))
        {
            pos1Count = 3;
            meleeGO.transform.SetParent(pos1.transform);
            meleeGO.transform.position = pos1.transform.position;
        }

        if (pos1Count == 3 && !eMe_Alive && eMa_Alive && (pos2Count == 2 || pos3Count == 2))
        {
            pos1Count = 2;
            mageGO.transform.SetParent(pos1.transform);
            mageGO.transform.position = pos1.transform.position;
        }

        if (pos1Count == 3 && !eMe_Alive && eT_Alive && (pos2Count == 1 || pos3Count == 1))
        {
            pos1Count = 1;
            tankGO.transform.SetParent(pos1.transform);
            tankGO.transform.position = pos1.transform.position;
        } 
    }

    private void spawnEnemies()
    {
        if (spawnEnemyCount == 0)
        {
            errorDenier1.transform.SetParent(errDenierHolder.transform);
            errorDenier2.transform.SetParent(errDenierHolder.transform);
            errorDenier3.transform.SetParent(errDenierHolder.transform);

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
        if (eT_Health <= 0)
        {
            eT_Alive = false;
        }

        if (eMa_Health <= 0)
        {
            eMa_Alive = false;
        }

        if (eMe_Health <= 0)
        {
            eMe_Alive = false;
        }

        

        if (!eT_Alive)
        {
            Destroy(tankGO);

            if (pos1Count == 1)
            {
                errorDenier1.transform.SetParent(pos1.transform);
                inPos1 = true;
            }
            if (pos2Count == 1)
            {
                errorDenier1.transform.SetParent(pos2.transform);
                inPos2 = true;
            }

            if (pos3Count == 1)
            {
                errorDenier1.transform.SetParent(pos3.transform);
                inPos3 = true;
            }

            eT_TurnTaken = true;
        }

        if (!eMa_Alive)
        {
            Destroy(mageGO);

            if (pos1Count == 2)
            {
                errorDenier3.transform.SetParent(pos1.transform);
            }
            if (pos2Count == 2)
            {
                errorDenier3.transform.SetParent(pos2.transform);
            }

            if (pos3Count == 2)
            {
                errorDenier3.transform.SetParent(pos3.transform);
            }

            eMa_Mana = 0;
            eMa_TurnTaken = true;
        }

        if (!eMe_Alive)
        {
            Destroy(meleeGO);

            if (!eMa_Alive)
            {
                Destroy(mageGO);

                if (pos1Count == 3)
                {
                    errorDenier2.transform.SetParent(pos1.transform);
                }
                if (pos2Count == 3)
                {
                    errorDenier2.transform.SetParent(pos2.transform);
                }

                if (pos3Count == 3)
                {
                    errorDenier2.transform.SetParent(pos3.transform);
                }

                eMa_Mana = 0;
                eMa_TurnTaken = true;
            }

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

            pCombatManager.attackButton.SetActive(true);
            pCombatManager.endTurnButton.SetActive(true);
            pCombatManager.ability1.SetActive(true);
            pCombatManager.ability2.SetActive(true);

            //eMa_CoroutineHolder.SetActive(false);

            eT_TurnTaken = false;
            eMe_TurnTaken = false;
            eMa_TurnTaken = false;
            eMa_Attacking = false;
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