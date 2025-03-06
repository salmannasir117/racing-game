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
    }

    public void StartNewGame()
    {
        levelPanel.SetActive(false);

        if (startPanel != null)
        {
            startPanel.SetActive(false); 
        
        }

        SceneManager.LoadScene("Level1_Beach");
    }

    public void Update(){
        // Toggle pause menu with Escape key
        if (Input.GetKeyUp(KeyCode.W))
        {
            winPanel.SetActive(true);
            losePanel.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.L)) 
        {
            losePanel.SetActive(true);
            winPanel.SetActive(false);
        }
    }

    public void ChooseNewLevel()
    {
        startPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void SelectLevel1()
    {
        SceneManager.LoadScene("Level1_Beach");
    }

    public void SelectLevel2()
    {
        SceneManager.LoadScene("Level2_Forest");
    }

    public void SelectLevel3()
    {
        SceneManager.LoadScene("Level3_Volcano");
    }
}
