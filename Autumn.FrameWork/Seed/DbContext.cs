using Autumn.Common;
using SqlDbLite;
using System;

namespace Autumn.FrameWork
{
    public class DbContext
    {
        private static string _connectionString = DBConfigHelper.ConnectionString;
        private static DbType _dbType = (DbType)DBConfigHelper.DbType;
        private SqlDbLiteClient _db;

        /// <summary>
        /// 连接字符串 
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 数据库类型 
        /// </summary>
        public static DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// 数据连接对象 
        /// </summary>
        public SqlDbLiteClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }

        /// <summary>
        /// 数据库上下文实例（自动关闭连接）
        /// </summary>
        public static DbContext Context
        {
            get
            {
                return new DbContext();
            }

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DbContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException("数据库连接字符串为空.");
            _db = new SqlDbLiteClient(new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = true,
                IsShardSameThread = false,
                InitKeyType = InitKeyType.Attribute,//mark
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache()
                },
                MoreSettings = new ConnMoreSettings()
                {
                    //IsWithNoLockQuery = true,
                    IsAutoRemoveDataCache = true
                }
            });
        }

        #region 静态方法

        /// <summary>
        /// 获得一个DbContext
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDbContext()
        {
            return new DbContext();
        }

        /// <summary>
        /// 设置初始化参数
        /// </summary>
        /// <param name="strConnectionString">连接字符串</param>
        /// <param name="enmDbType">数据库类型</param>
        public static void Init(string strConnectionString, DbType enmDbType = SqlDbLite.DbType.SqlServer)
        {
            _connectionString = strConnectionString;
            _dbType = enmDbType;
        }

        /// <summary>
        /// 创建一个链接配置
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="blnIsShardSameThread">是否夸类事务</param>
        /// <returns>ConnectionConfig</returns>
        public static ConnectionConfig GetConnectionConfig(bool blnIsAutoCloseConnection = true, bool blnIsShardSameThread = false)
        {
            ConnectionConfig config = new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = blnIsAutoCloseConnection,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache()
                },
                IsShardSameThread = blnIsShardSameThread
            };
            return config;
        }

        /// <summary>
        /// 获取一个自定义的DB
        /// </summary>
        /// <param name="config">config</param>
        /// <returns>返回值</returns>
        public static SqlDbLiteClient GetCustomDB(ConnectionConfig config)
        {
            return new SqlDbLiteClient(config);
        }

        /// <summary>
        /// 获取一个自定义的数据库处理对象
        /// </summary>
        /// <param name="DbLiteClient">DbLiteClient</param>
        /// <returns>返回值</returns>
        public static SimpleClient<T> GetCustomEntityDB<T>(SqlDbLiteClient DbLiteClient) where T : class, new()
        {
            return new SimpleClient<T>(DbLiteClient);
        }

        /// <summary>
        /// 获取一个自定义的数据库处理对象
        /// </summary>
        /// <param name="config">config</param>
        /// <returns>返回值</returns>
        public static SimpleClient<T> GetCustomEntityDB<T>(ConnectionConfig config) where T : class, new()
        {
            SqlDbLiteClient DbLiteClient = GetCustomDB(config);
            return GetCustomEntityDB<T>(DbLiteClient);
        }
        #endregion

        #region 实例方法
        /// <summary>
        /// 获取数据库处理对象
        /// </summary>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDB<T>() where T : class, new()
        {
            return new SimpleClient<T>(_db);
        }

        /// <summary>
        /// 获取数据库处理对象
        /// </summary>
        /// <param name="db">db</param>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDB<T>(SqlDbLiteClient db) where T : class, new()
        {
            return new SimpleClient<T>(db);
        }

        #region 根据数据库表生产实体类
        /// <summary>
        /// 根据数据库表生产实体类
        /// </summary>       
        /// <param name="strPath">实体类存放路径</param>
        public void CreateClassFileByDBTalbe(string strPath)
        {
            CreateClassFileByDBTalbe(strPath, "Autumn.Entity");
        }

        /// <summary>
        /// 根据数据库表生产实体类
        /// </summary>
        /// <param name="strPath">实体类存放路径</param>
        /// <param name="strNameSpace">命名空间</param>
        public void CreateClassFileByDBTalbe(string strPath, string strNameSpace)
        {
            CreateClassFileByDBTalbe(strPath, strNameSpace, null);
        }

        /// <summary>
        /// 根据数据库表生产实体类
        /// </summary>
        /// <param name="strPath">实体类存放路径</param>
        /// <param name="strNameSpace">命名空间</param>
        /// <param name="lstTableNames">生产指定的表</param>
        public void CreateClassFileByDBTalbe(
            string strPath,
            string strNameSpace,
            string[] lstTableNames)
        {
            CreateClassFileByDBTalbe(strPath, strNameSpace, lstTableNames, string.Empty);
        }


        /// <summary>
        /// 根据数据库表生产实体类
        /// </summary>
        /// <param name="strPath">实体类存放路径</param>
        /// <param name="strNameSpace">命名空间</param>
        /// <param name="lstTableNames">生产指定的表</param>
        /// <param name="strInterface">实现接口</param>
        /// /// <param name="blnSerializable">可序列化</param>
        public void CreateClassFileByDBTalbe(
          string strPath,
          string strNameSpace,
          string[] lstTableNames,
          string strInterface,
          bool blnSerializable = false)
        {
            if (lstTableNames != null && lstTableNames.Length > 0)
            {
                _db.DbFirst.Where(lstTableNames).IsCreateDefaultValue().IsCreateAttribute()
                    .SettingClassTemplate(p => p = @"
{using}

namespace {Namespace}
{
    {ClassDescription}{DbLiteTable}" + (blnSerializable ? "\r\n    [Serializable]" : "") + @"
    public partial class {ClassName}" + (string.IsNullOrEmpty(strInterface) ? ":BaseEntity" : (" :BaseEntity, " + strInterface)) + @"
    {
        public {ClassName}()
        {
{Constructor}
        }
{PropertyName}
    }
}
")
                    .SettingPropertyTemplate(p => p = @"
        {DbLiteColumn}
        public {PropertyType} {PropertyName}{get;set;}")
                    //.SettingPropertyDescriptionTemplate(p => p = "          private {PropertyType} _{PropertyName};\r\n" + p)
                    //.SettingConstructorTemplate(p => p = "              this._{PropertyName} ={DefaultValue};")
                    .CreateClassFile(strPath, strNameSpace);
            }
            else
            {
                _db.DbFirst.IsCreateAttribute().IsCreateDefaultValue()
                    .SettingClassTemplate(p => p = @"
{using}

namespace {Namespace}
{
    {ClassDescription}{DbLiteTable}" + (blnSerializable ? "\r\n    [Serializable]" : "") + @"
    public partial class {ClassName}" + (string.IsNullOrEmpty(strInterface) ? ":BaseEntity" : (" :BaseEntity, " + strInterface)) + @"
    {
        public {ClassName}()
        {
{Constructor}
        }
{PropertyName}
    }
}
")
                    .SettingPropertyTemplate(p => p = @"
        {DbLiteColumn}
        public {PropertyType} {PropertyName}{get;set;}")
                    //.SettingPropertyDescriptionTemplate(p => p = "          private {PropertyType} _{PropertyName};\r\n" + p)
                    //.SettingConstructorTemplate(p => p = "              this._{PropertyName} ={DefaultValue};")
                    .CreateClassFile(strPath, strNameSpace);
            }
        }

        //        /// <summary>
        //        /// 根据数据库表生产实体类
        //        /// </summary>
        //        /// <param name="strPath">实体类存放路径</param>
        //        /// <param name="strNameSpace">命名空间</param>
        //        /// <param name="lstTableNames">生产指定的表</param>
        //        /// <param name="strInterface">实现接口</param>
        //        /// /// <param name="blnSerializable">可序列化</param>
        //        public void CreateClassFileByDBTalbe(
        //          string strPath,
        //          string strNameSpace,
        //          string[] lstTableNames,
        //          string strInterface,
        //          bool blnSerializable = false)
        //        {
        //            if (lstTableNames != null && lstTableNames.Length > 0)
        //            {
        //                _db.DbFirst.Where(lstTableNames).IsCreateDefaultValue().IsCreateAttribute()
        //                    .SettingClassTemplate(p => p = @"
        //{using}

        //namespace {Namespace}
        //{
        //    {ClassDescription}{DbLiteTable}" + (blnSerializable ? "[Serializable]" : "") + @"
        //    public partial class {ClassName}" + (string.IsNullOrEmpty(strInterface) ? "" : (" : " + strInterface)) + @"
        //    {
        //        public {ClassName}()
        //        {
        //{Constructor}
        //        }
        //{PropertyName}
        //    }
        //}
        //")
        //                    .SettingPropertyTemplate(p => p = @"
        //            {DbLiteColumn}
        //            public {PropertyType} {PropertyName}
        //            {
        //                get
        //                {
        //                    return _{PropertyName};
        //                }
        //                set
        //                {
        //                    if(_{PropertyName}!=value)
        //                    {
        //                        base.SetValueCall(" + "\"{PropertyName}\",_{PropertyName}" + @");
        //                    }
        //                    _{PropertyName}=value;
        //                }
        //            }")
        //                    .SettingPropertyDescriptionTemplate(p => p = "          private {PropertyType} _{PropertyName};\r\n" + p)
        //                    .SettingConstructorTemplate(p => p = "              this._{PropertyName} ={DefaultValue};")
        //                    .CreateClassFile(strPath, strNameSpace);
        //            }
        //            else
        //            {
        //                _db.DbFirst.IsCreateAttribute().IsCreateDefaultValue()
        //                    .SettingClassTemplate(p => p = @"
        //{using}

        //namespace {Namespace}
        //{
        //    {ClassDescription}{DbLiteTable}" + (blnSerializable ? "[Serializable]" : "") + @"
        //    public partial class {ClassName}" + (string.IsNullOrEmpty(strInterface) ? "" : (" : " + strInterface)) + @"
        //    {
        //        public {ClassName}()
        //        {
        //{Constructor}
        //        }
        //{PropertyName}
        //    }
        //}
        //")
        //                    .SettingPropertyTemplate(p => p = @"
        //            {DbLiteColumn}
        //            public {PropertyType} {PropertyName}
        //            {
        //                get
        //                {
        //                    return _{PropertyName};
        //                }
        //                set
        //                {
        //                    if(_{PropertyName}!=value)
        //                    {
        //                        base.SetValueCall(" + "\"{PropertyName}\",_{PropertyName}" + @");
        //                    }
        //                    _{PropertyName}=value;
        //                }
        //            }")
        //                    .SettingPropertyDescriptionTemplate(p => p = "          private {PropertyType} _{PropertyName};\r\n" + p)
        //                    .SettingConstructorTemplate(p => p = "              this._{PropertyName} ={DefaultValue};")
        //                    .CreateClassFile(strPath, strNameSpace);
        //            }
        //        }
        #endregion

        #region 根据实体类生成数据库表
        /// <summary>
        /// 根据实体类生成数据库表
        /// </summary>
        /// <param name="blnBackupTable">是否备份表</param>
        /// <param name="lstEntitys">指定的实体</param>
        public void CreateTableByEntity<T>(bool blnBackupTable, params T[] lstEntitys) where T : class, new()
        {
            Type[] lstTypes = null;
            if (lstEntitys != null)
            {
                lstTypes = new Type[lstEntitys.Length];
                for (int i = 0; i < lstEntitys.Length; i++)
                {
                    T t = lstEntitys[i];
                    lstTypes[i] = typeof(T);
                }
            }
            CreateTableByEntity(blnBackupTable, lstTypes);
        }

        /// <summary>
        /// 根据实体类生成数据库表
        /// </summary>
        /// <param name="blnBackupTable">是否备份表</param>
        /// <param name="lstEntitys">指定的实体</param>
        public void CreateTableByEntity(bool blnBackupTable, params Type[] lstEntitys)
        {
            if (blnBackupTable)
            {
                _db.CodeFirst.BackupTable().InitTables(lstEntitys); //change entity backupTable            
            }
            else
            {
                _db.CodeFirst.InitTables(lstEntitys);
            }
        }
        #endregion

        #endregion
    }
}
