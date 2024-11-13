using UnityEngine;

public class TransitionClose : MonoBehaviour
{
    private float alpha;
    private bool onSceneLoad = false;

    public void Awake()
    {
        alpha = 1f;
        onSceneLoad = true;
    }

    public void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0.02532968f, 0.1886792f, alpha);

        alphaDecrease();
    }

    public void alphaDecrease()
    {
        if (onSceneLoad && alpha > 0)
        {
            alpha = alpha - 0.25f * Time.deltaTime;
        }

        if (alpha <= 0)
        {
            alpha = 0;
            Destroy(gameObject);
        }
    }
}
