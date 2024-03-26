## How To Run It
> 1、Tpf.Auth.Api 可 F5 运行 <br />
> 2、目前 BaseApiController 已添加 [Authorize] 以进行登陆验证，可自行注册、登录操作 or 取消 Authorize <br />

## Plan
> Done & Todo <br />
> 
> 即将开始 SqlSugar ⌛️

### 鉴权（Authentication）*
- [x] JWT
- [ ] OAuth 2.0
- [ ] OpenId
- [ ] Ids4

### 授权（Authorization）*
- [ ] RBAC

### 仓储（Generic Repository）*
- [x] EFCore / TpfDbContextBase
- [x] Dapper / IDapperRepository (80%)
- [x] MongoDB / IMongoDBRepository
- [ ] SqlSugar
- [ ] FreeSql

### 依赖注入（DI） *
- [x] Autofac

### 对象映射 （Objetc Mapper）*
- [x] AutoMapper
- [ ] Mapster
 
### 定时任务（Schedule Task）
- [ ] Quartz.Net
- [ ] Hangfire
- [ ] Timer

### 服务通信
- [x] gRpc / 提供 Tpf.Grpc.Client 与 Serve，帮助访问 gRpc 服务 和 发布 gRpc 服务
- [ ] Refit / 基于Http的服务间通信组件
- [ ] WebApiClient / 基于Http的服务间通信组件 / https://github.com/dotnetcore/WebApiClient
- [x] WebService / 基于 SoapCore


### 文件服务（File Servcie）
- [x] Minio
- [ ] Ftp

### API文档 （API Doc）
- [x] Swagger UI
- [x] knife4j / http://localhost:8000/index.html#/home [https://doc.xiaominfo.com/docs/quick-start](https://doc.xiaominfo.com/docs/quick-start) https://doc.xiaominfo.com/docs/action/dotnetcore-knife4j-guid
- [ ] fytapi.mui / [https://gitee.com/feiyit/fytapi.mui#%E4%BD%BF%E7%94%A8%E8%AF%B4%E6%98%8E](https://gitee.com/feiyit/fytapi.mui#%E4%BD%BF%E7%94%A8%E8%AF%B4%E6%98%8E)

### 微服务相关组件（Micro Service）
- [ ] Ocelot
- [ ] Nacos
- [ ] Console
- [ ] Neety

### DevOps
- [ ] Jenkins
- [ ] K8s

### 工具（Tools）
- [x] ConfigHelper
- [x] WebApiHelper
- [ ] 等


