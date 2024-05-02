using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{

    public bool LevelEnded = false;
    public TextMeshPro KillCount;
    public GameObject Ui;
    public int Level;
    public Player_Movement PlayerStuff;
    public AudioSource EndSound;
    public AudioSource RadioSong;
    public AudioSource OtherEndSong;
    public string NextLevel;
    public bool UseLevelNumber;


    public void JumpStartLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelEnded == false)
        {
            LevelEnded = true;
            Ui.SetActive(true);
            EndSound.Play();
            RadioSong.Stop();
            OtherEndSong.Play();
            KillCount.text = "Kills: " + Player_Movement.KillCount.Kills;
            if (Player_Movement.KillCount.Kills >= Player_Movement.KillCount.EnemiesInLevel)
            {
                Player_Movement.KillCount. Award.SetActive(true);
            }
        }

        


    }
    private void Update()
    {
        if (LevelEnded == true)
        {
            PlayerStuff.WinCondition = 0; 
            if (Input.GetMouseButtonDown(0))
            {
                if (UseLevelNumber == true)
                {
                    SceneManager.LoadScene(Level);
                } else
                {
                    SceneManager.LoadScene(NextLevel) ;
                }
    
            }
        }
    }
}
