using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalDoorScript : MonoBehaviour
{
    public float TerminalRange;
    public DoorScript Door;
    public AudioSource TerminalSound;
    public float RangeFromPlayer;
    public bool TerminalActivated;
    void Start()
    {
        
    }

 
    void Update()
    {
        RangeFromPlayer = Vector3.Distance(Player_Movement.Player.transform.position, transform.position); 
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (RangeFromPlayer < TerminalRange)
            {
                TerminalSound.Play();
                Door.OpenDoor();
                TerminalActivated = true;
            }
        }
      
    }
}
