using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunUiScript : MonoBehaviour
{

    public GameObject[] Bullets;
    public GunScript PlayerGun;
    public Player_Movement PlayerMovement;
    public DemonCoreThrow DemonCoreScript;
    public SpriteRenderer DoubleJumpSprite;
    public Sprite JumpNormal;
    public Sprite JumpEmpty;
    public PlayerHealth Health;
    public TextMeshPro HealthText;
    public TextMeshPro KillCount;
    
    public SpriteRenderer DemonCoreIcon;
    public Sprite DemonCoreNormal;
    public Sprite DemonCoreEmpty;



    void Start()
    {
        
    }



    void Update()
    {

        KillCount.text = "Kills: " + Player_Movement.KillCount.Kills;  
        HealthText.text = (Mathf.Round(Health.Health * 10) /10).ToString() + "/100";
     
        if (PlayerMovement.JumpsLeft !=0)
        {
            DoubleJumpSprite.sprite = JumpNormal;
        }
        if (PlayerMovement.JumpsLeft == 0)
        {
            DoubleJumpSprite.sprite = JumpEmpty;
        }

        if (DemonCoreScript.OffFrames > DemonCoreScript.Cooldown)
        {
            DemonCoreIcon.sprite = DemonCoreNormal;
        }
        if (DemonCoreScript.OffFrames < DemonCoreScript.Cooldown)
        {
            DemonCoreIcon.sprite = DemonCoreEmpty;
        }

        if (PlayerGun.Ammo > 0)
        {
            Bullets[0].SetActive(true);

        } else
        {
            Bullets[0].SetActive(false);
        }

        if (PlayerGun.Ammo > 1)
        {
            Bullets[1].SetActive(true);

        }
        else
        {
            Bullets[1].SetActive(false);
        }

        if (PlayerGun.Ammo > 2)
        {
            Bullets[2].SetActive(true);

        }
        else
        {
            Bullets[2].SetActive(false);
        }

        if (PlayerGun.Ammo > 3)
        {
            Bullets[3].SetActive(true);

        }
        else
        {
            Bullets[3].SetActive(false);
        }

        if (PlayerGun.Ammo > 4)
        {
            Bullets[4].SetActive(true);

        }
        else
        {
            Bullets[4].SetActive(false);
        }

        if (PlayerGun.Ammo > 5)
        {
            Bullets[5].SetActive(true);

        }

        else
        {
            Bullets[5].SetActive(false);
        }
    }
}
