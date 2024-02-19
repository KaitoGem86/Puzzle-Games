using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	private Transform camTransform;

	// How long the object should shake for.
	private float shakeDuration = 0f;
	// Amplitude of the shake. A larger value shakes the camera harder.
	private float shakeAmount = 0.4f;
	private float decreaseFactor = 2.0f;

    private bool isShake = false;
	Vector3 originalPos;
	private static CameraShake mInstance;


	public static CameraShake Instance {
		get {
			return mInstance;
		}
	}

	public void Vibrate (float shakeDuration, float shakeAmount) {
        originalPos = camTransform.localPosition;
        this.shakeDuration = shakeDuration;
		this.shakeAmount = shakeAmount;
        isShake = true;
	}

	public void StopVibrate () {
		shakeDuration = 0;
        isShake = false;
    }

	void Awake () {
		mInstance = this;
		if (camTransform == null) {
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}


	void Update () {
        if (!isShake)
            return;
		if (shakeDuration > 0) {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		} else {
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
            isShake = false;
		}
	}
}