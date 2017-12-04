using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayParticles : MonoBehaviour
{

    public ParticleSystem particlesA, particlesB, environment, absorbA, absorbB, absorbA2, absorbB2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitParticles(Transform slashedObject, float slope, float delay = 0)
    {
        StartCoroutine(InitParticlesCo(slashedObject, slope, delay));
    }

    IEnumerator InitParticlesCo(Transform slashedObject, float slope, float delay)
    {
        yield return new WaitForSeconds(delay);
        environment.Play();
        transform.position = new Vector3(slashedObject.position.x, slashedObject.position.y, transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan(slope));
        particlesA.Play();
        particlesB.Play();
        yield return new WaitForSeconds(0.25f);
        float time = 0.25f;
        ParticleSystem.EmissionModule emA = particlesA.emission;
        ParticleSystem.EmissionModule emB = particlesB.emission;
        ParticleSystem.MainModule mainA = particlesA.main;
        var startSpeedA = mainA.startSpeed;
        ParticleSystem.MainModule mainB = particlesB.main;
        var startSpeedB = mainB.startSpeed;
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            emA.rateOverTime = Mathf.Lerp(0, 400, t / time);
            emB.rateOverTime = Mathf.Lerp(0, 400, t / time);
            startSpeedA.constantMax = Mathf.Lerp(0, 6, t / time);
            startSpeedA.constantMin = Mathf.Lerp(0, 1, t / time);
            mainA.startSpeed = startSpeedA;
            startSpeedB.constantMax = Mathf.Lerp(0, 6, t / time);
            startSpeedB.constantMin = Mathf.Lerp(0, 1, t / time);
            mainB.startSpeed = startSpeedB;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        particlesA.Stop();
        particlesB.Stop();
    }

    public void InitAbsorbParticles(float delay)
    {
        StartCoroutine(InitAbsorbParticlesCo(delay));
    }

    IEnumerator InitAbsorbParticlesCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        absorbA.Play();
        absorbB.Play();
        absorbA2.Play();
        absorbB2.Play();
        float time = 0.6f;
        ParticleSystem.EmissionModule emA = absorbA.emission;
        ParticleSystem.EmissionModule emB = absorbB.emission;
        ParticleSystem.ShapeModule shapeA = absorbA.shape;
        var radiusA = shapeA.radius;
        ParticleSystem.ShapeModule shapeB = absorbB.shape;
        var radiusB = shapeB.radius;
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            emA.rateOverTime = Mathf.Lerp(500, 100, t / time);
            emB.rateOverTime = Mathf.Lerp(500, 100, t / time);
            radiusA = Mathf.Lerp(5.0f, 1.0f, t / time);
            shapeA.radius = radiusA;
            radiusB = Mathf.Lerp(5.0f, 1.0f, t / time);
            shapeB.radius = radiusB;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        absorbA.Stop();
        absorbB.Stop();
        absorbA2.Stop();
        absorbB2.Stop();
        absorbA2.Clear();
        absorbB2.Clear();
    }
}
