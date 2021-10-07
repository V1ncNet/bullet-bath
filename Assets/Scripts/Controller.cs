using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //public static Controller controller;
    bool vr = false; // false per default for testing. should be true for a build.

    //void Awake()
    //{
    //    if (controller != null)
    //    {
    //        Debug.LogError("More than one Controller");
    //        return;
    //    }
    //    controller = this;
    //}

    ControllerDesktop desktop;
    ControllerVR controllerVr;

    // Start is called before the first frame update
    void Start()
    {
        desktop = GetComponent<ControllerDesktop>();
        controllerVr = GetComponent<ControllerVR>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F9))
        {
            vr = !vr;
            // VR
            controllerVr.enabled = vr;
            // Desktop
            desktop.enabled = !vr;
            GameObject.Find("Player").SetActive(!vr);
        }
    }
}
