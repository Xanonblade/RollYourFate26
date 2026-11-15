using System;
using UnityEngine;

public class QueuedTimeEvent
{
    private float duration;
    private float timeStarted;
    private Action action;

    public QueuedTimeEvent(float duration, Action action)
    {

    }
}
