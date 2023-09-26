using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleBehaviour : MonoBehaviour
{
    public float dayTimer;
    public float nightTimer;

    /*private int _timer;

    private IEnumerator _timerCoroutine;

    public Action OnTimeOut;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer(10, TimeOut);
    }

    private void TimeOut()
    {
        Debug.Log("Your timer is up.");
    }

    public void StartTimer(int timer, Action onTimeOut)
    {
        OnTimeOut = onTimeOut;
        _timer = 0;
        _timerCoroutine = StartTimer(timer);
        StartCoroutine(_timerCoroutine);
    }

    private IEnumerator StatTimer(int totalTime)
    {
        while (_timer < totalTime)
        {
            yield return new WaitForSecondsRealTime(1);
            _timer++;
            Debug.Log("Timer is :" + _timer);
        }

        OnTimeOut?.Invoke();
    }

    public void StopTimer()
    {
        if(_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
            OnTimeOut =
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        // I want to transition from night and day.

        //I want it to track which part is night and which part is day 

        // during the day I want there will not be any enemy to attack the base.

        //The night should spawn enemies to try to attack the house.
    }
}
