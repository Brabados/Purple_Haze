using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticalClamp : MonoBehaviour {

    public Canvas UI;
    public GameObject ParticalsPrefab;

    public GameObject MyPartical_Object;
    public ParticleSystem MyParticalSystem;
    public MeshRenderer MyMesh;
    public RectTransform rectTrans;
    bool running;

    public GameObject Lac;

    public Transform cube;

    public Camera cam;

	// Use this for initialization
	void Start ()
    {
        running = false;
        cube = Lac.gameObject.transform;
        MyPartical_Object = Instantiate(ParticalsPrefab, UI.transform,true);
        MyParticalSystem = MyPartical_Object.GetComponent<ParticleSystem>();
        MyMesh = gameObject.GetComponentInChildren<MeshRenderer>();
        rectTrans = MyPartical_Object.GetComponent<RectTransform>();
        GlobleEvents.TriggerEnter += Begin;
        GlobleEvents.TriggerExit += Stop;

        Stop();
    }

	
	// Update is called once per frame
	void Update ()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(cube.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            if (running == true)
            {
                Vector3 worldPos = cam.WorldToScreenPoint(cube.position);
                MyPartical_Object.transform.position = worldPos;

                float distace = 1 - Mathf.Clamp(Vector3.Distance(cam.transform.position, cube.position) / 100, 0, 1);

                rectTrans.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 1), new Vector3(8, 8, 8), distace);



                if (MyMesh.isVisible == false)
                {
                    MyParticalSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                else
                {
                    MyParticalSystem.Play(true);
                }
            }
        }
        else
        {
            MyParticalSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        MyPartical_Object.GetComponent<RectTransform>().transform.position = new Vector3(MyPartical_Object.GetComponent<RectTransform>().transform.position.x, MyPartical_Object.GetComponent<RectTransform>().transform.position.y, 0);
    
    }

    void Stop()
    {
        running = false;
        MyParticalSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Begin()
    {
        running = true;
        if (this.enabled == true)
        {
            MyParticalSystem.Play(true);
        }
        var Main = MyParticalSystem.main;
        Main.startColor = ParticalsPrefab.GetComponent<ParticleSystem>().main.startColor;
    }

    public void Toggle()
    {
        var Main = MyParticalSystem.main;
        Main.startColor = new Color(0,0,255);
    }
    


}
