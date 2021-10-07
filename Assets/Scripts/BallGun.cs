using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGun : MonoBehaviour
{
    public float range = 20f;

    //public float fireRate = 2f;
    public float launchSpeed = 500f;
    public float launchAngle = 22f;
    public float offCenter = 10f;
    public float correction = 10f;

    public Transform muzzle;
    //public Transform barrel;
    public GameObject[] projectiles;
    int curProjectile = 0;

    public bool wielded = false;

    ObjectPooler pooler;

    // Start is called before the first frame update
    void Start()
    {
        pooler = ObjectPooler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wielded) return;

        // adjust angle to aim where the player is looking at
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, range))
        {
            // angle is equal to the complementary angle of arctan(dis/local)
            float a = hit.distance / correction * transform.localPosition.x;
            // aproximate arctan
            offCenter = Mathf.PI / 2 - a / (a*a + .28f);
            //offCenter = Mathf.Atan(a);
        }

        Vector3 camAngles = Camera.main.transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(camAngles.x - launchAngle, camAngles.y - offCenter, camAngles.z);

        if (Input.GetKey(KeyCode.Alpha1)) curProjectile = 0;
        if (Input.GetKey(KeyCode.Alpha2)) curProjectile = 1;

        if (Input.GetButtonDown("Fire1"))
        {
            if (curProjectile == 0)
            {
                //Vector3 force = new Vector3(0f, launchAngle, launchSpeed);
                GameObject ball = Instantiate(projectiles[curProjectile], muzzle.transform.position, transform.rotation);
                //GameObject ball = pooler.SpawnFromPool("Ball", muzzle.transform.position, transform.rotation);
                ball.GetComponent<Rigidbody>().AddForce(muzzle.transform.up * launchSpeed);
                return;
            }
            if (curProjectile == 1)
            {
                GameObject missile = Instantiate(projectiles[curProjectile], muzzle.transform.position, transform.localRotation);
                //GameObject go = pooler.SpawnFromPool("Missile", muzzle.position, muzzle.rotation);
                //missile.transform.LookAt((muzzle.transform.position - transform.Find("Barrel").position) * 5f);
                return;
            }
            Debug.LogError("Tried to fire undefined projectile.");
        }
    }
}
