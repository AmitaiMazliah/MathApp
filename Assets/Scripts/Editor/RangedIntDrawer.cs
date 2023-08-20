using UnityEngine;
using UnityEditor;
using System;
using Assets.Scripts;
using Assets.Scripts.Utils;

[CustomPropertyDrawer(typeof(RangedInt), true)]
public class RangedIntDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        SerializedProperty minProp = property.FindPropertyRelative("minValue");
        SerializedProperty maxProp = property.FindPropertyRelative("maxValue");

        float minValue = minProp.intValue;
        float maxValue = maxProp.intValue;

        float rangeMin = 0;
        float rangeMax = 100;

        var ranges = (IntRangeAttribute[])fieldInfo.GetCustomAttributes(typeof(IntRangeAttribute), true);
        if (ranges.Length > 0)
        {
            rangeMin = ranges[0].Min;
            rangeMax = ranges[0].Max;
        }

        const float rangeBoundsLabelWidth = 40f;

        var rangeBoundsLabel1Rect = new Rect(position);
        rangeBoundsLabel1Rect.width = rangeBoundsLabelWidth;
        GUI.Label(rangeBoundsLabel1Rect, new GUIContent(Convert.ToInt32(minValue).ToString()));
        position.xMin += rangeBoundsLabelWidth;

        var rangeBoundsLabel2Rect = new Rect(position);
        rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
        GUI.Label(rangeBoundsLabel2Rect, new GUIContent(Convert.ToInt32(maxValue).ToString()));
        position.xMax -= rangeBoundsLabelWidth;

        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
        if (EditorGUI.EndChangeCheck())
        {
            minProp.intValue = Convert.ToInt32(minValue);
            maxProp.intValue = Convert.ToInt32(maxValue);
        }

        EditorGUI.EndProperty();
    }
}