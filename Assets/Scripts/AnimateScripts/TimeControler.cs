using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harrison
{
    
    
public class TimeControler : MonoBehaviour {

    //Bool describing direction of animation. True = forwards, False = backwards.
    public static bool timeDirection = true;
	
    // Events for animators to subscribe to
    public delegate void AlterTime();
    public static event AlterTime OnTimeRewind;
    public static event AlterTime OnTimeStop;
    public static event AlterTime OnTimePlay;

    // strings for input detection
    public string playInput, rewindInput, pauseInput;
    
    // input values bound to their corresponding axis
    private float playInputValue { get { return Input.GetAxis(playInput); } }
    private float rewindInputValue { get { return Input.GetAxis(rewindInput); } }
    private float pauseInputValue { get { return Input.GetAxis(pauseInput); } }
    
    // bools to prevent multiple inputs
    private bool isPlaying, isRewinding, isPaused;
    
    private void Awake()
    {
        OnTimeRewind += ToggleDirection;
        OnTimePlay += ToggleDirection;
    }

    // Update is called once per frame
	void Update () 
    {
        
        if (rewindInputValue > 0 && !isRewinding)
        {
            // Preventing multiple inputs
            isRewinding = true;
            // Run appropriate event based on time direction
            if (timeDirection == true)
            {
                if (OnTimeRewind != null) OnTimeRewind();
            }
            else
            {
                if (OnTimeStop != null) OnTimeStop();
            }
        }

        // resetting safety bool
        if (rewindInputValue <= 0) isRewinding = false; 

        if (playInputValue > 0 && !isPlaying)
        {
            // Preventing multiple inputs
            isPlaying = true;
            // Run appropriate event based on time direction
            if (timeDirection == false)
            {
                if (OnTimePlay != null) OnTimePlay();
            }
            else 
            {
                if (OnTimeStop != null) OnTimeStop();
            }
        }

        // resetting safety bool
        if (playInputValue <= 0) isPlaying = false; 
        
        if (pauseInputValue > 0 && !isPaused)
        {
            // Preventing multiple inputs
            isPaused = true;
            // Run stop event
            if (OnTimeStop != null) OnTimeStop();
        }
        
        // resetting safety bool
        if (pauseInputValue <= 0) isPaused = false; 
    }

	// Run on Play + Rewind events to have an accurate indicator of current time flow
    private void ToggleDirection()
    {
        timeDirection = !timeDirection;
    }

    // removing events in case of destruction
    private void OnDestroy()
    {
        OnTimeRewind -= ToggleDirection;
        OnTimePlay -= ToggleDirection;
    }

    public void AniPlay()
    {
        if (OnTimePlay != null)
        OnTimePlay();
    }

    public void AniStop()
    {
        if (OnTimeStop != null)
            OnTimeStop();
    }
    
    /* // OLD CODE: Replaced with event system linking with ClueController.cs
    void Play()
    {
        foreach (Animator x in AnimateList)
        {
            x.SetFloat("Time", 1);
        }
    }

    void Rewind()
    {
        foreach (Animator x in AnimateList)
        {
            x.SetFloat("Time", -1);
        }
    }

    void Stop()
    {
        if (pause == false)
        {
            foreach (Animator x in AnimateList)
            {
                x.SetFloat("Time", 0);
            }
            pause = !pause;
        }
        else
        {
            if(direction == true)
            {
                Play();
            }
            else
            {
                Rewind();
            }
            pause = !pause;
        }
    }
*/

}

}

