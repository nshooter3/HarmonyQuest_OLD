using UnityEngine;
using System.Collections;

public class PortraitEmotions : MonoBehaviour {

    Animator anim;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetEmotion(GameObject portrait, string emotion)
    {
        anim = portrait.GetComponent<Animator>();
        if (anim != null)
        {
            if (portrait != null)
            {
                switch (emotion)
                {
                    case "happy":
                        anim.SetTrigger("happy");
                        break;
                    case "happyTalking":
                        anim.SetTrigger("happyTalking");
                        break;
                    case "mad":
                        anim.SetTrigger("mad");
                        break;
                    case "madTalking":
                        anim.SetTrigger("madTalking");
                        break;
                    case "sad":
                        anim.SetTrigger("sad");
                        break;
                    case "sadTalking":
                        anim.SetTrigger("sadTalking");
                        break;
                    case "shock":
                        anim.SetTrigger("shock");
                        break;
                    case "surprise":
                        anim.SetTrigger("surprise");
                        break;
                    case "neutral":
                        anim.SetTrigger("neutral");
                        break;
                    case "talking":
                        anim.SetTrigger("talking");
                        break;
                    case "blush":
                        anim.SetTrigger("blush");
                        break;
                    case "concern":
                        anim.SetTrigger("concern");
                        break;
                    case "concernTalking":
                        anim.SetTrigger("concernTalking");
                        break;
                    case "invisible":
                        anim.SetTrigger("invisible");
                        break;
                }
            }
        }
    }

    public void DoneTalking(GameObject portrait, bool isTalking)
    {
        if (portrait != null)
        {
            anim = portrait.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("DoneTalking", isTalking);
            }
        }
    }
}
