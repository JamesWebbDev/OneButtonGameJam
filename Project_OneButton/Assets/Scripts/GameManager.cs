using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Template Setup by James

public class GameManager : Singleton<GameManager>
{
	[SerializeField] VerticalLayoutGroup _outputGroup;
	[SerializeField] RectTransform _outputPrefab;

	public void AddOutputToUI(string outputString)
	{
		var output = Instantiate(_outputPrefab, _outputGroup.transform);
		var textMesh = output.GetComponentInChildren<TMP_Text>();
		textMesh.text = outputString;
	}
}
