using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlepCam : MonoBehaviour
{
    public Transform Cam;
    public Camera MatchCam;
    public Camera Cvercam;
    public bool Slepcams = false;
    public bool begin = false;
    public Canvas Can;
    public Canvas AnyKey;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Cam = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            begin = true;
        }
        if (begin == true)
        {
            Cvercam.depth = Cvercam.depth - Time.deltaTime;
        }
        if(Cvercam.depth < -2)
        {
            Slepcams = true;
            Can.enabled = true;
            AnyKey.enabled = false;
        }
        if (Slepcams != false)
        {
            SlepCamera();
        }
    }

    void SlepCamera()
    {

            Cam.position = Vector3.Lerp(Cam.position, MatchCam.transform.position,Time.deltaTime);
            Cam.rotation = Quaternion.Slerp(Cam.rotation, MatchCam.transform.rotation, Time.deltaTime);

    }
}
