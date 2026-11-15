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
    [SerializeField] private GameObject[] targetObjects;
    [SerializeField] private float[] durations;
    [SerializeField] private string nameID;
    [SerializeField] private string postActionID;

    private int moveIndex;
    private bool moveFinished;
    private int rotateIndex;
    private bool rotateFinished;

    private Action onCompleteAction;

    [SerializeField] public readonly int IncomingFlag = 0;
    [SerializeField] public readonly int OutgoingFlag = 1;

    public string Name { get { return nameID; } }

    /// <summary>
    /// Adds this animation to the animation manager
    /// </summary>
    private void Start()
    {
        AnimationManager.Instance.AddAnimation(this);
        
        // If there is an id for a action, add it.
        if (postActionID != null && postActionID.Count() > 0)
        {
            onCompleteAction = ExtraActionImplementations.GetAction(postActionID);
        }
    }

    /// <summary>
    /// Starts playing the animation
    /// </summary>
    public void StartAnimation()
    {
        StartAnimation(null);
    }

    /// <summary>
    /// Starts playing the animation with given function invoked on completion
    /// </summary>
    /// <param name="onComplete">The function to play on complete</param>
    public void StartAnimation(Action onComplete)
    {
        // This couldl be a bug starter
        transform.DOKill();

        if (this.onCompleteAction == null)
        {
            onCompleteAction = onComplete;
        }

        moveIndex = 0;
        rotateIndex = 0;

        moveFinished = false;
        rotateFinished = false;

        transform.DOMove(Position(), MoveDuration())
            .OnComplete(ContinueMoveAnimation);
        transform.DORotate(Rotation(), RotateDuration())
            .OnComplete(ContinueRotateAnimation);
    }

    /// <summary>
    /// Starts the next movement of the animation
    /// </summary>
    private void ContinueMoveAnimation()
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
    private void ContinueRotateAnimation()
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
    private void TryFinish()
    {
        if (!moveFinished || !rotateFinished) return;

        var cb = onCompleteAction;
        onCompleteAction = null;
        cb?.Invoke();
    }

    /// <summary>
    /// Gets the duration for the current movement
    /// </summary>
    /// <returns>The current duration for movement</returns>
    private float MoveDuration() { return durations[moveIndex]; }

    /// <summary>
    /// Gets the duration for the current duration
    /// </summary>
    /// <returns>The current duration for rotation</returns>
    private float RotateDuration() { return durations[rotateIndex]; }

    /// <summary>
    /// Gets the position of the current target object
    /// </summary>
    /// <returns>The position of the current target object</returns>
    private Vector3 Position() { return targetObjects[moveIndex].transform.position; }

    /// <summary>
    /// Gets the euler rotation of the current target object
    /// </summary>
    /// <returns>The euler rotation of the current target object</returns>
    private Vector3 Rotation() { return targetObjects[rotateIndex].transform.rotation.eulerAngles; }
}
