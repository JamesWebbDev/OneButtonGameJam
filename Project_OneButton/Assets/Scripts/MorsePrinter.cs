using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


[RequireComponent(typeof(MorseInterpreter))]
public class MorsePrinter : MonoBehaviour
{
    private MorseInterpreter _interpreter;

    [SerializeField] TMP_Text _currentText;
    private Coroutine _textFadeCo;
    private float _fadeTime;

    [Space]
    public UnityEvent<char> OnEnteredLetter = new UnityEvent<char>();
    [Space]
    public UnityEvent<string> OnFinishedWord = new UnityEvent<string>();

    private string _currentWord = "";

    private void Awake()
    {
        _interpreter = GetComponent<MorseInterpreter>();
        _fadeTime = _interpreter._wordDuration;
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
        OnEnteredLetter.Invoke(character);
        _currentText.text = _currentWord;
        DisplayText();
        
        Debug.Log(_currentWord);
    }

    public void ClearWord()
    {
        OnFinishedWord.Invoke(_currentWord);

        _currentWord = "";
        _currentText.text = _currentWord;
    }

    public void DisplayText()
    {
        if (_textFadeCo != null)
        {
            StopCoroutine(_textFadeCo);
            _currentText.alpha = 1;
            _currentText.color = Color.white;
        }

        _textFadeCo = StartCoroutine(ReduceAlphaTo0());
    }

    IEnumerator ReduceAlphaTo0()
    {
        float normalisedTime = 0;

        while (normalisedTime < 1f)
        {
            float factor = 1 - normalisedTime;

            
            _currentText.color = new Color(1, factor, factor);
            _currentText.alpha = factor;

            normalisedTime += Time.deltaTime / _fadeTime;
            yield return null;
        }

        _currentText.alpha = 0;
    }
}
