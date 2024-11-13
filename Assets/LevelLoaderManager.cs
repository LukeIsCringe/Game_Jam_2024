using UnityEngine;

public class LevelLoaderManager : MonoBehaviour
{
    [SerializeField] private GameObject pathManagerGO;
    public GameObject corpses;

    public void Update()
    {
        loadLevel1Assets();
    }

    private void loadLevel1Assets()
    {
        PathManager pathManger = pathManagerGO.GetComponent<PathManager>();

        if (pathManger.loadLevel1Assets)
        {
            Invoke("level1Assets", 6f);
        }


    }

    private void level1Assets()
    { 
        Destroy(corpses);
    }
}
