using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class OperatorAi : MonoBehaviour
{
    public bool Aggro;
    public EnemyHealth Health;
    public float AggroRange;
    public AudioSource IntroVoice;
    public bool IntroStarted;
    public bool CombatMode;
    public AudioSource OldSong;
    public Rigidbody2D Rb;
    public AudioSource NewSong;
    public SpriteRenderer MainSprite;
    public int MuzzleFrames;
    public bool IsFacingRight;
    public Rigidbody2D ArmRigidBody;
    public GameObject Rotetus;
    public GameObject Arm;
    public int Horizontal;
    public float WalkSpeed;
    public int Ammo;
    public int MaxAmmo;
    public bool Shooting;
    public int ShotTime;
    public int ReloadTime;
    private int TimeSinceLastShot;
    public float ShotDamage;
    public LayerMask EnemyLayer;
    public float DampingFrames;
    private int TimeSinceLastReload;

    public GameObject Muzzle;
    public LineRenderer Line;

    public bool Reloading = false;
    public AudioSource ShotSound;
    public AudioSource DashSound;
    public AudioSource JumpSound;
    public AudioSource ReloadSound;
    public float DropKickVelocity;

    public float TimeSinceLastDash;
    public float DashTime;
    public float DashCoolDown;
    private int DampedFrames;
    public bool IsOnDropkickCooldown;
    public Vector2 VelocityStorage;
    public float BleedDamage;
    public LayerMask GroundLayer;
    public AudioSource DeathSpeech;
    public Enemy_Information Info;
    public bool ImportantOnAggro = true;

    public GameObject GroundChecker;
    public Animator Anims;
    public int JumpVelocity;
    public bool Dead;
    public GameObject Wall;
    public GameObject Bridge;
    public AudioSource BridgeSound;

    public GameObject BlockedWall;
    private bool BridgeSoundPlayed;
    IEnumerator DropKick()
    {
        yield return new WaitForSeconds(DashTime);
        VelocityStorage.x = 0;
        yield return new WaitForSeconds(DashCoolDown);
        IsOnDropkickCooldown = false;
    }
    void Start()
    {
        
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundChecker.transform.position, .5f, GroundLayer);
    }

    void Update()
    {

        Anims.SetBool("Falling", !IsGrounded());

        if (Shooting == false && CombatMode == true)
        {
            if (Player_Movement.Player.transform.position.y > transform.position.y + 1)
            {
                if (IsGrounded() == true)
                {
                   
                    Rb.velocity = new Vector2 (Rb.velocity.x, JumpVelocity);
                    if (Rb.velocity.y < 2)
                    {
                        JumpSound.Play();
                    }
                }
     

            }
        }
        float PlayerDistance = Vector2.Distance(gameObject.transform.position, Player_Movement.Player.transform.position);
        float PlayerX = Player_Movement.Player.transform.position.x;
        if (PlayerDistance < AggroRange )
        {
            if (Aggro == false)
            {
                BlockedWall.SetActive(true);
                Aggro = true;
                IntroStarted = true;
                OldSong.Stop();

                IntroVoice.Play();
            }
       
        }

        if (IntroVoice.isPlaying == false )
        {
            if (IntroStarted == true)
            {
                if (CombatMode == false)
                {
                  NewSong.Play();
                }
                CombatMode = true;
            }
        }

        if (Aggro == true)
        {

            if (transform.position.x < PlayerX)
            {
                MainSprite.flipX = false;
                IsFacingRight = true;
                Horizontal = 1;
            }
            if (transform.position.x > PlayerX)
            {
                MainSprite.flipX = true;
                IsFacingRight = false;
                Horizontal = -1;
            }


        }


     if (   Health.Health <= 0)
        {
            if (DeathSpeech.isPlaying == false && Dead == false)
            {
                DeathSpeech.Play();

            }
            Dead = true;
            NewSong.Stop();
            Anims.Play("Death");
            Arm.GetComponent<SpriteRenderer>().enabled = false;

            Rb.velocity = new Vector2(0,Rb.velocity.y);
   if (Dead == true)
            {
                if (DeathSpeech.isPlaying == false)
                {
                    Wall.SetActive(false);
                    Bridge.SetActive(true); 
                    if (BridgeSound.isPlaying == false && BridgeSoundPlayed == false) 
                   
                    {
                        BridgeSoundPlayed = true;
                        BridgeSound.Play();
                    }
                 
                }
            }
            CombatMode = false;
            Health.Health = 0;
        }


        if (CombatMode == false)
        {
            if (Dead == false)
            {
                Health.Health = Health.MaxHealth;
            }

        }

    }

    private void FixedUpdate()
    {
        MuzzleFrames += 1;
        if (MuzzleFrames <= 5)
        {
            Muzzle.SetActive(true);
            Line.SetPosition(0, Muzzle.transform.position);
        }
        else
        {
            Muzzle.SetActive(false);
        }
        TimeSinceLastShot += 1;
        ArmRigidBody.position = transform.position + new Vector3(0, .25f, 0);
        if (Shooting == false )
        {
            if (CombatMode == true)
            {
                Anims.SetBool("Walking", true);

            }

            Vector2 LookDirection = Player_Movement.PlayerSystem.Rb.position - Rb.position;
            float angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
            ArmRigidBody.rotation = angle;
            if (CombatMode == true)
            {

                if (IsOnDropkickCooldown == false)
                {
                    DashSound.Play();
                    // PlayerAnims.Play("DropKick");
                    IsOnDropkickCooldown = true;
                    float Direction = Horizontal;
                    if (Direction == 0) Direction = 1;
                    VelocityStorage.x = Direction * DropKickVelocity;
                    StartCoroutine(DropKick());

                }
            }


        }
        if (Aggro == true )
        {
            Info.Important = true;
        }
        if (CombatMode == true)
        {

         


               WalkSpeed = 7;
            if (Ammo != 0)
            {
              
                DampedFrames += 1;
                WalkSpeed =1;
                Anims.SetBool("Walking", false);
                Shooting = true;
                if (DampedFrames > DampingFrames)
                {
                 
                    if (TimeSinceLastShot > ShotTime)
                    {
                        

                        MuzzleFrames = 0;
                        ShotSound.Play();
                        Ammo -= 1;
                        TimeSinceLastShot = 0;
                        if (Physics2D.Raycast(Rotetus.transform.position, Rotetus.transform.right, 100, ~EnemyLayer))
                        {
                      
                            RaycastHit2D Hit = Physics2D.Raycast(Rotetus.transform.position, Rotetus.transform.right, 100, ~EnemyLayer);
                            Debug.Log(Hit.collider.gameObject.name);
                       
                            Line.SetPosition(1, Hit.point);
                            if (Hit.collider.gameObject.GetComponent<Player_Movement>() != null)
                            {
                                Player_Movement.PlayerSystem.Health.Health -= ShotDamage;
                                Player_Movement.PlayerSystem.Health.BleedingHealth += BleedDamage;
                            }
                        }
                        Vector2 LookDirection = Player_Movement.PlayerSystem.Rb.position - Rb.position;
                        float angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
                        ArmRigidBody.rotation = angle;   
                    }
                }
           

            }
            if (Ammo == 0)
            {
                DampedFrames = 0;

                if (Reloading == false)
                {
                    Reloading = true;
                    TimeSinceLastReload = 0;
                }


                TimeSinceLastShot = 0;
                Shooting = false;
            }
           
            if (Reloading == true)
            {
                TimeSinceLastReload += 1;
            }

            if (TimeSinceLastReload > ReloadTime)
            {
                if (Reloading == true)
                {
                    Ammo = MaxAmmo;
                    ReloadSound.Play();
                    Reloading = false;
                }
              
            }
       


            Rb.velocity = new Vector2(Horizontal * WalkSpeed +VelocityStorage.x, Rb.velocity.y);
        }
    }
}
