using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightCycleBehaviour : MonoBehaviour
{
    public float dayDuration;
    public float nightDuration;

    private float timeOfDay = 0.0f;
    private bool isDaytime = true;

    public Action OnDayStart;
    public Action OnNightStart;

    // Start is called before the first frame update
    void Start()
    {
        StartDayNightCycle();
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += Time.deltaTime;

        if(isDaytime && timeOfDay >= dayDuration)
        {
            SwitchToNight();
            Debug.Log("It's night");
        }
        else if(!isDaytime && timeOfDay >= nightDuration)
        {
            SwitchToDay();
            Debug.Log("It's day");
        }
    }

    private void StartDayNightCycle()
    {
        timeOfDay = 0.0f;
        isDaytime = true;
        OnDayStart?.Invoke();
    }

    private void SwitchToNight()
    {
        timeOfDay = 0.0f;
        isDaytime = false;
        OnNightStart?.Invoke();
    }

    private void SwitchToDay()
    {
        timeOfDay = 0.0f;
        isDaytime = true;
        OnDayStart?.Invoke();
    }
}
