using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcCycle : MonoBehaviour
{
    public Vector3 ArcTheta;
    [SerializeField, Range(0, 90)]
    float range;
    [SerializeField]
    bool isNeck;
    [SerializeField]
    GameObject neck, face, front, back;
    float cycleDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNeck)
        {
            neck.SetActive(true);
            face.SetActive(false);
        }
        else
        {
            neck.SetActive(false);
            face.SetActive(true);
        }

        ArcTheta =  Deg360to180(ArcTheta);
        cycleDist = ArcTheta.z / 360;
        
        if (-range < ArcTheta.x && ArcTheta.x < range)
        {
            front.transform.localEulerAngles = Vector3.zero;
            back.transform.localEulerAngles = Vector3.zero;
        }else if(range<=ArcTheta.x){
            front.transform.localEulerAngles = Vector3.zero;
            back.transform.localEulerAngles = new Vector3(-ArcTheta.x, 0, 0);
        }else if(ArcTheta.x <= -range)
        {
            front.transform.localEulerAngles = new Vector3(-ArcTheta.x, 0, 0);
            back.transform.localEulerAngles = Vector3.zero;
        }
        
        transform.rotation = Quaternion.Euler(0, ArcTheta.y+180, -ArcTheta.z);
        transform.position = new Vector3(cycleDist*Mathf.PI, 1, -1.5f);

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

        Vector3 deg180 = new Vector3(x, y, z);
        return deg180;
    }
}
