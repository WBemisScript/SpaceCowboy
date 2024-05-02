using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTurret : MonoBehaviour
{
    public TurrentAi Turret;
    public TerminalDoorScript Terminal;
    private bool Ran;
        private void FixedUpdate()
    {
        if (Terminal.TerminalActivated == true)
        {
            if (Ran == false)
            {
                Ran = true;
                Turret.Aggro = true;
            }
        }
    }
}
