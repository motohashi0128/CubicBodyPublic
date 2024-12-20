using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject tracker;
    [SerializeField]
    bool isQuarternion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuarternion)
            transform.rotation = tracker.transform.rotation;
        else
        {
            Vector3 rot = tracker.transform.eulerAngles;
            transform.rotation = Quaternion.Euler(rot);
        }
    }
}
