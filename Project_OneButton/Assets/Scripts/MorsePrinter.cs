using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MorseInterpreter))]
public class MorsePrinter : MonoBehaviour
{
    private MorseInterpreter _interpreter;

    [Space]
    public UnityEvent<string> OnClearedWord = new UnityEvent<string>();

    private string _currentWord = "";

    private void Awake()
    {
        _interpreter = GetComponent<MorseInterpreter>();
    }

    private void OnEnable()
    {
        _interpreter.OnNewLetter.AddListener(AppendToCurrentWord);
        _interpreter.OnClearWord.AddListener(ClearWord);
    }

    private void OnDisable()
    {
        _interpreter.OnNewLetter.RemoveListener(AppendToCurrentWord);
        _interpreter.OnClearWord.RemoveListener(ClearWord);
    }

    public void AppendToCurrentWord(char character)
    {
        _currentWord += character;
        Debug.Log(_currentWord);
    }

    public void ClearWord()
    {
        OnClearedWord.Invoke(_currentWord);

        _currentWord = "";
    }
}
