using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField] 
    private GameObject _restartText;
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
        _gameOverText.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game manager is NULL");
        }
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprite[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _restartText.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameOverTextFlicker());
    }

    IEnumerator GameOverTextFlicker()
    {

        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

  

}
