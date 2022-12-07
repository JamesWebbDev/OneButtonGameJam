using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] List<MorseGoal> _goals;
    private Queue<MorseGoal> _goalsQueue;
    private MorseGoal _currentGoal;

    public UnityEvent OnFailedWord = new UnityEvent();

	private MorsePrinter _printer;

	private string _targetWord;

    new void Awake()
    {
        base.Awake();

        _printer = FindObjectOfType<MorsePrinter>();

        _goalsQueue = new Queue<MorseGoal>();
        foreach (MorseGoal goal in _goals) _goalsQueue.Enqueue(goal);
    }

    private void OnEnable()
    {
        if (_printer != null)
            _printer.OnFinishedWord.AddListener(CheckIfWordMatchesTarget);

        InputManager.Instance.OnStartRestart += RestartScene;
        InputManager.Instance.OnStartQuit += QuitGame;
    }

    private void OnDisable()
    {
        if (_printer != null)
            _printer.OnFinishedWord.RemoveListener(CheckIfWordMatchesTarget);
    }

    public void RestartScene() => SceneManager.LoadScene(0);

    public void QuitGame() => Application.Quit();


    private void Start()
    {
        EnableNextGoal();
    }

    public void EnableNextGoal()
    {
        if (_currentGoal != null)
        {
            
            _currentGoal = null;
        }

        if (_goalsQueue.Count == 0)
        {
            Debug.LogWarning("YOU WIN!!!");
            return;
        }

        _currentGoal = _goalsQueue.Dequeue();

        _currentGoal.EnableGoal();
    }

    public void SetNewTargetWord(string newWord)
    {
        _targetWord = newWord;

        Debug.LogWarning($"The target word is '{_targetWord}'!");
    }

    void CheckIfWordMatchesTarget(string submittedWord)
    {
        AddOutputToUI(submittedWord);

        // If player successfully submitted the correct word... 
        if (submittedWord == _targetWord)
        {
            Debug.Log("Cleared the goal");
            _currentGoal.CompletedGoal();
            EnableNextGoal();
        }
        else
        {
            Debug.Log($"Words don't MATCH {submittedWord} != {_targetWord}!! Try Again!");
            OnFailedWord.Invoke();
        }
    }

	void AddOutputToUI(string outputString)
	{
		
	}


}
