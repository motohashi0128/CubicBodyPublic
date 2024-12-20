using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerParameterWithHMD : MonoBehaviour
{
    // Start is called before the first frame update
    public float ThetaX_tracker_hmd, ThetaY_tracker_hmd, ThetaZ_tracker_hmd,AdThetaX_tracker_hmd, AdThetaY_tracker_hmd, AdThetaZ_tracker_hmd,Dist_tracker_hmd,dist_vec3;
    [SerializeField]
    Transform hmd;
    [SerializeField]
    Vector3 adjustHMDpos;//HMDposをずらす
    
    void Update()
    {
        Dist_tracker_hmd = transform.localPosition.y - hmd.localPosition.y;//HMDとトラッカーの高さの差
        dist_vec3 = Vector3.Distance(transform.localPosition, hmd.localPosition);//HMDとトラッカーの距離

        Vector3 offset = transform.position - hmd.position;
        Vector3 adjustOffset = transform.position - hmd.position + adjustHMDpos;
        
        //HMDを原点としたトラッカーとの角度
        ThetaX_tracker_hmd = AdjustTheta(Mathf.Atan(offset.x / offset.y) * Mathf.Rad2Deg);
        ThetaY_tracker_hmd = AdjustTheta(Mathf.Atan(offset.x / offset.z) * Mathf.Rad2Deg);
        ThetaZ_tracker_hmd = AdjustTheta(Mathf.Atan(offset.z / offset.y) * Mathf.Rad2Deg);

        //adjustHMDposを原点としたトラッカーとの角度
        AdThetaX_tracker_hmd = AdjustTheta(Mathf.Atan(adjustOffset.x / adjustOffset.y) * Mathf.Rad2Deg);
        AdThetaY_tracker_hmd = AdjustTheta(Mathf.Atan(adjustOffset.x / adjustOffset.z) * Mathf.Rad2Deg);
        AdThetaZ_tracker_hmd = AdjustTheta(Mathf.Atan(adjustOffset.z / adjustOffset.y) * Mathf.Rad2Deg);
    }

    float AdjustTheta(float theta)//角度を0~360から-180~180に変換するメソッド、角度を掛けたり割ったりするときに使う
    {
        if (Dist_tracker_hmd < 0)
            theta -= 180;

        if (theta < 0)
            theta += 360;
        if (theta > 360)
            theta -= 360;

        return theta;
    }
}
