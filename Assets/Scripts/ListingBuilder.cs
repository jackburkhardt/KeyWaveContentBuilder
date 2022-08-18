using System;
using System.Reflection;
using KeyWave;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class ListingBuilder : MonoBehaviour
    {
        [SerializeField] private Transform listingViewHolder;
        [SerializeField] private Object listingViewPrefab;
        [SerializeField] private Object listingTextLine;
        private EditorBuilder editorBuilder;

        private void Awake()
        {
            editorBuilder = FindObjectOfType<EditorBuilder>();
            if (!editorBuilder)
            {
                throw new Exception("ListingBuilder: An active EditorBuilder was not found in this scene.");
            }
        }

        public void AddListing(object obj)
        {
            var newListing = Instantiate(listingViewPrefab, listingViewHolder) as GameObject;
            newListing.GetComponentInChildren<Button>().onClick.AddListener(() => editorBuilder.CreateEditor(obj));
            var contentViewTransform = newListing.transform.Find("Content");
            
            FieldInfo[] fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                AddTextField(contentViewTransform, field, obj);
            }
        }

        private void AddTextField(Transform contentView, FieldInfo field, object obj)
        {
            var newFieldGO = Instantiate(listingTextLine, contentView) as GameObject;
            newFieldGO.GetComponent<TMP_Text>().text = $"{field.Name}: {field.GetValue(obj)}";
        }
    }
}