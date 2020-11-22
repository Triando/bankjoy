using System.Threading.Tasks;
using Xunit;
using BankingApi.Controllers;
using BankingApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi
{
    public class InstitutionControllerTests
    {
        private InstitutionController GetInstitutionController(string name)
        {
            var options = new DbContextOptionsBuilder<InstitutionContext>()
            .UseInMemoryDatabase(databaseName: name)
            .Options;
            var context = new InstitutionContext(options);
            var controller = new InstitutionController(context);
            return controller;
        }

        private async Task<InstitutionController> GetInstitutionController(string name, Institution institution)
        {
            var options = new DbContextOptionsBuilder<InstitutionContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
            var context = new InstitutionContext(options);
            context.Institutions.Add(institution);
            await context.SaveChangesAsync();
            var controller = new InstitutionController(context);
            return controller;
        }

        [Fact]
        public async Task GetInstitutionsEmpty()
        {
            var controller = GetInstitutionController("testEmpty");
            var response = await controller.GetInstitutions();
            Assert.Empty(response.Value);
        }

        [Fact]
        public async Task GetInstitutionsSingle()
        {
            var institutionId = 123;
            var name = "First Institution";
            var institution = new Institution { InstitutionId = institutionId, Name = name };

            var controller = await GetInstitutionController("testSingle", institution);

            var actionResult = await controller.GetInstitutions();
            var value = actionResult.Value;

            Assert.Equal(institutionId, value.First().InstitutionId);
            Assert.Equal(name, value.First().Name); ;
        }

        [Fact]
        public async Task AddInstitution()
        {
            var controller = GetInstitutionController("testAdd");
            var name = "First Institution";
            var institution = new Institution { Name = name };

            var result = await controller.PostMember(institution);
 
            var actionResult = Assert.IsType<ActionResult<Institution>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Institution>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.InstitutionId);
            Assert.Equal(name, returnValue.Name);
        }

        [Fact]
        public async Task AddInstitutionNameRequired()
        {
            var controller = GetInstitutionController("nameRequired");
            var institution = new Institution { };
            var result = await controller.PostMember(institution);

            var actionResult = Assert.IsType<ActionResult<Institution>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Institution>(createdAtActionResult.Value);
            // TODO: Learn how to test valid model
        }
    }
}
