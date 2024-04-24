using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourKeys : MonoBehaviour
{
    public Material[] materials;

    [SerializeField]
    private Renderer objectRenderer;
    

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.sharedMaterial = materials[0];

    }
   
    public void ToggleHighlight(int val) //0 = no colour, 1 = red, 2 = green
    {
        if (val == 1)
        {
            objectRenderer.material = materials[1];
        }
        else if(val == 2)
        {
            objectRenderer.material = materials[2];
        }
        else
        {
            objectRenderer.material = materials[0];
        }
            
       
    }
}
