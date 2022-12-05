using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(MorsePrinter))]
public class MorseInterpreter : MonoBehaviour
{
    private InputManager _inputManager;
    private MorsePrinter _printer;

    private float _inputTime = 0;
    private string _currentLetter;

    private Coroutine _nextLetter;

    [Header("Input Thresholds")]
    [Tooltip("Player must release input before this duration!")]
    [SerializeField] float _dotDuration = 0.1f;
    [Tooltip("Player must release input before this duration, MUST be greater than 'Dot Duration'!")]
    [SerializeField] float _dashDuration = 0.4f;

    public Dictionary<int, char> MorseDictionary { get; private set; } = new Dictionary<int, char>()
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
        _inputManager = GetComponent<InputManager>();
        _printer = GetComponent<MorsePrinter>();

        _inputManager.OnStartTelegraphInput += ReceiveInput;
    }

    void ReceiveInput(bool isPressing)
    {
        if (isPressing) TelegraphDown();
        else TelegraphUp();
    }

    void TelegraphDown()
    {
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
        yield return null;
    }

    void InputDot()
    {
        Debug.Log($"Input 'Dot' at {Time.realtimeSinceStartup}");
    }

    void InputDash()
    {
        Debug.Log($"Input 'Dash' at {Time.realtimeSinceStartup}");
    }

    void FailedInput()
    {
        Debug.LogWarning($"Failed input at {Time.realtimeSinceStartup}");
    }

    #region Morse Dictionary And ALL Morse Code Keys

    private static readonly int morseA = Animator.StringToHash("01");
    private static readonly int morseB = Animator.StringToHash("1000");
    private static readonly int morseC = Animator.StringToHash("1010");
    private static readonly int morseD = Animator.StringToHash("100");
    private static readonly int morseE = Animator.StringToHash("0");
    private static readonly int morseF = Animator.StringToHash("0010");
    private static readonly int morseG = Animator.StringToHash("110");
    private static readonly int morseH = Animator.StringToHash("0000");
    private static readonly int morseI = Animator.StringToHash("00");
    private static readonly int morseJ = Animator.StringToHash("0111");
    private static readonly int morseK = Animator.StringToHash("101");
    private static readonly int morseL = Animator.StringToHash("0100");
    private static readonly int morseM = Animator.StringToHash("11");
    private static readonly int morseN = Animator.StringToHash("10");
    private static readonly int morseO = Animator.StringToHash("111");
    private static readonly int morseP = Animator.StringToHash("0110");
    private static readonly int morseQ = Animator.StringToHash("1101");
    private static readonly int morseR = Animator.StringToHash("010");
    private static readonly int morseS = Animator.StringToHash("000");
    private static readonly int morseT = Animator.StringToHash("1");
    private static readonly int morseU = Animator.StringToHash("001");
    private static readonly int morseV = Animator.StringToHash("0001");
    private static readonly int morseW = Animator.StringToHash("011");
    private static readonly int morseX = Animator.StringToHash("1001");
    private static readonly int morseY = Animator.StringToHash("1011");
    private static readonly int morseZ = Animator.StringToHash("1100");

    private static readonly int morse0 = Animator.StringToHash("01111");
    private static readonly int morse1 = Animator.StringToHash("00111");
    private static readonly int morse2 = Animator.StringToHash("00011");
    private static readonly int morse3 = Animator.StringToHash("00001");
    private static readonly int morse4 = Animator.StringToHash("00000");
    private static readonly int morse5 = Animator.StringToHash("10000");
    private static readonly int morse6 = Animator.StringToHash("11000");
    private static readonly int morse7 = Animator.StringToHash("11100");
    private static readonly int morse8 = Animator.StringToHash("11110");
    private static readonly int morse9 = Animator.StringToHash("11111");

    #endregion


}
