using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Check_InDB.Repositories
{
    /// <summary>
    /// 資料存取層/通用型 - 介面 (代表一個Repository的interface。)
    /// </summary>
    /// <typeparam name="T">任意model的class</typeparam>
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        DbContext _context { get; set; }

        /// <summary>
        /// 新增一筆資料。
        /// </summary>
        /// <param name="instance">要新增的Entity</param>
        void Create(TEntity instance);

        /// <summary>
        /// 新增多筆資料到指定實體
        /// </summary>
        /// <param name="insertList"></param>
        void CreateMultiple(List<TEntity> insertList);

        /// <summary>
        /// 更新一筆資料的內容。
        /// </summary>
        /// <param name="instance">要更新的內容</param>
        void Update(TEntity instance);

        /// <summary>
        /// 更新多筆資料到指定實體
        /// </summary>
        /// <param name="updateList">資料實體</param>
        void UpdateMultiple(List<TEntity> updateList);

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="instance">要被刪除的Entity。</param>
        void Delete(TEntity instance);

        /// <summary>
        /// 由資料實體刪除多筆資料
        /// </summary>
        /// <param name="deleteList"></param>
        void DeleteMultiple(List<TEntity> deleteList);

        /// <summary>
        /// 更新一筆資料的內容。只更新部分欄位。
        /// Lambda 運算式 只需要傳遞欄位屬性 EX : x => { x.ColumnName1, x.Column2 }
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        /// <param name="predicate">需要更新的欄位。</param>
        void UpdateBy(TEntity entity, Expression<Func<TEntity, object>>[] predicate);

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根據條件尋找 TEntity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

    }

}
