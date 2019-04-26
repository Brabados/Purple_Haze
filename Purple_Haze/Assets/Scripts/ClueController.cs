using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Harrison; // Just for event system

public class ClueController : MonoBehaviour {

    public Clue enabler;
    public GameObject Cluemod;
    public ParticalClamp ParticalToggle;
    public Animator myAnimator;

    private bool paused;

    private void Awake()
    {
        // Adding animation direction functions to events
        TimeControler.OnTimePlay += Play;
        TimeControler.OnTimeRewind += Rewind;
        TimeControler.OnTimeStop += Stop;
    }

    private void Start()
    {
        enabler = GetComponent<Clue>();
        if (ParticalToggle == null)
        {
            ParticalToggle = GetComponent<ParticalClamp>();
        }
        myAnimator = GetComponentInParent<Animator>();
    }

    public void IsClue()
    {
        enabler.enabled = true;
        Cluemod.tag = "Clue";
        ParticalToggle.enabled = true;
       
    }

    public void IsNotClue()
    {
        enabler.enabled = false;
        Cluemod.tag = "Untagged";
        ParticalToggle.MyParticalSystem.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
        ParticalToggle.enabled = false;
    }

    // removing events in case of destruction
    private void OnDestroy()
    {
        TimeControler.OnTimePlay -= Play;
        TimeControler.OnTimeRewind -= Rewind;
        TimeControler.OnTimeStop -= Stop;
    }
    
    // Linked to events in TimeController
    void Play()
    {
        // Setting animation to play forwards
        myAnimator.SetFloat("Time", 1);
    }

    // Linked to events in TimeController
    void Rewind()
    {
        // Setting animation to play backwards
        myAnimator.SetFloat("Time", -1);
    }

    // Linked to events in TimeController
    void Stop()
    {
        // Pausing animation if not already paused
        if (paused == false)
        {
            myAnimator.SetFloat("Time", 0);
            paused = !paused;
        }
        else // Resumes animation in correct direction 
        {
            if(TimeControler.timeDirection)
            {
                Play();
            }
            else
            {
                Rewind();
            }
            paused = !paused;
        }
    }
    
}
