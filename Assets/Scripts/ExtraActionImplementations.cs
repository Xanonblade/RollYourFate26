using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponentInChildren<ParticleSystem>().Play();
            player.GetComponentInChildren<FadeController>().IncrementFlag();
            Debug.Log(player.GetComponentInChildren<ParticleSystem>());
            TimedEventsManager.Instance.QueueTimedEvent(5f, "next_level.2");
        } },
        // Loads the next leve;
        {"next_level.2", ()=>
        {
            LevelManager.NextLevel();
        }
        },

        {"player_grow", () => {
            AnimationManager.TriggerAnimation("player_grow");
        }
        },

        {"rope_fire", () =>
        {
            AnimationManager.TriggerAnimation("rope_fire");
        } },

        {"death.1", () =>
        {
            TimedEventsManager.Instance.QueueTimedEvent(2f, "death.2");
        } },

        {"death.2", () =>
        {
            LevelManager.RestartLevel();
        } },
        {"keypad.1", () => {
            AnimationManager.TriggerAnimation("keypad_unlock");
        } },
        {"open_doors", () => {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("AirlockDoor")) {
                GameObject.Destroy(item);
}        } },
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
