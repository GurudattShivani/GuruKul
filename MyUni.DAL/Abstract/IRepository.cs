using System.Linq;
using Gurukul.Business;
using StageDocs.DAL.Abstract;

namespace Gurukul.DAL.Abstract
{
    public interface IRepository<T> where T:class, IModel
    {
        T GetById(int id);

        IQueryable<T> GetAll();

        T Add(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Update(T entity);

        IUoW UoW { get; set; }
    }
}