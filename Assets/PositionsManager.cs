using UnityEngine;

public class PositionsManager : MonoBehaviour
{
    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    [SerializeField] private GameObject pos3;

    public static int pos1Child;
    public static int pos2Child;
    public static int pos3Child;

    private void Start()
    {
        pos1Child = 0;
        pos2Child = 0;
        pos3Child = 0;
    }

    public void Update()
    {
        pos1Checker();
        pos2Checker();
        pos3Checker();
    }

    private void pos1Checker()
    {
        if (pos1.transform.GetChild(1).gameObject.tag == "Tank")
        {
            pos1Child = 1;
        }

        if (pos1.transform.GetChild(1).gameObject.tag == "Melee")
        {
            pos1Child = 2;
        }

        if (pos1.transform.GetChild(1).gameObject.tag == "Mage")
        {
            pos1Child = 3;
        }
    }

    private void pos2Checker()
    {
        if (pos2.transform.GetChild(1).gameObject.tag == "Tank")
        {
            pos2Child = 1;
        }

        if (pos2.transform.GetChild(1).gameObject.tag == "Melee")
        {
            pos2Child = 2;
        }

        if (pos2.transform.GetChild(1).gameObject.tag == "Mage")
        {
            pos2Child = 3;
        }
    }

    private void pos3Checker()
    {
        if (pos3.transform.GetChild(1).gameObject.tag == "Tank")
        {
            pos3Child = 1;
        }

        if (pos3.transform.GetChild(1).gameObject.tag == "Melee")
        {
            pos3Child = 2;
        }

        if (pos3.transform.GetChild(1).gameObject.tag == "Mage")
        {
            pos3Child = 3;
        }
    }
}
