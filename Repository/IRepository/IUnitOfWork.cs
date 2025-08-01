namespace JardeuxBlogV1.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBlogRepository Blog { get; }
        void Save();



    }
}
