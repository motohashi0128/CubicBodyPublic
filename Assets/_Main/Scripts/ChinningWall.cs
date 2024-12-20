using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChinningWall : MonoBehaviour
{
    public float WiiBoardWeight,theta;
    public bool CamFollow;
    [SerializeField]
    Transform rotPivot,top_scale,bottom_scale,mesh_bottom,floor_front,floor_back;
    [SerializeField]
    float coefficient_elastic, scaleX_MAX, coefficient_rot, coefficient_bottom_rot, coefficient_theta, offset_theta,offset_weight;
    [SerializeField]
    bool isWhole;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            ElasticCube();
        
        float rotLock = 1-(WiiBoardWeight/offset_weight);
        rotLock = Mathf.Clamp(rotLock, 0, 100);
        Debug.Log(theta);
        if (theta > 180)
            theta -= 360;
        rotPivot.eulerAngles = new Vector3(rotLock*theta*coefficient_theta+offset_theta, 0, 0);
    }
    
    void ElasticCube()
    {
        float scaleX,rotX;

        mesh_bottom.transform.localPosition = new Vector3(-0.5f, 0, 0);
        float rotZ = 180 - WiiBoardWeight * coefficient_rot;
        rotZ = Mathf.Clamp(rotZ, 0, 180);
        floor_front.rotation = Quaternion.Euler(0, 90, rotZ);
        floor_back.rotation = Quaternion.Euler(0, -90, rotZ);
        if (isWhole)
        {
            scaleX = scaleX_MAX - WiiBoardWeight * coefficient_elastic;
            scaleX = Mathf.Clamp(scaleX, 1f, scaleX_MAX);
            top_scale.localScale = new Vector3(scaleX, 1, 1);
            bottom_scale.localScale = new Vector3(scaleX, 1, 1);
        }
        else
        {
            scaleX = scaleX_MAX - WiiBoardWeight * coefficient_elastic * 2;
            scaleX = Mathf.Clamp(scaleX, 1f, scaleX_MAX);
            top_scale.localScale = new Vector3(scaleX, 1, 1);
            bottom_scale.localScale = new Vector3(1, 1, 1);
            rotX = 60 - WiiBoardWeight * coefficient_bottom_rot;
            rotX = Mathf.Clamp(rotX, 0, 60);
            bottom_scale.localRotation = Quaternion.Euler(rotX, -0.84f, 90);
        }
    }
    public void ResetFloor()
    {
        floor_front.rotation = Quaternion.Euler(0, 90, 0);
        floor_back.rotation = Quaternion.Euler(0, -90, 0);
    }
}
