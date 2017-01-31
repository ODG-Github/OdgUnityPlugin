using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdgUnityPlugin : MonoBehaviour 
{
	#if UNITY_ANDROID
		AndroidJavaObject plugin;
	#endif

	/// <summary>
	/// Initializes android plugin to access libraries needed for stero rendering
	/// </summary>
	public OdgUnityPlugin()
	{
		// Plugin is only for Android platforms
		#if !UNITY_EDITOR
			RuntimePlatform runtimePlatform = Application.platform;
			Debug.AssertFormat(runtimePlatform.Equals(RuntimePlatform.Android),
				"ODG Plugin not supported for " + runtimePlatform);

			#if UNITY_ANDROID
				AndroidJavaClass odgPlugin = new AndroidJavaClass("com.osterhoutgroup.odgunityext.OsterhoutUnityActivity");
				plugin = odgPlugin.CallStatic<AndroidJavaObject>("getInstance");
				plugin.Call("init");
			#endif
		#endif
	}

	/// <summary>
	/// Gets the device's field of view (in degrees)
	/// </summary>
	/// <param name="fov">Reference to float value</param>
	/// <returns>Returns true if successful</returns>
	public bool GetFieldOfView(ref float fov)
	{
		bool success = true;
		fov = 0;

		if (plugin != null) {
			fov = plugin.Call<float> ("getFieldOfView");
		} else {
			Debug.Log ("ODG plugin must be initialized first.");
		}

		if (fov <= 0) {
			Debug.Log ("Failed to read device's field of view.");
			success = false;
		}
		return success;
	}
}


