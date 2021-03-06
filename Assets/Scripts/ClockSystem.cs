using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockSystem : MonoBehaviour
{
    const float degreesPerHour = 30f,
                degreesPerMinute = 6f,
                degreesPerSecond = 6f;

    private DateTime time;

    private AudioManager AudioManager;

    public Transform hoursTransform, minutesTransform, secondsTransform;

    public Text computerUI_date, computerUI_clock;

    private void Update()
    {
        RotateNeedle();
    }

    /// <summary>
    /// Update clock model needle
    /// </summary>
    private void RotateNeedle()
    {
        hoursTransform.localRotation = Quaternion.Euler(180 + (time.Hour * degreesPerHour), 0, 0);
        minutesTransform.localRotation = Quaternion.Euler(180 + (time.Minute * degreesPerMinute), 0, 0);
        secondsTransform.localRotation = Quaternion.Euler(180 + (time.Second * degreesPerSecond), 0, 0);
    }

    private IEnumerator TickTime()
    {
        time = time.AddMilliseconds(GameConfiguration.millisecondPerRealMillisecond);
        computerUI_clock.text = time.ToString("HH:mm");
        computerUI_date.text = time.ToString("dddd, MMMM dd");

        if (GameConfiguration.DebugMode)
        {
            // Debug.Log((int)(time.Hour) + ":" + (int)(time.Minute) + ":" + (int)(time.Second));
        }

        yield return new WaitForSeconds(0.001f);
        StartCoroutine(TickTime());
    }

    /// <summary>
    /// Set gameplay start date and time
    /// </summary>
    /// <param name="dateTime"></param>
    public void SetStartDateTime(DateTime dateTime)
    {
        time = dateTime;
        if (GameConfiguration.DebugMode) Debug.Log("Start DateTime :" + time);
    }

    /// <summary>
    /// Get the current date and time
    /// </summary>
    /// <returns></returns>
    public DateTime GetCurrentDateTime()
    {
        return time;
    }

    /// <summary>
    /// Start the current clock
    /// </summary>
    public void StartClock()
    {
        AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        AudioManager.PlaySfxClock();
        StartCoroutine(TickTime());
    }
}