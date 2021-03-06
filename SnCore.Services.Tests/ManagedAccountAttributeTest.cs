using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace SnCore.Services.Tests
{
    [TestFixture]
    public class ManagedAccountAttributeTest : ManagedCRUDTest<AccountAttribute, TransitAccountAttribute, ManagedAccountAttribute>
    {
        private ManagedAttributeTest _attribute = new ManagedAttributeTest();
        private ManagedAccountTest _account = new ManagedAccountTest();

        public ManagedAccountAttributeTest()
        {

        }

        [SetUp]
        public override void SetUp()
        {
            _account.SetUp();
            _attribute.SetUp();
            base.SetUp();
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
            _attribute.TearDown();
            _account.TearDown();
        }

        public override TransitAccountAttribute GetTransitInstance()
        {
            TransitAccountAttribute t_instance = new TransitAccountAttribute();
            t_instance.AccountId = _account.Instance.Id;
            t_instance.AttributeId = _attribute.Instance.Id;
            return t_instance;
        }
    }
}
