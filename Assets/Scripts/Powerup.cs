using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    private float _speed = 3.0f;

    [SerializeField]
    private int PowerUpID;

    [SerializeField]
    private AudioClip _powerUpSound;


    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -17.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     

        if(collision.tag == ("Player"))
        {
            Player playerComponent = collision.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);
            
            if (playerComponent != null)
                switch (PowerUpID)
            {
                case 0:
                        playerComponent.PowerUp_SpeedBoost();                    
                    break;
                case 1:
                        playerComponent.PowerUp_TripleShot();
                    break;
                case 2:
                        playerComponent.ShieldActive();
                        break;
                default:
                    Debug.Log("Default Value");
                    break;

            }

            
            
            Destroy(this.gameObject);
        }
    }
}
