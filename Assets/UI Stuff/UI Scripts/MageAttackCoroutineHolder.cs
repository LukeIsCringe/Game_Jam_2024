using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MageAttackCoroutineHolder : MonoBehaviour
{
    [SerializeField] private GameObject enemiesManager;
    private void Start()
    {
        StartCoroutine(mageAttackLoop(1f));
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
}
