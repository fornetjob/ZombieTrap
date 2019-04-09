using Entitas;

public class DestroySystem : IExecuteSystem
{
    #region Poolings

    private PrefabsPooling _prefabsPooling = null;

    #endregion

    [Group(GameComponentsLookup.Destroy)]
    private IGroup<GameEntity> _destroyed = null;

    public void Execute()
    {
        if (_destroyed.count > 0)
        {
            var destroyed = _destroyed.GetEntities();

            for (int i = 0; i < destroyed.Length; i++)
            {
                var toDestroy = destroyed[i];

                if (toDestroy.hasAttached)
                {
                    _prefabsPooling.Destroy(toDestroy.attached.value, toDestroy);
                }

                toDestroy.Destroy();
            }
        }
    }
}