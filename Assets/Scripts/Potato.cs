using UnityEngine;

public class Potato : MonoBehaviour
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
    }
}
