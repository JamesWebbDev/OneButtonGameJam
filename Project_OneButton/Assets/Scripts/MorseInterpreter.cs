using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MorseInterpreter : MonoBehaviour
{
    private InputManager _inputManager;

    public UnityEvent<char> OnNewLetter { get; private set; } = new UnityEvent<char>();
    public UnityEvent OnClearWord { get; private set; } = new UnityEvent();

    private float _inputTime = 0;
    private string _currentLetter;

    private Coroutine _nextLetter;
    private Coroutine _nextWord;

    [Header("Input Thresholds")]
    [Tooltip("This is the standard length of a 'unit' used in morse code. Gap = 1u, Dot = 1u, Dash = 3u, Letter = 3u, Word = 7u")]
    [SerializeField] float _inputDuration = 0.25f;
    [Tooltip("Player must release input before this duration!")]
    private float _dotDuration = 0.25f;
    [Tooltip("Player must release input before this duration, MUST be greater than 'Dot Duration'!")]
    private float _dashDuration = 0.75f;
    [Tooltip("Player must not input until this time has passed to start the next LETTER, MUST be greater than 'Dash Duration'!")]
    private float _letterDuration = 0.75f;
    [Tooltip("Player must not input until this time has passed to start the next WORD, MUST be greater than 'Letter Duration'!")]
    private float _wordDuration = 1.75f;


    public Dictionary<string, char> MorseDictionary { get; private set; } = new Dictionary<string, char>()
    {
        { morseA, 'A' }, { morseB, 'B' }, { morseC, 'C' }, { morseD, 'D' }, { morseE, 'E' }, { morseF, 'F' },
        { morseG, 'G' }, { morseH, 'H' }, { morseI, 'I' }, { morseJ, 'J' }, { morseK, 'K' }, { morseL, 'L' },
        { morseM, 'M' }, { morseN, 'N' }, { morseO, 'O' }, { morseP, 'P' }, { morseQ, 'Q' }, { morseR, 'R' },
        { morseS, 'S' }, { morseT, 'T' }, { morseU, 'U' }, { morseV, 'V' }, { morseW, 'W' }, { morseX, 'X' },
        { morseY, 'Y' }, { morseZ, 'Z' }, { morse0, '0' }, { morse1, '1' }, { morse2, '2' }, { morse3, '3' },
        { morse4, '4' }, { morse5, '5' }, { morse6, '6' }, { morse7, '7' }, { morse8, '8' }, { morse9, '9' }
    };

    private void Awake()
    {
        _dotDuration = _inputDuration;
        _dashDuration = _inputDuration * 3;
        _letterDuration = _inputDuration * 3;
        _wordDuration = _inputDuration * 7;
    }

    private void Start()
    {
        _inputManager = InputManager.Instance;

        _inputManager.OnStartTelegraphInput += ReceiveInput;
    }

    void ReceiveInput(bool isPressing)
    {
        if (isPressing) TelegraphDown();
        else TelegraphUp();
    }

    void TelegraphDown()
    {
        if (_nextLetter != null)
        {
            StopCoroutine(_nextLetter);
            _nextLetter = null;
        }

        if (_nextWord != null)
        {
            StopCoroutine(_nextWord);
            _nextWord = null;
        }

        _inputTime = Time.realtimeSinceStartup;
    }

    void TelegraphUp()
    {
        float releaseTime = Time.realtimeSinceStartup - _inputTime;

        if (releaseTime < 0)
        {
            Debug.LogError("Released Key before Pressing key");
            return;
        }

        if (releaseTime < _dotDuration) InputDot();
        else if (releaseTime < _dashDuration) InputDash();
        else FailedInput();

        
    }

    IEnumerator TimeTillNextLetter()
    {
        yield return Helper.GetWait(_letterDuration);

        GetLetterFromMorseCode();

        _nextWord = StartCoroutine(TimeTillNextWord());
    }

    IEnumerator TimeTillNextWord()
    {
        yield return Helper.GetWait(_wordDuration);

        OnClearWord.Invoke();

        _nextWord = null;
    }

    void GetLetterFromMorseCode()
    {
        // check if a key exists for this character
        if (MorseDictionary.TryGetValue(_currentLetter, out char letter))
        {
            OnNewLetter.Invoke(letter);
        }
        else
        {
            Debug.LogWarning("Letter identification failed! Resetting 'current letter'!");
        }

        _currentLetter = "";
    }


    void InputDot()
    {
        Debug.Log($"Input 'Dot'");
        _currentLetter += 0;
        _nextLetter = StartCoroutine(TimeTillNextLetter());
    }

    void InputDash()
    {
        Debug.Log($"Input 'Dash'");
        _currentLetter += 1;
        _nextLetter = StartCoroutine(TimeTillNextLetter());
    }

    void FailedInput()
    {
        Debug.LogWarning($"Failed input at {Time.realtimeSinceStartup}");
    }

    #region Morse Dictionary And ALL Morse Code Keys

    private static readonly string morseA = "01";
    private static readonly string morseB = "1000";
    private static readonly string morseC = "1010";
    private static readonly string morseD = "100";
    private static readonly string morseE = "0";
    private static readonly string morseF = "0010";
    private static readonly string morseG = "110";
    private static readonly string morseH = "0000";
    private static readonly string morseI = "00";
    private static readonly string morseJ = "0111";
    private static readonly string morseK = "101";
    private static readonly string morseL = "0100";
    private static readonly string morseM = "11";
    private static readonly string morseN = "10";
    private static readonly string morseO = "111";
    private static readonly string morseP = "0110";
    private static readonly string morseQ = "1101";
    private static readonly string morseR = "010";
    private static readonly string morseS = "000";
    private static readonly string morseT = "1";
    private static readonly string morseU = "001";
    private static readonly string morseV = "0001";
    private static readonly string morseW = "011";
    private static readonly string morseX = "1001";
    private static readonly string morseY = "1011";
    private static readonly string morseZ = "1100";

    private static readonly string morse1 = "01111";
    private static readonly string morse2 = "00111";
    private static readonly string morse3 = "00011";
    private static readonly string morse4 = "00001";
    private static readonly string morse5 = "00000";
    private static readonly string morse6 = "10000";
    private static readonly string morse7 = "11000";
    private static readonly string morse8 = "11100";
    private static readonly string morse9 = "11110";
    private static readonly string morse0 = "11111";

    #endregion


}
