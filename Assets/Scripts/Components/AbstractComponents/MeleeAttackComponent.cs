namespace Game.Components.AbstractComponents
{
    public abstract class MeleeAttackComponent : GameComponent
    {       
        public abstract void Attack(DamageComponent damage);
        public override void Initialize() { }
    }
}