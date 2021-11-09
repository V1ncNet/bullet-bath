using UnityEngine;

/*
 * script controls missiles
 * 
 * placed on:       Prefab.Missile
 * author:          Johannes Mueller
 * last changed:    09.11.2021
 */

public class Missile : MonoBehaviour
{
    public float maxSpeed = 50f;
    public float acceleration = 5f;
    public float maxFuel = 3f;
    public float range = 40f;
    public float minHeight = 0f;
    public float explosionForce = 50f;
    public float explosionRadius = 5f;
    public GameObject explosionEffect;

    public float deviation = .5f;
    public float smoothness = .4f;
    public int steps = 50;
    public int maxWaypoints = 4;

    //[SerializeField] GameObject debugHelper;

    float speed = 0f;
    float fuel = 3f;
    
    Vector3[] waypoints;
    Vector3[] bezierPoints;
    Vector3[] path;
    int index = 0;
    int step = 0;

    Vector3 currentTarget;

    void OnEnable()
    {
        fuel = maxFuel;
        speed = 0f;
        index = 0;
        step = 0;

        CreateWaypoints();
        CreateBezierPoints();
        //DrawDebugHelper();

        currentTarget = transform.position;
    }

    void Update()
    {
        if (fuel <= 0f)
        {
            Explode();
            return;
        }
        if ((currentTarget - transform.position).sqrMagnitude < .02f)
        {
            step++;
            if (step == steps)
            {
                index++;
                //Debug.Log(index);
                step = 0;
            }
            if (index == maxWaypoints - 1)
            {
                currentTarget = transform.position + transform.forward * range;
                Debug.Log(currentTarget);
            }
            else
            {
                currentTarget = GetCurrentTarget(index, (float)step / steps);
                //Debug.Log(currentTarget);
            }
        }
        Propel();
    }

    void Propel()
    {
        transform.LookAt(currentTarget);
        speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        fuel -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        Explode();
    }

    void Explode()
    {
        Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 5);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("PhysicObject"))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        gameObject.SetActive(false);
    }

    void CreateWaypoints()
    {
        if (maxWaypoints < 1) maxWaypoints = 1;
        int interval = (int)(range / maxWaypoints + .5f);
        waypoints = new Vector3[maxWaypoints + 1];
        waypoints[0] = transform.position;
        for (int i = 1; i < maxWaypoints; i++)
        {
            waypoints[i] = transform.position + i * interval * transform.forward + UnityEngine.Random.insideUnitSphere * deviation;
        }
    }

    void CreateBezierPoints()
    {
        int maxPoints = 3 * maxWaypoints;
        bezierPoints = new Vector3[maxPoints + 1];

        bezierPoints[0] = transform.position;
        bezierPoints[1] = transform.position + transform.forward * smoothness;
        for (int i = 1; i < maxWaypoints; i++)
        {
            bezierPoints[3 * i] = waypoints[i]; // current anchor point
            Vector3 toPreviousPoint = waypoints[i - 1] - waypoints[i];
            Vector3 toNextPoint = waypoints[i] - waypoints[i + 1];
            bezierPoints[3 * i - 1] = waypoints[i] + (toPreviousPoint - toNextPoint).normalized * toPreviousPoint.magnitude / 2; // control point before
            bezierPoints[3 * i + 1] = waypoints[i] + (toNextPoint - toPreviousPoint).normalized * toNextPoint.magnitude / 2; // control point after
        }
        bezierPoints[maxPoints] = waypoints[maxWaypoints];
        bezierPoints[maxPoints - 1] = bezierPoints[maxPoints] - transform.forward * smoothness;
    }

    Vector3 QuadraticCurve(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 p = Vector3.Lerp(a, b, t);
        Vector3 q = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(p, q, t);
    }

    Vector3 CubicCurve(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 p = QuadraticCurve(a, b, c, t);
        Vector3 q = QuadraticCurve(b, c, d, t);
        return Vector3.Lerp(p, q, t);
    }

    Vector3 GetCurrentTarget(int idx, float t)
    {
        return CubicCurve(bezierPoints[3 * idx], bezierPoints[3 * idx + 1], bezierPoints[3 * idx + 2], bezierPoints[3 * idx + 3], t);
    }

    //void DrawDebugHelper()
    //{
    //    if (debugHelper == null)
    //    {
    //        Debug.LogError("No Debug Helper prefab.");
    //        return;
    //    }

    //    for (int i = 0; i < maxWaypoints; i++)
    //    {
    //        for (int s = 0; s < steps; s++)
    //        {
    //            float t = (float)s / steps;
    //            GameObject g = Instantiate(debugHelper, GetCurrentTarget(i, t), Quaternion.identity);
    //            Debug.Log(t);
    //        }
    //    }
    //}
}
