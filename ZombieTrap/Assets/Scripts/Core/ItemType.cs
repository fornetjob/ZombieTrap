namespace Game.Core
{
    public static class ItemTypeHelper
    {
        public static bool IsDamagable(this ItemType type)
        {
            return IsZombie(type);
        }

        public static bool IsZombie(this ItemType type)
        {
            return type == ItemType.BigZombie
                || type == ItemType.SmallZombie;
        }

        public static bool IsProjectile(this ItemType type)
        {
            return type == ItemType.FireProjectile;
        }
    }

    public enum ItemType
    {
        SmallZombie,
        BigZombie,
        FireProjectile,
        Lamp,
        CylinderObtacle,
    }
}