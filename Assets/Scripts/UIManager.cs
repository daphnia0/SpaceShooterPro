using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _TextScore;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private GameObject _GameOver;
    [SerializeField]
    private GameObject _restart;
    
    // Start is called before the first frame update
    void Start()
    {
        _TextScore.text = "Score: " + 0;
        _GameOver.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateScore(int Score)
    {
        _TextScore.text = "Score: " + Score;
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            _restart.SetActive(true);
            StartCoroutine(GameOverFlicker());
        }
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _GameOver.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _GameOver.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

   
}
