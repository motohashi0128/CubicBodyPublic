using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Dekoboko : MonoBehaviour
{

    [SerializeField]
    Transform hmd, tracker_chair,tracker_stick;
    [SerializeField]
    GameObject mirror_wall, mirror_ceiling;

    [SerializeField]
    GameObject avatar_wall, avatar_wall_head, avatar_wall_head_pivot;
    [SerializeField]
    Vector3 adjust_wall_pos;
    [SerializeField]
    float coefficient_wall_high,coefficient_wall_low,adjust_head_height;

    [SerializeField]
    GameObject avatar_pillar,avatar_pillar_head;
    [SerializeField]
    Vector3 adjust_pillar_pos;
    [SerializeField]
    float range_pillar_rot;
    [SerializeField]
    float coefficient_pillar = 1f;
    [SerializeField]
    float coefficient_cone_over = 10f;
    [SerializeField]
    float coefficient_cone_under = 5f;
    [SerializeField]
    ConeController cc;

    [SerializeField]
    HingeWall hw;

    [SerializeField]
    GameObject pivot_camera;
    [SerializeField]
    float coefficent_pivot_camera;

    [SerializeField]
    bool modeBind,isSeparate,isCone;

    private int mode = 0;
    private float log_tracker_stick_height;
    private float log_tracker_chair_height;
    private float log_hmd_height;

    void Start()
    {
        cc.height_joint_over = 0;
        cc.scale_joint_under = 0;
        log_tracker_stick_height = tracker_stick.position.y;
        log_tracker_chair_height = tracker_chair.position.y;
        log_hmd_height = hmd.position.y;
        Debug.Log(log_tracker_stick_height);
        ModeChange(0);
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardAction();

        float elasticity_wall = 0;
        float elasticity_cone = 0;
        float elasticity_pillar = 0;

        if (mode == 0)
        {
            elasticity_wall = tracker_stick.position.y - log_tracker_stick_height;

            if (270 - range_pillar_rot < tracker_stick.rotation.eulerAngles.z && tracker_stick.rotation.eulerAngles.z < 270 + range_pillar_rot && !modeBind)
            {
                ModeChange(1);
            }
        }
        else if (mode == 1)
        {
            elasticity_cone = tracker_stick.position.y - hmd.localPosition.y;
            elasticity_pillar = hmd.position.y - log_hmd_height;

            if (0 - range_pillar_rot < tracker_stick.rotation.eulerAngles.z && tracker_stick.rotation.eulerAngles.z < 0 + range_pillar_rot && !modeBind)
            {
                ModeChange(0);
            }
        }
        else if (mode == 2)
        {

        }

        Mode_0_Wall(elasticity_wall);

        Mode_1_Pillar(elasticity_cone, elasticity_pillar);

        Mode_2_Hinge();

    }

    void KeyboardAction()
    {
        var keyboard = Keyboard.current;

        if (keyboard.digit0Key.wasPressedThisFrame)
            ModeChange(0);
        else if (keyboard.digit1Key.wasPressedThisFrame)
            ModeChange(1);
        else if (keyboard.digit2Key.wasPressedThisFrame)
            ModeChange(2);
        else if (keyboard.aKey.wasPressedThisFrame)
        {
            log_tracker_stick_height = hmd.position.y;
            avatar_wall.transform.GetChild(2).localPosition = new Vector3(0, hmd.localPosition.y + adjust_head_height, -0.03f);
        }
        else if (keyboard.spaceKey.wasPressedThisFrame)
        {
            hw.isTilt = !hw.isTilt;
        }
    }

    void ModeChange(int after)
    {
        mode = after;
        if (after == 0)
        {
            avatar_wall.SetActive(true);
            avatar_wall_head.SetActive(true);
            avatar_pillar.SetActive(false);
            avatar_pillar_head.SetActive(false);
            log_tracker_stick_height = hmd.position.y;
            avatar_wall_head_pivot.transform.localPosition = new Vector3(0, hmd.localPosition.y + adjust_head_height, 0.2f);
            hw.isTilt = false;
        }
        else if (after == 1)
        {
            avatar_wall.SetActive(false);
            avatar_wall_head.SetActive(false);
            avatar_pillar.SetActive(true);
            avatar_pillar_head.SetActive(true);
            log_hmd_height = hmd.localPosition.y;
        }
        else if(after == 2)
        {
            avatar_wall.SetActive(true);
            avatar_wall_head.SetActive(true);
            avatar_pillar.SetActive(false);
            avatar_pillar_head.SetActive(false);
        }
    }
    
    void Mode_0_Wall(float ew)
    {
        if (mode != 0)
            return;

        float coefficient_box = 3;

        avatar_wall.transform.position = new Vector3(hmd.localPosition.x, avatar_wall.transform.position.y, hmd.localPosition.z) + adjust_wall_pos;
        

        if (isSeparate)
        {
            hw.hinge_forward.rotation = Quaternion.Euler(0, tracker_stick.rotation.eulerAngles.y + 180, 0);
            avatar_wall.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (avatar_wall_head.transform.position.y < 1)
            {
                hw.hinge_forward.rotation = Quaternion.Euler(0, 0, 0);
                coefficient_box = 7;

            }
            else
                coefficient_box = 3;
        }
        else
        {
            hw.hinge_forward.rotation = Quaternion.Euler(0, tracker_stick.rotation.eulerAngles.y + 180, 0);
            avatar_wall.transform.rotation = Quaternion.Euler(0, tracker_stick.rotation.eulerAngles.y + 180, 0);
        }

        if (ew > 0)
        {
            avatar_wall.transform.localScale = new Vector3(1, 1 + ew*coefficient_wall_high, 1);
            pivot_camera.transform.position = new Vector3(0, ew * coefficent_pivot_camera, 0);
        }
        else
        {
            avatar_wall.transform.localScale = new Vector3(1, 1 + ew * coefficient_wall_low, 1- ew*coefficient_box);
            pivot_camera.transform.position = new Vector3(0, 0, 0);
        }

        
    }

    void Mode_1_Pillar(float ec, float ep)
    {
        if (mode != 1)
            return;
        avatar_pillar.transform.position = new Vector3(hmd.localPosition.x, avatar_pillar.transform.position.y, hmd.localPosition.z) + adjust_pillar_pos;
        avatar_pillar.transform.rotation = Quaternion.Euler(0, tracker_stick.rotation.eulerAngles.y+90, 0);
        pivot_camera.transform.position = new Vector3(0, 0, 0);
        if (isCone)
        {
            if (ec > 0)
            {
                cc.pos_joint_over = tracker_stick.position - hmd.position;
                cc.height_joint_over = ec/coefficient_cone_over;
                cc.scale_joint_under = ec*coefficient_cone_under;
            }
            if (ep < 0)
            {
                avatar_pillar.transform.localScale = new Vector3(1-ep,1+ep*coefficient_pillar,1-ep);
            }
        }
        else
            avatar_pillar.transform.localScale = new Vector3(1,1,1);
    }

    void Mode_2_Hinge()
    {
        if (mode != 2)
            return;
        avatar_wall.transform.rotation = Quaternion.Euler(0, tracker_stick.rotation.eulerAngles.y + 180, 0);

        if (hw.isTilt)
        {
            hw.theta = tracker_stick.rotation.eulerAngles.x;

            avatar_wall.transform.position = new Vector3(hmd.localPosition.x, avatar_wall.transform.position.y, hmd.localPosition.z) + adjust_wall_pos;
            if(270<hw.theta&&hw.theta<360)
            pivot_camera.transform.position = new Vector3(0, 0, (360-hw.theta)*0.02f);
        }
        else
        {
            hw.theta = tracker_stick.rotation.eulerAngles.z;
            pivot_camera.transform.position = new Vector3(0, 0, 0);
            
        }
    }
}

//mode0 ‚Ë‚¶‚ê‚½ó‘Ô‚Å‚µ‚á‚ª‚ñ‚¾‚ç‰ò‚É‚È‚é
