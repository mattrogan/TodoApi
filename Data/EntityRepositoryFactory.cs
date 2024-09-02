namespace TodoApi.Data;

public class EntityRepositoryFactory(TodoContext context)
{
    private readonly TodoContext context = context;

    public EntityRepository<T> RepositoryFor<T>() where T : class
    {
        return new EntityRepository<T>(context);
    }
}
