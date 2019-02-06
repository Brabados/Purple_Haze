using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour 
{

    public delegate void NoClueEvent();
    public static event NoClueEvent NoEvent;

    public delegate void EnableClueEvent();
    public static event EnableClueEvent EnableEvent;

}
