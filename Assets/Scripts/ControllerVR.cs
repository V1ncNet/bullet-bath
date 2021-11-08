using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerVR : MonoBehaviour
{
    List<InputDevice> rightHandedControllers;
    Transform muzzle;
    ObjectPooler pooler;

    // Start is called before the first frame update
    void Start()
    {
        rightHandedControllers = new List<InputDevice>();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        pooler = ObjectPooler.instance;
        muzzle = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (muzzle == null)
        {
            Debug.LogError("Weapon has no muzzle transform");
            return;
        }

        foreach (InputDevice rightHand in rightHandedControllers)
        {
            bool triggerValue;
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", rightHand.name, rightHand.characteristics.ToString()));
                Debug.Log("Trigger button is pressed.");

                GameObject potato = pooler.SpawnFromPool("Potato", muzzle.transform.position, Random.rotation);
                Rigidbody rb = potato.GetComponent<Rigidbody>();
                rb.AddForce(muzzle.forward * PotatoSettings.startForce);
            }
        }
    }
}
