using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;

    private bool _destroyed = false;
    private Player _Player;
    private Animator _anim;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _ExplosionSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player").GetComponent<Player>();
        if(_Player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The Audio Source is NULL");
        }
        else
        {
            _audioSource.clip = _ExplosionSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Random rnd = new Random();
        if (-7f > transform.position.y)
        {
            float RandomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(RandomX, 10f);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.transform.tag == "Lazer")
        {
            _audioSource.Play();
            Destroy(other.gameObject);
            if (_Player != null)
            {
                _Player.AddScore();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _destroyed = true;
            Destroy(this.gameObject,2.8f);
        }
        if(other.transform.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                if(!_destroyed)
                {
                    player.Damage();
                   _audioSource.Play();
                   _destroyed = true;
                }
            }
            _speed = 0;
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject,2.8f);
        }
    }

    
}
