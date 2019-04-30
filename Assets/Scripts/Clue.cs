using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour {

    public List<ClipStruct> ClipsToSpwan;
    public bool NeedsTrigger;
    public GameObject TheTrigger;
    public bool BeenActive = false;

    public void Activate()
    {
        if (BeenActive == false)
        {
            foreach (ClipStruct A in ClipsToSpwan)
            {
                ClipStruct placeholder;
                placeholder = Instantiate(A);
                GlobleEvents.OnClueActivate_Func(placeholder);
            }
            BeenActive = true;
        }
    }
}
