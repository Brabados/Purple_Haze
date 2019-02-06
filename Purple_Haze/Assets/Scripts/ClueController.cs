using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueController : MonoBehaviour {

    public Clue enabler;
    public GameObject Cluemod;
    public ParticalClamp ParticalToggle;

    private void Start()
    {
        enabler = GetComponent<Clue>();
        ParticalToggle = GetComponent<ParticalClamp>();
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

}
