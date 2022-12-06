using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed;
    [SerializeField] Transform target;

    private bool isMoving = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LightsOn();
    }

    public void SetMoving(bool result)
    {
        isMoving = result;
    }
    public void LightsOn()
    {
        if (!isMoving)        
            return;        
        
        var step = speed * Time.deltaTime;
        var rotationStep = rotationSpeed * Time.deltaTime;
        Vector3 targetDirection = target.position - transform.position;

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f); 

        transform.rotation = Quaternion.LookRotation(newDirection);
       
    }
}
