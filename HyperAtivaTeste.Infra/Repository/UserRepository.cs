using Dapper;
using HyperAtivaTeste.Domains.Interfaces.Repository;
using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbDapperContext _db;

        public UserRepository(DbDapperContext db)
            => _db = db;

        public async Task<UserModel> GetUserByEmail(string email)
            => await _db.DapperConnection.QueryFirstOrDefaultAsync<UserModel>($"exec dbo.spGetUser '{email}';");
    }
}
