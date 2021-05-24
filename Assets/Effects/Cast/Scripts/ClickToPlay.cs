using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToPlay : MonoBehaviour
{
    
    [SerializeField] ParticleSystem ps;
   




    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           
           if(ps.isStopped )
           {

                ps.Play();
                
           }

        }
    }
}
