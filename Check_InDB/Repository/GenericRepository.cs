using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Check_InDB.Models;

namespace Check_InDB.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected IDbSet<TEntity> dbSet { get; set; }   //使用 Entity Framework 的機制

        public DbContext _context { get; set; }

        public GenericRepository()
            : this(new Check_testEntities())
        {
        }

        public GenericRepository(DbContext context)
        {
            this._context = context ?? throw new ArgumentNullException("context");

            this.dbSet = this._context.Set<TEntity>();
        }

        /// <summary>
        /// 新增資料到指定實體
        /// </summary>
        /// <param name="instance">資料實體</param>
        /// <exception cref="System.ArgumentNullException">資料實體</exception>
        public void Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add(instance);
                this.SaveChanges();
            }
        }
        /// <summary>
        /// 新增多筆資料到指定實體
        /// </summary>
        /// <param name="insertList"></param>
        public void CreateMultiple(List<TEntity> insertList)
        {
            foreach (var instance in insertList)
            {
                this._context.Set<TEntity>().Add(instance);
            }
            this.SaveChanges();
        }

        /// <summary>
        /// 更新資料到指定實體
        /// </summary>
        /// <param name="instance">資料實體</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        /// <summary>
        /// 更新多筆資料到指定實體
        /// </summary>
        /// <param name="updateList">資料實體</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UpdateMultiple(List<TEntity> updateList)
        {
            foreach (var instance in updateList)
            {
                this._context.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
            this.SaveChanges();
        }

        /// <summary>
        /// 由資料實體刪除資料
        /// </summary>
        /// <param name="instance">資料實體</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }
        /// <summary>
        /// 由資料實體刪除多筆資料
        /// </summary>
        /// <param name="deleteList"></param>
        public void DeleteMultiple(List<TEntity> deleteList)
        {
            foreach (var instance in deleteList)
            {
                this._context.Entry(instance).State = EntityState.Deleted;
            }
            this.SaveChanges();
        }

        /// <summary>
        /// 更新一筆資料的內容。只更新部分欄位的。
        /// Lambda 運算式 只需要傳遞欄位屬性 EX : x => x.ColumnName1, x => x.Column2....
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        public void UpdateBy(TEntity entity, Expression<Func<TEntity, object>>[] updateProperties)
        {
            // 想要略過 EF 檢查 關閉自動追蹤實體的驗證
            this._context.Configuration.ValidateOnSaveEnabled = false;
            // 其屬性還未更新到資料庫 但先做紀錄
            this._context.Entry(entity).State = EntityState.Unchanged;

            if (updateProperties != null)
            {
                // 確認那些欄位是要修改的做上記號
                foreach (var item in updateProperties)
                {
                    this._context.Entry(entity).Property(item).IsModified = true;
                }
            }
        }

        /// <summary>
        /// 儲存
        /// </summary>
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 根據條件尋找 TEntity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }

    }

}
