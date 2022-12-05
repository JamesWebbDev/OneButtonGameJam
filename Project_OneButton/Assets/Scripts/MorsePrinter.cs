using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MorseInterpreter))]
public class MorsePrinter : MonoBehaviour
{
    private MorseInterpreter _interpreter;

    private string _currentWord = "";

    private void Awake()
    {
        _interpreter = GetComponent<MorseInterpreter>();
    }

    public void AppendToCurrentWord(char character)
    {
        _currentWord += character;
        Debug.Log(_currentWord);
    }

    public void ClearWord()
    {
        _currentWord = "";
        Debug.LogWarning("Cleared Current word from Printer");
    }
}
