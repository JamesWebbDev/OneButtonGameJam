using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform _setTransform;
    public float _moveTime = 2f;

    public void MovingCamera(Transform t)
    {
        _setTransform = t;

        StartCoroutine(MoveCameraToTransform());
    }

    IEnumerator MoveCameraToTransform()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = _setTransform.position;
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = _setTransform.rotation;

        float normalisedTime = 0;

        while (normalisedTime < 1f)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, normalisedTime);
            transform.rotation = Quaternion.Lerp(currentRot, targetRot, normalisedTime);

            normalisedTime += Time.deltaTime / _moveTime;
            yield return null;
        }

        transform.SetPositionAndRotation(targetPos, targetRot);
    }
}
