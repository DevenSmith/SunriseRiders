using System.Collections.Generic;
using UnityEngine;

namespace Devens
{
    public class MultiTag : MonoBehaviour
    {
        [SerializeField]
        private List<TagSO> tags = new List<TagSO>();

        private void OnEnable()
        {
            RegisterTags();
        }

        private void OnDisable()
        {
            DeregisterTags();
        }

        public void AddTag(TagSO tag)
        {
            if (tag != null && !tags.Contains(tag))
            {
                tags.Add(tag);
                TagUtility.RegisterTag(tag, this.gameObject);
            }
        }

        public void RemoveTag(TagSO tag)
        {
            if (tag != null && tags.Contains(tag))
            {
                tags.Remove(tag);
                TagUtility.DeregisterTag(tag, this.gameObject);
            }
        }

        public bool HasTag(TagSO tag)
        {
            return tag != null && tags.Contains(tag);
        }

        public List<TagSO> GetTags()
        {
            return new List<TagSO>(tags);
        }

        private void RegisterTags()
        {
            foreach (var tag in tags)
            {
                if (tag != null && gameObject != null)
                {
                    TagUtility.RegisterTag(tag, this.gameObject);
                }
            }
        }

        private void DeregisterTags()
        {
            foreach (var tag in tags)
            {
                if (tag != null && gameObject != null)
                {
                    TagUtility.DeregisterTag(tag, this.gameObject);
                }
            }
        }

        public bool HasAnyTag(params TagSO[] tagsToCheck)
        {
            foreach (var tag in tagsToCheck)
            {
                if (tags.Contains(tag))
                {
                    return true;
                }
            }
            return false;
        }
    }
}