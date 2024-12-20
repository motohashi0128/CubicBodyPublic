using UnityEngine;

public class ElasticWall : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform center_top,rot_top,top_scale, center_bottom,face_wall;
    public float elasticity_whole,elasticity_top,elasticity_back;
    public float theta,hight_face_wall;
    public bool isElastic, isWhole_rot;

    HingeManager hm;

    const float MIN_top = -1; const float MAX_top = 10000;
    const float MIN_back = -1; const float MAX_back = 100;
    void Start()
    {
        hm = GetComponent<HingeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isElastic)
        {
            rot_top.localRotation = Quaternion.Euler(0, 0, 0);
            center_bottom.localRotation = Quaternion.Euler(0, 0, -90);
            center_top.transform.localScale = new Vector3(1, 1, 1);
            center_bottom.transform.localScale = new Vector3(1, 1, 1);

            top_scale.localScale = new Vector3(1, 1f, 1);
            return;
        }

        hm.SetActiveFalseAll();

        if (theta < 0)
            theta += 360;
        if (theta > 360)
            theta -= 360;

        elasticity_top = Mathf.Clamp(elasticity_top, MIN_top, MAX_top);
        elasticity_back = Mathf.Clamp(elasticity_back, MIN_back, MAX_back);

        center_top.transform.localScale = new Vector3(1 + elasticity_whole + elasticity_top, 1, 1 + elasticity_back);
        center_bottom.transform.localScale = new Vector3(1 + elasticity_whole, 1, 1 + elasticity_back);

        

        if (isWhole_rot)
        {
            rot_top.localRotation = Quaternion.Euler(0, 0, 0);
            center_bottom.localRotation = Quaternion.Euler(0, theta, -90);
            top_scale.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rot_top.localRotation = Quaternion.Euler(-theta, 0, 0);
            center_bottom.localRotation = Quaternion.Euler(0, 0, -90);
            top_scale.localScale = new Vector3(1, 0.95f, 1);
        }



    }

    public void WallFace(float hight)
    {
        hight_face_wall = hight;
        face_wall.position = new Vector3(face_wall.position.x, hight_face_wall, face_wall.position.z);
    }
    public void WallFaceOnUpdate(float hight)
    {

        hight_face_wall = hight;
        face_wall.localPosition = new Vector3(face_wall.localPosition.x, hight_face_wall-1.9f, face_wall.localPosition.z);
    }

    public void ResetElasticity()
    {
        elasticity_whole = 0;
        elasticity_top = 0;
        elasticity_back = 0;
        theta = 0;
    }
}
