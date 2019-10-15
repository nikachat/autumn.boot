using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlDbLite
{
    public partial interface IAdo
    {
        string SqlParameterKeyWord { get; }
        IDbConnection Connection { get; set; }
        IDbTransaction Transaction { get; set; }
        IDataParameter[] ToIDbDataParameter(params DbLiteParameter[] pars);
        DbLiteParameter[] GetParameters(object obj, PropertyInfo[] propertyInfo = null);
        SqlDbLiteProvider Context { get; set; }
        void ExecuteBefore(string sql, DbLiteParameter[] pars);
        void ExecuteAfter(string sql, DbLiteParameter[] pars);
        bool IsEnableLogEvent{get;set;}

        IDataParameterCollection DataReaderParameters { get; set; }
        CommandType CommandType { get; set; }

        bool IsDisableMasterSlaveSeparation { get; set; }
        bool IsClearParameters { get; set; }
        int CommandTimeOut { get; set; }
        TimeSpan SqlExecutionTime { get; }
        IDbBind DbBind { get; }
        void SetCommandToAdapter(IDataAdapter adapter, DbCommand command);
        IDataAdapter GetAdapter();
        DbCommand GetCommand(string sql, DbLiteParameter[] parameters);


        DataTable GetDataTable(string sql, object parameters);
        DataTable GetDataTable(string sql, params DbLiteParameter[] parameters);
        DataTable GetDataTable(string sql, List<DbLiteParameter> parameters);

        Task<DataTable> GetDataTableAsync(string sql, object parameters);
        Task<DataTable> GetDataTableAsync(string sql, params DbLiteParameter[] parameters);
        Task<DataTable> GetDataTableAsync(string sql, List<DbLiteParameter> parameters);

        DataSet GetDataSetAll(string sql, object parameters);
        DataSet GetDataSetAll(string sql, params DbLiteParameter[] parameters);
        DataSet GetDataSetAll(string sql, List<DbLiteParameter> parameters);

        Task<DataSet> GetDataSetAllAsync(string sql, object parameters);
        Task<DataSet> GetDataSetAllAsync(string sql, params DbLiteParameter[] parameters);
        Task<DataSet> GetDataSetAllAsync(string sql, List<DbLiteParameter> parameters);

        IDataReader GetDataReader(string sql, object parameters);
        IDataReader GetDataReader(string sql, params DbLiteParameter[] parameters);
        IDataReader GetDataReader(string sql, List<DbLiteParameter> parameters);


        Task<IDataReader> GetDataReaderAsync(string sql, object parameters);
        Task<IDataReader> GetDataReaderAsync(string sql, params DbLiteParameter[] parameters);
        Task<IDataReader> GetDataReaderAsync(string sql, List<DbLiteParameter> parameters);


        object GetScalar(string sql, object parameters);
        object GetScalar(string sql, params DbLiteParameter[] parameters);
        object GetScalar(string sql, List<DbLiteParameter> parameters);

        Task<object> GetScalarAsync(string sql, object parameters);
        Task<object> GetScalarAsync(string sql, params DbLiteParameter[] parameters);
        Task<object> GetScalarAsync(string sql, List<DbLiteParameter> parameters);

        int ExecuteCommand(string sql, object parameters);
        int ExecuteCommand(string sql, params DbLiteParameter[] parameters);
        int ExecuteCommand(string sql, List<DbLiteParameter> parameters);

        Task<int> ExecuteCommandAsync(string sql, params DbLiteParameter[] parameters);
        Task<int> ExecuteCommandAsync(string sql, object parameters);
        Task<int> ExecuteCommandAsync(string sql, List<DbLiteParameter> parameters);

        string GetString(string sql, object parameters);
        string GetString(string sql, params DbLiteParameter[] parameters);
        string GetString(string sql, List<DbLiteParameter> parameters);
        Task<string> GetStringAsync(string sql, object parameters);
        Task<string> GetStringAsync(string sql, params DbLiteParameter[] parameters);
        Task<string> GetStringAsync(string sql, List<DbLiteParameter> parameters);


        int GetInt(string sql, object pars);
        int GetInt(string sql, params DbLiteParameter[] parameters);
        int GetInt(string sql, List<DbLiteParameter> parameters);

        Task<int> GetIntAsync(string sql, object pars);
        Task<int> GetIntAsync(string sql, params DbLiteParameter[] parameters);
        Task<int> GetIntAsync(string sql, List<DbLiteParameter> parameters);


        long GetLong(string sql, object pars=null);

        Task<long> GetLongAsync(string sql, object pars=null);


        Double GetDouble(string sql, object parameters);
        Double GetDouble(string sql, params DbLiteParameter[] parameters);
        Double GetDouble(string sql, List<DbLiteParameter> parameters);


        Task<Double> GetDoubleAsync(string sql, object parameters);
        Task<Double> GetDoubleAsync(string sql, params DbLiteParameter[] parameters);
        Task<Double> GetDoubleAsync(string sql, List<DbLiteParameter> parameters);


        decimal GetDecimal(string sql, object parameters);
        decimal GetDecimal(string sql, params DbLiteParameter[] parameters);
        decimal GetDecimal(string sql, List<DbLiteParameter> parameters);

        Task<decimal> GetDecimalAsync(string sql, object parameters);
        Task<decimal> GetDecimalAsync(string sql, params DbLiteParameter[] parameters);
        Task<decimal> GetDecimalAsync(string sql, List<DbLiteParameter> parameters);


        DateTime GetDateTime(string sql, object parameters);
        DateTime GetDateTime(string sql, params DbLiteParameter[] parameters);
        DateTime GetDateTime(string sql, List<DbLiteParameter> parameters);

        Task<DateTime> GetDateTimeAsync(string sql, object parameters);
        Task<DateTime> GetDateTimeAsync(string sql, params DbLiteParameter[] parameters);
        Task<DateTime> GetDateTimeAsync(string sql, List<DbLiteParameter> parameters);


        Tuple<List<T>, List<T2>> SqlQuery<T,T2>(string sql, object parameters = null);
        Tuple<List<T>, List<T2>, List<T3>> SqlQuery<T, T2,T3>(string sql, object parameters = null);
        Tuple<List<T>, List<T2>, List<T3>,List<T4>> SqlQuery<T,T2,T3,T4>(string sql, object parameters = null);
        Tuple<List<T>, List<T2>, List<T3>, List<T4>, List<T5>> SqlQuery<T, T2, T3, T4,T5>(string sql, object parameters = null);
        Tuple<List<T>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>> SqlQuery<T, T2, T3, T4, T5,T6>(string sql, object parameters = null);
        Tuple<List<T>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>, List<T7>> SqlQuery<T, T2, T3, T4, T5, T6,T7>(string sql, object parameters = null);

        Task<Tuple<List<T>, List<T2>>> SqlQueryAsync<T, T2>(string sql, object parameters = null);
        Task<Tuple<List<T>, List<T2>, List<T3>>> SqlQueryAsync<T, T2, T3>(string sql, object parameters = null);
        Task<Tuple<List<T>, List<T2>, List<T3>, List<T4>>> SqlQueryAsync<T, T2, T3, T4>(string sql, object parameters = null);
        Task<Tuple<List<T>, List<T2>, List<T3>, List<T4>, List<T5>>> SqlQueryAsync<T, T2, T3, T4, T5>(string sql, object parameters = null);
        Task<Tuple<List<T>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>>> SqlQueryAsync<T, T2, T3, T4, T5, T6>(string sql, object parameters = null);
        Task<Tuple<List<T>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>, List<T7>>> SqlQueryAsync<T, T2, T3, T4, T5, T6, T7>(string sql, object parameters = null);

        List<T> SqlQuery<T>(string sql, object parameters = null);
        List<T> SqlQuery<T>(string sql, params DbLiteParameter[] parameters);
        List<T> SqlQuery<T>(string sql, List<DbLiteParameter> parameters);

        Task<List<T>> SqlQueryAsync<T>(string sql, object parameters = null);
        Task<List<T>> SqlQueryAsync<T>(string sql, List<DbLiteParameter> parameters);
        Task<List<T>> SqlQueryAsync<T>(string sql, params DbLiteParameter[] parameters);

        T SqlQuerySingle<T>(string sql, object whereObj = null);
        T SqlQuerySingle<T>(string sql, params DbLiteParameter[] parameters);
        T SqlQuerySingle<T>(string sql, List<DbLiteParameter> parameters);

        Task<T> SqlQuerySingleAsync<T>(string sql, object whereObj = null);
        Task<T> SqlQuerySingleAsync<T>(string sql, params DbLiteParameter[] parameters);
        Task<T> SqlQuerySingleAsync<T>(string sql, List<DbLiteParameter> parameters);


        void Dispose();
        void Close();
        void Open();
        void CheckConnection();

        void BeginTran();
        void BeginTran(IsolationLevel iso);
        void BeginTran(string transactionName);
        void BeginTran(IsolationLevel iso, string transactionName);
        void RollbackTran();
        void CommitTran();

        DbResult<bool> UseTran(Action action, Action<Exception> errorCallBack = null);
        DbResult<T> UseTran<T>(Func<T> action, Action<Exception> errorCallBack = null);
        Task<DbResult<bool>> UseTranAsync(Action action, Action<Exception> errorCallBack = null);
        Task<DbResult<T>> UseTranAsync<T>(Func<T> action, Action<Exception> errorCallBack = null);
        IAdo UseStoredProcedure();



        #region Obsolete
        [Obsolete("Use db.ado.UseStoredProcedure().MethodName()")]
        void UseStoredProcedure(Action action);
        [Obsolete("Use db.ado.UseStoredProcedure().MethodName()")]
        T UseStoredProcedure<T>(Func<T> action);
        [Obsolete("Use SqlQuery<dynamic>(sql)")]
        dynamic SqlQueryDynamic(string sql, object whereObj = null);
        [Obsolete("Use SqlQuery<dynamic>(sql)")]
        dynamic SqlQueryDynamic(string sql, params DbLiteParameter[] parameters);
        [Obsolete("Use SqlQuery<dynamic>(sql)")]
        dynamic SqlQueryDynamic(string sql, List<DbLiteParameter> parameters); 
        #endregion
    }
}
