using System.Collections.Generic;
using UnityEngine;

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
                        Debug.Log("Ani");
                        AnimationManager.Instance.TriggerAnimation(events[i].actionID);
                        break;
                    case EventType.END_GAME:
                        Debug.Log("End Game");
                        GameOver();
                        break;
                    case EventType.ANIMATION_THEN_ANIMATION:
                        Debug.Log("Ani then Ani");
                        string followUp = events[i].actionID2;
                        AnimationManager.Instance.TriggerAnimation(events[i].actionID,
                            () => AnimationManager.Instance.TriggerAnimation(followUp));
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

    private void GameOver()
    {
        GameObject.Instantiate(gameOverPrefab).SetActive(true);
    }
}