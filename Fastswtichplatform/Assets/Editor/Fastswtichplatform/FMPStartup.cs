using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class FMPStartup  {
	public enum Platform
	{
		IOS,
		PC,
		ANDROID
	};

	static FMPStartup()
	{
		if(PlayerPrefs.HasKey(FastSwitchPlatformWindow.KEY))
		{
			Platform pl = (Platform)PlayerPrefs.GetInt(FastSwitchPlatformWindow.KEY);
			PlayerPrefs.DeleteKey(FastSwitchPlatformWindow.KEY);
			BuildTarget buildTarget = BuildTarget.StandaloneOSXUniversal;
			switch(pl)
			{
			case Platform.ANDROID:
				buildTarget = BuildTarget.Android;
				break;
			case Platform.IOS:
				buildTarget = BuildTarget.iPhone;
				break;
			case Platform.PC:
				buildTarget = BuildTarget.StandaloneOSXUniversal;
				break;
			}
			EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
			
		}

	}


}
