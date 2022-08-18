using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace KeyWave
{
    public class EditorBuilder : MonoBehaviour
    {
        public static readonly Type[] EDITABLE_TYPES =
        {
            typeof(Email), 
            typeof(TextMessage), 
            typeof(TextConversation),
            typeof(Assignment),
            typeof(PhoneContact),
            typeof(SearchItem)
        }; 

        [SerializeField] private Transform fieldEditorHolder;
        [SerializeField] private Object fieldEditorPrefab;

        public void CreateEditor(object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                PopulateEditor($"{field.Name}: ", field.GetValue(obj));
            }
        }

        private void PopulateEditor(string label, object data)
        {
            GameObject newEditor = Instantiate(fieldEditorPrefab, fieldEditorHolder) as GameObject;
            newEditor.GetComponentInChildren<TMP_Text>().text = label;
            GameObject newField = newEditor.transform.Find("Field").gameObject;
            FetchInputField(data.GetType(), newField, data);
        }

        private void FetchInputField(Type inputType, GameObject go, object existingValue = null)
        {
            if (existingValue != null && existingValue.GetType() != inputType)
            {
                throw new Exception(
                    "FetchInputField: argument mismatch, existing value does not work for provided type.");
            }


            if (inputType == typeof(bool))
            {
                var a = go.AddComponent<Toggle>();
                if (existingValue != null) a.isOn = (bool)existingValue;
            }
            else if (inputType == typeof(int) || inputType == typeof(string))
            {
                var b = go.AddComponent<TMP_InputField>();
                if (existingValue != null) b.text = (string)existingValue;
            }
            else if (inputType == typeof(Assignment.AssignmentType) || inputType == typeof(Assignment.AssignmentState))
            {
                var c = go.AddComponent<TMP_Dropdown>();
                if (existingValue != null)
                {
                    c.AddOptions(Enum.GetNames(inputType.GetType()).ToList());
                }

                if (existingValue != null) c.value = (int)existingValue;
            }
            else if (inputType == typeof(string[]) || inputType == typeof(List<string>))
                // should change all to use lists, but this exists as fallback
            {
                var d = go.AddComponent<TMP_InputField>();
                if (existingValue == null) return;
                foreach (var item in (List<string>)existingValue)
                {
                    d.text += $"{item}\n";
                }
            }
            
        }
    }
}