using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteData 
{
    public float StartTime; // In seconds relative to the start of the song
    public float Duration; // In seconds 
    public int Position; // represent a piano key index
    public bool IsSharp; // Is the note sharp?
    public bool IsFlat; // Is the note flat?

    public NoteData(float startTime, float duration, int position, bool isSharp, bool isFlat)
    {
        StartTime = startTime;
        Duration = duration;
        Position = position;
        IsSharp = isSharp;
        IsFlat = isFlat;
    }

}
