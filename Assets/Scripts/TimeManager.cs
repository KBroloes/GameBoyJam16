using UnityEngine;

public class TimeManager : DrawableUI {

    [Header("Time")]
    public float timeLeft = 999;
    
    void Update()
    {
        //TODO: Nasty hack because interfaces are not supported in the inspector.
        UIValue = Mathf.Round(timeLeft).ToString();
    }

    public void Set(float time)
    {
        timeLeft = time >= 0 ? time : 0;
    }
    
    public float Get()
    {
        return timeLeft;
    }
}
