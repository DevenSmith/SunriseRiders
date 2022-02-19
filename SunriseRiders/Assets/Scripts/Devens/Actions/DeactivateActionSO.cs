using UnityEngine;

namespace Devens
{
    [CreateAssetMenu(menuName = "Devens/ActionSOs/DeactiveActionSO")]
    public class DeactivateActionSO : ActionSO
    {
        public override void PerformAction(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
