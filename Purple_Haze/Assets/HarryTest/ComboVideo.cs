using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Harrison
{
    
    [CreateAssetMenu(fileName = "ComboVideo", menuName = "ComboVideo", order = 1)]
    public class ComboVideo : ScriptableObject
    {
        public ClipStruct clipStruct;
        public List<int> ids = new List<int>();   
    }

}
