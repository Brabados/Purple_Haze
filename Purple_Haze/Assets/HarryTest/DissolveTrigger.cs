using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTrigger : MonoBehaviour {

    Renderer[] rend;
    WaitForSeconds loopWait = new WaitForSeconds(0.03f); // delay between updates
    WaitForSeconds duration = new WaitForSeconds(0.01f); // duration of a update

    public MeshRenderer[] mat;
    public Texture tex;

    public int framesToRun = 15;
    int fps = 24;
    private int frameCount;

    public Animation anim;
    private Animator animator;
    private AnimatorStateInfo clip;
    

    private void Awake()
    {
        rend = GetComponentsInChildren<Renderer>();

        mat = GetComponentsInChildren<MeshRenderer>();

        animator = GetComponentInChildren<Animator>();
        
        frameCount = (int)(fps * clip.length);

    }

    private void Update()
    {
        Debug.Log(animator);
    }

    // subscribe to pause event to stop coroutine
    // if stopped subscribe to restart coroutine
    // make state for forwards/backwards/paused
    // animation events at beginning/end to start dissolving
    
//    IEnumerator Start()
//    { 
//
//        while (true)
//        {
//            StartCoroutine(Dissolve());
//            yield return loopWait;
//        }
//
//    }

    IEnumerator Dissolve() // turning on/randomizing glitch function
    {

        foreach (Renderer r in rend)
        {
            float value = r.material.GetFloat("_DissolveThreshold");

            if (frameCount * clip.normalizedTime < framesToRun)
            {
                r.material.SetFloat("_DissolveThreshold", Mathf.Lerp(1.2f,0, frameCount * clip.normalizedTime));
            }
            
        }

        yield return duration;

    }

}