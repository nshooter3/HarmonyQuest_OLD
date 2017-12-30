using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleTriggerObject : MonoBehaviour {

    public List<SpeechBubbleQuipObject> quips;
    public string triggerType;
    public float maxCooldown = 0;
    private float cooldown = 0;

    private int weightTotal;

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public SpeechBubbleQuipObject GetQuip()
    {
        if (cooldown <= 0)
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
                    cooldown = maxCooldown;
                    return temp;
                }
                else
                {
                    sum += quip.weight;
                }
            }
            return null;
        }
        else
        {
            Debug.Log(triggerType + " trigger is in cooldown, will not fire quip");
            return null;
        }
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
