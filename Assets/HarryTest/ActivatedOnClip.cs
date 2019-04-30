using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedOnClip : MonoBehaviour
{

    public static List<GameObject> ToDisable = new List<GameObject>();
    public static List<GameObject> ToEnable = new List<GameObject>();

    public enum Type { toEnable, toDisable }

    public Type myType;
    public int videoId;
    
    private void Awake()
    {
        switch (myType)
        {
            case Type.toEnable:

                ToEnable.Add(this.gameObject);
                this.gameObject.SetActive(false);
                
                break;
            case Type.toDisable:

                ToDisable.Add(this.gameObject);
                
                break;
        }
    }

    private void OnDestroy()
    {
        if (ToEnable.Contains(this.gameObject))
        {
            ToEnable.Remove(this.gameObject);
        }

        if (ToDisable.Contains(this.gameObject))
        {
            ToDisable.Remove(this.gameObject);
        }
    }
}
