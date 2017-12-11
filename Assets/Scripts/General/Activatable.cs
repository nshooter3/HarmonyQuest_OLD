using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

    public AudioSource sfx;
    public float duration, delay;
    public int quarterNotes;
    public bool isComplete = false, isTypeFade = true, wait = true;

    //Position/scale related vars
    public Vector3 startPos, endPos, startScale, endScale, startRot, endRot;

    public virtual void Activate(){ }

    protected IEnumerator PlaySFX()
    {
        yield return new WaitForSeconds(delay);
        if (sfx != null)
        {
            sfx.Play();
        }
    }

    protected IEnumerator PlayParticles()
    {
        yield return new WaitForSeconds(delay);
        if (GetComponent<ParticleSystem>() != null)
        {
            GetComponent<ParticleSystem>().Play();
        }
    }

}
