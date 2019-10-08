using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private GameObject _Enemy;
    [SerializeField]
    private GameObject _EnemyContainer;

    [SerializeField]
    private GameObject[] _PowerUp;
    [SerializeField]
    private GameObject _PowerUpContainer;

    private IEnumerator spawnCoroutine;

    private bool _stopSpawning = false;

    void Start()
    {

    }

    public void startSpawning()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerUpRoutine());
    }


    IEnumerator spawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_Enemy, posToSpawn,Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator spawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(_PowerUp[randomPowerUp], posToSpawn, Quaternion.identity);
            newPowerUp.transform.parent = _PowerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(1.0f,7.0f));
        }
    }

    public void playerDeath()
    {
        _stopSpawning = true;
    }
}
