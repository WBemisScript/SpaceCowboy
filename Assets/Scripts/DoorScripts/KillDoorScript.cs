using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDoorScript : MonoBehaviour
{
    public bool DoorActivated;
    public int KillsNeeded;
    public int KillsAtStart;
    public DoorScript Door;
public bool DoorOpened;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorActivated == true)
        {
            if (KillsAtStart == 0)
            {
                KillsAtStart = Player_Movement.KillCount.Kills;
            }
            if (Player_Movement.KillCount.Kills - KillsAtStart >= KillsNeeded)
            {
                if (DoorOpened== false)
                {
                    Door.OpenDoor();
                    DoorOpened = true;
                }
             
            }
        }
    }
}
