public class FactoryBase : IDependency, IContextInitialize
{
    protected Contexts
        _context;

    void IContextInitialize.Initialize(Contexts context)
    {
        _context = context;
    }
}