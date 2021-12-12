using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PastryShopApi;
using PastryShopApi.BL.Abstract;
using PastryShopApi.BL.Concrete;
using PastryShopApi.DAL.Abstract;
using PastryShopApi.DAL.Concrete;
using PastryShopApi.ModelPastryShop;
using System;

namespace TestPastryShop
{
    public class VetrinaTests
    {
        private IVetrina _vetrina;

        [SetUp]
        public void Setup()
        {
            var service = new ServiceCollection();
            service.AddAutoMapper(typeof(Startup));
            service.AddTransient<IVetrina, VetrinaOperations>();
            service.AddTransient(typeof(IReader<DolciInVenditum>), typeof(RepositoryVetrinaReader));
            service.AddTransient<IVetrinaDolciWriter, RepositoryVetrinaWriter>();
            service.AddTransient(typeof(IReader<Dolce>), typeof(RepositoryDolceReader));

            var cs = "Data Source=(localdb)\\MSSQLLocalDB;Database=PastryShop;Trusted_Connection=True;MultipleActiveResultSets=True";
            service.AddDbContext<PastryShopApi.ModelPastryShop.PastryShopContext>(opt =>
                opt.UseSqlServer(cs));

            var serviceProvider = service.BuildServiceProvider();
            _vetrina = serviceProvider.GetService<IVetrina>();
        }

        [Test]
        public void AddValidDolceInVetrina()
        {
            var d = _vetrina.AggiungiDolce(new PastryShopApi.BL.Models.DolceInVenditaInsert()
            {
                Disponibilita = 10,
                IdDolce = 1
            });
        }

        [Test]
        public void LoginUserOrPasswordNotValid()
        {

        }
    }
}