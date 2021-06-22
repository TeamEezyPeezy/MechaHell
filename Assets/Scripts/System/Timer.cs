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
    private bool runOnStart = true;

    private void Start()
    {
        if (runOnStart)
        {
            Run(startTime);
        }
    }

    public Timer(float t)
    {
        startTime = t;
    }

    public bool IsComplete
    {
        get { return currentTime + startTime >= lastStartTime; }
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
        currentTime = startTime;
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