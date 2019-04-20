using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //Refrence to the camera
    [HideInInspector] public Camera Cam;

    //Ray to be used to find Objects under the crosshair
    private RaycastHit hitForward;

    //bool to define when inside a clue trigger box
    public bool InTrigger = false;

    private void Awake()
    {
        Cam = GetComponent<Camera>();
    }

    void Start()
    {
        GlobleEvents.TriggerEnter += Enter;
        GlobleEvents.TriggerExit += Exit;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hitForward, 15, 1, QueryTriggerInteraction.Ignore))
        {
            if (hitForward.transform != transform)
            {
                if (hitForward.transform.CompareTag("Clue"))
                {
                    Clue InView = hitForward.transform.parent.GetComponentInChildren<Clue>();
                    if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Attempting Activation");
                        InView.Activate();
                    }
                }

            }
        }
        if (InTrigger == true)
        {
            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hitForward, 40000, 9, QueryTriggerInteraction.Ignore))
            {
                if (hitForward.transform != transform)
                {
                    if (hitForward.transform.CompareTag("Clue"))
                    {
                        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
                        {
                            GlobleEvents.ActiveToggle(hitForward.transform.parent.gameObject);
                        }
                    }
                }
            }
        }
    }

    void Enter()
    {
        InTrigger = true;
    }

    void Exit()
    {
        InTrigger = false;
    }
}
