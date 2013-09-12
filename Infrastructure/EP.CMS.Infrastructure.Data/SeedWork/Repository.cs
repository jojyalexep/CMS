using EP.CMS.Domain;
using EP.CMS.Domain.SeedWork;
using EP.CMS.Infrastructure.Data.UnitOfWork;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.SeedWork
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {

        private MainUnitOfWork _UnitOfWork { get; set; }
        public Repository()
        {
            _UnitOfWork = new MainUnitOfWork();
        }

        #region IRepository<TEntity> Members

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_UnitOfWork == null)
                    _UnitOfWork = new MainUnitOfWork();
                return _UnitOfWork;
            }
        }

        public virtual TEntity Add(TEntity item)
        {
            item.Id = (int)_UnitOfWork.Session.Save(item);
            return item;
        }

        public virtual void Remove(TEntity item)
        {
            _UnitOfWork.Session.Delete(item);
        }

        public virtual void SoftRemove(TEntity item)
        {
            if (item != null)
            {
                item.IsDeleted = true;
                _UnitOfWork.Session.SaveOrUpdate(item);
            }
        }

        public virtual void Modify(TEntity item)
        {
            _UnitOfWork.Session.SaveOrUpdate(item);
        }

        public virtual void TrackItem(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual void Merge(TEntity persisted, TEntity current)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Get(int id)
        {
            return _UnitOfWork.Session.Get<TEntity>(id) as TEntity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _UnitOfWork.Session.CreateCriteria<TEntity>().List<TEntity>().Where(p => p.IsDeleted == false);
        }

        public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            throw new NotImplementedException();
            //return GetAll().Where<TEntity>(specification.SatisfiedBy());
        }

        public virtual IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            int skipRecord = (pageIndex - 1) * pageCount;
            return (ascending)
                ? GetAll().Skip(skipRecord).Take(pageCount).OrderBy(orderByExpression.Compile())
                : GetAll().Skip(skipRecord).Take(pageCount).OrderByDescending(orderByExpression.Compile());
        }

        public virtual IEnumerable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return GetAll().Where(p => p.IsDeleted == false).Where(filter.Compile());
        }

        public virtual int GetMax()
        {
            return Convert.ToInt32(_UnitOfWork.Session.CreateCriteria<TEntity>()
                .SetProjection(Projections.Max("Id"))
                .UniqueResult());
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _UnitOfWork.Session.Close();
        }

        #endregion
    }
}
