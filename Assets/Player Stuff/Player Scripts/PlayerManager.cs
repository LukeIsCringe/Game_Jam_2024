using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int p_Health;
    public int p_Mana;
    public int p_Damage;

    public bool p_TurnTaken;

    public TMP_Text healthText;

    public void Update()
    {
        setHealthText();
    }

    private void setHealthText()
    {
        healthText.text = new string ("Player Health: "+p_Health.ToString());
    }
}
