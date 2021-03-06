using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Web.Services.Protocols;

namespace SnCore.Web.Soap.Tests.WebSyndicationServiceTests
{
    [TestFixture]
    public class AccountFeedItemTest : WebServiceTest<WebSyndicationService.TransitAccountFeedItem, WebSyndicationServiceNoCache>
    {
        private AccountFeedTest _accountfeed = new AccountFeedTest();
        private int _accountfeed_id = 0;
        private UserInfo _user = null;

        [SetUp]
        public override void SetUp()
        {
            _user = CreateUserWithVerifiedEmailAddress();
            _accountfeed.SetUp();
            _accountfeed_id = _accountfeed.Create(GetAdminTicket());
        }

        [TearDown]
        public override void TearDown()
        {
            _accountfeed.Delete(GetAdminTicket(), _accountfeed_id);
            _accountfeed.TearDown();
            DeleteUser(_user.id);
        }

        public AccountFeedItemTest()
            : base("AccountFeedItem")
        {

        }

        public override object[] GetCountArgs(string ticket)
        {
            object[] args = { ticket, _accountfeed_id };
            return args;
        }

        public override object[] GetArgs(string ticket, object options)
        {
            object[] args = { ticket, _accountfeed_id, options };
            return args;
        }

        public override WebSyndicationService.TransitAccountFeedItem GetTransitInstance()
        {
            WebSyndicationService.TransitAccountFeedItem t_instance = new WebSyndicationService.TransitAccountFeedItem();
            t_instance.AccountFeedId = _accountfeed_id;
            t_instance.AccountId = _user.id;
            t_instance.Description = GetNewString();
            t_instance.Guid = GetNewString();
            t_instance.Link = GetNewUri();
            t_instance.Title = GetNewString();
            return t_instance;
        }

        [Test]
        public void SearchAccountFeedItemsEmptyTest()
        {
            WebSyndicationService.TransitAccountFeedItem[] items = EndPoint.SearchAccountFeedItems(
                _user.ticket, string.Empty, null);
            Assert.AreEqual(0, items.Length);
        }

        [Test]
        public void SearchAccountFeedItemsTest()
        {
            string s = GetNewString();
            int count = EndPoint.SearchAccountFeedItemsCount(_user.ticket, s);
            Assert.IsTrue(count >= 0);
            WebSyndicationService.TransitAccountFeedItem[] items = EndPoint.SearchAccountFeedItems(_user.ticket, s, null);
            Assert.IsNotNull(items);
            Console.WriteLine("Feed items: {0}", items.Length);
        }

        [Test]
        public void GetAllAccountFeedItemsTest()
        {
            WebSyndicationService.TransitAccountFeedItemQueryOptions options = new WebSyndicationService.TransitAccountFeedItemQueryOptions();
            int count = EndPoint.GetAllAccountFeedItemsCount(GetAdminTicket(), options);
            Console.WriteLine("Count: {0}", count);
            WebSyndicationService.TransitAccountFeedItem[] items = EndPoint.GetAllAccountFeedItems(GetAdminTicket(), options, null);
            Console.WriteLine("Length: {0}", items.Length);
            Assert.AreEqual(count, items.Length);
        }

        [Test]
        protected void GetLatestAccountFeedItemFeatureByAccountFeedIdTest()
        {

        }
    }
}
