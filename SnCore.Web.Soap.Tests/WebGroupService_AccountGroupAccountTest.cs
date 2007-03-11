using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Web.Services.Protocols;
using SnCore.Web.Soap.Tests.WebAccountServiceTests;

namespace SnCore.Web.Soap.Tests.WebGroupServiceTests
{
    [TestFixture]
    public class AccountGroupAccountTest : WebServiceTest<WebGroupService.TransitAccountGroupAccount, WebGroupServiceNoCache>
    {
        private AccountGroupTest _group = new AccountGroupTest();
        private int _group_id = 0;
        private AccountTest _account = new AccountTest();
        private int _account_id = 0;

        [SetUp]
        public override void SetUp()
        {
            _group.SetUp();
            _group_id = _group.Create(GetAdminTicket());
            _account.SetUp();
            _account_id = _account.Create(GetAdminTicket());
        }

        [TearDown]
        public override void TearDown()
        {
            _account.Delete(GetAdminTicket(), _account_id);
            _account.TearDown();
            _group.Delete(GetAdminTicket(), _group_id);
            _group.TearDown();
        }

        public override object[] GetCountArgs(string ticket)
        {
            object[] args = { ticket, _group_id };
            return args;
        }

        public override object[] GetArgs(string ticket, object options)
        {
            object[] args = { ticket, _group_id, options };
            return args;
        }

        public AccountGroupAccountTest()
            : base("AccountGroupAccount")
        {

        }

        public override WebGroupService.TransitAccountGroupAccount GetTransitInstance()
        {
            WebGroupService.TransitAccountGroupAccount t_instance = new WebGroupService.TransitAccountGroupAccount();
            t_instance.AccountGroupId = _group_id;
            t_instance.AccountId = _account_id;
            t_instance.IsAdministrator = true;
            return t_instance;
        }

        [Test]
        protected void GetAccountGroupAccountsByAccountIdTest()
        {

        }

        [Test]
        protected void GetAccountGroupAccountByAccountGroupIdTest()
        {

        }
    }
}
