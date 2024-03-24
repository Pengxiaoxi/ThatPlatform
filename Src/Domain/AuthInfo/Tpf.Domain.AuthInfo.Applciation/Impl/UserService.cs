using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Domain.AuthInfo.Applciation.Svc;
using Tpf.Domain.AuthInfo.Domain;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.AuthInfo.GrpcApplciation.Client.Dto;
using Tpf.Domain.AuthInfo.GrpcApplciation.Client.Svc;
using Tpf.Domain.Base.Application;
using Tpf.EntityFrameworkCore;
using Tpf.Grpc.Client;
using Tpf.Uow;
using Tpf.Utils;

namespace Tpf.Domain.AuthInfo.Applciation.Impl
{
    /// <summary>
    /// UserService
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UserService : BaseService<UserInfo>, IUserService
    {
        #region Field
        private readonly IGrpcService _grpcService;

        //private readonly IDapperRepository<UserInfo> _dapperRepository;

        //private readonly AuthInfoDbContext _dbContext;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public UserService(
            //ILogger<UserService> log

            //IBaseRepository<UserInfo> repository
            //AuthInfoDbContext dbContext
            //, IDapperRepository<UserInfo> dapperRepository
            IGrpcService grpcService
            , IMapper mapper
            , IUnitOfWork unitOfWork
            )
        {
            //_baseInfoDbContext = baseInfoDbContext;
            //_dapperRepository = dapperRepository;

            //_dbContext = dbContext;

            _grpcService = grpcService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        
        #endregion

        #region Public Method

        /// <summary>
        /// GetOrgByUserByGrpc
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetOrgByUserByGrpc()
        {
            var req = new GetOrgRequest() { OrgName = "pxx" };

            var orgGrpcServerAddress = ConfigHelper.Get("gRpc:Organization");
            var _client = _grpcService.GetClient<IOrganizationService>(orgGrpcServerAddress);
            var rsp = await _client.GetOrganization(req);

            System.Console.WriteLine(JsonConvert.SerializeObject(rsp)); ;
            return rsp;
        }

        public async Task<List<UserInfoOutputDto>> GetUserInfoList()
        {
            // inner join example
            //var leftJoinQuery = from user in _dbContext.UserInfos
            //                    join dept in _dbContext.Depts
            //                    on user.DeptId equals dept.Id into grouping
            //                    from dept in grouping.DefaultIfEmpty()
            //                    select new UserInfoOutputDto()
            //                    {
            //                        Account = user.Account,
            //                        UserName = user.UserName,
            //                        DeptName = dept.DeptName,
            //                    };
            //var leftJoinResult = leftJoinQuery.ToList();

            //using (var dbContext = new AuthInfoDbContext())
            //{
            //    // inner join example
            //    var innerJoinQuery = from user in dbContext.UserInfos
            //                         join dept in dbContext.Depts
            //                         on user.DeptId equals dept.Id
            //                         select new UserInfoOutputDto()
            //                         {
            //                             Account = user.Account,
            //                             UserName = user.UserName,
            //                             DeptName = dept.DeptName,
            //                         };
            //    var innerJoinResult = innerJoinQuery.ToList();

            //    //// left join example
            //    //var leftJoinQuery = from user in dbContext.UserInfos
            //    //            join dept in dbContext.Depts
            //    //            on user.DeptId equals dept.Id into grouping
            //    //            from dept in grouping.DefaultIfEmpty()
            //    //            select new UserInfoOutputDto()
            //    //            {
            //    //                Account = user.Account,
            //    //                UserName = user.UserName,
            //    //                DeptName = dept.DeptName,
            //    //            };
            //    //var leftJoinResult = leftJoinQuery.ToList();

            //    // Execute Sql
            //    //var selectSql = "";

            //    //dbContext.UserInfos.FromSqlRaw(selectSql);
            //    //dbContext.UserInfos.FromSqlInterpolated($"{selectSql}");

            //    //await dbContext.Database.ExecuteSqlRawAsync(selectSql);
            //    //await dbContext.Database.ExecuteSqlInterpolatedAsync($"{selectSql}");

            //    var result = await this.GetUserInfoListByDapper();

            //    //var userList = dbContext.UserInfos.ToList();
            //    return result;
            //}

            //var userList = await GetUserInfoListByDapper();

            var userList = await base.GetListAsync();

            var result = _mapper.Map<List<UserInfoOutputDto>>(userList);

            return result;

        }

        public async Task<bool> AddUser(UserInfo model)
        {
            var result = true;

            #region TODO: Uow
            //var trans = await _unitOfWork.BeginTransaction();
            //try
            //{
            //    result = await base.InsertAsync(model);

            //    await _unitOfWork.SaveChangesAsync();

            //    throw new Exception();

            //    await trans.CommitAsync();
            //}
            //catch (Exception ex)
            //{
            //    await trans.RollbackAsync();
            //    throw ex;
            //} 
            #endregion

            #region DbContext

            using var _context = new AuthInfoDbContext();
            using var _trans = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();


                throw new Exception();

                await _trans.CommitAsync();

                
            }
            catch
            {
                await _trans.RollbackAsync();
                throw;
            }
            #endregion

            return result;
        }


        #endregion

        /// <summary>
        /// Dapper 查询示例
        /// </summary>
        /// <returns></returns>
        private async Task<List<UserInfo>> GetUserInfoListByDapper()
        {
            //var querySql = $"SELECT `t`.`Account`, `t`.`UserName`, `t0`.`DeptName` FROM `base_user` AS `t` " +
            //                $"LEFT JOIN `tpf_dept` AS `t0` " +
            //                $"ON `t`.`DeptId` = `t0`.`Id`";

            //var querySql = $"SELECT `t`.* FROM `base_user` AS `t` ";
             
            //var result = (await _dapperRepository.Db.QueryAsync<UserInfo>(querySql)).ToList();

            var result = await base.GetListAsync();

            return result;
        }
    }
}
