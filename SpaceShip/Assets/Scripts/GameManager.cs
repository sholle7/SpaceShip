using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;

    [SerializeField] private HealthSpot[] _healthSpots;
    [SerializeField] private Canvas _pauseMenu;
    [SerializeField] private Canvas _gameOverMenu;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private GameObject _nameInputGameObject;
    [SerializeField] private Canvas _upgradeCanvas;
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _allPlayers;
    
    private Vector2 _mousePosition; 

    private int _health;
    private int _score;
    private int _coins;
    private bool _paused;
    private int _shipLevel;
    int _requiredCoinsForUpgrade;
    private string _playerColor;
    void Awake()
    {
       
        if (_instance == null)
        {
            SetStartingVariables();
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            GameManager._instance.SetStartingVariables();
            GameManager._instance.SetRefferences();
            Destroy(gameObject);
        }
    }
    public void SetStartingVariables()
    {
        _shipLevel = 1;
        _score = 0;
        _coins = 0;
        _health = 3;
        _requiredCoinsForUpgrade = 10;
        _playerColor = "Blue";
    }
    public void Start()
    {
        GameManager._instance.ResetLives();
        GameManager._instance.ResetScoreAndCoinText();
    }
    public void ResetScoreAndCoinText()
    {
        _scoreText.text = "Score: " + 0;
        _coinsText.text = "0";
    }
    public void ResetLives()
    {
        _healthSpots[0].SetEnabledLife();
        _healthSpots[1].SetEnabledLife();
        _healthSpots[2].SetEnabledLife();
    }
    public void Update()
    {
        if (CanUpgrade())
        {
            if (_upgradeCanvas.gameObject.activeSelf == false) _upgradeCanvas.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space)) Upgrade();
        }
        else
        {
            if (_upgradeCanvas.gameObject.activeSelf == true) _upgradeCanvas.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused())
            {
                ResumeGame();
                
            }

            else
            {
                PauseGame();    
            }
        }
    }
    public bool CanUpgrade()
    {
        return _shipLevel<15 &&_coins >= _requiredCoinsForUpgrade;
    }
    public bool IsGamePaused()
    {
        return _paused;
    }
    public void SetRefferences()
    {
        _nameInputGameObject = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("InputName")); 
        _scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        _coinsText = GameObject.Find("CoinsText").GetComponent<TMP_Text>();
        _upgradeCanvas = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("Upgrade")).GetComponent<Canvas>();
        _gameOverMenu = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("GameOverMenu")).GetComponent<Canvas>();
        _pauseMenu = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("PauseMenu")).GetComponent<Canvas>();
        _healthSpots[0] = GameObject.Find("HealthSpot0").GetComponent<HealthSpot>();
        _healthSpots[1] = GameObject.Find("HealthSpot1").GetComponent<HealthSpot>();
        _healthSpots[2] = GameObject.Find("HealthSpot2").GetComponent<HealthSpot>();
        _player = GameObject.Find("Player" + _playerColor + _shipLevel);


    }
    public void PauseGame()
    {
        _mousePosition = Mouse.current.position.ReadValue();
        _player.GetComponent<Player>().enabled = false;
        Time.timeScale = 0;
        Cursor.visible = true;
        _paused = true;
        _pauseMenu.gameObject.SetActive(true);
    }
    public void TakeDamage()
    {
        _health--;
        if (_health == 0) {
            _health = 3;
             Cursor.visible = true;
            _healthSpots[0].SetDisabledLife();
            _gameOverMenu.gameObject.SetActive(true);

            if (CanSaveHighScore()) _nameInputGameObject.SetActive(true);
            else _nameInputGameObject.SetActive(false);
            

        }
        if (_health == 1)
        {
            _healthSpots[1].SetDisabledLife();
        }
        if(_health == 2)
        {
            _healthSpots[2].SetDisabledLife();
        }
    }
    public void AddCoin()
    {
        _coins++;
        _coinsText.text = _coins.ToString();
    }
    public void ResumeGame()
    {
        Mouse.current.WarpCursorPosition(_mousePosition);
        _player.GetComponent<Player>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        _paused = false;
        _pauseMenu.gameObject.SetActive(false);
    }
    public void AddScore()
    {
        _score += 10;
        _scoreText.text="Score: "+_score;
    }

    public void Upgrade()
    {
        if (_shipLevel >= 15)
        {          
            return;
        }
        _shipLevel++;
        _coins -= _requiredCoinsForUpgrade;
        _coinsText.text = _coins.ToString();
        GameObject upgradedPlayer = _allPlayers[_shipLevel-1];
        GameObject newPlayer = Instantiate(upgradedPlayer,_player.transform.position, _player.transform.rotation);
        Destroy(_player);
        _player = newPlayer;
    }
    public void SaveHighScore(String name)
    {
        if (PlayerPrefs.GetInt("HighScore1Score") != 0 && PlayerPrefs.GetInt("HighScore1Score") < _score)
        {
            String name1 = PlayerPrefs.GetString("HighScore1Name");
            int score1 = PlayerPrefs.GetInt("HighScore1Score");
            String name2 = PlayerPrefs.GetString("HighScore2Name");
            int score2 = PlayerPrefs.GetInt("HighScore2Score");

            PlayerPrefs.SetString("HighScore1Name", name);
            PlayerPrefs.SetInt("HighScore1Score", _score);

            PlayerPrefs.SetString("HighScore2Name", name1);
            PlayerPrefs.SetInt("HighScore2Score", score1);

            PlayerPrefs.SetString("HighScore3Name", name2);
            PlayerPrefs.SetInt("HighScore3Score", score2);

            _nameInputGameObject.SetActive(false);

            return;
        }
        if (PlayerPrefs.GetInt("HighScore1Score") == 0 && PlayerPrefs.GetInt("HighScore1Score") < _score)
        {
            PlayerPrefs.SetString("HighScore1Name", name);
            PlayerPrefs.SetInt("HighScore1Score", _score);
            _nameInputGameObject.SetActive(false);

            return;
        }

        if (PlayerPrefs.GetInt("HighScore2Score") != 0 && PlayerPrefs.GetInt("HighScore2Score") < _score)
        {
            String name2 = PlayerPrefs.GetString("HighScore2Name");
            int score2 = PlayerPrefs.GetInt("HighScore2Score");

            PlayerPrefs.SetString("HighScore2Name", name);
            PlayerPrefs.SetInt("HighScore2Score", _score);

            PlayerPrefs.SetString("HighScore3Name", name2);
            PlayerPrefs.SetInt("HighScore3Score", score2);
            _nameInputGameObject.SetActive(false);

            return;
        }
        if (PlayerPrefs.GetInt("HighScore2Score") == 0 && PlayerPrefs.GetInt("HighScore2Score") < _score)
        {
            PlayerPrefs.SetString("HighScore2Name", name);
            PlayerPrefs.SetInt("HighScore2Score", _score);
            _nameInputGameObject.SetActive(false);

            return;
        }
        if(PlayerPrefs.GetInt("HighScore3Score") < _score)
        {
            PlayerPrefs.SetString("HighScore3Name", name);
            PlayerPrefs.SetInt("HighScore3Score", _score);
            _nameInputGameObject.SetActive(false);

            return;
        }
    }
    public bool CanSaveHighScore()
    {
        if(_score == 0) return false;
        bool flag = PlayerPrefs.GetInt("HighScore1Score") == 0 || PlayerPrefs.GetInt("HighScore2Score") == 0 || PlayerPrefs.GetInt("HighScore3Score") == 0;
        if (PlayerPrefs.GetInt("HighScore1Score") != 0 && PlayerPrefs.GetInt("HighScore1Score") < _score) flag = true;
        if (PlayerPrefs.GetInt("HighScore2Score") != 0 && PlayerPrefs.GetInt("HighScore2Score") < _score) flag = true;
        if (PlayerPrefs.GetInt("HighScore3Score") != 0 && PlayerPrefs.GetInt("HighScore3Score") < _score) flag = true;
        return flag;
    }
}
