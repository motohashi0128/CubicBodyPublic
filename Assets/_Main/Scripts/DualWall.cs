using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DualWall : MonoBehaviour
{
    public float dualTheta;
    public Quaternion trackerRot;
    [SerializeField]
    float adjust;
    [SerializeField]
    GameObject top_front,top_back, front, back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = trackerRot;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y+180, 0);
        float x = dualTheta - 180 + adjust;

        if (0 <= x && x <= 180)
        {
            back.transform.parent.parent.GetComponent<ParentConstraint>().enabled = true;
            top_front.transform.localRotation = Quaternion.Euler(x, 0, -90);
            back.transform.localRotation = Quaternion.Euler(x / 2, -90, 0);

            front.transform.parent.parent.GetComponent<ParentConstraint>().enabled = false;
            top_back.transform.localRotation = Quaternion.Euler(0, 0, 0);
            front.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            front.transform.parent.parent.GetComponent<ParentConstraint>().enabled = true;
            top_back.transform.localRotation = Quaternion.Euler(0, x, 0);
            front.transform.localRotation = Quaternion.Euler(-x / 2, -90, 0);

            back.transform.parent.parent.GetComponent<ParentConstraint>().enabled = false;
            top_front.transform.localRotation = Quaternion.Euler(0, 0, -90);
            back.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
