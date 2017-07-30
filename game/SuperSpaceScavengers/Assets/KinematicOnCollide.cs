using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicOnCollide : MonoBehaviour
{
    // Use this for initialization
    void OnCollisionEnter(Collision _collision)
    {
        //Debug.Log("Got here");

        ////if (_collision.gameObject.layer == 8)
        //    transform.root.GetComponent<Rigidbody>().isKinematic = true;
    }
}
