using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float _speed = 20f;
    private float _fireRate = 0.5f;
    private float _canFire = -1.0f;
    private bool _istripleshotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private Spawn_Manager _spawnManager;

    [SerializeField]
    private GameObject _laserPrefabs;

    [SerializeField]
    private GameObject _shieldVisualiser;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSound;

    
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn_Manager is NULL!");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on the Player is null");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();   
        }
                     
    }

    void CalculateMovement()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float Verticalinput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalinput, Verticalinput, 0);
       

        if(_isSpeedBoostActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else if (_isSpeedBoostActive == true)
        {
            transform.Translate(direction * (_speed * 2) * Time.deltaTime);
        }
        
        
        if (transform.position.y <= -8)
        {
            transform.position = new Vector3(transform.position.x, 11, transform.position.z);
        }
        else if (transform.position.y >= 11)
        {
            transform.position = new Vector3(transform.position.x, -8, transform.position.z);
        }
        
        if (transform.position.x <= -27.2)
        {
            transform.position = new Vector3(26, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= 27)
        {
            transform.position = new Vector3(-25.6f, transform.position.y, transform.position.z);
        }

     
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_istripleshotActive == false)
        {
            Instantiate(_laserPrefabs, transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);
        }
        else if (_istripleshotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        _audioSource.Play();

        
    }   

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualiser.SetActive(false);
            return;
        }
        _lives = _lives - 1;
        _uiManager.UpdateLives(_lives);
        
        if(_lives < 1)
        {
            _spawnManager.PlayerDied();
            Destroy(this.gameObject);
        }
    }
    public void PowerUp_SpeedBoost()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostRoutine());
    }

    IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }

    public void PowerUp_TripleShot()
    {
        _istripleshotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _istripleshotActive = false;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        
        _shieldVisualiser.SetActive(true);
    }
    public void Addscore(int points)
    {
        //_score += 10;
        _score = _score + points;
        _uiManager.UpdateScore(_score);
        

    }


}
