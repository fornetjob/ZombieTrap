namespace Game.Core
{
    public static class ItemTypeHelper
    {
        public static bool IsZombie(this ItemType type)
        {
            return type == ItemType.BigZombie
                || type == ItemType.SmallZombie;
        }
    }

    public enum ItemType
    {
        SmallZombie,
        BigZombie,
        Lamp
    }
}