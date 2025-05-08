using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCounter : MonoBehaviour
{
    public List<GameObject> piecesToPop;
    public int popCount = 0;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (popCount >= 4)
        {
            foreach (GameObject piece in piecesToPop)
            {

            }
        }
    }
}
