using HyperAtivaTeste.Infra.Services;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Infra.Services
{
    public class AuthorizationServiceTest
    {
        public AuthorizationServiceTest() { }

        [Fact]
        public void GetUserIdLogged_TokenNull_Test()
            => Assert.Null(new AuthorizationService().GetUserIdLogged(""));

        [Fact]
        public void GetUserIdLogged_ValidToken_Test()
            => Assert.IsType<Guid>(new AuthorizationService().GetUserIdLogged("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJlYmMxYTE5OC0yNWRlLTRiN2YtYTcwMi01NjY4ZmY0ZDJjMzAiLCJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsIm5iZiI6MTY3NjM5MDE2NSwiZXhwIjoxNjc2Mzk3MzY1LCJpYXQiOjE2NzYzOTAxNjV9.m8kvuze5kXfL8XtKhzOs7PfvEjoFJZhEgycBLt_gIjQ"));

        [Fact]
        public void GetUserIdLogged_InvalidToken_Test()
            => Assert.Null(new AuthorizationService().GetUserIdLogged("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyR2V0SWQiOiJlYmMxYTE5OC0yNWRlLTRiN2YtYTcwMi01NjY4ZmY0ZDJjMzAiLCJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsIm5iZiI6MTY3NjM5MDM0MCwiZXhwIjoxNjc2Mzk3NTQwLCJpYXQiOjE2NzYzOTAzNDB9.bV_tfCGeTCtTX2a39Gup59gF_fDEt6P6LiaJvbvCRIA"));
    }
}
