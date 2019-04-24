using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTrigger : MonoBehaviour {

    Renderer[] rend;
    WaitForSeconds loopWait = new WaitForSeconds(0.03f); // delay between updates
    WaitForSeconds duration = new WaitForSeconds(0.01f); // duration of a update

    public MeshRenderer[] mat;
    public Texture tex;

    int fps = 24;
    private int frameCount;

    private Animator animator;
    private AnimatorStateInfo clip;
    

    private void Awake()
    {
        rend = GetComponentsInChildren<Renderer>();

        mat = GetComponentsInChildren<MeshRenderer>();

        animator = GetComponentInChildren<Animator>();
        clip = animator.GetCurrentAnimatorStateInfo(0);
        frameCount = (int)(fps * clip.length);

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

        foreach (Renderer r in rend)
        {
            r.material.SetFloat("_DissolveThreshold", r.material.GetFloat("_DissolveThreshold") + 0.02f);
        }

        yield return duration;

    }


}
