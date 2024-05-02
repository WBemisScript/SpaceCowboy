using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGainScript : MonoBehaviour
{


    public float Health;
    public float Bleed;
    public bool SelfDestruct;
    public AudioSource CollectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Movement.PlayerSystem.Health.Health += Health;
        Player_Movement.PlayerSystem.Health.BleedingHealth += Bleed;
        if (SelfDestruct == true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            CollectSound.Play();
            Destroy(gameObject,.5f);
        }
    }
}
