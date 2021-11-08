using UnityEngine;

public class Potato : MonoBehaviour
{
    public float startForce = 800f;
    public Transform weapon;
    Transform muzzle;

    void OnEnable()
    {
        muzzle = weapon.GetChild(1);
        transform.rotation = Random.rotation;
        GetComponent<Rigidbody>().AddForce(muzzle.forward * startForce);
    }
}
