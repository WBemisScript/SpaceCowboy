using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{


    public int Kills;
    public int EnemiesInLevel;
    public int fuckall;
    public bool RestartDetection = false;
    public int FramesDampen;
    public GameObject Award;
    void Start()
    {

        Debug.Log("CUMCUMCUMCUMCUCM");

        Kills = fuckall;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (RestartDetection == false)
        {
            FramesDampen += 1;
            if (FramesDampen > 10) {

                Kills *= 0;
                RestartDetection = true;
            }
      
     
        }
    }
}
