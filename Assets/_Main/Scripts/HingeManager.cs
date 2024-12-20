using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> LR, FB;
    
    /// <summary>
    /// true��LR��\���Afalse��FB��\�����܂��B
    /// </summary>
    /// <param name="b"></param>
    public void SetActiveLR(bool b)
    {
        foreach(GameObject g in LR)
        {
            g.SetActive(b);
        }
        foreach(GameObject g in FB)
        {
            g.SetActive(!b);
        }
    }
    
    /// <summary>
    /// hinge�����ׂĔ�\���ɂ��܂��B
    /// </summary>
    public void SetActiveFalseAll ()
    {
        foreach (GameObject g in FB)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in LR)
        {
            g.SetActive(false);
        }
    }
}
