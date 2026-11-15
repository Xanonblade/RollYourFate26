using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// A animation of an object that moves it through multiple states 
/// </summary>
public class AnimationData : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] protected GameObject[] targetObjects;
    [SerializeField] protected float[] durations;
    [SerializeField] protected string nameID;

    [Header("Actions")]
    [SerializeField] protected string postActionID;
    [SerializeField] protected string parallelActionID;

    protected int moveIndex;
    protected bool moveFinished;
    protected int rotateIndex;
    protected bool rotateFinished;

    protected Action parallelAction;
    protected Action onCompleteAction;

    [SerializeField] public readonly int IncomingFlag = 0;
    [SerializeField] public readonly int OutgoingFlag = 1;

    public string Name { get { return nameID; } }

    /// <summary>
    /// Adds this animation to the animation manager
    /// </summary>
    protected void Start()
    {
        AnimationManager.Instance.AddAnimation(this);
        
        // If there is an id for a action, add it.
        if (postActionID != null && postActionID.Count() > 0)
        {
            onCompleteAction = ExtraActionImplementations.GetAction(postActionID);
        }
        if (parallelActionID != null && parallelActionID.Count() > 0)
        {
            parallelAction = ExtraActionImplementations.GetAction(parallelActionID);
        }
    }

    /// <summary>
    /// Starts playing the animation
    /// </summary>
    public void StartAnimation()
    {
        StartAnimation(onCompleteAction, parallelAction);
    }

    /// <summary>
    /// Starts playing the animation with given function invoked on completion
    /// </summary>
    /// <param name="onComplete">The function to play on complete</param>
    public virtual void StartAnimation(Action onComplete, Action parallelAction)
    {
        Debug.Log($"Animation {nameID} has Started");
        // This couldl be a bug starter
        transform.DOKill();

        if (this.onCompleteAction == null)
        {
            Debug.Log($"Adding on Complete {postActionID}");
            onCompleteAction = onComplete;
        }

        if (parallelActionID != null && parallelActionID.Count() > 0)
        {
            Debug.Log($"Invoking Parallel Action {parallelActionID}");
            ExtraActionImplementations.GetAction(parallelActionID).Invoke();
        }
        else if (this.parallelAction != null)
        {
            Debug.Log($"Invoking Parallel Action {parallelActionID}");
            this.parallelAction.Invoke();
        } else if (parallelAction != null)
        {
            Debug.Log($"Invoking Parallel Action {parallelActionID}");
            parallelAction.Invoke();
        }

        moveIndex = 0;
        rotateIndex = 0;

        moveFinished = false;
        rotateFinished = false;

        if (targetObjects.Length == 0)
        {
            moveFinished = true;
            rotateFinished = true;
            
            TryFinish();
            return;
        }

        transform.DOMove(Position(), MoveDuration())
        .OnComplete(ContinueMoveAnimation);
        transform.DORotate(Rotation(), RotateDuration())
        .OnComplete(ContinueRotateAnimation);
    }

    /// <summary>
    /// Starts the next movement of the animation
    /// </summary>
    protected void ContinueMoveAnimation()
    {
        moveIndex++;
        if (moveIndex >= targetObjects.Length)
        {
            moveFinished = true;
            TryFinish();
            return;
        }
        transform.DOMove(Position(), MoveDuration()).OnComplete(ContinueMoveAnimation);
    }

    /// <summary>
    /// Starts the next rotation of the animation
    /// </summary>
    protected void ContinueRotateAnimation()
    {
        rotateIndex++;
        if (rotateIndex >= targetObjects.Length)
        {
            rotateFinished = true;
            TryFinish();
            return;
        }
        transform.DORotate(Rotation(), RotateDuration()).OnComplete(ContinueRotateAnimation);
    }

    /// <summary>
    /// Trys to complete the onComplete action
    /// </summary>
    protected virtual void TryFinish()
    {
        if (!moveFinished || !rotateFinished) return;

        Debug.Log($"Try Finishing {nameID} Animation");

        Action cb = onCompleteAction;
        onCompleteAction = null;
        cb?.Invoke();
    }

    /// <summary>
    /// Gets the duration for the current movement
    /// </summary>
    /// <returns>The current duration for movement</returns>
    protected float MoveDuration() { return durations[moveIndex]; }

    /// <summary>
    /// Gets the duration for the current duration
    /// </summary>
    /// <returns>The current duration for rotation</returns>
    protected float RotateDuration() { return durations[rotateIndex]; }

    /// <summary>
    /// Gets the position of the current target object
    /// </summary>
    /// <returns>The position of the current target object</returns>
    protected Vector3 Position() { return targetObjects[moveIndex].transform.position; }

    /// <summary>
    /// Gets the euler rotation of the current target object
    /// </summary>
    /// <returns>The euler rotation of the current target object</returns>
    protected Vector3 Rotation() { return targetObjects[rotateIndex].transform.rotation.eulerAngles; }
}
