using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponController : MonoBehaviour
{
    List<InputDevice> rightHandedControllers;
    ObjectPooler pooler;
    Transform muzzle;

    int poolIndex = 0;
    float cooldown = 0;
    bool isA, wasA;
    bool isB, wasB;
    
    // Start is called before the first frame update
    void Start()
    {
        rightHandedControllers = new List<InputDevice>();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        pooler = ObjectPooler.instance;

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
        cooldown -= Time.deltaTime;

        foreach (InputDevice rightHand in rightHandedControllers)
        {
            bool triggerValue;
            if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                //Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", rightHand.name, rightHand.characteristics.ToString()));
                //Debug.Log("Trigger button is pressed.");

                if (cooldown <= .0f)
                {
                    GameObject go = pooler.SpawnFromPool(pooler.pools[poolIndex].tag, muzzle.position, muzzle.rotation);
                    Projectile projectile = go.GetComponent<Projectile>();
                    cooldown = 1 / projectile.fireRate;
                }
            }

            bool primaryButtonValue;
            if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue)
            {
                //Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", rightHand.name, rightHand.characteristics.ToString()));
                //Debug.Log("A button is pressed.");

                isA = true;
                if (isA != wasA)
                {
                    poolIndex--;
                    if (poolIndex < 0)
                    {
                        poolIndex = pooler.pools.Count -1;
                    }
                }
            }
            else
            {
                isA = false;
            }
            wasA = isA;

            bool secondaryButtonValue;
            if (rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue)
            {
                //Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", rightHand.name, rightHand.characteristics.ToString()));
                //Debug.Log("B button is pressed.");

                isB = true;
                if (isB != wasB)
                {
                    poolIndex++;
                    if (poolIndex >= pooler.pools.Count)
                    {
                        poolIndex = 0;
                    }
                }
            }
            else
            {
                isB = false;
            }
            wasB = isB;
        }
    }
}
