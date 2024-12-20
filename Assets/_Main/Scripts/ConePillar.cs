using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConePillar : MonoBehaviour
{
    [SerializeField]
    List<GameObject> joint_scale, joint_rot;
    [SerializeField]
    GameObject bottom;
    [SerializeField]
    GameObject joint1, joint2;
    public Transform face_pillar;
    public float hight_cone,scale_bottom,hight_bottom,hight_face_pillar;
    public Vector3 rot_cone;

    const float MIN_hight = 0; const float MAX_hight = 100;
    const float MIN_scale = -100; const float MAX_scale = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        hight_cone = Mathf.Clamp(hight_cone, MIN_hight, MAX_hight);

        hight_bottom = Mathf.Clamp(hight_bottom, MIN_hight, MAX_hight);
        scale_bottom = Mathf.Clamp(scale_bottom, MIN_scale, MAX_scale);
        foreach (GameObject obj_scale in joint_scale)
        {
            obj_scale.transform.localScale = new Vector3(hight_cone, 1-scale_bottom, 1-scale_bottom);
        }
        
        joint2.transform.localScale = new Vector3(0.7f, 1 - scale_bottom, 1 - scale_bottom);

        foreach (GameObject obj_rot in joint_rot)
        {
            if(hight_cone>0)
            obj_rot.transform.localRotation = Quaternion.Euler(rot_cone + new Vector3(0, 0, 90));
            else
                obj_rot.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 90));
        }

        bottom.transform.localScale = new Vector3(hight_bottom, 1 - scale_bottom, 1 - scale_bottom);
    }

    public void PillarFace(float hight)
    {
        hight_face_pillar = hight;
        face_pillar.localPosition = new Vector3(-0.428571194f, 0, 0.357820511f);
        face_pillar.position = new Vector3(face_pillar.position.x, hight_face_pillar, face_pillar.position.z);
    }
}
