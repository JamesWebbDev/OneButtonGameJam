using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MorseGoal : MonoBehaviour
{
    [SerializeField] string _goalWord;

    [Space]
    public UnityEvent OnGoalEnabled = new UnityEvent();
    [Space]
    public UnityEvent OnGoalReached = new UnityEvent();

    public void EnableGoal()
    {
        OnGoalEnabled.Invoke();
        GameManager.Instance.SetNewTargetWord(_goalWord);
    }

    public void CompletedGoal()
    {
        OnGoalReached.Invoke();
    }
}
