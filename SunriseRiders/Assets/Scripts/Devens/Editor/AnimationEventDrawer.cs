using UnityEngine;
using UnityEditor;

namespace Devens.Editor
{
    [CustomPropertyDrawer(typeof(AnimationEvent))]
    public class AnimationEventDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var stateNameProperty = property.FindPropertyRelative("eventName");
            var stateEventProperty = property.FindPropertyRelative("OnAnimationEvent");

            Rect stateNameRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            Rect stateEventRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width,
                EditorGUI.GetPropertyHeight(stateEventProperty));

            EditorGUI.PropertyField(stateNameRect, stateNameProperty);
            EditorGUI.PropertyField(stateEventRect, stateEventProperty, true);
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var stateEventProperty = property.FindPropertyRelative("OnAnimationEvent");
            return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(stateEventProperty) + 4;
        }
    }
}
