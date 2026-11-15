using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedEventsManager : MonoBehaviour
{
    public static TimedEventsManager Instance;
    public List<TimedEvent> events = new List<TimedEvent>();
    public Scenario scenario;
    private float clock;

    [SerializeField] private GameObject gameOverPrefab;

    private void Awake()
    {
        Instance = this;
        clock = 0;

        scenario = LevelManager.CurrentScenario;
    }

    private void Update()
    {
        clock += Time.deltaTime;

        for (int i = 0; i < events.Count;i++)
        {
            // prolly wanna remove if this happens but idk. ill look back later when my brain aint cooked
            if (scenario != events[i].setScenario) { continue; }

            if (clock >= events[i].timeToActivate)
            {
                switch (events[i].eventType)
                {
                    case EventType.ANIMATION:
                        AnimationManager.TriggerAnimation(events[i].actionID);
                        break;

                    case EventType.ACTION:
                        ExtraActionImplementations.GetAction(events[i].actionID).Invoke();
                        break;

                    case EventType.END_GAME:
                        GameOver();
                        break;

                    case EventType.ANIMATION_THEN_ANIMATION:
                        string followUp = events[i].actionID2;
                        AnimationManager.Instance.TriggerAnimation(events[i].actionID,
                            () => AnimationManager.TriggerAnimation(followUp));
                        break;

                    case EventType.ANIMATION_THEN_ACTION:
                        followUp = events[i].actionID2;
                        AnimationManager.Instance.TriggerAnimation(events[i].actionID,
                            ExtraActionImplementations.GetAction(followUp));
                        break;
                }

                events.Remove(events[i]);
                i--;
            }
        }
    }

    public void QueueTimedEvent(float duration, string actionID)
    {
        TimedEvent te = ScriptableObject.CreateInstance<TimedEvent>();
        te.setScenario = scenario;
        te.timeToActivate = duration + clock;
        te.eventType = EventType.ACTION;
        te.actionID = actionID;
        events.Add(te);
    }

    public void UnqueueEvent(string actionID) {
        foreach (TimedEvent te in events)
        {
            if (te.actionID == actionID)
            {
                events.Remove(te);
                return;
            }
        }
    }

    private void GameOver()
    {
        // GameObject.Instantiate(gameOverPrefab).SetActive(true);
        //TODO: Maybe open new scene
        SceneManager.LoadScene("MainMenu");
    }
}