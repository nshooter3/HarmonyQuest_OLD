using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatableSequence : MonoBehaviour
{

    public Activatable[] actions;
    public bool isSequenceOver = false;

    // Use this for initialization
    void Start()
    {
        StartSequence(2.0f);
    }

    public void StartSequence(float delay)
    {
        StartCoroutine(StartSequenceCo(delay));
    }

    IEnumerator StartSequenceCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (Activatable a in actions) {
            a.Activate();
            if (a.wait)
            {
                yield return new WaitForSeconds(a.duration + a.delay);
            }
        }
        isSequenceOver = true;
    }
}
