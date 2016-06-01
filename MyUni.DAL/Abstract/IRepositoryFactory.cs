using Gurukul.Business;
using Gurukul.DAL.Abstract;

namespace StageDocs.DAL.Abstract
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class, IModel;
    }
}