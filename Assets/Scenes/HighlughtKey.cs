using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlughtKey : MonoBehaviour
{
    // Start is called before the first frame update
    public ColourKeys colourKey;
    void Start()
    {
        Debug.Log("Highlighting key");
         colourKey.ToggleHighlight(true);
    }

    // Update is called once per frame
  
}
