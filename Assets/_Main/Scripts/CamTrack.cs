using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrack : MonoBehaviour
{
    [SerializeField]
    GameObject camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.transform.position;
    }
}
