using UnityEngine;

public class Potato : MonoBehaviour
{
    // TODO again hardcoded index
    Rigidbody rb;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        //rb.AddForce(transform.forward * GetComponent<Projectile>().startForce);
        rb.AddForce(transform.forward * ProjectileManager.proManager.projectiles[1].power);
        transform.rotation = Random.rotation;
    }
}
