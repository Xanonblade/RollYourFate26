using System;
using System.Collections.Generic;
using UnityEngine;

public static class ExtraActionImplementations
{
    private static Dictionary<string, Action> actionDictionary = new Dictionary<string, Action>()
    {
        // Test Action
        {"test", () => {
            Debug.Log("Action has been Completed!");
        } },
        
        // Fired when a level is complete, and sets a timer to actually load the next level.
        {"next_level.1", () =>
        {
            TimedEventsManager.Instance.QueueTimedEvent(5f, "next_level.2");
        } },
        // Loads the next leve;
        {"next_level.2", ()=>
        {
            Debug.Log("Here the level ends");
        }
        },

        {"player_grow", () => {
            AnimationManager.TriggerAnimation("player_grow");
        }
        },

        {"rope_fire", () =>
        {
            AnimationManager.TriggerAnimation("rope_fire");
        } }
    };

    public static Action GetAction(string key)
    {
        if (actionDictionary.TryGetValue(key, out var action)) return action;
        if (AnimationManager.Instance.HasAnimation(key))
        {
            return () => { AnimationManager.TriggerAnimation(key); };
        }
        Debug.LogError($"No extra action registered for '{key}'");
        return () => { };
    }
}
