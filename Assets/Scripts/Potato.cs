using UnityEngine;

public class Potato : MonoBehaviour
{
    Rigidbody rb;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * GetComponent<Projectile>().startForce);
        transform.rotation = Random.rotation;
    }
}
