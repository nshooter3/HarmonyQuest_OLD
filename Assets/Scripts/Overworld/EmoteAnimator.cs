using UnityEngine;
using System.Collections;

public class EmoteAnimator : MonoBehaviour {

    Animator anim;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void SetEmotion(string emotion)
    {
        anim = GetComponent<Animator>();
        if (anim != null)
        {
            switch (emotion)
            {
                case "Surprise":
                    anim.SetTrigger("Surprise");
                    audio.clip = (AudioClip)Resources.Load("SFX/Emote/Surprise");
                    audio.volume = 0.25f;
                    audio.Play();
                    break;
                case "Neutral":
                    anim.SetTrigger("Neutral");
                    break;
            }
        }
    }
}
