using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitUpsWall : MonoBehaviour
{
    [SerializeField]
    Transform rotPivot,hinge,camCube;
    [SerializeField]
    Vector3 adjust;
    public float theta_situps;
    public Vector3 CamPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camCube.LookAt(rotPivot);
        //theta_situps = Mathf.Atan(CamPos.y - rotPivot.transform.position.y / CamPos.z - rotPivot.transform.position.z)*Mathf.Rad2Deg;
        theta_situps = camCube.transform.eulerAngles.x;
        //Debug.Log(theta_situps);
        if (camCube.transform.position.z > rotPivot.transform.position.z)
            //Debug.Log(rotPivot.transform.position.z); 
            //-->‚È‚º‚©‚±‚ê‚ª -1
            theta_situps = 180 - theta_situps;
        hinge.transform.localRotation = Quaternion.Euler(new Vector3(0, theta_situps, 0)+adjust);

    }
}
