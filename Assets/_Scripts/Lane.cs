using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public KeyCode input2;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    int spwnInd = 0;
    int inputInd = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spwnInd < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spwnInd] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spwnInd];
                spwnInd++;
            }
        }

        if (inputInd < timeStamps.Count)
        {
            double TS = timeStamps[inputInd];
            double errorMargin = SongManager.Instance.errorMargin;
            double AT = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelay / 1000.0);

            if (Input.GetKeyDown(input) || Input.GetKeyDown(input2))
            {
                if (Math.Abs(AT - TS) < errorMargin)
                {
                    Hit();
                    print($"Hit on {inputInd} note");
                    Destroy(notes[inputInd].gameObject);
                    inputInd++;
                }
                else
                {
                    print($"Hit inaccurate on {inputInd} note with {Math.Abs(AT - TS)} delay");
                }
            }
            if (TS + errorMargin <= AT)
            {
                Miss();
                print($"Missed {inputInd} note");
                inputInd++;
            }
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
    }

    private void Miss()
    {
        ScoreManager.Miss();
    }
}
