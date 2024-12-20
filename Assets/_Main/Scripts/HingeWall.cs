using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HingeWall : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform hinge_right, hinge_left, hinge_forward, hinge_back;
    public float theta;
    public bool isHinge,isTilt,isDefault;
    [SerializeField]
    float range;
    HingeManager hm;
    Vector3 log_hmd_pos;
    void Start()
    {
        hm = GetComponent<HingeManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHinge)
            return;
        KeyboardAction();

        if (theta < 0)
            theta += 360;
        if (theta > 360)
            theta -= 360;

        if (isTilt)
        {
            hm.SetActiveLR(false);
            if (270 < theta && theta < 360 - range)
            {
                hinge_forward.localRotation = Quaternion.Euler(hinge_forward.localRotation.eulerAngles.x, -theta, 0);
                hinge_back.localRotation = Quaternion.Euler(0, 0, 0);
                isDefault = false;
            }
            else if (0 + range <= theta && theta < 90)
            {
                hinge_forward.localRotation = Quaternion.Euler(hinge_forward.localRotation.eulerAngles.x, 0, 0);
                hinge_back.localRotation = Quaternion.Euler(0, -theta, 0);
                isDefault = false;
            }
            else if (360 - range < theta && theta < 360 || 0 <= theta && theta < 0 + range)
            {
                hinge_forward.localRotation = Quaternion.Euler(hinge_forward.localRotation.eulerAngles.x, 0, 0);
                hinge_back.localRotation = Quaternion.Euler(0, 0, 0);
                isDefault = true;
            }
        }
        else
        {
            hm.SetActiveLR(true);
            if (270 < theta && theta < 360 - range)
            {
                hinge_right.localRotation = Quaternion.Euler(0, 0, 0);
                hinge_left.localRotation = Quaternion.Euler(0, 0, -theta);
                isDefault = false;
            }
            else if (0 + range <= theta && theta < 90)
            {
                hinge_right.localRotation = Quaternion.Euler(0, 0, -theta);
                hinge_left.localRotation = Quaternion.Euler(0, 0, 0);
                isDefault = false;

            }
            else if (360 - range < theta && theta < 360 || 0 <= theta && theta < 0 + range)
            {
                hinge_right.localRotation = Quaternion.Euler(0, 0, 0);
                hinge_left.localRotation = Quaternion.Euler(0, 0, 0);
                isDefault = true;
            }
        }
    }
    public void ResetTheta()
    {
        theta = 0;
    }
    void KeyboardAction()
    {
        var keyboard = Keyboard.current;

        if (keyboard.aKey.wasPressedThisFrame)
        {
            
        }
    }
}
