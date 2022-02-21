using UnityEngine;

namespace Devens.Actions
{
    [CreateAssetMenu(menuName = "Devens/ActionSOs/DeactivateActionSO")]
    public class DeactivateActionSO : ActionSO
    {
        public override void PerformAction(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
