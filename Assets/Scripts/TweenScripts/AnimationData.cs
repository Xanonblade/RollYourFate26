using DG.Tweening;
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

    private int moveIndex;
    private int rotateIndex;

    [SerializeField] public readonly int IncomingFlag = 0;
    [SerializeField] public readonly int OutgoingFlag = 1;

    public string Name { get { return nameID; } }

    /// <summary>
    /// Adds this animation to the animation manager
    /// </summary>
    private void Start()
    {AnimationManager.Instance.AddAnimation(this);}

    /// <summary>
    /// Starts playing the animation
    /// </summary>
    public void StartAnimation()
    {
        transform.DOMove(Position(), MoveDuration()).onComplete = () =>
        { ContinueMoveAnimation(); };

        transform.DORotate(Rotation(), RotateDuration()).onComplete = () =>
        { ContinueRotateAnimation(); };
    }

    /// <summary>
    /// Starts the next movement of the animation
    /// </summary>
    private void ContinueMoveAnimation()
    {
        moveIndex++;
        if (moveIndex >= targetObjects.Length) return;

        transform.DOMove(Position(), MoveDuration()).onComplete = () =>
        { ContinueMoveAnimation(); };
    }

    /// <summary>
    /// Starts the next rotation of the animation
    /// </summary>
    private void ContinueRotateAnimation()
    {
        rotateIndex++;
        if (rotateIndex >= targetObjects.Length) return;

        transform.DORotate(Rotation(), RotateDuration()).onComplete = () =>
        {ContinueRotateAnimation();};
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
