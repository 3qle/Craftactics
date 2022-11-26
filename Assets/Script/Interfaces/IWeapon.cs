using Script.Character;
  public interface IWeapon
    {
        public void ShowWeapon(IFightable fightable,WeaponHand hand);
        public int Damage { get; }
        Element WeaponElement { get; }
        public int MaxRange { get; }
        public int MinRange { get; }
        int ApCost { get; }
        string Name { get; }
    }
