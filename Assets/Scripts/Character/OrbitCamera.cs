using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://catlikecoding.com/unity/tutorials/movement/orbit-camera/

[RequireComponent(typeof(Camera))]  //makes sure the object jhas camera

public class OrbitCamera : MonoBehaviour
{
    #region focus
    [SerializeField]
    Transform focus = default;
    [SerializeField]
    Vector3 focusOffset = default;

    [SerializeField, Min(0f)]
    float focusRadius = 1f;
    [SerializeField, Range(0, 1f)]
    float focusCentering = 0.5f;
    Vector3 focusPoint, previousFocusPoint;
    #endregion

    #region Orbit
    Vector2 orbitAngles = new Vector2(45f, 0);
    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;
    [SerializeField, Range(-89f, 89f)]
    float minVerticalAngle = -30f, maxVerticalAngle = 60f;
    #endregion

    #region Align
    [SerializeField, Min(0f)]
    float alignDelay = 5f;
    float lastManualRotationTime;
    //[SerializeField, Range(0f, 90f)]
    //float alignSmoothRange = 45f;
    #endregion

    [SerializeField, Range(1f, 20f)]
    float distance = 5f;

    private void Awake()
    {
        focusPoint = focus.position + focusOffset;
        transform.localRotation = Quaternion.Euler(orbitAngles);

    }

    private void LateUpdate() //used for mainly camera
    {
        UpdateFocusPoint();


        //setting the rotation and posiotn of the camera
        Quaternion lookRotation; //= Quaternion.Euler(orbitAngles);
        if (ManualRotation() || AutomaticRotation())
        {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else
        {
            lookRotation = transform.localRotation;
        }

        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;
        transform.SetPositionAndRotation(lookPosition, lookRotation);

        //transform.localPosition = focusPoint - lookDirection * distance;  //distnace magnitude with what the camera is looking at
    }

    bool AutomaticRotation()
    {
        if (Time.unscaledTime - lastManualRotationTime < alignDelay)
        {
            return false;
        }
        Vector2 movement = new Vector2(
                focusPoint.x - previousFocusPoint.x,
                focusPoint.z - previousFocusPoint.z);
        float movementDeltaSqr = movement.sqrMagnitude;
        if (movementDeltaSqr < 0.0001f)
        {
            return false;
        }
        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        //float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
        float rotationChange = rotationSpeed * Time.unscaledDeltaTime; //*Mathf.Min( Time.unscaledDeltaTime
                                                                       // , movementDeltaSqr);
        //if (deltaAbs < alignSmoothRange)
        //{
        //    rotationChange *= deltaAbs / alignSmoothRange;
        //}
        //else if (180f - deltaAbs < alignSmoothRange)
        //{
        //    rotationChange *= (180f - deltaAbs) / alignSmoothRange;
        //}
        orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);
        return true;
    }

    bool ManualRotation()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        const float e = 0.001f;
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            orbitAngles += rotationSpeed * input * Time.unscaledDeltaTime;
            lastManualRotationTime = Time.unscaledTime; //this is just after the last joysticl movement
            return true;
        }
        return false;
    }

    void UpdateFocusPoint()
    {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position + focusOffset; ; 
        if (focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            if (distance > 0.01f && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }
            if (distance > focusRadius)
            {
                //focusPoint = Vector3.Lerp(targetPoint, focusPoint, focusRadius/distance);
                t = Mathf.Min(t, focusRadius / distance);
            }
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            focusPoint = targetPoint;
        }
    }

    static float GetAngle(Vector2 direction)
    {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360f - angle : angle;
    }

    void ConstrainAngles()
    {
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        if (orbitAngles.y < 0f)
        {
            orbitAngles.y += 360f;
        }
        else if (orbitAngles.y < 360f)
        {
            orbitAngles.y -= 360f;
        }
    }

    private void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

}
