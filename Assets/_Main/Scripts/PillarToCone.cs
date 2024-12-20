using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarToCone : MonoBehaviour
{
    [SerializeField]
    Transform joint_over, joint_under;
    [SerializeField]
    public float coefficient_scale_over;
    public float coefficient_scale_under;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        joint_over.localScale = new Vector3(1 * coefficient_scale_over, 1, 1 * coefficient_scale_over);
        joint_under.localScale = new Vector3(1,1 * coefficient_scale_under, 1 * coefficient_scale_under);
    }
}
