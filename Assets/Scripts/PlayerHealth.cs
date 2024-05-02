using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float Health;
    public float MaxHealth;
    public bool ImmunityFrame;
    public float BleedingHealth;
    public float BleedDecent;
    public void TakeDamage(float damage)
    {
     if (ImmunityFrame == false )
        {
            Health -= damage;
        }  
    }

    private void Update()
    {
        if (BleedingHealth < 0)
        {
            BleedingHealth = 0;
        }
    }
    private void FixedUpdate()
    {
        if (BleedingHealth > 0)
        {
         
            Health -= BleedDecent;
        }
       
        BleedingHealth -= BleedDecent;
    }
}
