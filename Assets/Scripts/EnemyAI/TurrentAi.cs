using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentAi : MonoBehaviour
{
    public Rigidbody2D Rotatus;
    
    public GameObject ShotPoint;
    public bool Aiming;
    public bool Aggro;
    public float Damage;
    public int ChargeFrames;
    private int FramesCharging;
    public int AimFrames;
    private int FramesAiming;
    public LineRenderer BulletTracer;
    public GameObject Flash;
    public int FramesOfFlash;
    private int FlashedFrames;
    public GameObject ParticalHit;
    public Rigidbody2D Rb;
    public GameObject WarningLight;
    public AudioSource HitSound;
    public AudioSource WarningSound;
    public AudioSource ShotSound;
    public bool ImportantOnAggro;
    public Enemy_Information Info;
    public float BleedDamage;
    public EnemyHealth Health;
    public GameObject DeathEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.Health <= 0)
        {
            GameObject Death=  Instantiate(DeathEffect, transform.position,Quaternion.identity);
            Destroy(Death, 20);
            Death.transform.rotation = new Quaternion(0, 0, Random.Range(0,360), 0);
            Destroy(gameObject);
            Player_Movement.KillCount.Kills += 1;
        }
        Rotatus.position = transform.position;
        if (Aggro == true)
        {
            if (ImportantOnAggro == true)
            {
                Info.Important = true;
            }
            if (Aiming == true)
            {
                Vector2 LookDirection = new Vector2(Player_Movement.Player.transform.position.x, Player_Movement.Player.transform.transform.position.y) - Rb.position;
                float angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg + 90f;
              
                Rotatus.rotation = angle;
            }
        }
        FlashedFrames += 1;


        if (FlashedFrames < FramesOfFlash)
        {
            Flash.SetActive(true);
            BulletTracer.SetPosition(0, Flash.transform.position);


        }
        else if (FlashedFrames > FramesOfFlash)
        {
            Flash.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (Aggro == true)
        {
            if (Aiming == true)
            {
                FramesAiming += 1;
                if (FramesAiming > AimFrames)
                {
                    FramesAiming = 0;
                    Aiming = false;
                    WarningSound.Play();
                }
            }
            if (Aiming == false)
            {
                WarningLight.SetActive(true);
                FramesCharging += 1;

                if (FramesCharging > ChargeFrames)
                {
                    ShotSound.Play();
                    FramesCharging = 0;
                    Aiming = true;
                    if (Physics2D.Raycast(ShotPoint.transform.position, ShotPoint.transform.right, 100))
                    {
                        WarningLight.SetActive(false);
                        HitSound.Play();
                        RaycastHit2D HitEnemy = Physics2D.Raycast(ShotPoint.transform.position, ShotPoint.transform.right, 100);
                        Debug.Log(HitEnemy.collider.gameObject.name);
                        GameObject Partical = Instantiate(ParticalHit, HitEnemy.point, Quaternion.identity);
                        Destroy(Partical, .7f);
                        BulletTracer.SetPosition(1, HitEnemy.point);
                        if (HitEnemy.collider.gameObject.GetComponent<PlayerHealth>() != null)
                        {
                            if (HitEnemy.collider.gameObject.GetComponent<Player_Movement>().TakingCover == false)
                            {
                                HitEnemy.collider.gameObject.GetComponent<PlayerHealth>().Health -= Damage;
                                Player_Movement.PlayerSystem.Health.BleedingHealth += BleedDamage;
                            }
                          
                        }
                        FlashedFrames = 0;

                    }
                }
            }
        }
    }
}
