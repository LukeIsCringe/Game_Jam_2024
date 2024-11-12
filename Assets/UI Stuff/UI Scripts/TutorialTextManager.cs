using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class TutorialTextManager : MonoBehaviour
{
    public TMP_Text explanationText;
    public int textPart = 0;

    public GameObject continueButton;

    private string[] textParts = {
        "It seems you've come across two corpses in the forest, lucky you! To pick your path of power you must chose what to do with the corpses of your foes.",
        "You have 3 options, & you can pick 2 paths.",
        "You may carry a corpse in which you will gain health & the ability to PULL an enemy to the front of the attack order (the closer enemies are to you the more damage melee attacks do), however doing this makes you skip a turn of combat.",
        "Your second option is to absorb the corpse, by doing this you will increase your attack power however, you will lose mana meaning your attacks hit harder but there is less of them.",
        "You also gain the ability to PUSH the front enemy back a position which also deals damage (increases the more bodies absorbed).",
        "Finally, you may abstain and release the souls of the corpses. By selecting this you will increase your mana but lose health.",
        "You will also gain the ability to perform a FIRE ABILITY granted to you by a higher power which can deal a small amount of damage to all enemies which also does damage over time.",
        "The unique class abilites (the PUSH, PULL and FIRE) are also restricted to one per turn.",
        "If you are now ready to pick your paths (you must select 2) select CONTINUE or if you want it rexplained, click again...",
        ""};

    private void Start()
    {
        continueButton.SetActive(false);
    }

    public void Update()
    {
        textManagement();

        if (textPart == 8)
        {
            continueButton.SetActive(true);
        }
    }

    private void textManagement()
    {
        if (textPart < 8)
        {
            explanationText.text = (textParts[textPart] + " Click to continue...");
        }
        else
        {
            explanationText.text = (textParts[textPart]);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Invoke("textPartPlus", 0.2f);
        }

        if (textPart == 9)
        {
            textPart = 0;
        }
    }

    private void textPartPlus()
    {
        textPart++;
    }
}
