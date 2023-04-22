using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    private int slowmoTimer = 0;

    [Tooltip ("Valeurs min pour relancer le slowmo")]
    [Min(1)]
    [SerializeField] private float minKillForSlowMotion;

    [SerializeField] private float slowMotionBaseDuration;
    [SerializeField] private float slowMotionGain;


    [Space]
    [Header ("FLOW TIME CONTROL")]
    [SerializeField] private float normalFlowTime = 1;
    [SerializeField] private float slowMotionFlowTime = 0.1f;
    [SerializeField] private float FlowTimeLerpValue = 0.2f;

    private float targetFlowTime = 1;
    private float baseFixedDeltaTime;
    public TimeNonAffectedTimer slowMotionTimer;

    public bool isActivedOnce = false;

    public TimeNonAffectedTimer SlowMotionTimer { get => slowMotionTimer; set => slowMotionTimer = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        instance = this;

        targetFlowTime = 1;
        Time.fixedDeltaTime = 0.01f;
        baseFixedDeltaTime = Time.fixedDeltaTime;
        slowMotionTimer = new TimeNonAffectedTimer(slowMotionBaseDuration, EndSlowMotion);
    }

    void SetTime(float secondDuration)
    {
        targetFlowTime = secondDuration;
    }

    void FixedUpdate()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetFlowTime, FlowTimeLerpValue);
        Time.fixedDeltaTime = baseFixedDeltaTime * Time.timeScale;
    }

    public void LaunchSlowMotion()
    {
        slowMotionTimer = new TimeNonAffectedTimer(slowmoTimer, EndSlowMotion);

        isActivedOnce = true;
        slowMotionTimer.ResetPlay();
    }

    public void StartSlowMotion()
    {
        SetTime(slowMotionFlowTime);

        slowMotionTimer = new TimeNonAffectedTimer(slowMotionBaseDuration, EndSlowMotion);

        isActivedOnce = true;
        slowMotionTimer.ResetPlay();
    }

    public void MaintainSlowMotion()
    {
        if(slowMotionTimer.IsStarted() == true)
        {
            slowMotionTimer.Pause();
            slowMotionTimer = new TimeNonAffectedTimer((slowMotionTimer.endTime - slowMotionTimer.Time) + slowMotionGain, EndSlowMotion);
            slowMotionTimer.ResetPlay();
        }
    }


    public void EndSlowMotion()
    {
        isActivedOnce = false;
        SetTime(normalFlowTime);
    }

    public void increaseSlowMoTime()
    {
        slowmoTimer += 1;
    }

    public bool IsSlowMotion()
    {
        return slowMotionTimer.IsStarted();
    }
}
