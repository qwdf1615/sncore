using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SnCore.BackEndServices;
using SnCore.Data.Tests;
using SnCore.Services;
using SnCore.Tools.Web;

namespace SnCore.BackEnd.Tests
{
    [TestFixture]
    public class SystemSyndicationServicesTest : NHibernateTest
    {
        private SystemSyndicationService service = null;

        [SetUp]
        public override void SetUp()
        {
            service = new SystemSyndicationService();
            base.SetUp();
        }

        [Test]
        public void TestSyndication()
        {
            service.RunSyndication(Session, ManagedAccount.GetAdminSecurityContext(Session));
        }

        [Test]
        public void TestCleanupImg()
        {
            service.RunCleanupImg(Session, ManagedAccount.GetAdminSecurityContext(Session));
        }

        [Test]
        public void TestCleanupMedia()
        {
            service.RunCleanupMedia(Session, ManagedAccount.GetAdminSecurityContext(Session));
        }
    }
}
