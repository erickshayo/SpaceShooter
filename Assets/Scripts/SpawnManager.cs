using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    private bool stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());

    }

    // Update is called once per frame
    void Update()
    {


    }

    //spwan game objects every 5 seconds
    //CreateAssetMenuAttribute a coroutine of type IEnumerator -- Yield Events
    //while loop

    IEnumerator SpawnRoutine()
    {
        // while loopInfinite loop()
        //Instantiate enemy prefab
        //YieldInstruction wait for 5 seconds
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

        }

    }

    public void OnPlayerDeath(){
        stopSpawning = true;

    }
}
