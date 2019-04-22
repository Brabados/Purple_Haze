using System.Collections;
using System.Collections.Generic;
using Harrison;
using UnityEngine;
using UnityEngine.UI;
using VideoClip = UnityEngine.Video.VideoClip;

public class ClipStruct: MonoBehaviour
{
    //Discriptor string to be displayed on button. eg. Blood Splater or Vision Discrepency
    public string Question;

    //Video clip to play when button is pressed
    public VideoClip ToPlay;

    //Bool to remeber if clip has been viewed
    public bool beenPlayed = false;

    //List of objects to activate apon viewing
    public List<GameObject> ToActivate = new List<GameObject>();

    //List of objects to disable apon viewing
    public List<GameObject> ToDisable = new List<GameObject>();
    
    // For combination videos - Any unique number
    public int combineId;

    private void Start()
    {
        VideoClipManager.VCM.FoundCluesLisCS.Add(this);
    }
}

