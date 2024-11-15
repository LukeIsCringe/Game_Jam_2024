using UnityEngine;

public class PositionsManager : MonoBehaviour
{
    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    [SerializeField] private GameObject pos3;

    public static int pos1Child;
    public static int pos2Child;
    public static int pos3Child;

    [SerializeField] private GameObject errorDenier1;
    [SerializeField] private GameObject errorDenier2;
    [SerializeField] private GameObject errorDenier3;

    [SerializeField] private GameObject errorDenierHolder;

    [SerializeField] private GameObject enemiesManager;

    private void Start()
    {
        pos1Child = 0;
        pos2Child = 0;
        pos3Child = 0;
    }

    public void Update()
    {
        EnemiesManager eManager = enemiesManager.GetComponent<EnemiesManager>();

        if (eManager.eT_Alive || eManager.eMa_Alive || eManager.eMe_Alive)
        {
            pos1Checker();
            pos2Checker();
            pos3Checker();
        }
    }

    private void pos1Checker()
    {
        if (pos1.transform.childCount > 0)
        {
            errorDenier1.transform.SetParent(pos1.transform);
        }

        if (pos1.transform.GetChild(1).gameObject.tag == "Tank")
        {
            pos1Child = 1;
        }

        if (pos1.transform.GetChild(1).gameObject.tag == "Melee")
        {
            pos1Child = 3;
        }

        if (pos1.transform.GetChild(1).gameObject.tag == "Mage")
        {
            pos1Child = 2;
        }

        else
        {
            errorDenier1.transform.SetParent(errorDenierHolder.transform);
        }
    }

    private void pos2Checker()
    {
        if (pos2.transform.childCount > 0)
        {
            errorDenier2.transform.SetParent(pos2.transform);
        }

        if (pos2.transform.GetChild(1).gameObject.tag == "Tank")
        {
            pos2Child = 1;
        }

        if (pos2.transform.GetChild(1).gameObject.tag == "Melee")
        {
            pos2Child = 3;
        }

        if (pos2.transform.GetChild(1).gameObject.tag == "Mage")
        {
            pos2Child = 2;
        }

        else
        {
            errorDenier2.transform.SetParent(errorDenierHolder.transform);
        }
    }

    private void pos3Checker()
    {
        if (pos2.transform.childCount > 0)
        {
            errorDenier2.transform.SetParent(pos2.transform);
        }

        if (pos2.transform.GetChild(1).gameObject.tag == "Tank")
        {
            pos2Child = 1;
        }

        if (pos2.transform.GetChild(1).gameObject.tag == "Melee")
        {
            pos2Child = 3;
        }

        if (pos2.transform.GetChild(1).gameObject.tag == "Mage")
        {
            pos2Child = 2;
        }

        else
        {
            errorDenier3.transform.SetParent(errorDenierHolder.transform);
        }
    }
}
