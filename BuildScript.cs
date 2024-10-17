using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildScript
{
    public static void BuildAndroid()
    {
        string[] scenes = {
            "Assets/name/Scenes/*********.unity",
            "Assets/name/Scenes/*********.unity",
            "Assets/name/Scenes/*********.unity"
        };

        string version = "0.0.1";
        string versionFilePath = "version.txt";
        if (File.Exists(versionFilePath))
        {
            string fileContent = File.ReadAllText(versionFilePath).Trim();
            string[] parts = fileContent.Split('=');
            if (parts.Length == 2 && parts[0].Trim() == "NAME_NAME_VERSION")
            {
                version = parts[1].Trim();
                Debug.Log("Version loaded from file: " + version);
            }
            else
            {
                Debug.LogError("Invalid format in version.txt. Using default version: " + version);
            }
        }
        else
        {
            Debug.LogError("version.txt file not found. Using default version: " + version);
        }

        PlayerSettings.applicationIdentifier = "com.name.name";
        PlayerSettings.bundleVersion = version;

        int versionCode = 1;
        if (int.TryParse(version.Replace(".", ""), out int parsedVersion))
        {
            versionCode = parsedVersion;
        }
        PlayerSettings.Android.bundleVersionCode = versionCode;

        PlayerSettings.Android.targetSdkVersion = (AndroidSdkVersions)34;
        PlayerSettings.Android.keystoreName = "name-keystore.jks";
        PlayerSettings.Android.keystorePass = Environment.GetEnvironmentVariable("ANDROID_KEYSTORE_PASS");
        PlayerSettings.Android.keyaliasName = Environment.GetEnvironmentVariable("ANDROID_KEYALIAS_NAME");
        PlayerSettings.Android.keyaliasPass = Environment.GetEnvironmentVariable("ANDROID_KEYALIAS_PASS");

        EditorUserBuildSettings.buildAppBundle = true;

        string buildPath = $"build/Android/name-name-name-{version}.aab";
        Directory.CreateDirectory("build/Android");

        Debug.Log($"Building Android app bundle: {buildPath}");
        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.Android, BuildOptions.None);
        Debug.Log($"Build completed: {buildPath}");
    }
}
