using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicXml; //using parser from https://github.com/vdaron/MusicXml.Net/blob/develop/MusicXml
using MusicXml.Domain; 
public class SheetMusicParser 
{
    public static (List<NoteData>, List<NoteData>) ParseXML(string path)
    {
        List<NoteData> rightHandNotes = new List<NoteData>();
        List<NoteData> leftHandNotes = new List<NoteData>();
        // Load the MusicXML file
        var score = MusicXmlParser.GetScore(path);
        //get notes
        
        foreach (var part in score.Parts)
        {
            float startTimeTreble = 0.0f;
            float startTimeBass = 0.0f;
            int divisions = 4;
            int beats = 4;
            foreach (var measure in part.Measures) //measure is a bar
            {
                if(measure.Attributes != null)
                {
                    divisions = measure.Attributes.Divisions;
                    beats = measure.Attributes.Time.Beats;
                }
                
                if(divisions == 0)
                {
                    Debug.Log("Divisions is 0");
                    continue;
                }
                
                foreach (var element in measure.MeasureElements)
                {
                    if (element.Type == MeasureElementType.Note)
                    {
                        Note note = element.Element as Note;
                        float noteDuration = ((float)note.Duration/ divisions);  //assumes 60bpm
                        if (note.IsRest)
                        {
                            Debug.Log("Rest" + noteDuration);
                            if (note.Staff == 1)
                            {
                                startTimeTreble += noteDuration;
                            }
                            else
                            {
                                startTimeBass += noteDuration;
                            }
                            continue;
                        }
                        int position = GetPosition(note);
                        bool isSharp = note.Pitch.Alter > 0;
                        bool isFlat = note.Pitch.Alter < 0;
                        if (note.Staff == 1)
                        {
                            rightHandNotes.Add(new NoteData(startTimeTreble, noteDuration, position, isSharp, isFlat));
                            startTimeTreble += noteDuration;
                        }
                        else
                        {
                            leftHandNotes.Add(new NoteData(startTimeBass, noteDuration, position, isSharp, isFlat));
                            startTimeBass += noteDuration;
                        }
                         Debug.Log("added notes");
                    }
                }
            }

        }

        return (rightHandNotes, leftHandNotes);
    }
     private static int GetPosition(Note note)
    {
        //positions include sharps so 0 is C, 1 is C#, 2 is D, 3 is D# etc
        //per octave there are 11 notes, 7 white and 4 black
        //x flat is x-1 sharp
        int position = 0;
        switch (note.Pitch.Step)
        {
            case 'C':
                if (note.Pitch.Alter > 0)
                {
                    position = 1;
                }
                else
                {
                    position = 0;
                }
                break;
            case 'D':
                if (note.Pitch.Alter > 0) //d#
                {
                    position = 3;
                }
                else if (note.Pitch.Alter < 0) //c# is the same as db
                {
                    position = 1;
                    
                }
                else
                {
                    position = 2;
                }
                break;
            case 'E':
            if(note.Pitch.Alter < 0) //eb is the same as d#
            {
                position = 3; 
            }
            else
            {
                position = 4;
            }
                break;
            case 'F':
                if (note.Pitch.Alter > 0) //f#
                {
                    position = 6;
                }
                else
                {
                    position = 5;
                }
                break;
            case 'G':
                if (note.Pitch.Alter > 0) //g#
                {
                    position = 8;
                }
                else if (note.Pitch.Alter < 0) //f# is the same as gb
                {
                    position = 6;
                }
                else
                {
                    position = 7;
                }
                break;
            case 'A':
                if (note.Pitch.Alter > 0) //a#
                {
                    position = 10;
                }
                else if (note.Pitch.Alter < 0) //g# is the same as ab
                {
                    position = 8;
                }
                else
                {
                    position = 9;
                }
                break;
            case 'B':
                if (note.Pitch.Alter < 0) //bb is the same as a#
                {
                    position = 10;
                }
                else
                {
                    position = 11;
                }
                break;
        }
        return position + note.Pitch.Octave  * 12;
    }
}
