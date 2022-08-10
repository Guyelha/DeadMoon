using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    
   // public GameObject settingsCanvas;
    public bool settingsButton;
    public GameObject playHolder;
    public GameObject Store;
    public GameObject restartMenu;
    private bool isDead;
   
   
   private void Awake()
    {
    //   settingsCanvas.SetActive(false);
        settingsButton = false;
       // restartMenu.SetActive(false);

        
    }

   

    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
        Cursor.visible = false;
    }

    public void SettingsButton()
    {
        if(settingsButton == true)
        {
           // settingsCanvas.SetActive(true);
            print("settingspressed");
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("quiting");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }


    public void PlayHolderOff()
    {
     //   playHolder.SetActive(false);
    }
    public void PlayHolderOn()
    {
       // playHolder.SetActive(true);
    }
    public void StoreActive()
    {
     //   Store.SetActive(true);
    }
    public void Storeoff()
    {
      //  Store.SetActive(false);
    }
}
