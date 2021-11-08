using UnityEngine;

public class Potato : MonoBehaviour
{
    public float startForce = 800f;
    public Transform weapon;

    void OnEnable()
    {
        transform.rotation = Random.rotation;
        GetComponent<Rigidbody>().AddForce(weapon.GetChild(1).forward * startForce);
    }
}
