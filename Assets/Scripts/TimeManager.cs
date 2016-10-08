using UnityEngine;

public class TimeManager : DrawableUI {

    [Header("Time")]
    public int totalTime = 60;
    public int timePassRatePerSecond = 1;
    public float timeLeft;
    
    void Start()
    {
        timeLeft = totalTime;
    }

    void Update()
    {
        //TODO: Nasty hack because interfaces are not supported in the inspector.
        UIValue = Mathf.Round(timeLeft).ToString();
    }

    void FixedUpdate()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.fixedDeltaTime * timePassRatePerSecond;
            timeLeft = Mathf.Clamp(timeLeft, 0f, totalTime);
        }
    }
    
    public float Get()
    {
        return timeLeft;
    }
}
