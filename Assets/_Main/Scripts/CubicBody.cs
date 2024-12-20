using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CubicBody : MonoBehaviour
{
    [SerializeField]
    GameObject hmd, tracker, camPivot;
    public enum MODE
    {
        ELASTIC,
        HINGE,
        PILLAR,
        SITUPS,
        ARC,
        DRUM,
        JUMP,
        CHINNING,
        DUAL,
        FLOAT,
    }
    public MODE ModeIndex;

    public List<GameObject> Avatars;

    [SerializeField]
    bool modeBind;
    [SerializeField]
    GameObject wall;
    [SerializeField]
    GameObject hingeGap;
    [SerializeField]
    bool isFrontAndBack;
    [SerializeField]
    Vector3 adjust_wall_pos;
    [SerializeField]
    float coefficient_whole_hight = 1;
    [SerializeField]
    float coefficient_top_hight = 1;
    [SerializeField]
    float coefficient_back_scale = 1;
    [SerializeField]
    float coefficient_camera_hight = 1;
    [SerializeField]
    GameObject pillar;
    [SerializeField]
    Vector3 adjust_pillar_pos;
    [SerializeField]
    float coefficient_cone_hight = 1;
    [SerializeField]
    float coefficient_cone_rot = 1;
    [SerializeField]
    float coefficient_cone_scale = 1;
    [SerializeField]
    float coefficient_pillar_hight = 1;
    [SerializeField]
    float coefficient_rotX = 1;
    [SerializeField]
    float range_modechange = 30;
    [SerializeField]
    GameObject sitUpsWall;
    [SerializeField]
    Vector3 adjust_sitUpsWall_pos;
    [SerializeField]
    GameObject arc;
    [SerializeField]
    GameObject drum;
    [SerializeField]
    float coefficient_drumRoll;
    [SerializeField]
    GameObject wall_jump;
    [SerializeField]
    GameObject wall_chihnning, face_chinning;
    [SerializeField]
    GameObject wall_dual,dummyCamPivot;
    [SerializeField]
    GameObject wall_float;


    TrackerParameterWithHMD tp;
    VelocityEstimator ve;
    OSCManager om;
    MirrorManager mm;

    ElasticWall ew;
    HingeWall hw;
    ConePillar cp;
    SitUpsWall sw;
    ArcCycle ac;
    DrumCycle dc;
    ElasticWallJump ej;
    ChinningWall cw;
    DualWall dw;
    FloatWall fw;

    float hight_hmd_standing;
    List<float> accelList = new List<float>();
    void Start()
    {
        tp = tracker.GetComponent<TrackerParameterWithHMD>();
        ve = hmd.GetComponent<VelocityEstimator>();
        om = GetComponent<OSCManager>();
        mm = GetComponent<MirrorManager>();
        Debug.Log(om);

        ew = wall.GetComponent<ElasticWall>();
        hw = wall.GetComponent<HingeWall>();
        cp = pillar.GetComponent<ConePillar>();
        sw = sitUpsWall.GetComponent<SitUpsWall>();
        ac = arc.GetComponent<ArcCycle>();
        dc = drum.GetComponent<DrumCycle>();
        ej = wall_jump.GetComponent<ElasticWallJump>();
        cw = wall_chihnning.GetComponent<ChinningWall>();
        dw = wall_dual.GetComponent<DualWall>();
        fw = wall_float.GetComponent<FloatWall>();

        hight_hmd_standing = hmd.transform.localPosition.y;
        ve.BeginEstimatingVelocity();
        Debug.Log((int)ModeIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (hight_hmd_standing == 0)
            hight_hmd_standing = hmd.transform.localPosition.y;
        KeyboardAction();
        float offset_y_hmd = hmd.transform.localPosition.y - hight_hmd_standing;
        Mode_0_Elastic(offset_y_hmd);
        Mode_1_Hinge();
        Mode_2_Pillar(offset_y_hmd);
        Mode_3_SitUps();
        Mode_4_Arc();
        Mode_5_Drum();
        Mode_6_Jump(offset_y_hmd);
        Mode_7_Chinning();
        Mode_8_Dual();
        Mode_9_Float();
    }
    void KeyboardAction()
    {
        var keyboard = Keyboard.current;
        if (!hw.isDefault)
            return;
        if (keyboard.aKey.wasPressedThisFrame)
        {
            hight_hmd_standing = hmd.transform.localPosition.y;
            wall.transform.position = new Vector3(hmd.transform.localPosition.x, 0, hmd.transform.localPosition.z) + adjust_wall_pos;
            camPivot.transform.position = Vector3.zero;
            ew.ResetElasticity();
            hw.ResetTheta();
            ew.WallFace(hight_hmd_standing);
            cp.PillarFace(hight_hmd_standing);
        }
        else if (keyboard.dKey.wasPressedThisFrame)
        {
            isFrontAndBack = !isFrontAndBack;
        }
        else if (keyboard.digit0Key.wasPressedThisFrame)
            ModeChange(0);
        else if (keyboard.digit1Key.wasPressedThisFrame)
            ModeChange(1);
        else if (keyboard.digit2Key.wasPressedThisFrame)
            ModeChange(2);
        else if (keyboard.digit3Key.wasPressedThisFrame)
            ModeChange(3);
        else if (keyboard.digit4Key.wasPressedThisFrame)
            ModeChange(4);
        else if (keyboard.digit5Key.wasPressedThisFrame)
            ModeChange(5);
        else if (keyboard.digit6Key.wasPressedThisFrame)
            ModeChange(6);
        else if (keyboard.digit7Key.wasPressedThisFrame)
            ModeChange(7);
        else if (keyboard.digit8Key.wasPressedThisFrame)
            ModeChange(8);
        else if (keyboard.digit9Key.wasPressedThisFrame)
            ModeChange(9);
    }

    void ModeChange(int index)
    {
        ModeIndex = (MODE)index;

        camPivot.transform.position = Vector3.zero;
        hight_hmd_standing = hmd.transform.localPosition.y;

        ew.ResetElasticity();
        hw.ResetTheta();
        ew.WallFace(hight_hmd_standing + 0.1f);
        cp.PillarFace(hight_hmd_standing);
        cw.ResetFloor();

        foreach(GameObject g in Avatars)
        {
            g.SetActive(false);
        }
        Avatars[index].SetActive(true);

        if (index == 1)
        {
            wall.transform.position = new Vector3(hmd.transform.localPosition.x, 0, hmd.transform.localPosition.z) + adjust_wall_pos;
        }

        if (index == 0 || index == 1 || index == 2 || index == 6 || index == 8 || index == 9)
        {
            mm.Large();
        }else if(index == 4)
        {
            mm.Nearest();
        }else if (index == 3 || index == 5)
        {
            mm.NearUp();
        }else if (index == 7)
        {
            mm.LongFar();
        }
    }
    void Mode_0_Elastic(float offset)
    {
        if ((int)ModeIndex != 0)
            return;
        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = false;
        ew.isElastic = true;
        hw.isHinge = false;
        hingeGap.transform.localPosition = new Vector3(0.25f, 0, 0);
        wall.transform.position = new Vector3(hmd.transform.localPosition.x, 0, hmd.transform.localPosition.z) + adjust_wall_pos;
        ew.WallFaceOnUpdate(hight_hmd_standing);

        if (tp.Dist_tracker_hmd > 0)
        {
            ew.elasticity_top = tp.Dist_tracker_hmd * coefficient_top_hight;
            camPivot.transform.position = new Vector3(0, tp.Dist_tracker_hmd * coefficient_camera_hight, 0);
        }
        else
        {
            ew.elasticity_top = tp.Dist_tracker_hmd;
        }
        ew.elasticity_whole = offset * coefficient_whole_hight;
        if (offset < 0)
            ew.elasticity_back = -offset * coefficient_back_scale;
        ew.theta = tracker.transform.rotation.eulerAngles.y + 180;

        if (hmd.transform.localPosition.y < 1.2f || tp.Dist_tracker_hmd > 0.1f)
        {
            ew.isWhole_rot = true;
        }
        else
        {
            ew.isWhole_rot = false;
        }

        if (270 - range_modechange < tracker.transform.rotation.eulerAngles.z && tracker.transform.rotation.eulerAngles.z < 270 + range_modechange && !modeBind)
        {
            ModeChange(2);
        }
    }
    void Mode_1_Hinge()
    {
        if ((int)ModeIndex != 1)
            return;
        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = false;
        ew.isElastic = false;
        hw.isHinge = true;

        hingeGap.transform.localPosition = new Vector3(0.25f, 0.02f, 0);
        hw.isTilt = isFrontAndBack;
        if (hw.isTilt)
        {
            hw.theta = tracker.transform.rotation.eulerAngles.x;
            if (270 < hw.theta && hw.theta < 360)
                camPivot.transform.localPosition = new Vector3(0, 0, (360 - hw.theta) * 0.01f);
        }
        else
        {
            hw.theta = tracker.transform.rotation.eulerAngles.z;
        }
    }

    void Mode_2_Pillar(float offset)
    {
        if ((int)ModeIndex != 2)
            return;
        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = false;

        //pillar.transform.position = new Vector3(hmd.transform.localPosition.x, 0, hmd.transform.localPosition.z) + adjust_pillar_pos;
        //pillar.transform.rotation = Quaternion.Euler(0, tracker.transform.rotation.eulerAngles.y + 90, 0);
        float rotX = tracker.transform.eulerAngles.x;
        if (rotX > 180)
            rotX -= 360;
        cp.hight_cone = tp.dist_vec3 * coefficient_cone_hight*(cp.hight_bottom-0.4f)*(1+Mathf.Abs(rotX)*coefficient_rotX);
        float coef = hmd.transform.position.y - 0.7f;
        coef = Mathf.Clamp(coef, 0, 10);
        cp.rot_cone = new Vector3(0, -RotFunction(tracker.transform.eulerAngles.z+90, coefficient_cone_rot)*coef, RotFunction(tracker.transform.eulerAngles.x, coefficient_cone_rot)*coef);
        cp.hight_bottom = 1 + offset * coefficient_pillar_hight;
        cp.scale_bottom = offset * coefficient_cone_scale;
        if (0 - range_modechange < tracker.transform.rotation.eulerAngles.z && tracker.transform.rotation.eulerAngles.z < 0 + range_modechange && !modeBind)
        {
            ModeChange(0);
        }
    }

    float RotFunction(float theta, float coefficient)
    {
        if (theta > 180)
        {
            theta -= 360;
            theta *= coefficient;
        }
        else
        {
            theta *= coefficient;
        }
        return theta;
    }

    void Mode_3_SitUps()
    {
        if ((int)ModeIndex != 3)
            return;
        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = false;
        camPivot.transform.position = adjust_sitUpsWall_pos;

        sw.CamPos = hmd.transform.position;
    }

    void Mode_4_Arc()
    {

        if ((int)ModeIndex != 4)
            return;
        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = false;
        ac.ArcTheta = tracker.transform.rotation.eulerAngles;
    }

    void Mode_5_Drum()
    {

        if ((int)ModeIndex != 5)
            return;
        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = false;
        dc.DrumZ = tracker.transform.position.z;
        camPivot.transform.position = new Vector3(0, 0, (tracker.transform.position.z - 0.16f) * coefficient_drumRoll);
    }
    void Mode_6_Jump(float offset)
    {
        if ((int)ModeIndex != 6)
            return;
        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = false;

        wall_jump.transform.position = new Vector3(hmd.transform.localPosition.x, 0, hmd.transform.localPosition.z) + adjust_wall_pos;
        ej.WallFaceOnUpdate(hight_hmd_standing);
        if (ej.top_mesh.transform.localPosition.y > 0)
        {
            camPivot.transform.position = new Vector3(0, ej.top_mesh.transform.localPosition.y, 0);
        }
        else
        {

            if (tp.Dist_tracker_hmd > 0)
            {
                ej.elasticity_top = tp.Dist_tracker_hmd * coefficient_top_hight;
                camPivot.transform.position = new Vector3(0, tp.Dist_tracker_hmd * coefficient_camera_hight, 0);
            }
            else
            {
                ej.elasticity_top = tp.Dist_tracker_hmd;
            }
        }

        ej.elasticity_whole = offset * coefficient_whole_hight;

        if (offset < 0)
            ej.elasticity_back = -offset * coefficient_back_scale;

        ej.theta = tracker.transform.rotation.eulerAngles.y + 180;
        //ej.accel = ve.GetAccelerationEstimate().y/100;
        ej.accel = AccelListAverage() / 100;

        if (hmd.transform.localPosition.y < 1.2f || tp.Dist_tracker_hmd > 0.1f)
        {
            ej.isWhole_rot = true;
            if (hmd.transform.localPosition.y < 1.2)
                ej.isJump = true;
        }
        else
        {
            ej.isWhole_rot = false;
        }

        if (270 - range_modechange < tracker.transform.rotation.eulerAngles.z && tracker.transform.rotation.eulerAngles.z < 270 + range_modechange && !modeBind)
        {
            ModeChange(6);
        }

    }

    float AccelListAverage()
    {
        accelList.Add(ve.GetAccelerationEstimate().y);
        if (accelList.Count > 10)
            accelList.RemoveAt(0);

        float ave = 0;
        foreach (float a in accelList)
        {
            ave += a;
        }
        ave /= accelList.Count;
        return ave;
    }

    void Mode_7_Chinning()
    {
        if ((int)ModeIndex != 7)
            return;

        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = true;
        if (cw.CamFollow)
            hmd.transform.localPosition = new Vector3(0, 0.07f + face_chinning.transform.position.y, 0.15f+ face_chinning.transform.position.z);
        else
            hmd.transform.localPosition = new Vector3(0, 1.6f, -0.3f);
        camPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
        cw.WiiBoardWeight = om.Sum;
        cw.theta = tracker.transform.eulerAngles.x;
    }
    void Mode_8_Dual()
    {
        if ((int)ModeIndex != 8)
            return;

        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = true;
        //dummyCamPivot.transform.rotation = Quaternion.Euler(0, tracker.transform.eulerAngles.y-180, 0);
        dw.dualTheta = tp.ThetaX_tracker_hmd;

        float campos = 0.55f + dw.dualTheta;
        dw.trackerRot = tracker.transform.rotation;
        hmd.transform.localPosition = new Vector3(0, 1.6f, 0.55f);
    }
    void Mode_9_Float()
    {
        if ((int)ModeIndex != 9)
            return;
        hmd.GetComponent<SteamVR_TrackedObject>().is3DOF = true;
        hmd.transform.localPosition = new Vector3(0, 1.6f-fw.max-fw.floatHight, 0.4f);
        camPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
        fw.wii_board_weight = om.Sum;
        //fw.WallFaceOnUpdate(hight_hmd_standing);
    }
}

//1021 加速度の判定に難あり、平均がうまくいっていない？しゃがんだ時点でisJumpが外れてしまう
