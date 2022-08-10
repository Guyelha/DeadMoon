using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;


    public bool isPaused;



  private void Awake()
    {
       pauseMenu.SetActive(false);  
    }





    // Update is called once per frame
    void Update()
    {
    
        
    }




     public void PauseGame()
    {
       
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        
    }

  

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;  
        isPaused = false;

        
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
