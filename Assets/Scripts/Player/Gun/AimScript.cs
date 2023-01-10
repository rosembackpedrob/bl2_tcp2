using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    public GameObject Gun;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Gun.GetComponent<Animator>().Play("Aim");
        }

        if(Input.GetMouseButtonUp(1))
        {
            Gun.GetComponent<Animator>().Play("GunIdle");
        }
    }
}
