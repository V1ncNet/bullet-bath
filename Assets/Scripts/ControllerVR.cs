using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerVR : MonoBehaviour
{
    public Transform cam;
    public float movementSpeed = 2f;

    List<InputDevice> rightHandedControllers;
    ObjectPooler pooler;
    Transform muzzle;

    int poolIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rightHandedControllers = new List<InputDevice>();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        pooler = ObjectPooler.instance;
        cam = Camera.main.transform;

        muzzle = transform.GetChild(1);
        if (muzzle == null)
        {
            Debug.LogError("Weapon has no muzzle transform");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (InputDevice rightHand in rightHandedControllers)
        {
            Vector2 primary2DAxisValue;
            if (rightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
            {
                Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", rightHand.name, rightHand.characteristics.ToString()));
                Debug.Log("Right stick " + primary2DAxisValue);

                cam.Translate(primary2DAxisValue.normalized * movementSpeed * Time.deltaTime);
            }

            bool triggerValue;
            if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", rightHand.name, rightHand.characteristics.ToString()));
                Debug.Log("Trigger button is pressed.");

                pooler.SpawnFromPool(pooler.pools[poolIndex].tag, muzzle.transform.position, Quaternion.identity);
            }

            bool primaryButtonValue;
            if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue)
            {
                Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", rightHand.name, rightHand.characteristics.ToString()));
                Debug.Log("A button is pressed.");

                poolIndex--;
                if (poolIndex < 0)
                {
                    poolIndex = pooler.pools.Count;
                }
            }

            bool secondaryButtonValue;
            if (rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue)
            {
                Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", rightHand.name, rightHand.characteristics.ToString()));
                Debug.Log("B button is pressed.");

                poolIndex++;
                if (poolIndex >= pooler.pools.Count)
                {
                    poolIndex = 0;
                }
            }
        }
    }
}
