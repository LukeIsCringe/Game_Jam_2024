using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]public static int p_Health;
    public int p_MaxHealth;

    public int p_Mana;
    public int p_MaxMana;

    public int p_Damage;

    public static bool p_TurnTaken;

    public TMP_Text healthText;
    public TMP_Text manaText;

    private void Start()
    {
        p_MaxHealth = 80;
        p_Health = 80;
        p_Mana = 5;
        p_MaxMana = 5;
        p_Damage = 20;
    }

    public void Update()
    {
        setStatText();
        playerDeath();
    }

    private void setStatText()
    {
        healthText.text = new string ("Player Health: " + p_Health.ToString());
        manaText.text = new string("Player Mana: " + p_Mana.ToString());
    }

    private void playerDeath()
    {
        if (p_Health <= 0)
        {
            SceneManager.LoadScene("Main_Scene");
        }
    }
}
