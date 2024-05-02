using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAi : MonoBehaviour
{

    public Animator DummyAnims;
    public EnemyHealth Health;
    public bool Dead;
    void Start()
    {
        
    }

  

    void Update()
    {
        if (Health.Health <= 0)
        {
            Dead = true;

            Health.Health = 0;
            DummyAnims.SetBool("Dead", true);
        }
    }
}
