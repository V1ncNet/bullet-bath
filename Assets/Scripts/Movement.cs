using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class Movement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public XRNode source;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = .2f;

    float fallingSpeed = -10f;
    XRRig rig;
    CharacterController character;
    Vector2 primary2DAxisValue;

    GameObject weaponUI;
    bool uiActve = false;

    bool isPressed, wasPressed = false;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();

        weaponUI = ProjectileManager.proManager.weaponUI;
        weaponUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(source);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2DAxisValue);

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton) && primaryButton)
        {
            isPressed = true;
            if (isPressed != wasPressed)
            {
                uiActve = !uiActve;
                weaponUI.SetActive(uiActve);
            }
        }
        else
        {
            isPressed = false;
        }
        wasPressed = isPressed;
    }            

    void FixedUpdate()
    {
        CapsuleFollowingHeadset();

        Quaternion yaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 dir = yaw * new Vector3(primary2DAxisValue.x, 0.0f, primary2DAxisValue.y);

        character.Move(dir * movementSpeed * Time.fixedDeltaTime);

        if (IsGrounded())
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CapsuleFollowingHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
    }

    bool IsGrounded()
    {
        // sphere cast so we dont slip of edges
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        return Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hit, rayLength, groundLayer);
    }
}
