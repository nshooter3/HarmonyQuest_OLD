using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStartSequence : MonoBehaviour
{

    public DynamicText[] text;
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
        foreach (DynamicText t in text) {
            t.Activate();
            yield return new WaitForSeconds(t.duration + t.delay);
        }
        isSequenceOver = true;
    }
}
