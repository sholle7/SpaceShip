using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private static GameObject _highScoresPanel; 
    private Image image;
    private void Start()
    {
        if(_highScoresPanel == null) _highScoresPanel = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("HighScoresPanel"));
       
        image = GetComponent<Image>();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        Invoke(nameof(LoadNextScene), 0.2f); 
    }
    public void LoadNextScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.enabled = false;
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResumeGame()
    {
        image.enabled=false;
        GameManager._instance.ResumeGame();
    }
    public void ShowHighScores()
    {
        _highScoresPanel.SetActive(true);
    }
    public void Back()
    {
        _highScoresPanel.SetActive(false);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        Destroy(GameManager._instance.gameObject);
        Invoke(nameof(LoadMainMenu), 0.1f);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();
        SaveSystem.ShowHighScore();
    }
}
