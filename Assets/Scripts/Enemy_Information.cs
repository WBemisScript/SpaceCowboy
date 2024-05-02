using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy_Information : MonoBehaviour
{
    public EnemyHealth HealthSystem;
    public float Health;
    public float MaxHealth;
    public string EnemyName;
    public bool Important;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    Health = HealthSystem.Health; MaxHealth = HealthSystem.MaxHealth;
        if (Important == true)
        {
            EnemyShownHealth.ShownEnemy = this;
        }
    }
}
