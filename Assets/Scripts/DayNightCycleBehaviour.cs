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

    public Action OnDayStart; //replace with unity events to show events in editor
    public Action OnNightStart; // see docs for unity events here https://www.youtube.com/watch?v=ax1DiSutEy8
    //create another event that fires every frame OnDayUpdate
    //this event will require a float intput
    //declare like this: public UnityEvent<float> OnDayUpdate;

    // Start is called before the first frame update
    void Start()
    {
        StartDayNightCycle(); //replace with StartDay(), see notes above your functions to see what I mean
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += Time.deltaTime;

        if(isDaytime && timeOfDay >= dayDuration)
        {
            SwitchToNight();
            Debug.Log("It's night");
            //return;
        }
        else if(!isDaytime && timeOfDay >= nightDuration) //style note: "else" isn't necessary here, just use two if statements
        {
            SwitchToDay();
            Debug.Log("It's day");
            //return;
        }

        //if
        //Call OnDayUpdate(timeofDay/dayDuration or nightDuration based on if its day or night)
        //this will give certain objects the time of day/night we are at expressed as a value between 0 and 1
       
    }

    //start DayNightCycle and SwitchToDay are the same function
    //just use two functions, StartDay and StartNight instead of SwitchToDay and SwitchToNight
    //otherwise this looks good
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
