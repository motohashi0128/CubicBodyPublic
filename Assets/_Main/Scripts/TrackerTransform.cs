using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TrackerTransform : MonoBehaviour
{
    [SerializeField]
    Vector3 tracker_position,tracker_rotation_euler;
    Quaternion tracker_rotation;

    SteamVR_Action_Pose tracker = SteamVR_Actions.default_Pose;

    void Update()
    {
        tracker_position = tracker.GetLocalPosition(SteamVR_Input_Sources.Waist);
        tracker_rotation = tracker.GetLocalRotation(SteamVR_Input_Sources.Waist);

        tracker_rotation_euler = tracker_rotation.eulerAngles;
    }
}
