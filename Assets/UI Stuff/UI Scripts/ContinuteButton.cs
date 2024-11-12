using UnityEngine;

public class ContinuteButton : MonoBehaviour
{
    [SerializeField] private GameObject explanationText;
    [SerializeField] private GameObject pathSelectionUI;
    [SerializeField] private GameObject tutorialManager;

    public void Start()
    {
        pathSelectionUI.SetActive(false);
    }

    public void ButtonClicked()
    {
        explanationText.SetActive(false);
        pathSelectionUI.SetActive(true);
        tutorialManager.SetActive(false);
        gameObject.SetActive(false);
    }
}
