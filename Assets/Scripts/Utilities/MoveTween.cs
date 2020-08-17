using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MoveTween : MonoBehaviour
{
    public LeanTweenType inType;
    public float duration;
    public UnityEvent onComplete;

    private Vector3 origin;
    private Vector3 previous;

    private void Awake()
    {
        origin = transform.position;
    }

    /// <summary>
    /// Invoke any subscribed onComplete listeners
    /// </summary>
    private void OnCompleteCallback()
    {
        if(onComplete != null)
        {
            onComplete.Invoke();
        }
    }
    
    /// <summary>
    /// Move to target position, saving previous position
    /// </summary>
    /// <param name="target">position to move to</param>
    public void MoveLocally(Vector3 target)
    {
        previous = transform.position;
        LeanTween.moveLocal(gameObject, target, duration).setEase(inType).setOnComplete(OnCompleteCallback);
    }


}
