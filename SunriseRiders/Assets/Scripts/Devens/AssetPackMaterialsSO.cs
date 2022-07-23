using System.Collections.Generic;
using UnityEngine;

namespace Devens
{
    [CreateAssetMenu (menuName = "Devens/AssetPackMaterialsSO")]
    public class AssetPackMaterialsSO : ScriptableObject
    {
        public List<Material> Materials = new List<Material>();
    }
}
