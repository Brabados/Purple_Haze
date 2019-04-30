using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harrison
{
    

    public class DissolveTrigger : MonoBehaviour {

        Renderer[] rend;

        private bool paused = false;
        private enum DissolveState { onStart, notDissolving, onEnd }
        private DissolveState dissolving;
        
        [Range(0,0.3f)] public float dissolveSpeed = 0.02f;

        private void Awake()
        {
            rend = GetComponentsInChildren<Renderer>();

            foreach (Renderer r in rend)
            {
                if (!r.material.HasProperty("_DissolveThreshold")) continue;

                r.material.SetFloat("_DissolveThreshold", 1.2f);

            }

            dissolving = DissolveState.onStart;
            
            TimeControler.OnTimeStop += Stop;
            TimeControler.OnTimePlay += Resume;
            TimeControler.OnTimeRewind += Resume;
            
            
        }
    
        IEnumerator Start()
        { 
    
            while (true)
            {
                StartCoroutine(Dissolve());
                yield return new WaitForSeconds(0.01f);
            }
    
        }

        private IEnumerator Dissolve() // turning on/randomizing glitch function
        {

            if (paused) yield break;
            
            foreach (Renderer r in rend)
            {
                if (!r.material.HasProperty("_DissolveThreshold")) continue;
                
                float value = r.material.GetFloat("_DissolveThreshold");

                switch (dissolving)
                {
                    case DissolveState.onStart:

                        if (TimeControler.timeDirection)
                            r.material.SetFloat("_DissolveThreshold", value - dissolveSpeed);
                        else
                            r.material.SetFloat("_DissolveThreshold", value + dissolveSpeed);
                        
                        break;
                    case DissolveState.onEnd:

                        if (TimeControler.timeDirection)
                            r.material.SetFloat("_DissolveThreshold", value + dissolveSpeed);
                        else
                            r.material.SetFloat("_DissolveThreshold", value - dissolveSpeed);
                        
                        break;
                }

            }

            yield return null;

        }

        public void OnStart()
        {
            if (TimeControler.timeDirection)
                dissolving = DissolveState.onStart;
        }

        public void OnEnd()
        {
            if (!TimeControler.timeDirection)
                dissolving = DissolveState.onEnd;
        }
        
        public void ToggleDissolve()
        {
            switch (dissolving)
            {
                case DissolveState.onStart:
                    
                    if (TimeControler.timeDirection)
                        dissolving = DissolveState.notDissolving;
                    else
                        dissolving = DissolveState.onEnd;

                    break;
                case DissolveState.notDissolving:
                    
                    if (TimeControler.timeDirection)
                        dissolving = DissolveState.onEnd;
                    else
                        dissolving = DissolveState.onStart;
                    
                    break;
                case DissolveState.onEnd:
                    
                    if (TimeControler.timeDirection)
                        dissolving = DissolveState.onStart;
                    else
                        dissolving = DissolveState.notDissolving;

                    break;
            }
        }

        void Resume()
        {
            if (paused)
                paused = !paused;
        }
        
        void Stop()
        {
            paused = !paused;
        }

        private void OnDestroy()
        {
            TimeControler.OnTimeStop -= Stop;
            TimeControler.OnTimePlay -= Resume;
            TimeControler.OnTimeRewind -= Resume;
        }
    }
    
}