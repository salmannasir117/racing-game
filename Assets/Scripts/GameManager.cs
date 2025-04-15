using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject levelPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject tutPanel;
    public GameObject creditPanel;

    void Start()
    {
        if (startPanel != null)
            startPanel.SetActive(true); 

        if (levelPanel != null)
            levelPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (losePanel != null)
            losePanel.SetActive(false);

        if (tutPanel != null)
            tutPanel.SetActive(false);
            
        if (creditPanel != null)
            creditPanel.SetActive(false);
    }

    public void StartNewGame()
    {
        levelPanel.SetActive(false);

        if (startPanel != null)
        {
            startPanel.SetActive(false); 
        
        }

        SceneManager.LoadScene("BeachScene");
    }

    public void Update(){
        
    }

    public void ChooseNewLevel()
    {
        startPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void SelectLevel1()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("BeachScene");
    }

    public void SelectLevel2()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("ForestMap");
    }

    public void SelectLevel3()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("VolcanoScene");
    }

    /* 
        for testing purposes. test that the timer resets on lose condition and restart level.
        used in scene Scenes/Timer/Beach Timer Test
    */
    public void TimerTestSelectLevel1() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Beach Timer Test");
    }
        
    public void tutPanelController() {
        if (tutPanel != null) {
            tutPanel.SetActive(true);
            startPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Resume before quitting
        Application.Quit();
    }

    public void BackToStart()
    {
        Time.timeScale = 1f;
        startPanel.SetActive(true);
        levelPanel.SetActive(false);
        tutPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void ShowCredits() {
        creditPanel.SetActive(true);
        startPanel.SetActive(false);
    }
}
