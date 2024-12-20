using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public List<GameObject> mirrors;
    CubicBody cb;
    void Start()
    {
        cb = GetComponent<CubicBody>();
    }

    public void Large()
    {
        foreach(GameObject m in mirrors)
        {
            m.SetActive(false);
        }

        mirrors[5].SetActive(true);
    }
    public void Nearest()
    {
        foreach (GameObject m in mirrors)
        {
            m.SetActive(false);
        }

        mirrors[1].SetActive(true);
    }
    public void NearUp()
    {
        foreach (GameObject m in mirrors)
        {
            m.SetActive(false);
        }

        mirrors[2].SetActive(true);
        mirrors[6].SetActive(true);
    }
    public void LongFar()
    {
        foreach (GameObject m in mirrors)
        {
            m.SetActive(false);
        }

        mirrors[4].SetActive(true);
    }
}
