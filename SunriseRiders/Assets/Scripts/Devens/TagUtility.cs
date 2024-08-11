using System.Collections.Generic;
using UnityEngine;

namespace Devens
{
    public static class TagUtility
    {
        private static readonly Dictionary<TagSO, HashSet<GameObject>> tagDictionary = new Dictionary<TagSO, HashSet<GameObject>>();

        public static void RegisterTag(TagSO tag, GameObject gameObject)
        {
            if (!tagDictionary.ContainsKey(tag))
            {
                tagDictionary[tag] = new HashSet<GameObject>();
            }
            tagDictionary[tag].Add(gameObject);
        }

        public static void DeregisterTag(TagSO tag, GameObject gameObject)
        {
            if (tagDictionary.ContainsKey(tag))
            {
                tagDictionary[tag].Remove(gameObject);
                if (tagDictionary[tag].Count == 0)
                {
                    tagDictionary.Remove(tag);
                }
            }
        }

        public static GameObject[] FindGameObjectsWithTag(TagSO tag)
        {
            if (tagDictionary.ContainsKey(tag))
            {
                return new List<GameObject>(tagDictionary[tag]).ToArray();
            }
            return new GameObject[0];
        }
    }
}