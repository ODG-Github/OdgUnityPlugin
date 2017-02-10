using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdgStereoCameraPlugin : MonoBehaviour {

	[Tooltip("Use sensor data to track head rotations")] 
	public bool headTracking;

	private Transform leftEye;
	private Transform rightEye;
	private bool gyroReady;

	private float IPD = 0.064f;

	private Quaternion initRotation;
	private Quaternion initGyroRotation;
	private OdgUnityPlugin odgUnityPlugin;

	void Start () 
	{
		odgUnityPlugin = new OdgUnityPlugin();
		SetupCameraRig ();
		initRotation = transform.rotation;
	}

	void Update () 
	{
		// Read IMU sensor data
		if (headTracking) {
			// Gyro data takes a few frames to initiailze
			Quaternion gyroData = Input.gyro.attitude;
			if (!gyroReady && GyroDataReady(gyroData.eulerAngles)) {
				gyroReady = true;
				initGyroRotation = CorrectGyroData(gyroData);
			}
			transform.rotation = initRotation * RotateFromGyro ();
		}
	}

	/// <summary>
	/// Sets up the camera rig for stereo rendering
	/// </summary>
	void SetupCameraRig()
	{
		leftEye = transform.GetChild (0);
		rightEye = transform.GetChild (1);

		// Set the interpupilary distance of stereo camera
		float halfIpd = IPD / 2f;
		leftEye.localPosition = new Vector3 (-halfIpd, 0f, 0f);
		rightEye.localPosition = new Vector3 (halfIpd, 0f, 0f);

		Camera leftCamera = leftEye.gameObject.GetComponent<Camera>();
		Camera rightCamera = rightEye.gameObject.GetComponent<Camera> ();

		// Split the viewport for stereo rendering
		leftCamera.rect = new Rect (0f, 0f, 0.5f, 1f);
		rightCamera.rect = new Rect (0.5f, 0f, 0.5f, 1f);

	}

	/// <summary>
	/// Enables the gyro data to be read and determines when data is ready
	/// </summary>
	/// <param name="rotation">Vector3 representation of gyro data</param>
	/// <returns>Returns true when gyro data is ready</returns>
	bool GyroDataReady(Vector3 rotation)
	{
		if (!Input.gyro.enabled) {
			Input.gyro.enabled = true;
		}
		return rotation.x != 0f && rotation.y != 0f && rotation.z != 0;
	}

	/// <summary>
	/// Converts the gyro data to correct coordinate frame
	/// </summary>
	/// <param name="gyro">Quaternion representation of gyro data</param>
	/// <returns>Returns a Quaternion of corrected rotation</returns>
	Quaternion CorrectGyroData(Quaternion gyro)
	{
		// Converts the rotation from right handed to left handed
		return new Quaternion (gyro.x, gyro.y, -gyro.z, -gyro.w);
	}

	/// <summary>
	/// Reads and converts the gyro's rotation values
	/// </summary>
	/// <returns>Returns a Quaternion for head tracking</returns>
	Quaternion RotateFromGyro()
	{
		return Quaternion.Inverse(initGyroRotation) * CorrectGyroData(Input.gyro.attitude);
	}
}
