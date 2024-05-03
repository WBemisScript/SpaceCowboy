using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBSide : MonoBehaviour
{
    public GameObject OpenScreen;
    public Animator ScreenAnim;
    public float ShowStartsCD;
    public GameObject Radio1;

    IEnumerator StartBSide()
    {
        yield return new WaitForSeconds(ShowStartsCD);
        ScreenAnim.Play("OpenBSideAnimation");
     
        Radio1.SetActive(true);
    
    }

    void Start()
    {
        OpenScreen.SetActive(true);
        StartCoroutine(StartBSide());
    }


}
