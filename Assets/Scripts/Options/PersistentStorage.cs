using MathApp;
using UnityEngine;

public static class PersistentStorage
{
    private const int VALUE_TRUE = 1;
    private const int VALUE_FALSE = 0;

    public static bool GetBool(string key, bool defaultValue = false)
    {
        int defaultValueInt = defaultValue ? VALUE_TRUE : VALUE_FALSE;
        return PlayerPrefs.GetInt(key, defaultValueInt) == VALUE_TRUE;
    }

    public static float GetFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public static int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static string GetString(string key, string defaultValue = null)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public static T GetObject<T>(string key, T defaultValue = default)
    {
        var objectJson = GetString(key);

        if (objectJson.HasValue() == false)
            return defaultValue;

        return JsonUtility.FromJson<T>(objectJson);
    }

    public static void SetBool(string key, bool value, bool saveImmediately = true)
    {
        PlayerPrefs.SetInt(key, value ? VALUE_TRUE : VALUE_FALSE);

        if (saveImmediately)
        {
            PlayerPrefs.Save();
        }
    }

    public static void SetInt(string key, int value, bool saveImmediately = true)
    {
        PlayerPrefs.SetInt(key, value);

        if (saveImmediately)
        {
            PlayerPrefs.Save();
        }
    }

    public static void SetFloat(string key, float value, bool saveImmediately = true)
    {
        PlayerPrefs.SetFloat(key, value);

        if (saveImmediately)
        {
            PlayerPrefs.Save();
        }
    }

    public static void SetString(string key, string value, bool saveImmediately = true)
    {
        PlayerPrefs.SetString(key, value);

        if (saveImmediately)
        {
            PlayerPrefs.Save();
        }
    }

    public static void SetObject(string key, object value, bool saveImmediately = true)
    {
        var objectJson = JsonUtility.ToJson(value);
        SetString(key, objectJson, saveImmediately);
    }

    public static void Delete(string key, bool saveImmediately = true)
    {
        PlayerPrefs.DeleteKey(key);

        if (saveImmediately)
        {
            PlayerPrefs.Save();
        }
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }
}