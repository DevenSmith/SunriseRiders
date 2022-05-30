namespace Devens
{
    /// <summary>
    /// A seperate ObjectPoolerForUIElements
    /// </summary>
    public class UIObjectPooler : ObjectPooler
    {
        public static ObjectPooler UIInstance;

        public override void Awake()
        {
            if (UIInstance != null)
            {
                Destroy(this);
                return;
            }

            UIInstance = this;
            Initialize();
        }
    }
}
