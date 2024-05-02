using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Vector3 LeavingTo;
    public bool Moving;

    public float DoorSpeed;

    public AudioSource DoorSound;
    public bool DoorOpened;
    void Start()
    {
        
    }


    public void OpenDoor()
    {
        if (DoorOpened == false)
        {
            Moving = true;
            DoorSound.Play();
        }

    }


    void Update()
    {
  
    }

    private void FixedUpdate()
    {
        if (Moving == true && DoorOpened == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, LeavingTo, DoorSpeed);
            if (transform.position == LeavingTo)
            {
                Moving = false;
                DoorOpened = true;

            }
        }
    }
}
