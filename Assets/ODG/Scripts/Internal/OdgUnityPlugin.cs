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
}


