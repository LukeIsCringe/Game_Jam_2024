using UnityEngine;

public class ContinuteButton : MonoBehaviour
{
    [SerializeField] private GameObject explanationText;
    [SerializeField] private GameObject pathSelectionUI;

    public void ButtonClicked()
    {
        explanationText.SetActive(false);
        pathSelectionUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
