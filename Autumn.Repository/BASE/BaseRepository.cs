using Autumn.Common;
using Autumn.FrameWork;
using Autumn.IRepository.Base;
using SqlDbLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbType = SqlDbLite.DbType;

namespace Autumn.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private DbContext _context;
        private SqlDbLiteClient _db;
        private SimpleClient<TEntity> _entityDb;

        public DbContext DBContext
        {
            get { return _context; }
            set { _context = value; }
        }
        internal SqlDbLiteClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }
        internal SimpleClient<TEntity> entityDb
        {
            get { return _entityDb; }
            private set { _entityDb = value; }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public BaseRepository()
        {
            DbContext.Init(DBConfigHelper.ConnectionString, (DbType)DBConfigHelper.DbType);
            _context = DbContext.GetDbContext();
            _db = _context.Db;
            _entityDb = _context.GetEntityDB<TEntity>(_db);
        }

        /// <summary>
        /// 根据ID查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [DbLiteColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId)
        {
            return await _db.Queryable<TEntity>().In(objId).SingleAsync();
        }

        /// <summary>
        /// 根据ID查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [DbLiteColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await _db.Queryable<TEntity>().WithCacheIF(blnUseCache).In(objId).SingleAsync();
        }

        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [DbLiteColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await _db.Queryable<TEntity>().In(lstIds).ToListAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>影响行数</returns>
        public async Task<int> Insert(TEntity entity)
        {
            var insert = _db.Insertable(entity);
            return await insert.ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="insertColumns">指定只插入列</param>
        /// <returns>返回自增量列</returns>
        public async Task<int> Insert(TEntity entity, Expression<Func<TEntity, object>> insertColumns = null)
        {
            var insert = _db.Insertable(entity);
            if (insertColumns == null)
            {
                return await insert.ExecuteReturnIdentityAsync();
            }
            else
            {
                return await insert.InsertColumns(insertColumns).ExecuteReturnIdentityAsync();
            }
        }

        /// <summary>
        /// 批量插入实体(速度快)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        public async Task<int> Insert(List<TEntity> listEntity)
        {
            return await _db.Insertable(listEntity.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 更新实体数据以主键为条件
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="strWhere">更新条件</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await _db.Updateable(entity).Where(strWhere).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="parameters">更新参数</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(string strSql, DbLiteParameter[] parameters = null)
        {
            return await Task.Run(() => _db.Ado.ExecuteCommand(strSql, parameters) > 0);
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await _db.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteById(object id)
        {
            return await _db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await _db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await _db.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(strOrderByFileds != null, strOrderByFileds).ToListAsync();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
           return await _db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression,int intTop,string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToListAsync();
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere,int intTop,string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression,int intPageIndex,int intPageSize,string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageListAsync(intPageIndex, intPageSize);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryPage(string strWhere,int intPageIndex,int intPageSize,string strOrderByFileds,RefAsync<int> intTotalCount)
        {
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageListAsync(intPageIndex, intPageSize, intTotalCount);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await _db.Queryable<TEntity>()
            .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .WhereIF(whereExpression != null, whereExpression)
            .ToPageListAsync(intPageIndex, intPageSize);
        }

        /// <summary>
        /// 事务开始
        /// </summary>
        /// <param name="iso">事务隔离级别</param>
        public void BeginTran(IsolationLevel iso = IsolationLevel.ReadCommitted)
        {
            _db.BeginTran(iso);
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void CommitTran()
        {
            _db.CommitTran();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollbackTran()
        {
            _db.RollbackTran();
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        /// <returns></returns>
        public DbContext Context()
        {
            return _context;
        }
    }
}
