using System.Threading.Tasks;
using Xunit;
using BankingApi.Controllers;
using BankingApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi
{
    public class MemberControllerTest
    {
        private MemberController GetMemberController(string dbName)
        {
            var options = new DbContextOptionsBuilder<MemberContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var context = new MemberContext(options);
            var controller = new MemberController(context);
            return controller;
        }

        private async Task<MemberController> GetMemberController(string dbName, Member member)
        {
            var options = new DbContextOptionsBuilder<MemberContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var context = new MemberContext(options);
            context.Members.Add(member);
            await context.SaveChangesAsync();
            var controller = new MemberController(context);
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
        public async Task GetMembersEmpty()
        {
            var controller = GetMemberController("empty");
            var response = await controller.GetMembers();
            Assert.Empty(response.Value);
        }

        [Fact]
        public async Task GetMembersEmptyMultiple()
        {
            var member1 = new Member { GivenName = "Steve", Surname = "Yzerman", InstitutionId = 1, MemberId = 19 };
            var member2 = new Member { GivenName = "Isiah", Surname = "Thomas", InstitutionId = 1, MemberId = 11 };
            var controller = await GetMemberController("multiple", member1);
            await controller.PostMember(member2);

            var actionResult = await controller.GetMembers();
            var value = actionResult.Value;

            Assert.Contains(member1, value);
            Assert.Contains(member2, value);
        }

        [Fact]
        public async Task GetMemberById()
        {
            var member1 = new Member { GivenName = "Steve", Surname = "Yzerman", InstitutionId = 1, MemberId = 19 };
            var member2 = new Member { GivenName = "Isiah", Surname = "Thomas", InstitutionId = 1, MemberId = 11 };
            var controller = await GetMemberController("getById", member1);
            await controller.PostMember(member2);

            var actionResult = await controller.GetMember(member1.MemberId);
            var value = actionResult.Value;

            Assert.Equal(member1.GivenName, value.GivenName);
            Assert.Equal(member1.Surname, value.Surname);
        }

        [Fact]
        public async Task VerifyInstitutionExists()
        {
            // TODO: Add test to verify institutionID is valid before adding Member
        }

        [Fact]
        public async Task UpdateBalanceAdd()
        {

        }

        [Fact]
        public async Task UpdateBalanceSubtract()
        {

        }

        [Fact]
        public async Task UpdateBalanceInsufficientFunds()
        {

        }
    }
}
