using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumCycle : MonoBehaviour
{
    // Start is called before the first frame update
    public float DrumZ;

    [SerializeField]
    float rotOffset,distOffset,coefficient;
    float cycleDist;
    float drumTheta;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        drumTheta -= 90;
        if (180 < drumTheta && drumTheta <= 360)
            drumTheta -= 360;
        cycleDist = drumTheta / 360;

        transform.rotation = Quaternion.Euler(-drumTheta,0, 0);
        transform.position = new Vector3(0, 0.75f, offset-cycleDist*Mathf.PI);
        */
        float R = 0.75f * 2 * Mathf.PI;
        drumTheta = (DrumZ+0.38f)/1.24f * 180+rotOffset;


        transform.rotation = Quaternion.Euler(drumTheta, 0, 0);
        cycleDist = drumTheta / 360 * Mathf.PI;
        transform.position = new Vector3(0, 0.75f, cycleDist*coefficient + distOffset);
    }
}
