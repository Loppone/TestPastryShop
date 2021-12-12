using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PastryShopApi;
using PastryShopApi.BL.Abstract;
using PastryShopApi.BL.Concrete;
using PastryShopApi.DAL.Abstract;
using PastryShopApi.DAL.Concrete;
using System;

namespace TestPastryShop
{
    public class LoginTests
    {
        private IAuth _auth;
        private IRepoAuth _repoAuth;

        [SetUp]
        public void Setup()
        {
            var service = new ServiceCollection();
            service.AddAutoMapper(typeof(Startup));
            service.AddTransient<IAuth, AuthManager>();
            service.AddTransient<IRepoAuth, RepositoryAuthentication>();

            var cs = "Data Source=(localdb)\\MSSQLLocalDB;Database=PastryShop;Trusted_Connection=True;MultipleActiveResultSets=True";
            service.AddDbContext<PastryShopApi.ModelPastryShop.PastryShopContext>(opt =>
                opt.UseSqlServer(cs));

            var serviceProvider = service.BuildServiceProvider();
            _auth = serviceProvider.GetService<IAuth>();
            _repoAuth = serviceProvider.GetService<IRepoAuth>();
        }

        [Test]
        public void LoginValid()
        {
            var login = _auth.Login("lulu70", "luluuuu");
            Assert.AreEqual("Luana", login.Nome);
        }

        [Test]
        public void LoginUserOrPasswordNotValid()
        {
            var login = _auth.Login("lulu71", "luluuuu");
            Assert.IsNull(login);
        }
    }
}