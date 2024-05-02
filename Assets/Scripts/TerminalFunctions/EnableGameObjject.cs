using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjject : MonoBehaviour
{
    public GameObject Object;
    public TerminalDoorScript Terminal;

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
                Object.SetActive(false);
            }
            if (EnableInstead == true)
            {
                Object.SetActive(true);
            }
        }
    }
}
