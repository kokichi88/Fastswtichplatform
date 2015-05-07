using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Diagnostics;
using System.IO;

public class FastSwitchPlatformWindow : EditorWindow {
	private int selected;
	public static string KEY = "FastSwitchPlatform*@)*)($#&%)(&#&)!)#&#&%&";

	[MenuItem("Window/FastSwitchPlatformWindow")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(FastSwitchPlatformWindow));
	}

	void OnGUI()
	{
		GUILayout.Label ("Settings", EditorStyles.boldLabel);
		string[] options = System.Enum.GetNames(typeof(FMPStartup.Platform));
		selected = EditorGUILayout.Popup("Select platform", selected, options);
		if(GUILayout.Button("Switch"))
		{
			SwitchPlatform();
		}


	}

	void OnEnable()
	{
	}

	void OnDestroy()
	{
	}

	void SwitchPlatform ()
	{

		string path = Application.dataPath + "/Editor/Fastswtichplatform/";
		string projectPath = Application.dataPath.Substring(0,Application.dataPath.LastIndexOf("/")+1);
		FMPStartup.Platform pl = (FMPStartup.Platform) selected;
		BuildTarget buildTarget = BuildTarget.StandaloneOSXUniversal;
		switch(pl)
		{
		case FMPStartup.Platform.ANDROID:
			buildTarget = BuildTarget.Android;
			break;
		case FMPStartup.Platform.IOS:
			buildTarget = BuildTarget.iPhone;
			break;
		case FMPStartup.Platform.PC:
            buildTarget = BuildTarget.StandaloneOSXUniversal;
			break;
		}
        if (buildTarget == EditorUserBuildSettings.activeBuildTarget)
        {
            UnityEngine.Debug.LogWarning("Not need to change");
            return;
        }

		PlayerPrefs.SetInt(FastSwitchPlatformWindow.KEY, selected);
		PlayerPrefs.Save();
		ProcessStartInfo proc;
		proc = new ProcessStartInfo();
#if UNITY_EDITOR_OSX
		proc.FileName = "sh";
#else
        proc.FileName = path + "fastswitchplatform.bat";
#endif
		proc.WorkingDirectory = path;
#if UNITY_EDITOR_OSX
        string run = "fastswitchplatform.sh";
#else
        string run = "";
#endif
		proc.Arguments = run+ " \"" + projectPath + "\" " + pl + " \"" + EditorApplication.applicationPath + "\"";
		UnityEngine.Debug.Log(proc.Arguments);
		proc.WindowStyle = ProcessWindowStyle.Maximized;
		proc.CreateNoWindow = false;
		Process.Start(proc);
	}

	
}
