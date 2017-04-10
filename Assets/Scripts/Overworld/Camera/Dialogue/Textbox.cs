using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Textbox : MonoBehaviour {

    //References to all the gameObjects that make up the textbox GUI
    public GameObject textObj, boxObj, choicesObj, choice1, choice2, choice3, selection, portraitPlaceholder, all;
    private GameObject portrait;

    //Text component of the dialogue and each choice gameObject
    public Text text, c1, c2, c3;

    //Indicates current choice selected
    public int choice = 0;

    //How fast the text "types", and a bool that determines whether or not it has finished typing.
    //private float textTimer, textTimerMax;
    public bool textLoading = false;
    private int textIndex;

    //How fast the choiceUI moves up and down when changing state
    public float choiceUIMoveSpeed;
    public bool choiceUIWait;

    //Tells whether or not choices are currently active
    public bool choicesActive = false;

    //Variables for handling the portrait fade in
    RectTransform portraitTransform;
    private bool portraitMoving;
    private float portraitProgess, portraitSpeed, portraitFadeSpeed;
    private Vector2 portraitStartPos, portraitEndPos;
    private Color portraitCol;
    private float portraitMoveDistance;

    //Array of all the choices
    private Text[] choiceList;

    //Gamestate for whether or not choices are active, and how many are available
    private enum ChoiceState {none, twoChoice, threeChoice};
    private ChoiceState choiceState;

    //Values used to smoothly transition the choice UI up and down depending on the gamestate
    public bool choiceUIMoving;
    private Vector3 choiceUIMovingStartPos, choiceUIMovingEndPos;
    private float startTime;
    private float journeyLength;

    //Indicates the index of the last available choice
    private int choiceMax;

    //Placeholder pos floats
    private float x, y, z;

    /* Y position values for the choice UI based on how many choices there are
    //  Default: -200
    //  3 choices: -25
    //  2 choices: -80
    */
    private int choiceInit_Ypos = -200; int choice3_Ypos = -25, choice2_Ypos = -75;
    private Vector3 choiceInit_pos, choice3_pos, choice2_pos;

    //Cooldown timer that prevents user from accidentally blasting through a choice if they're skipping dialogue
    public float noChoiceTimer;

    //Used to tell the dialogue box when to indent stuff.
    public int textLineLength;

    /*
        TODO: Use these to do stuff, idiot
        TODO: Use these to do stuff, idiot
        TODO: Use these to do stuff, idiot
    */
    //If not empty, will be used to show name of speaker
    private string DName;
    //If not empty, will be concatenated with path to set the character portrait
    private string DPortrait, prevDPortrait;
    //Will be used to change emotion on portrait
    private string DMetadata;
    //Will be used to play new song during dialogue
    private string DTheme;
    //Used to store final text
    private string finalText;
    private bool pFade;

    //Script that contains methods for changing portrait animations
    public PortraitEmotions porAnim;

    public AudioSource textSFX, selectionSFX, moveSFX;
    //Used to  only play SFX every couple of characters
    private int sFXCount, maxSFXCount;
    private int pauseCount;

    // Use this for initialization
    void Start () {
        all.SetActive(true);
        ToggleOff();
        text = textObj.GetComponent<Text>();
        c1 = choice1.GetComponent<Text>();
        c2 = choice2.GetComponent<Text>();
        c3 = choice3.GetComponent<Text>();
        choiceList = new Text[]{ c1, c2, c3 };
        choiceUIMoveSpeed = 800;
        //textTimerMax = 0.02f;
        sFXCount = 0;
        maxSFXCount = 3;
        pauseCount = 0;

        x = choicesObj.transform.localPosition.x;
        z = choicesObj.transform.localPosition.z;
        choiceInit_pos = new Vector3(x, choiceInit_Ypos, z);
        choice3_pos = new Vector3(x, choice3_Ypos, z);
        choice2_pos = new Vector3(x, choice2_Ypos, z);
        choiceUIWait = false;

        noChoiceTimer = 0;
        prevDPortrait = "";

        portraitSpeed = 4f;
        portraitFadeSpeed = 3f;
        portraitMoveDistance = 50;

        Time.fixedDeltaTime = 0.025f;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(portraitMoving);
        choiceUIUpdate();
        PortraitUpdate();
        //textUpdate();
    }

    void FixedUpdate()
    {
        textUpdate();
    }

    void PortraitUpdate()
    {
        if (portraitMoving && portrait != null)
        {
            //Debug.Log(portraitProgess);
            if (portraitProgess < 1)
            {
                portraitProgess += Time.deltaTime * portraitSpeed;
                portraitTransform.anchoredPosition = Vector2.Lerp(portraitStartPos, portraitEndPos, portraitProgess);
                portraitCol = new Color(portrait.GetComponent<Image>().color.r, portrait.GetComponent<Image>().color.b, portrait.GetComponent<Image>().color.g, portraitProgess);
            }
            else
            {
                portraitTransform.anchoredPosition = portraitEndPos;
                portraitCol = new Color(portrait.GetComponent<Image>().color.r, portrait.GetComponent<Image>().color.b, portrait.GetComponent<Image>().color.g, 1);
                portraitMoving = false;
            }
            portrait.GetComponent<Image>().color = portraitCol;
        }
        else if (pFade && portrait != null)
        {
            if (portraitProgess < 1)
            {
                //Debug.Log(1f - portraitProgess);
                portraitProgess += Time.deltaTime* portraitFadeSpeed;
                portraitCol = new Color(portrait.GetComponent<Image>().color.r, portrait.GetComponent<Image>().color.b, portrait.GetComponent<Image>().color.g, 1f-portraitProgess);
                portrait.GetComponent<Image>().color = portraitCol;
            }
        }
    }

    void textUpdate()
    {
        if (textLoading)
        {
            if (pauseCount == 0)
            {
                if (textIndex < finalText.Length + 1)
                {
                    //Whether or not to play a voice sample
                    if (sFXCount == 0)
                    {
                        PlayTextSFX();
                        sFXCount++;
                    }
                    else
                        sFXCount = (sFXCount + 1) % maxSFXCount;

                    //Whether or not to include a character name
                    string temp = "";
                    if (DName != "")
                    {
                        temp = DName + ": ";
                    }
                    //Update dialogue UI
                    text.text = temp + finalText.Substring(0, textIndex) + "<color=#0000>" + finalText.Substring(textIndex) + "</color>";

                    //Adds slight pauses after certain characters for spacing
                    if (textIndex - 1 > 0 && (textIndex - 1) < finalText.Length && (finalText[(textIndex - 1)].Equals('.') || finalText[(textIndex - 1)].Equals(',') ||
                        finalText[(textIndex - 1)].Equals('!') || finalText[(textIndex - 1)].Equals('?')))
                    {
                        pauseCount = 8;
                        sFXCount = 0;
                    }
                    
                    textIndex++;
                }
                else
                {
                    finishText();
                }
            }
            else
            {
                pauseCount--;
            }
        }
    }

    void choiceUIUpdate()
    {
        if (choiceUIMoving && !choiceUIWait)
        {
            float distCovered = (Time.time - startTime) * choiceUIMoveSpeed;
            float fracJourney = distCovered / journeyLength;
            if (fracJourney >= 1)
            {
                choicesObj.transform.localPosition = choiceUIMovingEndPos;
                choiceUIMoving = false;
            }
            else
            {
                choicesObj.transform.localPosition = Vector3.Lerp(choiceUIMovingStartPos, choiceUIMovingEndPos, fracJourney);
            }
        }

        if(noChoiceTimer > 0)
        {
            noChoiceTimer -= Time.deltaTime;
        }
    }

    //Toggles on the visibility of all dialogue based UI
    public void ToggleOn()
    {
        textObj.SetActive(true);
        boxObj.SetActive(true);
        choicesObj.SetActive(true);
    }

    //Toggles off the visibility of all dialogue based UI
    public void ToggleOff()
    {
        textObj.SetActive(false);
        boxObj.SetActive(false);
        choicesObj.SetActive(false);
        ResetPortrait();
    }

    void ResetPortrait()
    {
        Destroy(portrait);
        prevDPortrait = "";
        porAnim.DoneTalking(portrait, true);
    }

    //Sets variables to assume that there are two choices and moves choice UI
    public void choices2()
    {
        choiceState = ChoiceState.twoChoice;
        choiceMax = 1;
        moveChoicesUI();
        choicesActive = true;
    }

    //Sets variables to assume that there are three choices and moves choice UI
    public void choices3()
    {
        choiceState = ChoiceState.threeChoice;
        choiceMax = 2;
        moveChoicesUI();
        choicesActive = true;
    }

    //Sets flags and end positions for moving the choice UI above/behind the dialogue UI based on whether or not it should be visible and how many choices there are
    private void moveChoicesUI()
    {
        if (choiceState == ChoiceState.twoChoice)
        {
            choiceUIMoving = true;
            choiceUIWait = true;
            choiceUIMovingStartPos = choicesObj.transform.localPosition;
            choiceUIMovingEndPos = choice2_pos;
            journeyLength = Vector3.Distance(choiceUIMovingStartPos, choiceUIMovingEndPos);
        }

        else if (choiceState == ChoiceState.threeChoice)
        {
            choiceUIMoving = true;
            choiceUIWait = true;
            choiceUIMovingStartPos = choicesObj.transform.localPosition;
            choiceUIMovingEndPos = choice3_pos;
            journeyLength = Vector3.Distance(choiceUIMovingStartPos, choiceUIMovingEndPos);
        }

        else
        {
            ResetChoiceUI();
        }
    }

    //Moves the choice selection up, or back to the bottom if at the top
    public void choiceUp()
    {
        changeChoice(-1);
        moveSFX.Play();
    }

    //Moves the choice selection down, or back to the top if at the bottom;
    public void choiceDown()
    {
        changeChoice(1);
        moveSFX.Play();
    }

    //Returns current choice and resets choice UI. NOT CURRENTLY BEING USED
    public int makeChoice()
    {
        int temp = choice;
        ResetChoiceUI();
        return temp;
    }

    //Changes the choice int based on the direction and the current min/max of choice values
    private void changeChoice(int direction)
    {
        if (direction > 0) {
            if (choice < choiceMax)
            {
                choice++;
            }
            else
            {
                choice = 0;
            }
        }
        else if (direction < 0)
        {
            if (choice > 0)
            {
                choice--;
            }
            else
            {
                choice = choiceMax;
            }
        }
        updateChoiceUI();
    }

    //Updates the colors of the choices and moves the selection bar to the current choice;
    private void updateChoiceUI()
    {
        for (int i = 0; i < choiceList.Length; i++)
        {
            if (choice != i)
            {
                choiceList[i].color = Color.white;
            }
            else
            {
                choiceList[i].color = Color.black;
                x = selection.transform.localPosition.x;
                z = selection.transform.localPosition.z;
                y = choiceList[i].gameObject.transform.localPosition.y;
                selection.transform.localPosition = new Vector3(x, y, z);
            }
        }
    }

    //Call this to update the text shown in the dialogue box. Also sets name and portrait variables
    public void SetText(string newText, string DName, string DPortrait, string DMetadata, string DTheme)
    {
        sFXCount = 0;
        this.DPortrait = DPortrait;
        this.DName = DName;
        this.DMetadata = DMetadata;
        this.DTheme = DTheme;
        //Debug.Log(DName);

        portraitMoving = false;
        portraitProgess = 0;
        pFade = false;
        if(DName != "")
            text.text = DName + ": " + newText;
        else
            text.text = newText;
        text.text = "<color=#0000>" + finalText + "</color>";
        finalText = newText;

        textLoading = true;
        textIndex = 0;
        ResetChoiceUI();
        //textTimer = textTimerMax;
        porAnim.DoneTalking(portrait, false);

        if (DTheme != ""){
            MusicManager.MM.PlayCutsceneSongInit(DTheme, 0.3f);
        }

        //Sets portrait prefab
        if (DPortrait != "")
        {

            //Checks if portrait needs to be changed
            if ((prevDPortrait != "" && prevDPortrait == DPortrait) || DPortrait == "static" || prevDPortrait == "static" || DMetadata == "fade")
            {
                //Debug.Log("No change");
            }
            else
            {
                Destroy(portrait);
                //Debug.Log("Assets/Resources/Portrait_Prefabs/" + DPortrait);
                portrait = Instantiate(Resources.Load("Portrait_Prefabs/" + DPortrait) as GameObject);
                if (portrait != null)
                {
                    portrait.transform.SetParent(portraitPlaceholder.transform, false);
                }
                portraitTransform = portrait.GetComponent<RectTransform>();

                portraitStartPos = new Vector2(portraitTransform.anchoredPosition.x - portraitMoveDistance, portraitTransform.anchoredPosition.y);
                portraitEndPos = portraitTransform.anchoredPosition;
                portraitMoving = true;
            }

            //Sets portrait emotion
            if (DMetadata == "fade")
            {
                pFade = true;
            }
            else if (DMetadata != "")
            {
                porAnim.SetEmotion(portrait, DMetadata);
            }
        }
        else
        {
            ResetPortrait();
        }
        prevDPortrait = DPortrait;
    }

    //Called upon end of text typing/skip
    public void finishText()
    {
        string temp = "";
        if (DName != "")
        {
            temp = DName + ": ";
        }
        text.text = temp + finalText;
        textLoading = false;
        startTime = Time.time;
        choiceUIWait = false;
        porAnim.DoneTalking(portrait, true);
    }

    //Call this to update the choice text. Automatically adds quotes
    public void SetChoices(string[] newChoices)
    {
        if (newChoices != null) {
            int tempMax = newChoices.Length;

            for (int i = 0; i < choiceList.Length; i++)
            {
                if (i < tempMax)
                {
                    choiceList[i].text = newChoices[i];
                }
                else
                {
                    choiceList[i].text = "null";
                }
            }

            if (tempMax == 2)
            {
                choices2();
            }
            else if (tempMax == 3)
            {
                choices3();
            }
            else
            {
                ResetChoiceUI();
            }
        }
        else
        {
            ResetChoiceUI();
        }
    }

    //If there's a choice, prevents user from accidentally blasting through it by mashing the skip button
    public void setNoChoiceTimer()
    {
        if(choiceState != ChoiceState.none)
            noChoiceTimer = 0.2f;
    }

    //Returns true if the noChoiceTimer is currently active
    public bool isNoChoiceTimerRunning()
    {
        if ( noChoiceTimer > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Resets current choice to first one and moves selection gameObject
    private void ResetChoiceUI()
    {
        choice = 0;
        choiceMax = 0;
        choicesActive = false;
        choiceState = ChoiceState.none;
        updateChoiceUI();
        choicesObj.transform.localPosition = choiceInit_pos;
    }

    //This one's pretty obvious, ya dink
    public void playSelectionSFX()
    {
        selectionSFX.Play();
    }

    public void PlayTextSFX()
    {
        //DPortrait being static tells me to keep the portrait, but play regular text sounds.
        if (portrait != null && DPortrait != "static" && DMetadata != "fade")
        {
            portrait.GetComponent<CharacterChatter>().PlayChatter();
        }
        else
        {
            textSFX.Play();
        }
    }
}
