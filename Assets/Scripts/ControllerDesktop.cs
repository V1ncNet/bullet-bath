using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerDesktop : MonoBehaviour
{
    public float range = 10f;
    public int grabLayer;
    public Transform hand;

    public float baseSpeed = 10f;
    public float rotationSpeed = 50f;
    public float sprintFactor = 2.5f;

    InputAction moveAction;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        moveAction = new InputAction("Move");
        moveAction.AddCompositeBinding("2DVector(mode=0)")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.Enable();

        // bit shift to get a bit mask
        grabLayer = 1 << grabLayer;
    }

    void FixedUpdate()
    {
        //var mouse = Mouse.current;
        //if (mouse == null)
        //    Debug.LogError("No Mouse detected");
        //var keyboard = Keyboard.current;
        //if (keyboard == null)
        //    Debug.LogError("No Keyboard detected");


        // translation
        // moveAction is normalized
        Vector3 dir = moveAction.ReadValue<Vector3>();
        transform.Translate(dir * baseSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    // rotation
    //    Vector3 camRot = new Vector3(-Input.GetAxis("Mouse Y"), 0f, 0f);
    //    Vector3 rot = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
    //    transform.Rotate(rot * rotationSpeed * Time.deltaTime);
    //    Camera.main.transform.Rotate(camRot * rotationSpeed * Time.deltaTime);
    //    // translation
    //    Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    //    transform.Translate(dir.normalized * baseSpeed * Time.deltaTime);

    //    // sprinting
    //    float speed = baseSpeed;
    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        speed = baseSpeed * sprintFactor;
    //    }

    //    // pick up
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range, Color.red);
    //        RaycastHit hit;
    //        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, range, grabLayer))
    //        {
    //            Debug.Log("hit " + hit.collider.gameObject.name);

    //            Transform gun = hit.transform.parent;
    //            gun.GetComponentInChildren<Rigidbody>().useGravity = false;
    //            gun.GetComponentInChildren<Rigidbody>().detectCollisions = false;
    //            gun.SetParent(transform);
    //            gun.localPosition = hand.localPosition;

    //            // TODO
    //            //gun.GetComponentInChildren<BallGun>().wielded = true;
    //        }
    //    }
    //}
}
