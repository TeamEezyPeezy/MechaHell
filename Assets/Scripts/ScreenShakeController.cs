using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController instance;

    private float shakeTimeReamaining, shakePower, shadeFadeTime, shakeRotation;
    public float rotationMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }


    void LateUpdate()
    {
        if(shakeTimeReamaining > 0)
        {
            shakeTimeReamaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shadeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shadeFadeTime * rotationMultiplier * Time.deltaTime);
        }

         transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }
    public void StartShake(float length, float power)
    {
        shakeTimeReamaining = length;
        shakePower = power;

        shadeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;
    }
}
