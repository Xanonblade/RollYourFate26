using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public List<TimedEvent> events = new List<TimedEvent>();
    public Scenario scenario;
    private float clock;

    [SerializeField] private GameObject gameOverPrefab;

    private void Awake()
    {
        Instance = this;
        clock = 0;
    }

    private void Update()
    {
        clock += Time.deltaTime;

        for (int i = 0; i < events.Count;i++)
        {
            // prolly wanna remove if this happens but idk. ill look back later when my brain aint cooked
            if (scenario != events[i].setScenario) { return; }

            if (clock >= events[i].timeToActivate)
            {
                switch (events[i].eventType)
                {
                    case EventType.ANIMATION:
                        AnimationManager.Instance.TriggerAnimation(events[i].actionID);
                        break;
                    case EventType.END_GAME:
                        GameOver();
                        break;
                }

                events.Remove(events[i]);
                i--;
            }
        }
    }

    private void GameOver()
    {
        GameObject.Instantiate(gameOverPrefab).SetActive(true);
    }
}