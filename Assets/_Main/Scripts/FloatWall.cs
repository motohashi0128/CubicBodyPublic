using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatWall : MonoBehaviour
{
    public Transform center_top, rot_top, top_scale, center_bottom, face_wall;
    public float theta, hight_face_wall,wii_board_weight,floatHight, max;
    [SerializeField]
    float coefficient;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        floatHight = wii_board_weight * coefficient;
        floatHight = Mathf.Clamp(floatHight, 0, 1.15f);
        center_top.localPosition = new Vector3(max + floatHight, -0.75f, 0.5f);
        
    }
    public void WallFaceOnUpdate(float hight)
    {
        hight_face_wall = hight;
        face_wall.localPosition = new Vector3(face_wall.localPosition.x, hight_face_wall - 1.9f, face_wall.localPosition.z);
    }
}
