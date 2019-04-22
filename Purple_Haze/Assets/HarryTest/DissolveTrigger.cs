using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTrigger : MonoBehaviour {

    Renderer[] rend;
    WaitForSeconds loopWait = new WaitForSeconds(0.03f); // delay between checking for glitches
    WaitForSeconds duration = new WaitForSeconds(0.01f); // duration of a glitch

    public bool dying = false;

    public MeshRenderer[] mat;
    public Texture tex;

    private void Awake()
    {
        rend = GetComponentsInChildren<Renderer>();

        mat = GetComponentsInChildren<MeshRenderer>();
    }

    IEnumerator Start()
    { 

        while (true)
        {
            StartCoroutine(Dissolve());
            yield return loopWait;
        }

    }

    IEnumerator Dissolve() // turning on/randomizing glitch function
    {

        if (dying)
        {
            foreach (Renderer r in rend)
            {
                r.material.SetFloat("_DissolveThreshold", r.material.GetFloat("_DissolveThreshold") + 0.02f);
            }

            yield return duration;
        }
        else
        {
            foreach (Renderer r in rend)
            {
                if (r.material.GetFloat("_DissolveThreshold") > 0)
                {
                    r.material.SetFloat("_DissolveThreshold", r.material.GetFloat("_DissolveThreshold") - 0.02f);
                }
            }

            yield return duration;
        }
      

    }


}
