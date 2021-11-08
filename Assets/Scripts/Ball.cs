using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startForce = 800f;
    public Transform weapon;
    Transform muzzle;

    void Start()
    {
        muzzle = weapon.GetChild(1);
    }

    void OnEnable()
    {
        transform.rotation = Random.rotation;
        GetComponent<Rigidbody>().AddForce(muzzle.forward * startForce);

        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.5f, 1f);
    }
}
