using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalOpenText : MonoBehaviour
{

    public float AllowedDistance;
    public float DistanceFromPlayer;
    public GameObject Text;
    public AudioSource TextOpenSound;
    public AudioSource TextCloseSound;
    public bool TextOpened;
    public GameObject Camera;

    void Start()
    {
        
    }




    void Update()
    {
        DistanceFromPlayer = Vector3.Distance (transform.position,Player_Movement.Player.transform.position);
       Text.SetActive (true);
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (TextOpened == false)
                {
                if (DistanceFromPlayer < AllowedDistance)
                {

                    
                    TextOpenSound.Play();
                    Text.transform.position = Camera.transform.position + new Vector3(0, 0, 10);
                    TextOpened = true;
                }
                }
                else
                {
                    TextCloseSound.Play();
                    Text.transform.position = Camera.transform.position + new Vector3(24, 0,  10);
                    TextOpened = false;
                }

            }
        
       
    }
}
