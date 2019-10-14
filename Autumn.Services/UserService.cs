using System;
using AutoMapper;
using Autumn.Model;
using Autumn.Services.BASE;
using Autumn.IServices;
using System.Threading.Tasks;
using Autumn.IRepository;
using Autumn.FrameWork;

namespace Autumn.Services
{
    /// <summary>
    /// 用户
    /// </summary>	
    public class UserService : BaseService<S01_User>, IUserService
    {
        /// <summary>
        /// 匹配Entity中的字段
        /// </summary>
        IMapper _mapper;

        /// <summary>
        /// 需要使用其他服务请在此构造时注入
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="mapper"></param>
        public UserService(IUserRepository dal, IMapper mapper)
        {
            BaseDal = dal;
            _mapper = mapper;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="userCode">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public async Task<UserModel> GetUser(string userCode, string Password="")
        {
            UserModel userModel = new UserModel();

            var demoList = await Query(a => a.S01_UserCode == userCode && a.S01_Password == Password);

            if (demoList.Count > 0)
            {
                //userModel = _mapper.Map<UserModel>(demoList);
                userModel.UserId = demoList[0].S01_UserId;
                userModel.UserName = demoList[0].S01_UserName;
                userModel.RoleIds = demoList[0].S02_RoleIds;
                //userModel.Urls = "user/getuser|user/getrole";
            }
            return userModel;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<int> InsertUser(UserModel userModel)
        {
            S01_User user = new S01_User();
            user.S01_UserCode = userModel.UserCode;
            user.S01_Password = userModel.UserName;
            user.S01_IsValid = 0;
            user.S01_CreateId = 9;
            user.S01_CreateBy = "user";
            user.S01_CreateTime = DateTime.Now;
            return await Insert(user);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UserModel userModel)
        {
            S01_User user = new S01_User();
            user.S01_Remarks = "备注";
            return await Update(user);
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<bool> TransExecuteUser(UserModel userModel)
        {
            Func<Task<bool>> action = async () =>
            {
                S01_User user = new S01_User();
                user.S01_Remarks = "备注2";
                return await Update(user);
            };

            action += async () =>
            {
                S01_User user = new S01_User();
                user.S01_UserCode = userModel.UserCode;
                user.S01_Password = userModel.UserName;
                user.S01_IsValid = 0;
                user.S01_CreateId = 9;
                user.S01_CreateBy = "user";
                user.S01_CreateTime = DateTime.Now;
                return await Insert(user) > 0 ? true : false;
            };

            return await ActionTran(action);
        }

        /// <summary>
        /// 附加其他实例
        /// </summary>
        private void TestDemo()
        {
            // 原始SQL语句
            //var dt = Db.Ado.GetDataTable("select * from table where id=@id and name=@name", new List<DbLiteParameter>(){
            //new DbLiteParameter("@id",1),
            //new DbLiteParameter("@name",2)
            //});

            // TOP20
            //var top20 = Db.Queryable<Student>().Take(20).ToList();

            // 前20剩余数据
            //var skip20 = db.Queryable<Student>().Skip(20).ToList();

            // 三表查询并且返回d和d1的完整对象集合
            //var list = Db.Queryable<Demo, Demo1, Demo2>((d, d1, d2) => new object[] {
            //JoinType.Left,d.Id==d1.Id,
            //JoinType.Left,d1.Id2==d2.Id
            // })
            //.Where((d, d1, d2) => d2.Id == 1 || d1.Id == 1 || d.Id == 1)
            //.OrderBy((d) => d.Id)
            //.OrderBy((d, d1) => d.Name, OrderByType.Desc)
            //.Select((d, d1, d2) => new { d = d, d1 = d1 }).ToList();

            // 分组查询
            //var group = Db.Queryable<Student>().GroupBy(it => it.Id)
            //.Having(it => SqlFunc.AggregateCount(it.Id) > 10)
            //.Select(it => new { id = SqlFunc.AggregateCount(it.Id) }).ToList();

            // 去重
            //var list3 = Db.Queryable<Student>()
            //.GroupBy(it => new { it.Id, it.Name }).Select(it => new { it.id, it.Name }).ToList();
            //效果性能优于 select distinct id,name from student

            // 自定义条件更新自定义列
            //var update = Db.Updateable(updateObj).UpdateColumns(s => new { s.RowStatus, s.Id }).WhereColumns(it => new { it.Id });

            //根据不同条件执行更新不同的列
            //var t3 = Db.Updateable(updateObj)
            //.UpdateColumnsIF(caseValue == "1", it => new { it.Name })
            //.UpdateColumnsIF(caseValue == "2", it => new { it.Name, it.CreateTime })
            //.ExecuteCommand();

            // 保存或修改 根据主键判段是否存在，如果存在则更新，不存在则插入，支持批量操作
            //主键存在库的情况
            //Db.Saveable<Student>(entity).ExecuteReturnEntity();
            //效果 UPDATE [STudent]  SET
            //[SchoolId]=@SchoolId,[Name]=@Name,[CreateTime]=@CreateTime WHERE[Id] = @Id 

            //主键不存在库的情况
            //Db.Saveable<Student>(new Student() { Name = "" }).ExecuteReturnEntity();
            //效果 INSERT INTO[STudent]
            //([SchoolId],[Name],[CreateTime])
            // VALUES
            //(@SchoolId, @Name, @CreateTime); SELECT SCOPE_IDENTITY();

            //可以设置插入过滤和指定列
            //Db.Saveable<Student>(new Student() { Name = "" }).InsertColumns(it => it.Name).ExecuteReturnEntity();
            //Db.Saveable<Student>(new Student() { Name = "" }).InsertIgnoreColumns(it => it.SchoolId).ExecuteReturnEntity();

            //也可以设置更新过滤和指定列
            //Db.Saveable<Student>(entity).UpdateIgnoreColumns(it => it.SchoolId).ExecuteReturnEntity();
            //Db.Saveable<Student>(entity).UpdateColumns(it => new { it.Name, it.CreateTime }).ExecuteReturnEntity();

            //插入并返回自增列用ExecuteReutrnIdentity
            //int t30 = Db.Insertable(insertObj).ExecuteReturnIdentity();
            //long t31 = Db.Insertable(insertObj).ExecuteReturnBigIdentity();

            // 高效批量插入
            //var insertObjs = new List<Student>();
            //var s9 = Db.Insertable(insertObjs.ToArray()).ExecuteCommand();

            //存储过程
            //var dt2 = Db.Ado.UseStoredProcedure().GetDataTable("sp_school", new { name = "张三", age = 0 });//  GetInt SqlQuery<T>  等等都可以用
            ////支持output
            //var nameP = new DbLiteParameter("@name", "张三");
            //var ageP = new DbLiteParameter("@age", null, true);//isOutput=true
            //var dt2 = Db.Ado.UseStoredProcedure().GetDataTable("sp_school", nameP, ageP);
            ////ageP.value可以拿到返回值

            //SQL调试
            //var sql = Db.Queryable<T>().ToSql();
        }
    }
}
