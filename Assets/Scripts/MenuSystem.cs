using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{


    public GameObject MenuHolder;
    public bool MenuOpened;
   
    public  void Quit()
    {
        Application.Quit();
    }
    public  void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            MenuOpened = !MenuOpened;
        }

        MenuHolder.SetActive(MenuOpened);
    }
}
