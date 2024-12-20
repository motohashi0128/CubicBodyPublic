using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPos : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Pivot;
    [SerializeField]
    bool rot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Pivot.transform.position;
        if (rot)
            transform.rotation = Pivot.transform.rotation;
    }
}
