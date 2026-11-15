using UnityEngine;

public enum EventType
{
    ANIMATION,
    END_GAME,
    ANIMATION_THEN_ANIMATION,
    ANIMATION_THEN_ACTION,
    ACTION
}

[CreateAssetMenu(fileName = "TimedEvent")]
public class TimedEvent : ScriptableObject
{
    public Scenario setScenario;
    public float timeToActivate;
    public EventType eventType;
    public string actionID;
    public string actionID2;

    public TimedEvent(Scenario scenario, float timeToActivate, EventType eventType, string actionID)
    {
        this.setScenario = scenario;
        this.timeToActivate = timeToActivate;
        this.eventType = eventType;
        this.actionID = actionID;
    }
}
