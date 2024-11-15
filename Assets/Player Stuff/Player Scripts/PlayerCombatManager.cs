using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatManager : MonoBehaviour
{
    // Button Game Objects

    public GameObject ability1;
    public GameObject ability2;

    public GameObject endTurnButton;

    [SerializeField] private GameObject pos1Button;
    [SerializeField] private GameObject pos2Button;
    [SerializeField] private GameObject pos3Button;

    public GameObject attackButton;

    // Position Bools

    [SerializeField] private bool pos1Selected;
    [SerializeField] private bool pos2Selected;
    [SerializeField] private bool pos3Selected;

    // Player UI Cover

    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject playerUICover;
    [SerializeField] private GameObject playerUICombatCover;

    // Alpha floats

    [SerializeField] private float alpha;
    [SerializeField] private float combatAlpha;

    // Attack type bools

    [SerializeField] private bool attacking;
    [SerializeField] private bool pulling;
    [SerializeField] private bool pushing;
    [SerializeField] private bool firing;

    // Particles

    [SerializeField] private ParticleSystem fireAttackPos1;
    [SerializeField] private ParticleSystem fireAttackPos2;
    [SerializeField] private ParticleSystem fireAttackPos3;

    // Misc

    public bool playerTurn;
    public int selectedEnemy;
    [SerializeField] private GameObject pathManagerGO;
    [SerializeField] private GameObject enemyManager;
    [SerializeField] private GameObject playerManager;
    [SerializeField] private GameObject posManager;

    [SerializeField] private int chainPosHolder;

    private void Start()
    {
        selectedEnemy = 0;

        pos1Button.SetActive(false);
        pos2Button.SetActive(false);
        pos3Button.SetActive(false);

        pos1Selected = false;
        pos2Selected = false;
        pos3Selected = false;

        playerTurn = true;

        playerUI.SetActive(false);
        playerUICover.SetActive(false);
        playerUICombatCover.SetActive(false);

        attacking = false;
        pulling = false;
        pushing = false;
        firing = false;

        chainPosHolder = 0;

        alpha = 1;

        combatAlpha = 0;
    }

    public void Update()
    {
        playerUICover.GetComponent<Image>().color = new Color(0.7647059f, 0.7647059f, 0.7647059f, alpha);
        playerUICombatCover.GetComponent<Image>().color = new Color(0.7647059f, 0.7647059f, 0.7647059f, combatAlpha);

        PathManager pManager = pathManagerGO.GetComponent<PathManager>();
        if (pManager.loadLevel1Assets)
        {
            Invoke("UICover", 13f);
        }

        turnChangeover();
        resetButtons();
        zeroMana();
    }

    private void turnChangeover()
    {
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        if (eManager.enemyTurn)
        {
            pManager.p_Mana = pManager.p_MaxMana;
        }

        if (playerTurn && !eManager.enemyTurn)
        {
            playerUICombatCover.SetActive(false);

            //attackButton.SetActive(true);

            //endTurnButton.SetActive(true);

            //ability1.SetActive(true);
            //ability2.SetActive(true);
        } 
    }

    private void UICover()
    {
        playerUICover.SetActive(true);
        playerUI.SetActive(true);

        if (alpha > 0)
        {
            alpha = alpha - 0.5f * Time.deltaTime;
        }

        if (alpha <= 0)
        {
            playerUICover.SetActive(false);
        }
    }

    // Default attack
    public void playerAttack()
    {
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();

        attacking = true;

        if (eManager.pos1Count == 1 && eManager.eT_Alive)
        {
            pos1Button.SetActive(true);
        }

        if (eManager.pos2Count == 1 && eManager.eT_Alive)
        {
            pos1Button.SetActive(true);
            pos2Button.SetActive(true);
        }

        if (eManager.pos3Count == 1 && eManager.eT_Alive)
        {
            pos1Button.SetActive(true);
            pos2Button.SetActive(true);
            pos3Button.SetActive(true);
        }

        /*if (!eManager.eT_Alive && eManager.pos1Count == 3 && eManager.pos2Count == 2)
        {
            pos1Button.SetActive(true);
            pos2Button.SetActive(true);
        }

        if (eManager.pos1Count == 2)
        {
            pos1Button.SetActive(true);
            pos2Button.SetActive(false);
            pos3Button.SetActive(false);
        }

        if (eManager.pos1Count == 3)
        {
            pos1Button.SetActive(true);
            pos2Button.SetActive(false);
            pos3Button.SetActive(false);
        }*/

        playerUICombatCover.SetActive(true);
        combatAlpha = 1;
    }

    // Default attack position checkers

    private void pos1Checker()
    {
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        if (eManager.pos1Count == 1)
        {
            eManager.eT_Health = eManager.eT_Health - pManager.p_Damage;
            pManager.p_Mana = pManager.p_Mana - 2;
        }

        if (eManager.pos1Count == 3)
        {
            eManager.eMe_Health = eManager.eMe_Health - pManager.p_Damage;
            pManager.p_Mana = pManager.p_Mana - 2;
        }

        if (eManager.pos1Count == 2)
        {
            eManager.eMa_Health = eManager.eMa_Health - pManager.p_Damage;
            pManager.p_Mana = pManager.p_Mana - 2;
        }
    }

    private void pos2Checker()
    {
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        if (eManager.pos2Count == 1)
        {
            eManager.eT_Health = eManager.eT_Health - (pManager.p_Damage - 3);
            pManager.p_Mana = pManager.p_Mana - 2;
        }

        if (eManager.pos2Count == 3)
        {
            eManager.eMe_Health = eManager.eMe_Health - (pManager.p_Damage - 3);
            pManager.p_Mana = pManager.p_Mana - 2;
        }

        if (eManager.pos2Count == 2)
        {
            eManager.eMa_Health = eManager.eMa_Health - (pManager.p_Damage - 3);
            pManager.p_Mana = pManager.p_Mana - 2;
        }
    }

    private void pos3Checker()
    {
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        if (eManager.pos3Count == 1)
        {
            eManager.eT_Health = eManager.eT_Health - (pManager.p_Damage - 3);
            pManager.p_Mana = pManager.p_Mana - 2;
        }

        if (eManager.pos3Count == 3)
        {
            eManager.eMe_Health = eManager.eMe_Health - (pManager.p_Damage - 3);
            pManager.p_Mana = pManager.p_Mana - 2;
        }

        if (eManager.pos3Count == 2)
        {
            eManager.eMa_Health = eManager.eMa_Health - (pManager.p_Damage - 3);
            pManager.p_Mana = pManager.p_Mana - 2;
        }
    }

    // Position buttons

    public void position1Button()
    {
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        pos1Selected = true;
        pos1Button.SetActive(false);
        pos2Button.SetActive(false);
        pos3Button.SetActive(false);

        if (pManager.p_Mana > 0 && attacking)
        {
            pos1Checker();
            attacking = false;
        }
    }

    public void position2Button()
    {
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();

        pos2Selected = true;
        pos1Button.SetActive(false);
        pos2Button.SetActive(false);
        pos3Button.SetActive(false);

        if (pulling)
        {
            pos1Checker();
            pos2Checker();
            pos3Checker();

            chainPosHolder = eManager.pos1Count;
            eManager.pos1Count = eManager.pos2Count;
            eManager.pos2Count = chainPosHolder;

            pManager.p_Mana = pManager.p_Mana - 1;

            pulling = false;
        }

        if (pManager.p_Mana > 0 && attacking)
        {

            pos2Checker();
            attacking = false;
        }

    }

    public void position3Button()
    {
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();

        pos3Selected = true;
        pos1Button.SetActive(false);
        pos2Button.SetActive(false);
        pos3Button.SetActive(false);

        if (pulling)
        {
            pos1Checker();
            pos2Checker();
            pos3Checker();

            chainPosHolder = eManager.pos1Count;
            eManager.pos1Count = eManager.pos3Count;
            eManager.pos3Count = chainPosHolder;

            pManager.p_Mana = pManager.p_Mana - 1;

            pulling = false;
        }

        if (pManager.p_Mana > 0 && attacking)
        {
            pos3Checker();
            attacking = false;
        }

    }

    // Abilities

    public void Abilty1()
    {
        Ability1Setter();
    }

    public void Ability2()
    {
        Ability2Setter();
    }

    private void Ability1Setter()
    {
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        if (ability1.tag == "Chain")
        {
            //chain ability
            Debug.Log("Chain");

            pulling = true;

            pos2Button.SetActive(true);
            pos3Button.SetActive(true);
            ability1.SetActive(false);
            ability2.SetActive(false);
        }

        if (ability1.tag == "Beast")
        {
            //beast ability
            Debug.Log("Beast");

            pos1Button.SetActive(true);
            pos2Button.SetActive(true);
            ability1.SetActive(false);
            ability2.SetActive(false);
        }

        if (ability1.tag == "Abstain")
        {
            //fire ability
            Debug.Log("Fire");

            pManager.p_Mana = pManager.p_Mana - 2;

            ability1.SetActive(false);
            ability2.SetActive(false);

            fireAttackPos1.Play();
            fireAttackPos2.Play();
            fireAttackPos3.Play();

            Invoke("fireDamage", 1f);
        }
    }

    private void Ability2Setter()
    {
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        if (ability2.tag == "Chain")
        {
            //chain ability
            Debug.Log("Chain");

            pos2Button.SetActive(true);
            pos3Button.SetActive(true);
            ability1.SetActive(false);
            ability2.SetActive(false);
        }

        if (ability2.tag == "Beast")
        {
            //beast ability
            Debug.Log("Beast");

            pos1Button.SetActive(true);
            pos2Button.SetActive(true);
            ability1.SetActive(false);
            ability2.SetActive(false);
        }

        if (ability2.tag == "Abstain")
        {
            //fire ability
            Debug.Log("Fire");

            pManager.p_Mana = pManager.p_Mana - 2;

            ability1.SetActive(false);
            ability2.SetActive(false);

            fireAttackPos1.Play();
            fireAttackPos2.Play();
            fireAttackPos3.Play();

            Invoke("fireDamage", 1f);
        }
    }

    private void fireDamage()
    {
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();

        eManager.eT_Health = eManager.eT_Health - 5;
        eManager.eMe_Health = eManager.eMe_Health - 5;
        eManager.eMa_Health = eManager.eMa_Health - 5;
    }

    private void resetButtons()
    {
        if (!attacking && !pulling && !pushing && !firing)
        {
            playerUICombatCover.SetActive(false);
        }
    }

    private void zeroMana()
    {
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        if (pManager.p_Mana <= 0)
        {
            pos1Button.SetActive(false);
            pos2Button.SetActive(false);
            pos3Button.SetActive(false);

            attackButton.SetActive(false);

            ability1.SetActive(false);
            ability2.SetActive(false);

            pManager.p_Mana = 0;
        }
    }

    public void endTurn()
    {
        EnemiesManager eManager = enemyManager.GetComponent<EnemiesManager>();
        PlayerManager pManager = playerManager.GetComponent<PlayerManager>();

        combatAlpha = 1;

        pManager.p_Mana = 0;

        eManager.eMa_Mana = 3;

        //eManager.eMa_CoroutineHolder.GetComponent<MageAttackCoroutineHolder>().enabled = false;

        eManager.eMa_TurnTaken = false;
        
        playerTurn = false;
        eManager.enemyTurn = true;

        zeroMana();

        endTurnButton.SetActive(false);
    }
}