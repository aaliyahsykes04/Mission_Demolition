using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class RigidbodySleep : MonoBehaviour
{
    private int sleepCounter = 4;
    private Rigidbody rigid;

    void Awake()
    {
      rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if ( sleepCounter > 0 )
        {
            rigid.Sleep(); 
            sleepCounter--;
        }
    }


}
