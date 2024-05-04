using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WorkerAi : MonoBehaviour
{
    public Animator EnemyAnims;
    public bool Aggro;
    public bool IsFacingRight = true;
    public Rigidbody2D Rb;
    public float WalkSpeed;
    public int AttackCooldown;
    public float AttackRange;
    private int WalkSpeedOffset;
    private bool InRange;
    public float RangeFromPlayer;
    public int FramesOfLastAttack;
    public AudioSource ChargeSound;
    public AudioSource HitSound;
    public EnemyHealth Health;
    public bool Dead;
    public float AggroDistance;
    void Start()
    {
        
    }

    


    void Update()
    {
        if (Dead == true)
        {
            EnemyAnims.Play("Dead");
        }
        if (Dead == false)
        {

            if (Health.Health <= 0)
            {
                Dead = true;
                Player_Movement.KillCount.Kills += 1;
                EnemyAnims.Play("Dead");
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Rb.bodyType = RigidbodyType2D.Static;
            }
            RangeFromPlayer = Vector3.Distance(transform.position, Player_Movement.Player.transform.position);



            if (RangeFromPlayer < AggroDistance)
            {
                Vector2 dir = Player_Movement.Player.transform.position - transform.position  ;
                UnityEngine. Debug.Log(dir);
              if (Physics2D.Raycast(transform.position, dir, 1000, ~Player_Movement.PlayerSystem.EnemyLayer) )
                {
                    RaycastHit2D CheckIfPlayer = Physics2D.Raycast(transform.position, dir, 1000, ~Player_Movement.PlayerSystem.EnemyLayer);
                    if (CheckIfPlayer.collider.gameObject.gameObject.GetComponent<Player_Movement>()  != null)
                    {
                        Aggro = true;
                    }
             
                }

               

            }
            if (Aggro == true)
            {

                if (RangeFromPlayer <= AttackRange)
                {

                    Rb.velocity = new Vector2(0, Rb.velocity.y);
                    EnemyAnims.SetBool("Walking", false);
                    if (FramesOfLastAttack > AttackCooldown)
                    {
                        ChargeSound.Play();
                        HitSound.Play();
                        EnemyAnims.Play("Attack");
                        FramesOfLastAttack = 0;
                        Player_Movement.PlayerSystem.Health.Health -= 10;
                    }
                }
                if (RangeFromPlayer > AttackRange)
                {
                    EnemyAnims.SetBool("Walking", true);
                    Rb.velocity = new Vector2(WalkSpeed * WalkSpeedOffset, Rb.velocity.y);
                }
                if (transform.position.x > Player_Movement.Player.transform.position.x)
                {
                    IsFacingRight = true;
                    WalkSpeedOffset = -1;
                }
                if (transform.position.x < Player_Movement.Player.transform.position.x)
                {
                    IsFacingRight = false;
                    WalkSpeedOffset = 1;
                }
            }
            if (IsFacingRight == true)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
            else if (IsFacingRight == false)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
        }
     
    }
    private void FixedUpdate()
    {
        FramesOfLastAttack += 1;
    }
}
