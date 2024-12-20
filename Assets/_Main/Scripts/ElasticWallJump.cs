using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElasticWallJump : MonoBehaviour
{
    public Transform center_top, rot_top, top_scale, center_bottom, face_wall,top_mesh,bottom_mesh;
    public float elasticity_whole, elasticity_top, elasticity_back,hight_hmd_jump;
    public float theta, hight_face_wall,accel,speed;
    public bool isWhole_rot,isJump;
    public Rigidbody top_rb,bottom_rb;
    [SerializeField]
    float boader_accel,boader_count;

    float count;
    bool counting;

    const float MIN_top = -1; const float MAX_top = 10000;
    const float MIN_back = -1; const float MAX_back = 100;
    

    void Start()
    {
        top_rb = top_mesh.GetComponent<Rigidbody>();
        bottom_rb = bottom_mesh.GetComponent<Rigidbody>();
        top_rb.useGravity = true ;
        bottom_rb.useGravity = true;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (theta < 0)
            theta += 360;
        if (theta > 360)
            theta -= 360;

        elasticity_top = Mathf.Clamp(elasticity_top, MIN_top, MAX_top);
        elasticity_back = Mathf.Clamp(elasticity_back, MIN_back, MAX_back);

        center_top.transform.localScale = new Vector3(1 + elasticity_whole + elasticity_top, 1, 1 + elasticity_back);
        center_bottom.transform.localScale = new Vector3(1 + elasticity_whole, 1, 1 + elasticity_back);

        
        if (isWhole_rot)
        {
            rot_top.localRotation = Quaternion.Euler(0, 0, 0);
            center_bottom.localRotation = Quaternion.Euler(0, theta, -90);
            top_scale.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rot_top.localRotation = Quaternion.Euler(-theta, 0, 0);
            center_bottom.localRotation = Quaternion.Euler(0, 0, -90);
            top_scale.localScale = new Vector3(1, 0.95f, 1);
        }

        if (counting)
        {
            count += Time.deltaTime;
            if(count > boader_count)
            {
                count = 0;
                counting = false;
            }
        }

        if (!top_rb.useGravity&&!counting)
        {
            if (!isJump)
            {
                if (boader_accel < accel)
                {
                    speed = 200;
                    Hopping();
                }
            }
            else
            {
                if (boader_accel < accel && !isWhole_rot)
                {
                    speed = 300;
                    Hopping();
                }
            }
        }

        if (top_mesh.transform.localPosition.y < 0)
        {
            ResetTop();
        }
        else if (top_mesh.transform.localPosition.y > 0.1f)
            top_rb.useGravity = true;

        if (bottom_mesh.transform.localPosition.y < 0)
        {
            ResetBottom();
        }
        else if (bottom_mesh.transform.localPosition.y > 0.1f)
            bottom_rb.useGravity = true;

    }

    void ResetTop()
    {
        top_mesh.transform.localPosition = new Vector3(top_mesh.transform.localPosition.x, 0, top_mesh.transform.localPosition.z);
        top_rb.useGravity = false;
        top_rb.velocity = Vector3.zero;
        top_rb.isKinematic = true;
    }

    void ResetBottom()
    {
        bottom_mesh.transform.localPosition = new Vector3(bottom_mesh.transform.localPosition.x, 0, bottom_mesh.transform.localPosition.z);
        bottom_rb.useGravity = false;
        bottom_rb.velocity = Vector3.zero;
        bottom_rb.isKinematic = true;
    }

    void Hopping()
    {
        top_rb.isKinematic = false;
        top_rb.velocity = Vector3.zero;
        top_rb.AddForce(Vector3.up * speed, ForceMode.Force);

        bottom_rb.isKinematic = false;
        bottom_rb.velocity = Vector3.zero;
        bottom_rb.AddForce(Vector3.up * 100, ForceMode.Force);
        isJump = false;
        count = 0;
        counting = true;
    }

    public void WallFace(float hight)
    {
        hight_face_wall = hight;
        face_wall.position = new Vector3(face_wall.position.x, hight_face_wall, face_wall.position.z);
    }
    public void WallFaceOnUpdate(float hight)
    {

        hight_face_wall = hight;
        face_wall.localPosition = new Vector3(face_wall.localPosition.x, hight_face_wall - 1.9f, face_wall.localPosition.z);
    }

    public void ResetElasticity()
    {
        elasticity_whole = 0;
        elasticity_top = 0;
        elasticity_back = 0;
        theta = 0;
    }
}
