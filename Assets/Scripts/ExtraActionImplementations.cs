using System;
using System.Collections.Generic;
using UnityEngine;

public static class ExtraActionImplementations
{
    private static Dictionary<string, Action> actionDictionary = new Dictionary<string, Action>()
    {
        {"test", () => {
            Debug.Log("Action has been Completed!");
        } }
    };

    public static Action GetAction(string key)
    {
        if (actionDictionary.TryGetValue(key, out var action)) return action;
        Debug.LogError($"No extra action registered for '{key}'");
        return () => { };
    }
}
