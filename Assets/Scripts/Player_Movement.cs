using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public static GameObject Player;
    public static Player_Movement PlayerSystem;
    public  PlayerHealth Health;
    public GameObject Arm;
    private float Horizontal;
    public static LevelSystem KillCount;
    public LevelSystem KillCountBase;
    [Header("Physics")]
    public Rigidbody2D Rb;
    public float Speed; 

    public float SpeedMultiplier;
    [Header("Cover")]
    public LayerMask CoverLayer;
    public bool TakingCover;
    [Header("Kicking")]
    public bool IsOnKickCooldown;
    public LayerMask EnemyLayer;
    public float Kickdamage;
    [Header("DropKicking")]
    public bool IsOnDropkickCooldown;
    public float DropKickDamage;
    public float DropKickVelocity;
    public Vector2 VelocityStorage;
    [Header("Rendering")]
    public Animator PlayerAnims;
    public SpriteRenderer PlayerSprite;
    public SpriteRenderer HandSprite;
    public GameObject MuzzelOffset;
    [Header("Aiming")]
    public Camera Cam;
    private Vector2 MousePos;
    public Rigidbody2D RotatusApperattus;
    [Header("Jumping")]
    public GameObject GroundChecker;
    public int JumpsLeft;
    public LayerMask GroundLayer;
    public AudioSource JumpSound;
    public AudioSource DoubleJumpSound;
    public AudioSource JumpRechargeSound;

    [Header("MeleeSounds")]
    public AudioSource KickSound;
    public AudioSource MeleeHitSound;


    public AudioSource DashSound;
    public AudioSource FootSteps;


    public int WinCondition = 1;


    public DeathScripts DeathCondition;
    public float JumpHieght;
    void Start()
    {
        Player = gameObject;
        PlayerSystem = this;
        KillCount = KillCountBase;
    }
    IEnumerator KickUWU()
    {
        yield return new WaitForSeconds(.7f);
        IsOnKickCooldown = false;
    }

    IEnumerator DropKick()
    {
        yield return new WaitForSeconds(.5f);
        VelocityStorage.x = 0;
        yield return new WaitForSeconds(.5f);
        IsOnDropkickCooldown = false;
    }
    
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundChecker.transform.position, .24f, GroundLayer);
    }

    void Update()
    {
        if (Rb.velocity.x != 0 && IsGrounded()  && VelocityStorage.x == 0)
        {
           if (FootSteps.isPlaying == false)
            {
                FootSteps.Play();
            }
        }
        if (Horizontal == 0 || IsGrounded() == false || VelocityStorage.x != 0)
        {
            FootSteps.Stop();

        }
        if (DeathCondition.Dead == false)
        {


            //Jump Anim
            if (IsGrounded() == false)
            {
                PlayerAnims.SetBool("Falling", true);

            }

            //Jumping
            if (IsGrounded() == true)
            {
                if (JumpsLeft == 0)
                {
                    JumpRechargeSound.Play();
                }
                JumpsLeft = 1;
                PlayerAnims.SetBool("Falling", false);

            }
            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded() == true || JumpsLeft != 0)
                {
                    JumpSound.Play();
                    Rb.velocity = new Vector2(Rb.velocity.x, JumpHieght);
                    if (IsGrounded() == false)
                    {
                        DoubleJumpSound.Play();
                        JumpsLeft -= 1;
                        PlayerAnims.Play("DoubleJump");
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (TakingCover == false)
                {
                    if (Physics2D.OverlapCircle(transform.position, 1.5f, CoverLayer))
                    {
                        transform.position = new Vector3(Physics2D.OverlapCircle(transform.position, 1.5f, CoverLayer).gameObject.transform.position.x, Physics2D.OverlapCircle(transform.position, 1.5f, CoverLayer).gameObject.transform.position.y, 0);
                        TakingCover = true;
                    }
                }
                else if (TakingCover == true)
                {

                    TakingCover = false;
                }


            }
            if (TakingCover == true)
            {
                SpeedMultiplier = 0;
                PlayerAnims.SetBool("Cover", true);
            }
            if (TakingCover == false)
            {
                Arm.SetActive(true);
                SpeedMultiplier = 1;
                PlayerAnims.SetBool("Cover", false);
            }
            if (TakingCover == false)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (IsOnKickCooldown == false && IsGrounded() == true)
                    {
                        KickSound.Play();
                        PlayerAnims.Play("Kick");
                        IsOnKickCooldown = true;
                        StartCoroutine(KickUWU());
                        if (Physics2D.OverlapCircle(transform.position - new Vector3(0, .5f, 0), 1.5f, EnemyLayer))
                        {
                            Collider2D[] EnemiesHit = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, .5f, 0), 1.5f, EnemyLayer);
                            foreach (Collider2D Enemy in EnemiesHit)
                            {
                                MeleeHitSound.Play();
                                Enemy.GetComponent<EnemyHealth>().Health -= Kickdamage;
                            }
                        }

                    }



                }
                if (Input.GetKeyDown(KeyCode.E))
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

            //Drop Kicking


            if (MousePos.x < transform.position.x)
            {
                HandSprite.flipY = true;
                MuzzelOffset.transform.localScale = new Vector3(1, -1, 1);
                PlayerSprite.flipX = true;
            }
            else if (MousePos.x > transform.position.x)
            {
                MuzzelOffset.transform.localScale = new Vector3(1, 1, 1);
                PlayerSprite.flipX = false;
                HandSprite.flipY = false;
            }
            if (Horizontal != 0)
            {
                PlayerAnims.SetBool("Walking", true);
            }
            else if (Horizontal == 0)
            {
                PlayerAnims.SetBool("Walking", false);
            }
            Horizontal = Input.GetAxisRaw("Horizontal");
            MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }


    private void FixedUpdate()
    {
        Rb.velocity = new Vector2((Horizontal * Speed + VelocityStorage.x) * SpeedMultiplier * WinCondition, (Rb.velocity.y + VelocityStorage.y) * SpeedMultiplier * WinCondition);
       
        Vector2 LookDirection = MousePos - Rb.position;
        float angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg ;
        RotatusApperattus.position = transform.position + new Vector3(0,.25f,0);
        RotatusApperattus.rotation = angle;
    }
}
