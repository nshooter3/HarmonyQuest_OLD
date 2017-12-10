using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GlobalFunctions : MonoBehaviour {

    public static GlobalFunctions instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public RaycastHit2D RaycastX(Transform start, Vector2 dir, float dis, LayerMask mask, float height)
    {
        RaycastHit2D temp;
        Vector2 pos = start.transform.position;
        Vector2 posBelow = new Vector2(start.transform.position.x, start.transform.position.y - height);
        Vector2 posAbove = new Vector2(start.transform.position.x, start.transform.position.y + height);

        temp = Physics2D.Raycast(pos, dir, dis, mask);
        if (temp.collider == null)
        {
            temp = Physics2D.Raycast(posAbove, dir, dis, mask);
            if (temp.collider == null)
            {
                temp = Physics2D.Raycast(posBelow, dir, dis, mask);
            }
        }
        return temp;
    }

    public RaycastHit2D RaycastY(Transform start, Vector2 dir, float dis, LayerMask mask, float width)
    {
        RaycastHit2D temp;
        Vector2 pos = start.transform.position;
        Vector2 posLeft = new Vector2(start.transform.position.x - width, start.transform.position.y);
        Vector2 posRight = new Vector2(start.transform.position.x + width, start.transform.position.y);

        temp = Physics2D.Raycast(pos, dir, dis, mask);
        if (temp.collider == null)
        {
            temp = Physics2D.Raycast(posLeft, dir, dis, mask);
            if (temp.collider == null)
            {
                temp = Physics2D.Raycast(posRight, dir, dis, mask);
            }
        }
        return temp;
    }

    public RaycastHit2D RaycastX(Transform start, Vector2 dir, float dis, float height)
    {
        RaycastHit2D temp;
        Vector2 pos = start.transform.position;
        Vector2 posBelow = new Vector2(start.transform.position.x, start.transform.position.y - height);
        Vector2 posAbove = new Vector2(start.transform.position.x, start.transform.position.y + height);

        temp = Physics2D.Raycast(pos, dir, dis);
        if (temp.collider == null)
        {
            temp = Physics2D.Raycast(posAbove, dir, dis);
            if (temp.collider == null)
            {
                temp = Physics2D.Raycast(posBelow, dir, dis);
            }
        }
        return temp;
    }

    public RaycastHit2D RaycastY(Transform start, Vector2 dir, float dis, float width)
    {
        RaycastHit2D temp;
        Vector2 pos = start.transform.position;
        Vector2 posLeft = new Vector2(start.transform.position.x - width, start.transform.position.y);
        Vector2 posRight = new Vector2(start.transform.position.x + width, start.transform.position.y);

        temp = Physics2D.Raycast(pos, dir, dis);
        if (temp.collider == null)
        {
            temp = Physics2D.Raycast(posLeft, dir, dis);
            if (temp.collider == null)
            {
                temp = Physics2D.Raycast(posRight, dir, dis);
            }
        }
        return temp;
    }

    public void AdjustRotationOverTime(Vector3 startRot, Vector3 endRot, float time, Transform obj)
    {
        StartCoroutine(AdjustRotationOverTimeCo(startRot, endRot, time, obj));
    }

    //Lerps a passed in transform between 2 rotations over time
    IEnumerator AdjustRotationOverTimeCo(Vector3 startRot, Vector3 endRot, float time, Transform obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.transform.eulerAngles = Vector3.Lerp(endRot, startRot, t/time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.transform.eulerAngles = endRot;
    }

    public void AdjustSizeOverTime(Vector3 startSize, Vector3 endSize, float time, Transform obj)
    {
        StartCoroutine(AdjustSizeOverTimeCo(startSize, endSize, time, obj));
    }

    //Lerps a passed in transform between 2 sizes over time
    IEnumerator AdjustSizeOverTimeCo(Vector3 startSize, Vector3 endSize, float time, Transform obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.transform.localScale = Vector3.Lerp(endSize, startSize, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.transform.localScale = endSize;
    }

    public void AdjustPositionOverTime(Vector3 startPos, Vector3 endPos, float time, Transform obj)
    {
        StartCoroutine(AdjustPositionOverTimeCo(startPos, endPos, time, obj));
    }

    //Lerps a passed in transform between 2 positions over time
    IEnumerator AdjustPositionOverTimeCo(Vector3 startPos, Vector3 endPos, float time, Transform obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.transform.position = Vector3.Lerp(endPos, startPos, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.transform.position = endPos;
    }

    public void AdjustPositionOverTimeTextMesh(Vector3 startPos, Vector3 endPos, float time, Text obj)
    {
        StartCoroutine(AdjustPositionOverTimeTextCo(startPos, endPos, time, obj));
    }

    //Lerps a passed in transform between 2 positions over time
    IEnumerator AdjustPositionOverTimeTextCo(Vector3 startPos, Vector3 endPos, float time, Text obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.rectTransform.anchoredPosition = Vector3.Lerp(endPos, startPos, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.transform.position = endPos;
    }

    public void AdjustPositionOverTimeSmoothText(Vector3 startPos, Vector3 endPos, float time, Text obj)
    {
        StartCoroutine(AdjustPositionOverTimeSmoothTextCo(startPos, endPos, time, obj));
    }

    //Lerps a passed in transform between 2 positions over time smoothly
    IEnumerator AdjustPositionOverTimeSmoothTextCo(Vector3 startPos, Vector3 endPos, float time, Text obj)
    {
        float t = 0;
        while (t <= 1.0)
        {
            t += Time.deltaTime / time;
            obj.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return new WaitForSeconds(0);
        }
    }

    public void AdjustScaleOverTimeSmoothText(Vector3 startScale, Vector3 endScale, float time, Text obj)
    {
        StartCoroutine(AdjustScaleOverTimeSmoothTextCo(startScale, endScale, time, obj));
    }

    //Lerps a passed in transform between 2 positions over time smoothly
    IEnumerator AdjustScaleOverTimeSmoothTextCo(Vector3 startScale, Vector3 endScale, float time, Text obj)
    {
        float t = 0;
        while (t <= 1.0)
        {
            t += Time.deltaTime / time;
            obj.rectTransform.localScale = Vector3.Lerp(startScale, endScale, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return new WaitForSeconds(0);
        }
    }

    public void AdjustRotOverTimeSmoothText(Vector3 startRot, Vector3 endRot, float time, Text obj)
    {
        StartCoroutine(AdjustRotOverTimeSmoothTextCo(startRot, endRot, time, obj));
    }

    //Lerps a passed in transform between 2 positions over time smoothly
    IEnumerator AdjustRotOverTimeSmoothTextCo(Vector3 startRot, Vector3 endRot, float time, Text obj)
    {
        float t = 0;
        while (t <= 1.0)
        {
            t += Time.deltaTime / time;
            obj.rectTransform.eulerAngles = Vector3.Lerp(startRot, endRot, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return new WaitForSeconds(0);
        }
    }

    public void AdjustPositionOverTimeSmooth(Vector3 startPos, Vector3 endPos, float time, Transform obj)
    {
        StartCoroutine(AdjustPositionOverTimeSmoothCo(startPos, endPos, time, obj));
    }

    //Lerps a passed in transform between 2 positions over time smoothly
    IEnumerator AdjustPositionOverTimeSmoothCo(Vector3 startPos, Vector3 endPos, float time, Transform obj)
    {
        float t = 0;
        while (t <= 1.0)
        {
            t += Time.deltaTime / time;
            obj.transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return new WaitForSeconds(0);
        }
    }

    public void AdjustColorOverTime(Color startCol, Color endCol, float time, SpriteRenderer obj)
    {
        StartCoroutine(AdjustColorOverTimeCo(startCol, endCol, time, obj));
    }

    //Lerps a passed in spriteRenderer between 2 colors over time
    IEnumerator AdjustColorOverTimeCo(Color startCol, Color endCol, float time, SpriteRenderer obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.color = Color.Lerp(endCol, startCol, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.color = endCol;
    }

    public void AdjustColorOverTimeMaterial(Color startCol, Color endCol, float time, Material obj)
    {
        StartCoroutine(AdjustColorOverTimeMaterialCo(startCol, endCol, time, obj));
    }

    //Lerps a passed in spriteRenderer between 2 colors over time
    IEnumerator AdjustColorOverTimeMaterialCo(Color startCol, Color endCol, float time, Material obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.SetColor("_TintColor", Color.Lerp(endCol, startCol, t / time));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.SetColor("_TintColor", new Color(1, 1, 1, 0));
        obj.color = endCol;
    }

    public void AdjustMultColorOverTime(Color col, float strength, float time, Material obj)
    {
        StartCoroutine(AdjustMultColorOverTimeCo(col, strength, time, obj));
    }

    //Lerps a passed in spriteRenderer between 2 colors over time
    IEnumerator AdjustMultColorOverTimeCo(Color col, float strength, float time, Material obj)
    {
        obj.SetColor("_MultColor", col);
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.SetFloat("_MultStrength", Mathf.Lerp(0, strength, t / time));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.SetFloat("_MultStrength", 0);
    }

    public void AdjustColorOverTimeTextMesh(Color startCol, Color endCol, float time, TextMesh obj)
    {
        StartCoroutine(AdjustColorOverTimeTextMeshCo(startCol, endCol, time, obj));
    }

    //Lerps a passed in textMesh between 2 colors over time
    IEnumerator AdjustColorOverTimeTextMeshCo(Color startCol, Color endCol, float time, TextMesh obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.color = Color.Lerp(endCol, startCol, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.color = endCol;
    }

    public void FadeInText(Color col, float time, Text obj)
    {
        StartCoroutine(FadeInTextCo(col, time, obj));
    }

    //Lerps a passed in textMesh between 2 colors over time
    IEnumerator FadeInTextCo(Color col, float time, Text obj)
    {
        Color startCol = new Color(col.r, col.g, col.b, 0);
        Color endCol = new Color(col.r, col.g, col.b, 1);
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.color = Color.Lerp(endCol, startCol, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.color = endCol;
    }

    public void FadeOutText(Color col, float time, Text obj)
    {
        StartCoroutine(FadeOutTextCo(col, time, obj));
    }

    //Lerps a passed in textMesh between 2 colors over time
    IEnumerator FadeOutTextCo(Color col, float time, Text obj)
    {
        Color startCol = new Color(col.r, col.g, col.b, 1);
        Color endCol = new Color(col.r, col.g, col.b, 0);
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.color = Color.Lerp(endCol, startCol, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.color = endCol;
    }

    public void DBZTeleport(SpriteRenderer sr)
    {
        StartCoroutine(DBZTeleportCo(sr));
    }

    //Fixed DBZ teleport effect, for cutscenes 
    IEnumerator DBZTeleportCo(SpriteRenderer sr)
    {
        int count = 0, maxCount = 30;
        float offset = 0.1f;
        Vector3 initPos = sr.transform.position;
        Color initCol = new Color(sr.color.r, sr.color.g, sr.color.b, 0.75f);
        Color finalCol = new Color(initCol.r,initCol.g, initCol.b, 0);
        while (count < maxCount)
        {
            yield return new WaitForSeconds(0.01f);
            sr.transform.position = new Vector3(initPos.x + offset*count/10f, initPos.y, initPos.z);
            sr.color = Color.Lerp(initCol, finalCol, count/((maxCount-1)*1f));
            offset *= -1;
            count++;
        }
        sr.transform.position = initPos;
        sr.color = finalCol;
    }

    //Used to round to pixel based on unity units, possibly useful for camera
    public float RoundToNearestPixel(float unityUnits, float pixelsPerUnit)
    {
        float valueInPixels = unityUnits * pixelsPerUnit;
        valueInPixels = Mathf.Round(valueInPixels);
        float roundedUnityUnits = valueInPixels * (1 / pixelsPerUnit);
        return roundedUnityUnits;
    }

    //Converts time in seconds to SS:MS format
    public string FormatTimeSeconds(float time)
    {
        int intTime = (int)time;
        int seconds = intTime;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        int fraction2 = (int)fraction / 10;
        string timeText = "";
        timeText = String.Format("{0:00}:{1:00}", seconds, fraction2);
        return timeText;
    }

    //Converts time in seconds to MM:SS:MS format
    public string FormatTimeMinutes(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        int fraction2 = (int)fraction / 10;
        string timeText = String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction2);
        return timeText;
    }

    //Resets a target transform to match a source transform
    public void CopyTranform(Transform target, Transform source)
    {
        target.position = source.position;
        target.eulerAngles = source.eulerAngles;
        target.localScale = source.localScale;
    }

    //Resets a target transform to default values
    public void ResetTranform(Transform target)
    {
        target.position = Vector3.zero;
        target.eulerAngles = Vector3.zero;
        target.localScale = Vector3.one;
    }

    //Converts a sprite to a Texture2D
    public Texture2D SpriteToTexture2D(Sprite sprite)
    {
        Texture2D tex = new Texture2D((int)(sprite.rect.width), (int) (sprite.rect.height));
        Color[] pixels = sprite.texture.GetPixels(  (int)sprite.rect.x,
                                                    (int)sprite.rect.y,
                                                    (int)sprite.rect.width,
                                                    (int)sprite.rect.height);
        tex.SetPixels(pixels);
        return tex;
    }

    //Converts a sprite to another sprite, pixel by pixel
    public Sprite DuplicateSprite(Sprite sprite)
    {
        Texture2D tex = new Texture2D((int)(sprite.rect.width), (int)(sprite.rect.height));
        Color[] pixels = sprite.texture.GetPixels(  (int)sprite.rect.x,
                                                    (int)sprite.rect.y,
                                                    (int)sprite.rect.width,
                                                    (int)sprite.rect.height);
        tex.SetPixels(pixels);
        tex.Apply();
        Sprite sprite2 = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit);
        return sprite2;
    }

    //Scale Sprite to match screen size
    public void ResizeSpriteToScreen(SpriteRenderer sr)
    {
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        sr.transform.localScale =  new Vector3(worldScreenWidth / width, worldScreenHeight / height, sr.transform.localScale.z);
    }

    //Wrapper for calling a function after a specific delay
    public void DelayedFunction(Action action, float delay)
    {
        StartCoroutine(DelayedFunctionCo(action, delay));
    }

    IEnumerator DelayedFunctionCo(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    //Return color with maxed alpha
    public Color ColorMaxAlpha(Color col)
    {
        return new Color(col.r, col.g, col.b, 1);
    }

    //Return color with min alpha
    public Color ColorMinAlpha(Color col)
    {
        return new Color(col.r, col.g, col.b, 0);
    }

    //Return color with passed in alpha
    public Color ColorChangeAlpha(Color col, float a)
    {
        return new Color(col.r, col.g, col.b, a);
    }

    public void RepeatingFunction(Action action, float delay, float duration)
    {
        StartCoroutine(RepeatingFunctionCo(action, delay, duration));
    }

    //Fires a function repeatedly at interval delay, until timer duration runs out
    IEnumerator RepeatingFunctionCo(Action action, float delay, float duration)
    {
        float d = delay;
        for (float i = duration; i > 0; i -= Time.deltaTime)
        {
            d -= Time.deltaTime;
            if (d <= 0)
            {
                d = delay;
                action();
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
