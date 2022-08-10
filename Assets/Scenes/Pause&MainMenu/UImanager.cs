using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    private bool pauseButton;
    public GameObject PauseCanvas;
    private bool inventoryButton;
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnPause();
    }
    public void OnPauseButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseButton = true;


        }
        if (context.canceled)
        {
            pauseButton = false;
        }




    }
    public void OnPause()
    {
        if (pauseButton)
        {
            PauseCanvas.SetActive(true);

            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

           
        }

    }
    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventoryButton = true;


        }
        if (context.canceled)
        {
            inventoryButton = false;
        }




    }
    public void OnInventory()
    {
        if (inventoryButton)
        {
            inventory.SetActive(true);

            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            Debug.Log("i");
        }

    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
