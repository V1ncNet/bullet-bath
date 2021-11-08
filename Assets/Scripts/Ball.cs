using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startForce = 800f;
    public Transform weapon;

    void OnEnable()
    {
        transform.rotation = Random.rotation;
        GetComponent<Rigidbody>().AddForce(weapon.GetChild(1).forward * startForce);

        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.5f, 1f);
    }
}
