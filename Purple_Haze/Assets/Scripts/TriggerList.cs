using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerList : MonoBehaviour {

    public List<GameObject> ObjectsToGetClue = new List<GameObject>();

    public List<GameObject> ToggledObjects = new List<GameObject>();

    public Clue clue;

    // Use this for initialization
    void Start () 
    {
        GlobleEvents.Toggle += AddGameObj;
        GlobleEvents.TriggerExit += cleartoggles;
	}

    void AddGameObj(GameObject A)
    {


        int compare = 0;

        bool NoAdd = false;

        foreach(GameObject n in ToggledObjects)
        {
            if(A == n)
            {
                NoAdd = true;
                A.GetComponent<ParticalClamp>().Toggle();
                ToggledObjects.Remove(n);
            }
        }
     

        if (NoAdd == false)
        {
            ToggledObjects.Add(A);
            A.GetComponent<ParticalClamp>().Toggle();
            foreach (GameObject n in ToggledObjects)
            {
                foreach (GameObject j in ObjectsToGetClue)
                {
                    if (n == j)
                    {
                        compare++;
                    }
                }
            }
        }

        if(compare == ObjectsToGetClue.Count && ObjectsToGetClue.Count == ToggledObjects.Count)
        {
            clue.Activate();
        }
    }

    void cleartoggles()
    {
        ToggledObjects.Clear();
    }
}
