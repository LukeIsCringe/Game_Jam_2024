using UnityEngine;
using TMPro;

public class PathManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject transitionScreen;
    [SerializeField] private float alphaIncrease;

    [SerializeField] private TMP_Text pathType;

    [SerializeField] private int selectionCount;

    [SerializeField] private GameObject carryButton, absorbButton, abstainButton;
    [SerializeField] private bool carrySelected, absorbSelected, abstainSelected;
    [SerializeField] private bool carryFirst, absorbFirst, abstainFirst;

    [SerializeField] private bool carry_absorb, carry_abstain, absorb_carry, absorb_abstain, abstain_carry, abstain_absorb;

    [SerializeField] private string[] pathTypes = {"Carry & Absorb", "Carry & Abstain", "Absorb & Carry", "Absorb & Abstain", "Abstain & Carry", "Abstain & Absorb"};
    [SerializeField] private int pathTypeNum = 0;

    [SerializeField] private bool pathSelected;

    private void Start()
    {
        selectionCount = 0;

        alphaIncrease = 0;

        carrySelected = false;
        absorbSelected = false;
        abstainSelected = false;
    }

    public void Update()
    {
        dualPathSelection();
        isPathSelected();
        pathTypeText();

        transitionScreen.GetComponent<SpriteRenderer>().color = new Color(0, 0.02532968f, 0.1886792f, alphaIncrease);
    }

    public void pathTypeText()
    {
        if (pathSelected)
        {
            if (alphaIncrease <= 1)
            {
                alphaIncrease = alphaIncrease + 0.1f * Time.deltaTime;
            }
            else if (alphaIncrease > 1)
            {
                alphaIncrease = 1;
            }

            pathType.text = ("You have chosen the path of " + pathTypes[pathTypeNum] + ". These are the paths you have dedicated yourself to on your journey. Good luck on your search for power...");
        }
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
        carryButton.SetActive(false);

        PlayerManager pManager = player.GetComponent<PlayerManager>();

        pManager.p_Health = pManager.p_Health + 15;
        carrySelected = true;

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
        pManager.p_Health = pManager.p_Health - 10;
        abstainSelected = true;

        abstainButton.SetActive(false);

        if (selectionCount == 0)
        {
            abstainFirst = true;
            selectionCount = 1;
        }
    }
}
