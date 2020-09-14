using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 19.0f;

    [SerializeField]
    private GameObject _explosionPrefab;

    private Spawn_Manager _spawnManager;

   

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate ( new Vector3(0, 0, 10) * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Laser" || other.tag == "Player")
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            _spawnManager.StartSpawning();
            
        }
        

    }
}
