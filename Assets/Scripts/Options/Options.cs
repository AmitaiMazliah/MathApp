using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Options
{
    public bool HasUnsavedChanges => dirtyValues.Count > 0;

    public string PersistencyPrefix => persistencyPrefix;

    public event Action ChangesSaved;
    public event Action ChangesDiscarded;

    private Dictionary<OptionType, string> values = new(128);
    private readonly Dictionary<OptionType, string> dirtyValues = new(128);
    private Dictionary<OptionType, string> defaultValues = new(128);

    private bool enablePersistency;
    private string persistencyPrefix;

    public void Initialize(Dictionary<OptionType, string> defaultValues, bool enablePersistency, string persistencyPrefix)
    {
        this.defaultValues = defaultValues;
        this.enablePersistency = enablePersistency;
        this.persistencyPrefix = persistencyPrefix;
        
        LoadValues();
    }

    public void SaveChanges()
    {
        if (dirtyValues.Count == 0)
            return;

        foreach (var pair in dirtyValues)
        {
            AddValue(pair.Key, pair.Value);

            if (enablePersistency == false)
                continue;

            var key = persistencyPrefix + pair.Key;

            if (pair.Value.Equals(defaultValues[pair.Key]))
            {
                PersistentStorage.Delete(key);
                continue;
            }
            
            PersistentStorage.SetString(key, pair.Value, false);
        }

        if (enablePersistency)
        {
            PersistentStorage.Save();
        }

        dirtyValues.Clear();

        ChangesSaved?.Invoke();
    }

    public void DiscardChanges()
    {
        dirtyValues.Clear();

        ChangesDiscarded?.Invoke();
    }

    public void ResetValueToDefault(OptionType key, bool saveImmediately)
    {
        if (defaultValues.TryGetValue(key, out var value))
        {
            Set(key, value, saveImmediately);
        }
    }

    public void ResetAllValuesToDefault()
    {
        foreach (var keyValuePair in defaultValues)
        {
            Set(keyValuePair.Key, keyValuePair.Value, false);
        }
        
        SaveChanges();
    }

    public void AddDefaultValue(OptionType type, string value)
    {
        if (values.ContainsKey(type))
            return;

        // optionsData.AddRuntimeValue(value);
        // LoadValue(value);
    }

    public string GetValue(OptionType key)
    {
        if (dirtyValues.TryGetValue(key, out var dirtyValue))
            return dirtyValue;

        if (values.TryGetValue(key, out var value))
            return value;

        Debug.LogError($"Missing options value with key {key}");

        return default;
    }

    public bool GetBool(OptionType key)
    {
        return bool.Parse(GetValue(key));
    }

    public float GetFloat(OptionType key)
    {
        return Convert.ToSingle(GetValue(key));
    }

    public int GetInt(OptionType key)
    {
        return Convert.ToInt32(GetValue(key));
    }

    public string GetString(OptionType key)
    {
        return GetValue(key);
    }

    public void Set(OptionType key, string newValue, bool saveImmediately)
    {
        values.TryGetValue(key, out var originalValue);
        
        if (newValue.Equals(originalValue))
        {
            dirtyValues.Remove(key); // Remove previous modification if exists
            return;
        }

        dirtyValues[key] = newValue;

        if (saveImmediately)
        {
            SaveChanges();
        }
    }

    public bool IsDirty(OptionType key)
    {
        return dirtyValues.ContainsKey(key);
    }

    private void AddValue(OptionType type, string value)
    {
        values[type] = value;
    }

    private void LoadValues()
    {
        values.Clear();
        dirtyValues.Clear();
        
        foreach (var keyValuePair in defaultValues)
        {
            LoadValue(keyValuePair.Key, keyValuePair.Value);
        }
    }

    private void LoadValue(OptionType type, string value)
    {
        if (enablePersistency)
        {
            var key = persistencyPrefix + type;
            
            var persistentValue = PersistentStorage.GetString(key);
            if (!string.IsNullOrEmpty(persistentValue)) value = persistentValue;
        }

        AddValue(type, value);
    }
}