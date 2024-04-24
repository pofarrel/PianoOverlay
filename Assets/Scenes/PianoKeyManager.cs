using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PianoKeyManager : MonoBehaviour
{
    // private ColourKeys[] pianoKeys;
    public static ColourKeys[] GetPianoKeys()
    {
        ColourKeys[] pianoKeys = FindObjectsOfType<ColourKeys>();
        // foreach (ColourKeys key in pianoKeys)
        // {
        //     key.ToggleHighlight(true);
        // 
        return sortKeys(pianoKeys);
    }
    private static ColourKeys[] sortKeys(ColourKeys[] pianoKeys)
    {
        // Convert array to a list and sort by tag
        var sortedPianoKeys = pianoKeys.OrderBy(pianoKey => float.Parse(pianoKey.gameObject.tag));

        return sortedPianoKeys.ToArray();
    }

}
