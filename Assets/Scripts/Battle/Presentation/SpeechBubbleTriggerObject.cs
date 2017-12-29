using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleTriggerObject : MonoBehaviour {

    public List<SpeechBubbleQuipObject> quips;
    public string triggerType;

    private int weightTotal;

    public SpeechBubbleQuipObject GetQuip()
    {
        weightTotal = GenerateWeightTotal();
        int ran = Random.Range(0, weightTotal);
        int sum = 0;
        SpeechBubbleQuipObject temp;

        foreach (SpeechBubbleQuipObject quip in quips)
        {
            if (ran < (sum + quip.weight))
            {
                temp = quip;
                if (quip.playOnce)
                {
                    quips.Remove(quip);
                }
                return temp;
            }
            else
            {
                sum += quip.weight;
            }
        }
        return null;
    }

    private int GenerateWeightTotal()
    {
        int total = 0;
        foreach (SpeechBubbleQuipObject quip in quips)
        {
            total += quip.weight;
        }
        return total;
    }
}
