using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR;

public class TrackerDeviceDetermine : MonoBehaviour
{
    // Start is called before the first frame update
    SteamVR_TrackedObject obj;

    bool isDetermined = false;
    float preX;
    int deviceIndex,count;
    void Start()
    {
        obj = GetComponent<SteamVR_TrackedObject>();
        isDetermined = false;
        count = 0;
        deviceIndex = 1;
        obj.SetDeviceIndex(deviceIndex);
        preX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
        if (isDetermined)
            return;

        count++;
        

        if (count>2)
        {
            if (preX == transform.position.x)
            {
                count = 0;
                deviceIndex++;
                if (deviceIndex > 16)
                    deviceIndex -= 16;
                obj.SetDeviceIndex(deviceIndex);
            }
            else
                isDetermined = true;

        }
        preX = transform.position.x;

        Debug.Log(isDetermined);
    }
    void Restart()
    {
        var keyboard = Keyboard.current;

        if (keyboard.rKey.wasPressedThisFrame)
        {
            isDetermined = false;
            count = 0;
            deviceIndex++;

            if (deviceIndex > 16)
                deviceIndex -= 16;
            obj.SetDeviceIndex(deviceIndex);
            preX = transform.position.x;
        }
    }
}
