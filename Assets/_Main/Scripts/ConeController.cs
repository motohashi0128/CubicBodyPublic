using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour
{
    public float height_joint_over, scale_joint_under;
    public Vector3 pos_joint_over;
    [SerializeField]
    Transform joint_over, joint_middle, joint_under;
    [SerializeField]
    Vector3 adjust_pos_over;
    [SerializeField]
    float coefficient_pos_over;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        joint_over.localPosition = new Vector3(-height_joint_over, pos_joint_over.x * coefficient_pos_over, pos_joint_over.z * coefficient_pos_over) + adjust_pos_over;
        joint_under.localScale = new Vector3(1 + scale_joint_under, 1 + scale_joint_under, 1 + scale_joint_under);
    }
}
