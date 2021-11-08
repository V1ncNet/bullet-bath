using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * GetComponent<Projectile>().startForce);
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.5f, 1f);
    }
}
