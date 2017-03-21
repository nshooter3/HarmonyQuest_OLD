using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCam : MonoBehaviour {

    public static BattleCam instance;

    float shakeTimer, maxShakeTimer = 0.1f;

    Vector3 startPos;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start() {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {

    }

    public void CamShake(float mag) {
        StartCoroutine(Shake(mag));
    }

    public void CamShake()
    {
        CamShake(Random.Range(0.3f, 0.4f));
    }

    IEnumerator Shake(float mag)
    {
        Vector3 tempDir = new Vector3(Random.Range(-1f, 1f) * mag, Random.Range(-1f, 1f) * mag, startPos.z);
        shakeTimer = maxShakeTimer* Random.Range(1f, 1.5f);
        for (var f = maxShakeTimer; f >= 0; f -= Time.deltaTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(startPos, tempDir, f / maxShakeTimer);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameObject.transform.localPosition = startPos;
        yield return null;
    }
}
