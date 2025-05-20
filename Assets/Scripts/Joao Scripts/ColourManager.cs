using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wilberforce;

public class ColourManager : MonoBehaviour
{
    public Colorblind colorblind;
    public int colorSet;


    
    // Start is called before the first frame update
    void Start()
    {
        colorblind = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Colorblind>();
        
    }

    // Update is called once per frame
    void Update()
    {
        colorSet = ColorController.colornumber;
        if(colorSet == 0)
        {
            colorblind.Type = 0;
        }
        if(colorSet == 1)
        {
            colorblind.Type = 1;
        }
        if (colorSet == 2)
        {
            colorblind.Type = 2;
        }
        if (colorSet == 3)
        {
            colorblind.Type = 3;
        }
    }
}
