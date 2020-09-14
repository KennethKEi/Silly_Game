using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    private Player _playerComponent;
    private Animator _anim;
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _playerComponent = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_playerComponent == null)
        {
            Debug.LogError("_playercomponent doesn't exist");
        }
        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("_anim is null!");

        }

        float _spawnPointX = Random.Range(-26f, 26.0f);
        transform.position = new Vector3(_spawnPointX, 19, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float _spawnPointX = Random.Range(-26f, 26.0f);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.6f)
        {
            transform.position = new Vector3(_spawnPointX, 10, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player playerComponent = other.transform.GetComponent<Player>();

            if(playerComponent != null)
            {
                playerComponent.Damage();
            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_playerComponent != null)
            {
                _playerComponent.Addscore(Random.Range(1, 3));
            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }
    }
}
