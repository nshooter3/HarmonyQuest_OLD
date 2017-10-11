using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour {

    //Used to calculate the tick duration based on the bpm of the music
    private float bpm = 120f;
    //The interval in seconds that a tick occurs
    private float tickDuration;

    //Delegate that is called upon an update tick
    public delegate void UpdateTick();
    public UpdateTick MyUpdateTick;

    public static BeatManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Calculate duration of quarter note from BPM
        tickDuration = 60f / bpm;
    }

    //Select a new BPM. Updates tickDuration as well
    public void SetBPM(float newBPM)
    {
        bpm = newBPM;
        tickDuration = 60f / bpm;
    }

    //Returns current BPM
    public float GetBPM()
    {
        return bpm;
    }

    //Return the duration of a Quarter Note at our current BPM
    public float GetQuarterNote()
    {
        return tickDuration;
    }

    //Return the duration of an 8th Note at our current BPM
    public float Get8thNote()
    {
        return tickDuration/2f;
    }

    //Return the duration of a 16th Note at our current BPM
    public float Get16thNote()
    {
        return tickDuration/4f;
    }

    //Calls the MyUpdateTick delegate once per beat based on the bpm. Add stuff to MyUpdateTick if it needs to be called on every beat
    IEnumerator TickCounter()
    {
        while (true)
        {
            MyUpdateTick();
            yield return new WaitForSeconds(tickDuration);
        }
    }

    //Starts the TickCounter coroutine
    public void StartTick()
    {
        StartCoroutine(TickCounter());
    }

    //Stops the TickCounter coroutine from firing
    public void StopTick()
    {
        StopCoroutine(TickCounter());
    }
}
