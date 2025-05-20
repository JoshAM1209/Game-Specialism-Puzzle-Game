using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wilberforce;
using UnityEngine.UI;
using TMPro;


public class ColorController : MonoBehaviour
{
    public Colorblind colorblind;
    [SerializeField] private TMP_Dropdown dropdown;
    public static int colornumber;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        colorblind = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Colorblind>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetDropdownNumber()
    {
        int pickedEntryIndex = dropdown.value;
    }
    public void ChangeType1()
    {
        if (dropdown.value == 0)
        {
            colornumber = 0;
            colorblind.Type = 0;
        }
        if (dropdown.value == 1)
        {
            colornumber = 1;
            colorblind.Type = 1;
        }
        if (dropdown.value == 2)
        {
            colornumber = 2;
            colorblind.Type = 2;
        }
        if (dropdown.value == 3)
        {
            colornumber = 3;
            colorblind.Type = 3;
        }

    }
}
