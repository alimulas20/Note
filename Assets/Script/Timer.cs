using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Timer : MonoBehaviour
{
    public Slider timer;
    public float gameTime;
    public bool stopTimer;
    float timeCount = 0;
    public Image imageBack;
    public Image imageFront;
    bool started;
    public bool finish;
    void Start()
    {
        // stopTimer = false;
        timer.maxValue = gameTime;
        timer.value = gameTime;
        timeCount = gameTime;

    }
    void Update()
    {
        float time = timeCount - Time.time;
        if (time <= 0)
        {
            stopTimer = true;
            if (started)
            {
                finish = true;
            }

        }
        if (stopTimer == false)
        {
            timer.value = time;
        }
    }
    public void play()
    {
        timeCount = gameTime;
        timeCount += Time.time;
        stopTimer = false;
        started = true;
        finish = false;
    }
    public void resTime()
    {
        timeCount = gameTime + Time.time;
        stopTimer = false;
    }
    public void incTime()
    {
        timeCount += 3;
    }
    public void decTime()
    {
        timeCount -= 3;
    }
    public void fadeOut()
    {
        Color a = imageBack.color;
        a.a = 0;
        imageBack.color = a;
        imageFront.color = a;
    }
    public void fadeIn()
    {
        Color a = imageBack.color;
        a.a = .5f;
        imageBack.color = a;
        a.a = 1;
        imageFront.color = a;
    }
    public void stop()
    {
        started = false;
        stopTimer = true;
    }
}