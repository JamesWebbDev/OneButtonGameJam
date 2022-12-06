using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    public bool _startOnAwake = false;
    public bool _loop = false;
    public float _speed = 1f;
    public float _distance = 5f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private Coroutine _coroutine;

    private void Awake()
    {
        _startPosition = transform.position;
        _endPosition = transform.position + (transform.forward * _distance);

        if (_startOnAwake) StartLerping();
    }

    public void StartLerping()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(MoveForward());
    }

    IEnumerator MoveForward()
    {
        transform.position = _startPosition;

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _endPosition) < 0.1f) break;
            yield return null;
        }

        if (_loop) _coroutine = StartCoroutine(MoveForward());
    }

}
