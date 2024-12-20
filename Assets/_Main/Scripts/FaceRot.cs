using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceRot : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)]
    float rangeCoefficient;
    [SerializeField]
    Vector3 defaultRot;
    [SerializeField]
    GameObject hmd;
    enum Mode
    {
        ARC,
        ARC_NECK,
        DRUM,
    }
    [SerializeField]
    Mode modeIndex;
    Quaternion hmdRot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hmdRot = Quaternion.Euler(Deg360to180(hmd.transform.eulerAngles) *rangeCoefficient);


        if ((int)modeIndex == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, hmd.transform.eulerAngles.z)*Quaternion.Euler(defaultRot)*Quaternion.Inverse(Quaternion.Euler(0, transform.root.eulerAngles.z, 0));
        }
        else if((int)modeIndex == 1)
        {
            transform.localRotation = Quaternion.Euler(0, hmd.transform.eulerAngles.y, 0);
        }
        else if((int)modeIndex == 2)
        {

            transform.rotation = hmdRot * Quaternion.Euler(defaultRot);
        }
        //transform.rotation = Quaternion.Euler(defaultRot) * Quaternion.Inverse(parent.transform.rotation) * Quaternion.Euler(adjustRot);
        //transform.localRotation = hmd.transform.rotation*Quaternion.Euler(defaultRot)*parent.transform.rotation*Quaternion.Euler(adjustRot);
    }
    Vector3 Deg360to180(Vector3 deg360)
    {
        
        float x, y, z;
        if (deg360.x > 180)
            x = deg360.x - 360;
        else
            x = deg360.x;
        if (deg360.y > 180)
            y = deg360.y - 360;
        else
            y = deg360.y;
        if (deg360.z > 180)
            z = deg360.z - 360;
        else
            z = deg360.z;
        Vector3 deg180 = new Vector3(x,y,z);
        return deg180;
    }
}
