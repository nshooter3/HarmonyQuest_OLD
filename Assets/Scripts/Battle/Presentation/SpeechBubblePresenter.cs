using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubblePresenter : MonoBehaviour {

    public GameObject speechBubble;
    public Text speechText;

    private IEnumerator showText;

    public static SpeechBubblePresenter instance;

    public SpeechBubbleQuipObject curQuip;

    public Animator enemyAnim;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        speechText.text = "";
        speechBubble.SetActive(false);
    }

    public void Show(SpeechBubbleQuipObject quip, float stickTime = 2.0f)
    {
        if (quip == null || (curQuip != null && quip.priority <= curQuip.priority))
        {
            return;
        }
        else
        {
            if (quip.text != "")
            {
                Hide();
                curQuip = quip;
                speechBubble.transform.localScale = Vector3.zero;
                speechBubble.SetActive(true);
                speechText.text = "";
                speechBubble.GetComponent<DynamicTransform>().Activate();
                showText = ShowText(curQuip.text, 0.25f, stickTime);
                StartCoroutine(showText);
            }
        }
    }

    public void Hide()
    {
        if (showText != null)
        {
            StopCoroutine(showText);
        }
        curQuip = null;
        speechBubble.SetActive(false);
    }

    IEnumerator ShowText(string text, float delay = 0.25f, float stickTime = 2.0f)
    {
        yield return new WaitForSeconds(delay);

        string finalText = text;
        int textIndex = 0;

        enemyAnim.SetBool("isTalking", true);

        //Update dialogue UI
        while (textIndex <= text.Length) {
            speechText.text = finalText.Substring(0, textIndex) + "<color=#0000>" + finalText.Substring(textIndex) + "</color>";

            //Adds slight pauses after certain characters for spacing
            if (textIndex - 1 > 0 && (textIndex - 1) < finalText.Length && (finalText[(textIndex - 1)].Equals('.') || finalText[(textIndex - 1)].Equals(',') ||
                finalText[(textIndex - 1)].Equals('!') || finalText[(textIndex - 1)].Equals('?')))
            {
                //Prevents pause if followed by quotation mark
                if (!(textIndex < finalText.Length && finalText[(textIndex)].Equals('"')))
                {
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else
            {
                yield return new WaitForSeconds(0.02f);
            }

            textIndex++;
        }

        yield return new WaitForSeconds(stickTime);
        enemyAnim.SetBool("isTalking", false);
        Hide();
    }
}
