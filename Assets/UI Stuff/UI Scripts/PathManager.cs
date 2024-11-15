using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PathManager : MonoBehaviour
{
    // Sprites

    [SerializeField] private Sprite carry_absorb_sprite, carry_abstain_sprite, absorb_carry_sprite, absorb_abstain_sprite, abstain_carry_sprite, abstain_absorb_sprite;

    // Player stuff
    public GameObject player;
    [SerializeField] private GameObject playerSprite;

    [SerializeField] private GameObject playerHealthUI;
    [SerializeField] private GameObject playerManaUI;

    [SerializeField] private GameObject playerUICover;

    [SerializeField] private float healthAlpha;

    // Transition & loading stuff

    [SerializeField] private GameObject transitionScreen;
    [SerializeField] private GameObject pathText;

    [SerializeField] private float alpha;
    [SerializeField] private float alphaDecrease;
    public bool level1Loaded;
    public bool loadLevel1Assets;
    
    // Path selection stuff

    [SerializeField] private TMP_Text pathType;

    [SerializeField] private int selectionCount;

    [SerializeField] private GameObject carryButton, absorbButton, abstainButton;
    [SerializeField] private bool carrySelected, absorbSelected, abstainSelected;
    [SerializeField] private bool carryFirst, absorbFirst, abstainFirst;

    [SerializeField] private bool carry_absorb, carry_abstain, absorb_carry, absorb_abstain, abstain_carry, abstain_absorb;

    [SerializeField] private string[] pathTypes = {"Carry & Absorb", "Carry & Abstain", "Absorb & Carry", "Absorb & Abstain", "Abstain & Carry", "Abstain & Absorb"};
    [SerializeField] private int pathTypeNum = 0;

    [SerializeField] private bool pathSelected;

    // Ability Setting stuff

    [SerializeField] private string[] abilityNames = {"Chain Pull", "Beastly Push", "Pharisaical Fire"};

    public bool chainPull;
    public bool beastlyPush;
    public bool pharFire;

    [SerializeField] private GameObject Ability_1_Button;
    [SerializeField] private TMP_Text Ability_1_Text;

    [SerializeField] private GameObject Ability_2_Button;
    [SerializeField] private TMP_Text Ability_2_Text;

    private void Start()
    {
        selectionCount = 0;

        alpha = 0;
        alphaDecrease = 0.25f;

        healthAlpha = 0;

        carrySelected = false;
        absorbSelected = false;
        abstainSelected = false;

        level1Loaded = false;

        loadLevel1Assets = false;

        chainPull = false;
        beastlyPush = false;
        pharFire = false;
    }

    public void Update()
    {
        dualPathSelection();
        isPathSelected();
        pathTypeText();
        abilitySetter();
        
        if (pathSelected)
        {
            Invoke("spriteSetter", 7f);
        }

        transitionScreen.GetComponent<SpriteRenderer>().color = new Color(0, 0.02532968f, 0.1886792f, alpha);
        pathType.GetComponent<TextMeshProUGUI>().color = new Color(0.6745098f, 0.803049f, 0.9607843f, alpha);
        playerHealthUI.GetComponent<TextMeshProUGUI>().color = new Color(1, 0.1745283f, 0.1745283f, healthAlpha);
        playerManaUI.GetComponent<TextMeshProUGUI>().color = new Color(0.4229263f, 0.5512053f, 0.9056604f, healthAlpha);
    }
    // carry_absorb_sprite, carry_abstain_sprite, absorb_carry_sprite, absorb_abstain_sprite, abstain_carry_sprite, abstain_absorb_sprite;
    private void spriteSetter()
    {
        if (carry_absorb)
        {
            playerSprite.GetComponent<SpriteRenderer>().sprite = carry_absorb_sprite;
        }

        if (carry_abstain)
        {
            playerSprite.GetComponent<SpriteRenderer>().sprite = carry_abstain_sprite;
        }

        if (absorb_carry)
        {
            playerSprite.GetComponent<SpriteRenderer>().sprite = absorb_carry_sprite;
        }

        if (absorb_abstain)
        {
            playerSprite.GetComponent<SpriteRenderer>().sprite = absorb_abstain_sprite;
        }

        if (abstain_carry)
        {
            playerSprite.GetComponent<SpriteRenderer>().sprite = abstain_carry_sprite;
        }

        if (abstain_absorb)
        {
            playerSprite.GetComponent<SpriteRenderer>().sprite = abstain_absorb_sprite;
        }
    }

    private void abilitySetter()
    {
        if (carry_absorb)
        {
            chainPull = true;
            beastlyPush = true;

            Ability_1_Text.text = abilityNames[0];
            Ability_2_Text.text = abilityNames[1];

            Ability_1_Button.tag = "Chain";
            Ability_2_Button.tag = "Beast";
        }

        if (carry_abstain)
        {
            chainPull = true;
            pharFire = true;

            Ability_1_Text.text = abilityNames[0];
            Ability_2_Text.text = abilityNames[2];

            Ability_1_Button.tag = "Chain";
            Ability_2_Button.tag = "Abstain";
        }

        if (absorb_carry)
        {
            beastlyPush = true;
            chainPull = true;

            Ability_1_Text.text = abilityNames[1];
            Ability_2_Text.text = abilityNames[0];

            Ability_1_Button.tag = "Beast";
            Ability_2_Button.tag = "Chain";
        }

        if (absorb_abstain)
        {
            beastlyPush = true;
            pharFire = true;

            Ability_1_Text.text = abilityNames[1];
            Ability_2_Text.text = abilityNames[2];

            Ability_1_Button.tag = "Beast";
            Ability_2_Button.tag = "Abstain";
        }

        if (abstain_carry)
        {
            pharFire = true;
            chainPull = true;

            Ability_1_Text.text = abilityNames[2];
            Ability_2_Text.text = abilityNames[0];

            Ability_1_Button.tag = "Abstain";
            Ability_2_Button.tag = "Chain";
        }

        if (abstain_absorb)
        {
            pharFire = true;
            beastlyPush = true;

            Ability_1_Text.text = abilityNames[2];
            Ability_2_Text.text = abilityNames[1];

            Ability_1_Button.tag = "Abstain";
            Ability_2_Button.tag = "Beast";
        }
    }

    public void pathTypeText()
    {
        if (pathSelected)
        {
            if (alpha < 1 && !level1Loaded)
            {
                alpha = alpha + 0.2f * Time.deltaTime;
            }
            else if (alpha > 1 && !level1Loaded)
            {
                alpha = 1;
            }

            pathType.text = ("You have chosen the path of " + pathTypes[pathTypeNum] + ". These are the paths you have dedicated yourself to on your journey. Good luck on your search for power...");

            loadLevel1Assets = true;

            Invoke("loadLevel1", 10);
        }
    }

    private void loadLevel1()
    {
        transitionClose();

        //Make player sprite correct
        
    }

    private void transitionClose()
    {
        level1Loaded = true;

        alpha = alpha - alphaDecrease * Time.deltaTime;

        if (alpha <= 0)
        {
            alphaDecrease = 0;
            alpha = 0;
        }

        healthAlpha = healthAlpha + 0.25f * Time.deltaTime;
    }

    public void dualPathSelection()
    {
        if (carrySelected && carryFirst && absorbSelected)
        {
            abstainButton.SetActive(false);
            carry_absorb = true;
            pathTypeNum = 0;
        }

        if (carrySelected && carryFirst && abstainSelected)
        {
            absorbButton.SetActive(false);
            carry_abstain = true;
            pathTypeNum = 1;
        }

        if (absorbSelected && absorbFirst && carrySelected)
        {
            abstainButton.SetActive(false);
            absorb_carry = true;
            pathTypeNum = 2;
        }

        if (absorbSelected && absorbFirst && abstainSelected)
        {
            carryButton.SetActive(false);
            absorb_abstain = true;
            pathTypeNum = 3;
        }

        if (abstainSelected && abstainFirst && carrySelected)
        {
            absorbButton.SetActive(false);
            abstain_carry = true;
            pathTypeNum = 4;
        }

        if (abstainSelected && abstainFirst && absorbSelected)
        {
            carryButton.SetActive(false);
            abstain_absorb = true;
            pathTypeNum = 5;
        }
    }

    private void isPathSelected()
    {
        if (carry_absorb || carry_abstain || absorb_carry || absorb_abstain || abstain_carry || abstain_absorb)
        {
            pathSelected = true;
        }
    }


    public void carryPath()
    {
        PlayerManager pManager = player.GetComponent<PlayerManager>();

        PlayerManager.p_Health = PlayerManager.p_Health + 15;
        pManager.p_MaxHealth = pManager.p_MaxHealth + 15;

        carrySelected = true;

        carryButton.SetActive(false);

        if (selectionCount == 0)
        {
            carryFirst = true;
            selectionCount = 1;
        }
    }

    public void absorbPath()
    {
        PlayerManager pManager = player.GetComponent<PlayerManager>();

        pManager.p_Damage = pManager.p_Damage + 10;

        pManager.p_Mana = pManager.p_Mana - 2;
        pManager.p_MaxMana = pManager.p_MaxMana - 2;

        absorbSelected = true;

        absorbButton.SetActive(false);

        if (selectionCount == 0)
        {
            absorbFirst = true;
            selectionCount = 1;
        }
    }

    public void abstainPath()
    {
        PlayerManager pManager = player.GetComponent<PlayerManager>();

        pManager.p_Mana = pManager.p_Mana + 3;
        pManager.p_MaxMana = pManager.p_MaxMana + 3;

        PlayerManager.p_Health = PlayerManager.p_Health - 10;
        //PlayerManager.p_MaxHealth = PlayerManager.p_MaxHealth - 10;
        abstainSelected = true;

        abstainButton.SetActive(false);

        if (selectionCount == 0)
        {
            abstainFirst = true;
            selectionCount = 1;
        }
    }
}
