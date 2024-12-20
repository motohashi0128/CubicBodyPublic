using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSync : MonoBehaviour
{
    Camera cam,parent;
    void Start()
    {
        parent = transform.parent.GetComponent<Camera>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = parent.fieldOfView;
    }
}
