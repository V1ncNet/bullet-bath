using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<Projectile>().startForce);
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.5f, 1f);
    }
}
