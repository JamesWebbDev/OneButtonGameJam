using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MorseSimulator : MonoBehaviour
{
    private MorseInterpreter _interpreter;

    [Header("Timed Values")]
    [SerializeField] float _unitValue = 0.25f;
    private float _nautDuration;
    private float _dotDuration;
    private float _dashDuration;
    private float _letterDuration;
    private float _resetDuration;

    [Header("Target Word")]
    [SerializeField] string _targetWord = "EXAMPLE";
    [Tooltip("0 = Dot, 1 = Dash, 2 = NewLetter")]
    private int[] _morseTarget;
    private bool _onState = false;
    private Dictionary<char, string> _reversedMorseDictionary;

    [Space]
    public UnityEvent OnState = new UnityEvent();
    [Space]
    public UnityEvent OffState = new UnityEvent();

    private Queue<IEnumerator> _targetWordQueue;

    private void Awake()
    {
        _interpreter = MorseInterpreter.Instance;

        _reversedMorseDictionary = _interpreter.MorseDictionary.Reverse();
        SetMorseOrder();

        _nautDuration = _unitValue;
        _dotDuration = _unitValue;
        _dashDuration = _unitValue * 3;
        _letterDuration = _unitValue * 3;
        _resetDuration = _unitValue * 7;
    }

    private void Start()
    {
        _interpreter = MorseInterpreter.Instance;

        _reversedMorseDictionary = _interpreter.MorseDictionary.Reverse();
        SetMorseOrder();

        SetTargetWordQueue();
    }

    public void SetTargetWordQueue()
    {
        _targetWordQueue = new Queue<IEnumerator>();

        for (int i = 0; i < _morseTarget.Length; i++)
        {
            _targetWordQueue.Enqueue(GetCorrectTimer(_morseTarget[i]));
        }

        StartCoroutine(TimeNaut());

    }

    void SetMorseOrder()
    {
        var charArray = new char[_targetWord.Length];
        
        // array = "T", "E", "S", "T"
        for (int i = 0; i < _targetWord.Length; i++)
        {
            charArray[i] = _targetWord[i];
        }

        List<int> morseInputList = new List<int>();

        // Try get Values from "T" "E" "S" and "T"
        for (int i = 0; i < charArray.Length; i++)
        {
            char letter = charArray[i];

            // IF this character has a morse version...
            if (_reversedMorseDictionary.TryGetValue(letter, out var value))
            {
                int[] morseInput = GetIntegersFromString(value);
                for (int j = 0; j < morseInput.Length; j++)
                {
                    morseInputList.Add(morseInput[j]);
                }

                // Add this on the end to signify a new Letter
                morseInputList.Add(2);
            }
            else
            {
                Debug.LogError($"Couldn't find {letter} in Dictionary", this);
            }
        }
        
        _morseTarget = morseInputList.ToArray();
    }

    int[] GetIntegersFromString(string input)
    {
        List<int> morseList = new List<int>();

        for (int i = 0; i < input.Length; i++)
        {
            char letter = input[i];

            if (Char.IsDigit(letter)) morseList.Add((int)Char.GetNumericValue(letter));
        }

        return morseList.ToArray();
    }

    

    IEnumerator GetCorrectTimer(int index)
    {
        switch (index)
        {
            case 0: return TimeDot();
            case 1: return TimeDash();
            case 2: return TimeLetter();
        }

        return null;
    }

    

    IEnumerator TimeDot()
    {
        _onState = true;
        OnState.Invoke();

        yield return Helper.GetWait(_dotDuration);

        StartCoroutine(TimeNaut());
    }

    IEnumerator TimeDash()
    {
        _onState = true;
        OnState.Invoke();

        yield return Helper.GetWait(_dashDuration);

        StartCoroutine(TimeNaut());
    }

    IEnumerator TimeLetter()
    {
        _onState = false;
        OffState.Invoke();

        yield return Helper.GetWait(_letterDuration);

        if (_targetWordQueue.Count == 0)
        {
            StartCoroutine(TimeReset());
            yield break;
        }

        var nextCoroutine = _targetWordQueue.Dequeue();

        StartCoroutine(nextCoroutine);
    }

    IEnumerator TimeNaut()
    {
        _onState = false;
        OffState.Invoke();

        yield return Helper.GetWait(_nautDuration);

        if (_targetWordQueue.Count == 0)
        {
            StartCoroutine(TimeReset());
            yield break;
        }

        var nextCoroutine = _targetWordQueue.Dequeue();

        StartCoroutine(nextCoroutine);
    }

    

    IEnumerator TimeReset()
    {
        yield return Helper.GetWait(_resetDuration);

        SetTargetWordQueue();
    }

}
