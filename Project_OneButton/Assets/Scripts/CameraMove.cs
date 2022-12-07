using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform _doorTransform;
    private Transform _setTransform;
    public float _moveTime = 2f;

    public void MovingCamera(Transform t)
    {
        _setTransform = t;

        StartCoroutine(MoveCameraToDoor());
    }

    IEnumerator MoveCameraToTransform(Transform t)
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = t.position;
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = t.rotation;

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

    IEnumerator MoveCameraToDoor()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = _doorTransform.position;
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = _doorTransform.rotation;

        float normalisedTime = 0;

        while (normalisedTime < 1f)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, normalisedTime);
            transform.rotation = Quaternion.Lerp(currentRot, targetRot, normalisedTime);

            normalisedTime += Time.deltaTime / _moveTime;
            yield return null;
        }

        transform.SetPositionAndRotation(targetPos, targetRot);

        yield return Helper.GetWait(2f);

        StartCoroutine(MoveCameraToTransform(_setTransform));
    }
}
