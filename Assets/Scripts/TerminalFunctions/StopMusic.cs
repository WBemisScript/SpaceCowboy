using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    public AudioSource Song;
    public TerminalDoorScript Terminal;
    public bool DisableTerminalAfterWards;
    public bool EnableInstead;

    void Start()
    {
        
    }





    void Update()
    {
     if (Terminal.TerminalActivated == true)
        {
            if (EnableInstead == false)
            {
                Song.Stop();
                if (DisableTerminalAfterWards == true)
                {
                    this.enabled = false;
                }
            }
            if (EnableInstead == true)
            {
                if (Song.isPlaying == false)
                {
                    Song.Play();
                    if (DisableTerminalAfterWards == true)
                    {
                        this.enabled = false;
                    }
                }


            }
        }   
    }
}
