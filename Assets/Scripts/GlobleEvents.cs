using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobleEvents{

    //List of events to be used sparingly
    public static event Action<ClipStruct> OnClueActivate;
    public static event Action TriggerEnter;
    public static event Action TriggerExit;
    public static event Action<GameObject> Toggle;

    //Function to be called to call event 
    public static void OnClueActivate_Func(ClipStruct A)
    {
        if (OnClueActivate != null)
        {
            OnClueActivate(A);
        }
    }

    //Function to be called to call event 
    public static void OnTriggerEnter_Func()
    {
        if (TriggerEnter != null) TriggerEnter();
    }

    //Function to be called to call event 
    public static void OnTriggerExit_Func()
    {
        if (TriggerExit != null) TriggerExit();
    }

    //Function to be called to call event 
    public static void ActiveToggle(GameObject A)
    {
        if (Toggle != null) Toggle(A);
    }
}
