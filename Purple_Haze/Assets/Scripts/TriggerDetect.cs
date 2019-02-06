using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerDetect : MonoBehaviour {

    //Does event call when triggered by the player controller
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MainCamera")
        {
            GlobleEvents.OnTriggerEnter_Func();
        }
    }

    //Does event call when trigger left by the player controller
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            GlobleEvents.OnTriggerExit_Func();
        }
    }
}
