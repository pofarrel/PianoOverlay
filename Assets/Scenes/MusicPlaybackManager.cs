using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class MusicPlaybackManager : MonoBehaviour
{
    public GameObject SheetMusicImage;
    private ColourKeys[] colourKeys;
    private List<NoteData> leftHandNotes;
    private List<NoteData> rightHandNotes;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = "/Users/peterofarrell/Desktop/fourth_year/Augmented_Reality/project/demo piano/Music/Example-3.xml";
        (rightHandNotes, leftHandNotes) = SheetMusicParser.ParseXML(filePath);
        colourKeys = PianoKeyManager.GetPianoKeys();
        StartCoroutine(RightHandPlayMusic());
        StartCoroutine(LeftHandPlayMusic());
        
    }

    private IEnumerator RightHandPlayMusic()
    {
        float currentTime = 0.0f;   
        foreach (NoteData note in rightHandNotes)
        {
            if(currentTime < note.StartTime)
            {
                yield return new WaitForSeconds(note.StartTime - currentTime);
                currentTime = note.StartTime;
            }
            colourKeys[note.Position].ToggleHighlight(1);
            Debug.Log("Playing note" + note.Position);
            Debug.Log("Duration" + note.Duration);
            yield return new WaitForSeconds(note.Duration);
            colourKeys[note.Position].ToggleHighlight(0);
            currentTime += note.Duration;
        }
    }
    private IEnumerator LeftHandPlayMusic()
    {
        float currentTime = 0.0f;   
        foreach (NoteData note in leftHandNotes)
        {
            if(currentTime < note.StartTime)
            {
                yield return new WaitForSeconds(note.StartTime - currentTime);
                currentTime = note.StartTime;
            }
            colourKeys[note.Position].ToggleHighlight(2);
            Debug.Log("Playing note" + note.Position);
            Debug.Log("Duration" + note.Duration);
            yield return new WaitForSeconds(note.Duration);
            colourKeys[note.Position].ToggleHighlight(0);
            currentTime += note.Duration;
        }
    }
}
