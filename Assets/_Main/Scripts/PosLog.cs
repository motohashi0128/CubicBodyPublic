using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosLog : MonoBehaviour
{
    [SerializeField]
    Vector3 worldPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.transform.position);
        worldPos = transform.position;
    }
}
