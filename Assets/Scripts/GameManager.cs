using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool _IsGameover = false;
    public void GameOver()
    {
        _IsGameover = true;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && _IsGameover)
        { 
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
