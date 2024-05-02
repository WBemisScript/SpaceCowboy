using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScripts : MonoBehaviour
{

    public bool Dead;
    public PlayerHealth Health;
    public GameObject SensorBar;
    public GameObject HealthBar;
    public GameObject EndingScreen;
    void Start()
    {
        
    }

   



    void Update()
    {
     if (Health.Health <= 0)
        {
            Dead = true;
        }   


     if (Dead == true)
        {
            HealthBar.SetActive(false);
            EndingScreen.SetActive(true);
            SensorBar.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
        }
    }
}
