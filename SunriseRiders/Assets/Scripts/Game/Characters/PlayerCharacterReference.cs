namespace Game.Characters
{
    public class PlayerCharacterReference : CharacterReference
    {
        protected override void Initialize()
        {
            base.Initialize();
            GameManager.PlayerReference = this;
        }
    }
}
