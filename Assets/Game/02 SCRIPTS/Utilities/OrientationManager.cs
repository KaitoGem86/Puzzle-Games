using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrientationManager : MonoBehaviour
{
    public UnityEvent OnResolutionChange = new UnityEvent();
    public UnityEvent OnOrientationChange = new UnityEvent();
    public static float CheckDelay = 0.5f;        // How long to wait until we check again.

    public static Vector2 resolution;                    // Current Resolution
    public static DeviceOrientation orientation;        // Current Device Orientation
    static bool isAlive = true;                    // Keep this script running?

    void Start()
    {
        StartCoroutine(CheckForChange());
    }

    private void Update()
    {
        //transform.localRotation = gyro.attitude * rot;
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);

        //Debug.LogError(transform.eulerAngles.x);
    }
    IEnumerator CheckForChange()
    {
        resolution = new Vector2(Screen.width, Screen.height);
        orientation = Input.deviceOrientation;

        while (isAlive)
        {
            // Check for a Resolution Change
            if (resolution.x != Screen.width || resolution.y != Screen.height)
            {
                resolution = new Vector2(Screen.width, Screen.height);
                OnResolutionChange.Invoke();
            }

            // Check for an Orientation Change
            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Unknown:            // Ignore
                case DeviceOrientation.FaceUp:            // Ignore
                case DeviceOrientation.FaceDown:        // Ignore
                    break;
                default:
                    if (orientation != Input.deviceOrientation)
                    {
                        orientation = Input.deviceOrientation;
                        OnOrientationChange.Invoke();
                    }
                    break;
            }

            yield return new WaitForSeconds(CheckDelay);
        }
    }

    public void Log1()
    {
        Debug.LogError("Even 1");
    }

    public void Log2()
    {
        Debug.LogError("Even 2");
    }

    //void OnDestroy()
    //{
    //    isAlive = false;
    //}
}
