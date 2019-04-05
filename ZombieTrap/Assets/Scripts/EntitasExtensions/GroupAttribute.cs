using Entitas;

public class GroupAttribute : System.Attribute
{
    public GroupAttribute(params int[] componentIndexes)
    {
        ComponentIndexes = componentIndexes;
    }

    public int[] ComponentIndexes;
}