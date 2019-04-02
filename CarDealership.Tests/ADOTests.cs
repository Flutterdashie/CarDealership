using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Repositories;
using CarDealership.Models.ServiceModels;

namespace CarDealership.Tests
{
    [TestFixture]
    public class ADOTests
    {
        //Please bear in mind that this ONLY tests ADO-based methods, NOT Entity Framework. 
        //Also, with the current state of the database, and how it is recreated (through entityframework in this case) I have to use specific ids, which change whenever the database is reset.
        [TestCase("notarealid",false)]
        [TestCase("4bb55121-4d58-4118-a056-6963f50ffac9",true)]
        public void GetUserTest(string id, bool expected)
        {
            var secRepo = new SecurityHandlerProd();
            UserView resultUser = secRepo.GetUser(id);
            bool actual = (resultUser != null);
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void GetAllUsersTest()
        {
            var secRepo = new SecurityHandlerProd();
            var result = secRepo.GetAllUsers();
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetSalesUsersTest()
        {
            var secRepo = new SecurityHandlerProd();
            var result = secRepo.GetSalesUsers();
            Assert.IsNotEmpty(result);
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        public void GetVehicleByIDTest(int id, bool expected)
        {
            var repo = new DataHandlerEF();
            var result = repo.GetVehicleByID(id);
            bool actual = (result != null);
            Assert.AreEqual(expected,actual);
        }
    }
}
