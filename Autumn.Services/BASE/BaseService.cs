using Autumn.Common;
using Autumn.IRepository.Base;
using Autumn.IServices.BASE;
using SqlDbLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Autumn.Services.BASE
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 通过子类的构造函数中注入
        /// </summary>
        public IBaseRepository<TEntity> BaseDal;

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="objId">主键</param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId)
        {
            return await BaseDal.QueryById(objId);
        }

        /// <summary>
        /// 根据ID查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [DbLiteColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await BaseDal.QueryById(objId, blnUseCache);
        }

        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [DbLiteColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await BaseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(TEntity entity)
        {
            return await BaseDal.Insert(entity);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await BaseDal.Update(entity);
        }

        /// <summary>
        /// 有条件更新实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await BaseDal.Update(entity, strWhere);
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await BaseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await BaseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await BaseDal.DeleteByIds(ids);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await BaseDal.Query();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await BaseDal.Query(strWhere);
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.Query(whereExpression);
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await BaseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await BaseDal.Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await BaseDal.Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await BaseDal.Query(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds)
        {
            return await BaseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds)
        {
            return await BaseDal.Query(
              whereExpression,
              intPageIndex,
              intPageSize,
              strOrderByFileds);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryPage(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds,
          RefAsync<int> intTotalCount)
        {
            return await BaseDal.QueryPage(
            strWhere,
            intPageIndex,
            intPageSize,
            strOrderByFileds,
            intTotalCount);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await BaseDal.QueryPage(whereExpression,intPageIndex = 0, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 数据库
        /// </summary>
        /// <returns></returns>
        public SqlDbLiteClient Db
        {
            get { return BaseDal.Context().Db; }
        }

        /// <summary>
        /// 事务处理
        /// </summary>
        /// <param name="action">委托</param>
        /// <param name="iso">隔离级别</param>
        /// <returns></returns>
        public async Task<bool> ActionTran(Func<Task<bool>> action, IsolationLevel iso = IsolationLevel.ReadCommitted)
        {
            try
            {
                BaseDal.BeginTran(iso);
                await action();
                BaseDal.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorLog(ex.Message, ex);
                BaseDal.RollbackTran();
                return false;
            }
        }
    }
}
