using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float intensity;
    public float duration;

    private Vector3 _origPosition;

    bool _isShaking = false;

    public void ShakeCamera()
    {
        if (_isShaking) return;

        _isShaking = true;

        _origPosition = transform.position;

        StartCoroutine(ShakingCamera());
    }

    IEnumerator ShakingCamera()
    {
        float normalisedTime = 0;

        while (normalisedTime < 1f)
        {
            Vector3 factoredPosition = new Vector3(Random.Range(-0.1f, 0.1f) * intensity, 
                                                   Random.Range(-0.1f, 0.1f) * intensity, 
                                                   0);
            transform.position = _origPosition + factoredPosition;

            normalisedTime += Time.deltaTime / duration;
            yield return null;
        }

        transform.position = _origPosition;
        _isShaking = false;
    }
}
