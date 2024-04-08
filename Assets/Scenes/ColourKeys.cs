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
   
    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            objectRenderer.material = materials[1];
        }
        else
        {
            objectRenderer.material = materials[0];
        }
            
       
    }
}
