using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadRipperAi : MonoBehaviour
{
    public bool Aggro = false;
    public bool Walking = false;
    public float SafeDistance;
    public bool FacingRight = true;
    public int HorizOffset = 1;
    public float FarDistance;
    public EnemyHealth Health;
    public float WalkSpeed;
    public bool Dead = false;


    public GameObject Muzzle;
    public int MuzzleFrames;

    public SpriteRenderer Sprite;
    public Rigidbody2D ArmRigidBody;
    public GameObject Rotetus;
    public float BleedDamage;
    public Rigidbody2D Rb;

    public GameObject FloorCheckingGantry;
    public GameObject FloorChecker;

    public GameObject Arm;

    public float Damage;
    public GameObject CameraGantryL;
    public GameObject CameraGantryR;

    public bool ImportantOnAggro;
    public Enemy_Information Info;

    public int TimeSinceNoShot;
    public LayerMask EnemyLayer;
    public LayerMask GroundLayer;

    public Animator EnemyAnims;
    public int CoolDownTime;

    public AudioSource ShotSound;

    public float AggroRange;

 


    void Update()
    {

        float Player_Relative_X = Player_Movement.Player.transform.position.x;
        float Distance_From_Player = Vector2.Distance(transform.position, Player_Movement.Player.transform.position);

        if (Distance_From_Player < AggroRange)
        {
            Vector2 dir = Player_Movement.Player.transform.position - transform.position;
            UnityEngine.Debug.Log(dir);
            if (Physics2D.Raycast(transform.position, dir, 1000, ~Player_Movement.PlayerSystem.EnemyLayer))
            {
                RaycastHit2D CheckIfPlayer = Physics2D.Raycast(transform.position, dir, 1000, ~Player_Movement.PlayerSystem.EnemyLayer);
                if (CheckIfPlayer.collider.gameObject.gameObject.GetComponent<Player_Movement>() != null)
                {
                    Aggro = true;
                    if (ImportantOnAggro == true)
                    {
                        Info.Important = true;
                    }
                }

            }
      
        }
        if (Health.Health <= 0)
        {
            Player_Movement.KillCount.Kills += 1;
            Destroy(gameObject);    
        }
        if (Dead == false)
        {


            if (Aggro == true)
            {




                if (Distance_From_Player < SafeDistance)
                {
                    Walking = true;
                }
                if (Distance_From_Player > FarDistance)
                {
                    HorizOffset = -1;
                    Walking = true;
                }

                if (Distance_From_Player <= FarDistance && Distance_From_Player >= SafeDistance)

                {
                    HorizOffset = 1;
                    Walking = false;
                }


                if (Walking == true)
                {
                    EnemyAnims.SetBool("Walking", true);

                    if (!Physics2D.Raycast(FloorChecker.transform.position, FloorChecker.transform.up * -1, 2f, GroundLayer))
                    {
                        Walking = false;
                        EnemyAnims.SetBool("Walking", false);
                    }
                }
                else
                {
                    EnemyAnims.SetBool("Walking", false);

                }


                if (Player_Relative_X > transform.position.x)
                {
                    CameraGantryL.SetActive(false);
                    CameraGantryR.SetActive(true);

                    FacingRight = true;

                    //CameraGantry.transform.localScale = new Vector3(1, CameraGantry.transform.localScale.y, CameraGantry.transform.localScale.z);
                    Sprite.flipX = false;
                }
                else if (Player_Relative_X < transform.position.x)
                {
                    CameraGantryL.SetActive(true);
                    CameraGantryR.SetActive(false);
                    FacingRight = false;
                    // CameraGantry.transform.localScale = new Vector3(-1, CameraGantry.transform.localScale.y, CameraGantry.transform.localScale.z);
                    Sprite.flipX = true;
                }
            }
        }




    }


    private void FixedUpdate()
    {

        MuzzleFrames += 1;
        if (MuzzleFrames <= 5)
        {
            Muzzle.SetActive(true);
        }
         else
        {
            Muzzle.SetActive(false);
        }
        if (Dead == false)
        {


            ArmRigidBody.position = transform.position + new Vector3(0, .25f, 0);
            int Horizontal = 0;
            if (FacingRight == true)
            {
                Horizontal = -1 * HorizOffset;
                FloorCheckingGantry.transform.localScale = new Vector3(-1 * HorizOffset, FloorCheckingGantry.transform.localScale.y, FloorCheckingGantry.transform.localScale.z);
            }
            else if (FacingRight == false)
            {
                Horizontal = 1 * HorizOffset;
                FloorCheckingGantry.transform.localScale = new Vector3(1 * HorizOffset, FloorCheckingGantry.transform.localScale.y, FloorCheckingGantry.transform.localScale.z);
            }
            if (Walking == true)
            {
                TimeSinceNoShot = 0;

                Rb.velocity = new Vector2(Horizontal * WalkSpeed, Rb.velocity.y);
            }
            if (Aggro == true)
            {
                if (Walking == false)
                {
                    TimeSinceNoShot += 1;

                    if (TimeSinceNoShot > CoolDownTime)
                    {
                        TimeSinceNoShot = 0;
                        ShotSound.Play();
                        MuzzleFrames = 0;
                        if (Physics2D.Raycast(Rotetus.transform.position, Rotetus.transform.right, 100, ~EnemyLayer))
                        {

                            RaycastHit2D Hit = Physics2D.Raycast(Rotetus.transform.position, Rotetus.transform.right, 100, ~EnemyLayer);
                            Debug.Log(Hit.collider.gameObject.name);
                            if (Hit.collider.gameObject.GetComponent<Player_Movement>() != null)
                            {
                                Player_Movement.PlayerSystem.Health.Health -= Damage;
                                Player_Movement.PlayerSystem.Health.BleedingHealth += BleedDamage;
                            }
                        }
                    }
                }



                Vector2 LookDirection = Player_Movement.PlayerSystem.Rb.position - Rb.position;
                float angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;

                ArmRigidBody.rotation = angle;
            }
        }
    }
}
