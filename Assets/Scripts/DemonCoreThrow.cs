using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DemonCoreThrow : MonoBehaviour
{
    public GameObject DemonCoreObject;
    public float DamageRange;
    public float Damage;
    public float Speed;
    public int BlowUpTimeFrames;
    public int FramesAlive;
    public Player_Movement PlayerMovement;
    public GameObject CurrectCore;
    public GameObject FuckAll;
    public Light2D DemonCoreLight;
    public LayerMask EnemyLayer;
    public GameObject GiantFuckingBeamOfDeath;
    public AudioSource BeamSound;
    public AudioSource ExplosionSound;


    public float Cooldown;
    public float OffFrames;
   public  void Nuke()
    {

        ExplosionSound.Play();
        if (Physics2D.OverlapCircle(CurrectCore.transform.position, DamageRange, EnemyLayer))
        {

            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(CurrectCore.transform.position, DamageRange, EnemyLayer);
            foreach (Collider2D Enemy in HitEnemies)
            {
                if (Enemy.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    Enemy.gameObject.GetComponent<EnemyHealth>().Health -= Damage;

                }
            }

        }

        GameObject Whoops = Instantiate(GiantFuckingBeamOfDeath, CurrectCore.transform.position, Quaternion.identity);
        Destroy(CurrectCore);
        CurrectCore = FuckAll;
        Destroy(Whoops, .1f);
    }

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (OffFrames > Cooldown)
            {
                if (CurrectCore == null)
                {
                    OffFrames =0;
                    GameObject Core = Instantiate(DemonCoreObject, transform.position, Quaternion.identity);
                    float Direction = 1;
                    FramesAlive = 0;
                    CurrectCore = Core;
                    DemonCoreLight = Core.GetComponent<Light2D>();
                   Direction = PlayerMovement.RotatusApperattus.rotation;
                    Core.GetComponent<Rigidbody2D>().rotation = Direction;
                    Core.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * PlayerMovement.RotatusApperattus.transform.right.x, (PlayerMovement.RotatusApperattus.transform.right.y * Speed) * (Speed / 8  ));
                }
            }
         
        }
    }

    private void FixedUpdate()
    {
        OffFrames += 1;
        if (CurrectCore != null) 
        
        {
            FramesAlive += 1;
            if (BlowUpTimeFrames - 50 == FramesAlive)
            {
                
                BeamSound.Play();
                //Kill People
                DemonCoreLight.enabled = true;


            }
            if (BlowUpTimeFrames < FramesAlive)
            {
                
                ExplosionSound.Play();
                if (Physics2D.OverlapCircle(CurrectCore.transform.position, DamageRange, EnemyLayer))
                {
                 
                    Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(CurrectCore.transform.position, DamageRange, EnemyLayer);
                    foreach (Collider2D Enemy in HitEnemies)
                    {
                        if (Enemy.gameObject.GetComponent<EnemyHealth>() != null)
                        {
                            Enemy.gameObject.GetComponent<EnemyHealth>().Health -= Damage;
                           
                        }
                    }
            
                }

                GameObject Whoops = Instantiate(GiantFuckingBeamOfDeath, CurrectCore.transform.position, Quaternion.identity);
                Destroy(CurrectCore);
                CurrectCore = FuckAll;
                Destroy(Whoops, .1f);
            }
        
        }

      
    }
}
