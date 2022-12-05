using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : Singleton<GameManager>
{
	[SerializeField] VerticalLayoutGroup _outputGroup;
	[SerializeField] RectTransform _outputPrefab;
	[SerializeField] List<string> _possibleTargetWords;

	private MorsePrinter _printer;

	private string _targetWord;

    new void Awake()
    {
        _printer = FindObjectOfType<MorsePrinter>();
    }

    private void OnEnable()
    {
        if (_printer != null)
            _printer.OnClearedWord.AddListener(CheckIfWordMatchesTarget);
    }

    private void OnDisable()
    {
        if (_printer != null)
            _printer.OnClearedWord.RemoveListener(CheckIfWordMatchesTarget);
    }



    private void Start()
    {
		GetNewTargetWord();
    }

    void GetNewTargetWord()
    {
		_targetWord = _possibleTargetWords.GetRandom();

        Debug.LogWarning($"The target word is '{_targetWord}'!");
    }

    void CheckIfWordMatchesTarget(string submittedWord)
    {
        AddOutputToUI(submittedWord);

        // If player successfully submitted the correct word... 
        if (_possibleTargetWords.Contains(submittedWord))
        {
            Debug.Log("YOU WIN!!!");
        }
        else
        {
            Debug.Log("Words don't MATCH!! Try Again!");
            GetNewTargetWord();
        }
    }

	void AddOutputToUI(string outputString)
	{
		var output = Instantiate(_outputPrefab, _outputGroup.transform);
		var textMesh = output.GetComponentInChildren<TMP_Text>();
		textMesh.text = outputString;
	}
}
