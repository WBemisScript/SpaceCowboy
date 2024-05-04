using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickableDoorScript : MonoBehaviour
{
    public BoxCollider2D DoorCollider;
    public SpriteRenderer DoorClosedSprite;
    public Sprite DoorOpened;

    public AudioSource DoorOpenSound;
    public void ForceOpenDoor ()
    {
        DoorCollider.enabled = false;
        DoorClosedSprite.sprite = DoorOpened;
        DoorOpenSound.Play();
    }
}
