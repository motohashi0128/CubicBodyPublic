using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingLog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 rot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rot = target.rotation.eulerAngles;
        Debug.Log(rot);
    }
}
