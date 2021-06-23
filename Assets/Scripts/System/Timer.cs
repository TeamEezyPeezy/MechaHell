using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float startTime = 10;
    private float currentTime = 0;

    public float CurrentTime
    {
        get { return currentTime; }
    }

    private bool isRunning = false;
    private float lastStartTime;

    public Timer(float t)
    {
        startTime = t;
    }

    public bool IsComplete
    {
        get { return currentTime <= 0; }
    }

    public void Run(float startTime)
    {
        currentTime = startTime;
        isRunning = true;
        lastStartTime = startTime;
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void Reset()
    {
        currentTime = lastStartTime;
        isRunning = false;
    }

    private void Update()
    {
        if (isRunning)
        {
            if (currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
            }
        }
    }
}