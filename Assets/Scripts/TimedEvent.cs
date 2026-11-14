using UnityEngine;

public enum EventType
{
    ANIMATION
}

[CreateAssetMenu(fileName = "TimedEvent")]
public class TimedEvent : ScriptableObject
{
    public Scenario setScenario;
    public float timeToActivate;
    public EventType eventType;
    public string actionID;
}
