using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField]
    int projectileIndex = 2;
    Rigidbody rb;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * ProjectileManager.proManager.projectiles[projectileIndex].power);
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.5f, 1f);
        float rx = Random.value / 5f;
        float ry = Random.value / 5f;
        float rz = Random.value / 5f;
        Vector3 randomScale = new Vector3(rx, ry, rz) + transform.localScale;
        transform.localScale = randomScale;
    }
}
