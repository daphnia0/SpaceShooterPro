using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float _speed = 3f;
    float _rotate = 30.0f;
    private Player _Player;

    [SerializeField]
    private GameObject _explosion;

    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * _rotate * Time.deltaTime);
        Random rnd = new Random();
        if (-7f > transform.position.y)
        {
            float RandomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(RandomX, 10f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lazer")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _spawnManager.startSpawning();
            Destroy(other.gameObject);
            Destroy(this.gameObject,0.25f);
        }
    }
}
