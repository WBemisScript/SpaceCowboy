using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GunScript : MonoBehaviour
{

    public Player_Movement MovementSystem;
    public GameObject Arm;
    [Header("Raycast Shot")]
    public LayerMask PlayerLayer;
    public GameObject ShotPoint;

    public LayerMask EnemyLayer;
    [Header("Magnum")]
    public float MagnumDamage;
    public AudioSource MagnumSound;
    [Header("Revolver")]
    public float Damage;
    public float NukeBoostHeight;
    [Header("Ammo")]
    public float Ammo;
    public float MaxAmmo;
    [Header("Reloading")]
    public float ReloadTimeFrames;
    private float FramesReloaded;
    public bool Reloading;
    [Header("Muzzel Flash")]
    public LineRenderer BulletTracer;
    public GameObject Flash;
    public int FramesOfFlash;
    private int FlashedFrames;
    public GameObject ParticalHit;
    [Header("Sounds")]
    public AudioSource ShotSound;
    public AudioSource HitSound;
    public AudioSource ReloadSound;

    [Header("Tech")]
    public DemonCoreThrow Nuker;
    [Header("Arms")]
    public SpriteRenderer ArmSprite;
    public Sprite RevolverSprite;
    public Sprite MagnumSprite;
    public string ArmId;

    public DeathScripts DeathCondition;
    void Start()
    {
        Ammo = MaxAmmo ;
    }

    // Update is called once per frame
    void Update()
    {

        if (DeathCondition.Dead == false)
        {

            if (MovementSystem.TakingCover == true)
            {
                Arm.SetActive(false);

            }



            if (Input.GetKeyDown(KeyCode.R))
            {
                if (Reloading == false)
                {
                    FramesReloaded = 0;
                    Reloading = true;
                }
            }
            //ArmChanger
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (ArmId == "Revolver")
                {
                    ArmId = "Magnum";
                }
                else if (ArmId == "Magnum")
                {
                    ArmId = "Revolver";
                }
            }


            if (MovementSystem.TakingCover == false)
            {
                if (ArmId == "Revolver")
                {
                    ArmSprite.sprite = RevolverSprite;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Ammo > 0)
                        {
                            if (Reloading == false)
                            {
                                if (Physics2D.Raycast(ShotPoint.transform.position, ShotPoint.transform.right, 100000, ~PlayerLayer))
                                {
                                    HitSound.Play();
                                    ShotSound.Play();

                                    RaycastHit2D HitEnemy = Physics2D.Raycast(ShotPoint.transform.position, ShotPoint.transform.right, 100000, ~PlayerLayer);
                                    if (HitEnemy.collider.gameObject.tag == "DemonCore")
                                    {
                                        float RotatNUM = Player_Movement.PlayerSystem.RotatusApperattus.rotation;
                                       Nuker.CurrectCore.GetComponent<Rigidbody2D>().rotation = RotatNUM;

                                        Nuker.BeamSound.Stop();
                                        //Kill People
                                        Nuker.DemonCoreLight.enabled = false;

                                        Nuker.CurrectCore.GetComponent<Rigidbody2D>().velocity = Player_Movement.PlayerSystem.RotatusApperattus.transform.right * NukeBoostHeight + new Vector3(0, NukeBoostHeight/2);
                                        Nuker.FramesAlive = 0;
                                    }
                                    if (HitEnemy.collider.gameObject.tag == "NonRobot")
                                    {
                                        if (Physics2D.Raycast(HitEnemy.collider.gameObject.transform.position, transform.up * -1, 100000, ~EnemyLayer))
                                        {
                                            RaycastHit2D Hit = Physics2D.Raycast(HitEnemy.collider.gameObject.transform.position, Vector2.down, 100000, ~EnemyLayer);
                                            Vector3Int BloodPosition = new Vector3Int(Mathf.RoundToInt(Hit.point.x), Mathf.RoundToInt(Hit.point.y - .6f), 0);
                                            GameObject Blood = Instantiate(EnviromentSystem.Blood,HitEnemy.collider.gameObject.transform.position, Quaternion.identity);
                                            Destroy(Blood, 5);
                                            EnviromentSystem.SHOWNVIRO.RENDER_BLOOD(BloodPosition);
                                            EnviromentSystem.SHOWNVIRO.RENDER_BLOOD(BloodPosition + new Vector3Int(1,0,0));
                                            EnviromentSystem.SHOWNVIRO.RENDER_BLOOD(BloodPosition - new Vector3Int(1, 0, 0));
                                        }
                                    }
                                    Debug.Log(HitEnemy.collider.gameObject.name);
                                    GameObject Partical = Instantiate(ParticalHit, HitEnemy.point, Quaternion.identity);
                                    Destroy(Partical, .7f);
                                    BulletTracer.SetPosition(1, HitEnemy.point);
                                    if (HitEnemy.collider.gameObject.GetComponent<EnemyHealth>() != null)
                                    {
                                        HitEnemy.collider.gameObject.GetComponent<EnemyHealth>().Health -= Damage;
                                    }
                                }
                                FlashedFrames = 0;
                                Ammo -= 1;
                            }

                        }
                    }


                }
                if (ArmId == "Magnum")
                {
                    ArmSprite.sprite = MagnumSprite;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Ammo > 2)
                        {
                            if (Reloading == false)
                            {
                                if (Physics2D.Raycast(ShotPoint.transform.position, ShotPoint.transform.right, 100000, ~PlayerLayer))
                                {
                                    HitSound.Play();
                                    MagnumSound.Play();

                                    RaycastHit2D HitEnemy = Physics2D.Raycast(ShotPoint.transform.position, ShotPoint.transform.right, 100000, ~PlayerLayer);
                                    if (HitEnemy.collider.gameObject.tag == "DemonCore")
                                    {
                                        Nuker.Nuke();
                                    }
                                    if (HitEnemy.collider.gameObject.tag == "NonRobot")
                                    {
                                        if (Physics2D.Raycast(HitEnemy.collider.gameObject.transform.position, transform.up * -1, 100000, ~EnemyLayer))
                                        {
                                            RaycastHit2D Hit = Physics2D.Raycast(HitEnemy.collider.gameObject.transform.position, Vector2.down, 100000, ~EnemyLayer);
                                            Vector3Int BloodPosition = new Vector3Int(Mathf.RoundToInt(Hit.point.x), Mathf.RoundToInt(Hit.point.y - .6f), 0);
                                            GameObject Blood = Instantiate(EnviromentSystem.Blood, HitEnemy.collider.gameObject.transform.position, Quaternion.identity);
                                            Destroy(Blood, 5);
                                            EnviromentSystem.SHOWNVIRO.RENDER_BLOOD(BloodPosition);
                                            EnviromentSystem.SHOWNVIRO.RENDER_BLOOD(BloodPosition + new Vector3Int(1, 0, 0));
                                            EnviromentSystem.SHOWNVIRO.RENDER_BLOOD(BloodPosition - new Vector3Int(1, 0, 0));
                                        }
                                    }
                                    Debug.Log(HitEnemy.collider.gameObject.name);
                                    GameObject Partical = Instantiate(ParticalHit, HitEnemy.point, Quaternion.identity);
                                    Destroy(Partical, .7f);
                                    BulletTracer.SetPosition(1, HitEnemy.point);
                                    if (HitEnemy.collider.gameObject.GetComponent<EnemyHealth>() != null)
                                    {
                                        HitEnemy.collider.gameObject.GetComponent<EnemyHealth>().Health -= MagnumDamage;
                                    }
                                }
                                FlashedFrames = 0;
                                Ammo -= 3;
                            }

                        }
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
        }

    }

    private void FixedUpdate()
    {
        if (DeathCondition.Dead == false)
        {
            if (Reloading == false)
            {
                FramesReloaded = 0;
            }
            if (Reloading == true)
            {
                FramesReloaded += 1;
                if (FramesReloaded > ReloadTimeFrames)
                {
                    ReloadSound.Play();
                    Reloading = false;
                    Ammo = MaxAmmo;
                }
            }
        }
    }
}
