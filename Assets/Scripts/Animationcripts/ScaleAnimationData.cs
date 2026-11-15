using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public class ScaleAnimationData : AnimationData
{
    private int scaleIndex;
    private bool scaleFinished;

    /// <summary>
    /// Starts playing the animation with given function invoked on completion
    /// </summary>
    /// <param name="onComplete">The function to play on complete</param>
    override
    public void StartAnimation(Action onComplete, Action parallelAction)
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
        }
        else if (parallelAction != null)
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
            scaleFinished = true;

            TryFinish();
            return;
        }

        transform.DOMove(Position(), MoveDuration())
        .OnComplete(ContinueMoveAnimation);
        transform.DORotate(Rotation(), RotateDuration())
        .OnComplete(ContinueRotateAnimation);
        transform.DOScale(LossyScale(), ScaleDuration())
            .OnComplete(ContinueScaleAnimation);
    }

    protected void ContinueScaleAnimation()
    {
        scaleIndex++;
        if (scaleIndex >= targetObjects.Length)
        {
            scaleFinished = true;
            TryFinish();
            return;
        }
        transform.DOScale(LocalScale(), ScaleDuration()).OnComplete(ContinueScaleAnimation);
    }

    override
    protected void TryFinish()
    {
        if (!moveFinished || !rotateFinished || !scaleFinished) return;

        Debug.Log($"Try Finishing {nameID} Animation");

        Action cb = onCompleteAction;
        onCompleteAction = null;
        cb?.Invoke();
    }

    protected float ScaleDuration() { return durations[scaleIndex]; }

    protected Vector3 LossyScale() { return targetObjects[moveIndex].transform.lossyScale; }

    protected Vector3 LocalScale() { return targetObjects[moveIndex].transform.localScale; }
}
