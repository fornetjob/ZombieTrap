public interface IDependencyContainer
{
    T Provide<T>()
           where T : IDependency;
    void Registrate<TInterface, TDependency>();
}