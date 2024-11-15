using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MageAttackCoroutineHolder : MonoBehaviour
{
    [SerializeField] private GameObject enemiesManager;
    private void Awake()
    {
        StartCoroutine(mageAttackLoop(1f));
    }

    private void Update()
    {
        turnOffGO();
    }

    private IEnumerator mageAttackLoop(float delay)
    {
        EnemiesManager eManager = enemiesManager.GetComponent<EnemiesManager>();

        while (!eManager.outOfMana)
        {
            eManager.mageAttackLoop();
            yield return new WaitForSeconds(delay);
        }
    }

    private void turnOffGO()
    {
        EnemiesManager eManager = enemiesManager.GetComponent<EnemiesManager>();

        if (eManager.outOfMana)
        {
            gameObject.SetActive(false);
        }
    }
}
