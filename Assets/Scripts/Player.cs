using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _lazerPrefab;
    [SerializeField]
    private GameObject _triplelazer;
    [SerializeField]
    private float firerate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lifes = 3;

    private bool _shieldEnabled = false;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _tripleshotenabled = false;
    [SerializeField]
    private GameObject _ShieldVisualizer;

    [SerializeField]
    private int _Score = 0;

    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private GameManager _GameManager;

    [SerializeField]
    private GameObject _leftFire;
    [SerializeField]
    private GameObject _rightFire;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _Explosionclip;


    void Start()
    {
        // take the current position = new position (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The Canvas is NULL");
        }
        _GameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI manager is NULL");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The Audio Source is NULL");
        }
        else
        {
            _audioSource.clip = _Explosionclip;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        CalcualteMovement();

        if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) 
        {
            FireLazer();
        }
        
        

    }
    void CalcualteMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        if (-11.3f > transform.position.x)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }
    void FireLazer()
    {
        _canFire = Time.time + firerate;
        if (_tripleshotenabled)
        {
            Instantiate(_triplelazer, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_lazerPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }
    public void Damage()
    {
        if (_shieldEnabled)
        {
            _ShieldVisualizer.SetActive(false);
            _shieldEnabled = false;
            return;
        }
        _lifes--;

        _uiManager.UpdateLives(_lifes);
        switch (_lifes)
        {
            case 1 :
                _rightFire.SetActive(true);
                _audioSource.Play();
                break;
            case 2 :
                _leftFire.SetActive(true);
                _audioSource.Play();
                break;
            default:
                break;
        }




        if (1 > _lifes)
        {
            _spawnManager.playerDeath();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _GameManager.GameOver();
        }
    }

    public void Tripleshotactive()
    {
        _tripleshotenabled = true;
        StartCoroutine(Tripleshotshutdown());
    }

    public void SpeedBoostactive()
    {
        _speed = 8.5f;
        StartCoroutine(SpeedBoostshutdown());
    }

    public void ShieldActivate()
    {
        _ShieldVisualizer.SetActive(true);
        _shieldEnabled = true;
    }

    public IEnumerator Tripleshotshutdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            _tripleshotenabled = false;
        }
    }
    public IEnumerator SpeedBoostshutdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            _speed = 5.0f;
        }
    }

    public void AddScore()
    {
        _Score += 10;
        _uiManager.UpdateScore(_Score);
    }

}
