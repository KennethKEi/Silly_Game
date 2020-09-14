using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _isPlayerDead = false;

    [SerializeField]
    private GameObject[] powerUp;
    


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(Powerup_SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator SpawnRoutine()
    {
        while (_isPlayerDead == false)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-21.28f, 20f), 10, 0);
            GameObject newEmeny = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            newEmeny.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

        }

    }

    public void PlayerDied()
    {
        _isPlayerDead = true;
    }

IEnumerator Powerup_SpawnRoutine()
    {
        while (_isPlayerDead == false)
        {
            Vector3 TS_positionToSpawn = new Vector3(Random.Range(-21.28f, 20f), 10, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUp[randomPowerUp], TS_positionToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        
    }


}

