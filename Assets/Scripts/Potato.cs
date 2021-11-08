using UnityEngine;

public class Potato : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<Projectile>().startForce);
        transform.rotation = Random.rotation;
    }
}
