using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class EnemyShownHealth : MonoBehaviour
{

    public static Enemy_Information ShownEnemy;
    public GameObject Ui;
    public TextMeshPro Health;
    public TextMeshPro Name;
    private float ShowHealth = 0;
    void Start()
    {
        
    }

  


    void Update()
    {
        if (ShownEnemy == null)
        {
            Ui.SetActive(false);
           ShowHealth = 0;
}
         else
        {
            Ui.SetActive(true);

           
      

            Health.text = ShowHealth + "/" + ShownEnemy.MaxHealth.ToString();
            Name.text = ShownEnemy.EnemyName.ToString();
        }
    }

    private void FixedUpdate()
    {
        if (ShownEnemy != null)
        {

            if (ShowHealth > ShownEnemy.Health)
            {
                ShowHealth -= 1f;
            }
            if (ShowHealth < ShownEnemy.Health)
            {
                ShowHealth += 1f;
            }

        }

    }
}
