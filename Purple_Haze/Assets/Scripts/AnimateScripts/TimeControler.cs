using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControler : MonoBehaviour {

    //List of all animators
    public Animator[] AnimateList = new Animator[0];

    //Bool discribing driecton of animation. True = forwards, False = backwards.
    public bool direction = true;

    //Bool for toggling pause animation on and off.
    public bool pause = false;


    // Use this for initialization
    void Start () 
    {
        //gets all animators in scene to be modded
      AnimateList =  FindObjectsOfType<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //RECODE: This should all be reoginized into a event system so as not to have ot get a list of all the animators.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (direction == true)
            {
                Rewind();
                direction = !direction;
            }
            else
            {
                Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (direction == false)
            {
                Play();
                direction = !direction;
            }
            else 
            {
                Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Stop();
        }
    }

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


}
