using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private int _powerUpID;
    [SerializeField]
    private AudioClip _PowerUpClip;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (-5f > transform.position.y)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Player player = collision.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_PowerUpClip, Vector3.down);
            switch (_powerUpID)
            {
                case 0:
                    player.Tripleshotactive();
                    break;
                case 1:
                    player.SpeedBoostactive();
                    break;
                case 2:
                    player.ShieldActivate();
                    break;
                default:
                    break;
            }  
            Destroy(this.gameObject);
        }
    }
}
