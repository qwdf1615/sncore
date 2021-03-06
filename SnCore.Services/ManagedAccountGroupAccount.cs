using System;
using NHibernate;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using NHibernate.Expression;
using System.Web.Services.Protocols;
using System.Xml;
using System.Resources;
using System.Net.Mail;
using System.IO;
using SnCore.Tools.Web;
using System.Collections.Generic;
using System.Web;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Net;
using SnCore.Data.Hibernate;

namespace SnCore.Services
{
    public class TransitAccountGroupAccount : TransitService<AccountGroupAccount>
    {
        private int mAccountId;

        public int AccountId
        {
            get
            {

                return mAccountId;
            }
            set
            {
                mAccountId = value;
            }
        }

        private int mAccountPictureId;

        public int AccountPictureId
        {
            get
            {

                return mAccountPictureId;
            }
            set
            {
                mAccountPictureId = value;
            }
        }

        private string mAccountName;

        public string AccountName
        {
            get
            {

                return mAccountName;
            }
            set
            {
                mAccountName = value;
            }
        }

        private int mAccountGroupId;

        public int AccountGroupId
        {
            get
            {
                return mAccountGroupId;
            }
            set
            {
                mAccountGroupId = value;
            }
        }

        private string mAccountGroupName;

        public string AccountGroupName
        {
            get
            {
                return mAccountGroupName;
            }
            set
            {
                mAccountGroupName = value;
            }
        }

        private int mAccountGroupPictureId;

        public int AccountGroupPictureId
        {
            get
            {
                return mAccountGroupPictureId;
            }
            set
            {
                mAccountGroupPictureId = value;
            }
        }

        private bool mIsAdministrator = false;

        public bool IsAdministrator
        {
            get
            {
                return mIsAdministrator;
            }
            set
            {
                mIsAdministrator = value;
            }
        }

        private DateTime mCreated;

        public DateTime Created
        {
            get
            {
                return mCreated;
            }
            set
            {
                mCreated = value;
            }
        }

        private DateTime mModified;

        public DateTime Modified
        {
            get
            {
                return mModified;
            }
            set
            {
                mModified = value;
            }
        }

        public TransitAccountGroupAccount()
        {

        }

        public TransitAccountGroupAccount(AccountGroupAccount value)
            : base(value)
        {

        }

        public override void SetInstance(AccountGroupAccount value)
        {
            AccountId = value.Account.Id;
            AccountName = value.Account.Name;
            AccountPictureId = ManagedAccount.GetRandomAccountPictureId(value.Account);
            if (! value.AccountGroup.IsPrivate) AccountGroupPictureId = ManagedAccountGroup.GetRandomAccountGroupPictureId(value.AccountGroup);
            AccountGroupId = value.AccountGroup.Id;
            AccountGroupName = value.AccountGroup.Name;
            IsAdministrator = value.IsAdministrator;
            Created = value.Created;
            Modified = value.Modified;
            base.SetInstance(value);
        }

        public override AccountGroupAccount GetInstance(ISession session, ManagedSecurityContext sec)
        {
            AccountGroupAccount instance = base.GetInstance(session, sec);

            if (Id == 0)
            {
                instance.Account = GetOwner(session, AccountId, sec);
                instance.AccountGroup = session.Load<AccountGroup>(AccountGroupId);
            }

            instance.IsAdministrator = IsAdministrator;
            return instance;
        }
    }

    public class ManagedAccountGroupAccount : ManagedAuditableService<AccountGroupAccount, TransitAccountGroupAccount>
    {
        public ManagedAccountGroupAccount()
        {

        }

        public ManagedAccountGroupAccount(ISession session)
            : base(session)
        {

        }

        public ManagedAccountGroupAccount(ISession session, int id)
            : base(session, id)
        {

        }

        public ManagedAccountGroupAccount(ISession session, AccountGroupAccount value)
            : base(session, value)
        {

        }

        protected override void Save(ManagedSecurityContext sec)
        {
            mInstance.Modified = DateTime.UtcNow;
            if (mInstance.Id == 0) mInstance.Created = mInstance.Modified;
            base.Save(sec);
        }

        public override ACL GetACL(Type type)
        {
            ACL acl = base.GetACL(type);
            // everyone is able to see this membership if the group is public
            if (!mInstance.AccountGroup.IsPrivate) acl.Add(new ACLEveryoneAllowRetrieve());
            // everyone is able to join the group if the group is public
            if (!mInstance.AccountGroup.IsPrivate) acl.Add(new ACLAuthenticatedAllowCreate());
            // member can remove and read himself from the group
            acl.Add(new ACLAccount(mInstance.Account, DataOperation.Delete | DataOperation.Retreive));
            // members can edit or see the membership depending on their permissions
            foreach (AccountGroupAccount account in Collection<AccountGroupAccount>.GetSafeCollection(
                mInstance.AccountGroup.AccountGroupAccounts))
            {
                acl.Add(new ACLAccount(account.Account, account.IsAdministrator
                    ? DataOperation.All
                    : DataOperation.Retreive));
            }
            // members with pending invitations can accept it
            foreach (AccountGroupAccountInvitation invitation in Collection<AccountGroupAccountInvitation>.GetSafeCollection(
                mInstance.AccountGroup.AccountGroupAccountInvitations))
            {
                acl.Add(new ACLAccount(invitation.Account, DataOperation.Create));
            }
            return acl;
        }

        public override void Delete(ManagedSecurityContext sec)
        {
            ManagedAccountGroup m_group = new ManagedAccountGroup(Session, mInstance.AccountGroup);
            m_group.Leave(mInstance.Account.Id, sec);
            // delete group subscription
            AccountRssWatch watch = GetGroupRssWatch();
            if (watch != null) Session.Delete(watch);
            base.Delete(sec);
        }

        private AccountRssWatch GetGroupRssWatch()
        {
            return Session.CreateCriteria(typeof(AccountRssWatch))
                .Add(Expression.Eq("Account.Id", mInstance.Account.Id))
                .Add(Expression.Eq("Url", string.Format("AccountGroupRss.aspx?id={0}", mInstance.AccountGroup.Id)))
                .UniqueResult<AccountRssWatch>();
        }

        public override int CreateOrUpdate(TransitAccountGroupAccount t_instance, ManagedSecurityContext sec)
        {
            ManagedAccountGroup m_group = new ManagedAccountGroup(Session, t_instance.AccountGroupId);

            // check whether the user is already a member
            if (t_instance.Id == 0 && m_group.HasAccount(t_instance.AccountId))
            {
                throw new Exception(string.Format(
                    "You are already a member of \"{0}\".", m_group.Instance.Name));
            }

            // ensure that user isn't trying to self promote
            if (t_instance.Id > 0 && t_instance.IsAdministrator)
            {
                // the caller isn't a group admin
                if (!m_group.HasAdministratorAccount(sec.Account.Id))
                    throw new ManagedAccount.AccessDeniedException();
            }

            // ensure that not removing last admin
            if (t_instance.Id > 0 && !t_instance.IsAdministrator)
            {
                AccountGroupAccount existing_instance = Session.Load<AccountGroupAccount>(t_instance.Id);
                if (existing_instance.IsAdministrator)
                {
                    bool fHasAnotherAdmin = false;
                    foreach (AccountGroupAccount instance in existing_instance.AccountGroup.AccountGroupAccounts)
                    {
                        if (instance.IsAdministrator && instance != existing_instance)
                        {
                            fHasAnotherAdmin = true;
                            break;
                        }
                    }

                    if (!fHasAnotherAdmin)
                    {
                        throw new Exception("Cannot demote the last group administrator.");
                    }
                }
            }

            int id = base.CreateOrUpdate(t_instance, sec);

            if (t_instance.Id == 0)
            {
                if (mInstance.AccountGroup.AccountGroupAccounts == null)
                    mInstance.AccountGroup.AccountGroupAccounts = new List<AccountGroupAccount>();
                mInstance.AccountGroup.AccountGroupAccounts.Add(mInstance);

                // subscribe the user to a group feed
                TransitAccountRssWatch t_watch = new TransitAccountRssWatch();
                t_watch.UpdateFrequency = 24;
                t_watch.Name = mInstance.AccountGroup.Name;
                t_watch.Url = string.Format("AccountGroupRss.aspx?id={0}", mInstance.AccountGroup.Id);
                t_watch.Enabled = true;
                t_watch.AccountId = mInstance.Account.Id;
                ManagedAccountRssWatch m_watch = new ManagedAccountRssWatch(Session);
                m_watch.CreateOrUpdate(t_watch, ManagedAccount.GetUserSecurityContext(Session, mInstance.Account.Id));
                Session.Flush();

                // e-mail the user a welcome message
                ManagedAccount recepient = new ManagedAccount(Session, mInstance.Account);
                ManagedSiteConnector.TrySendAccountEmailMessageUriAsAdmin(Session, recepient,
                    string.Format("EmailAccountGroupAccount.aspx?id={0}",
                    id));
            }

            return id;
        }

        public override IList<AccountAuditEntry> CreateAccountAuditEntries(ISession session, ManagedSecurityContext sec, DataOperation op)
        {
            List<AccountAuditEntry> result = new List<AccountAuditEntry>();
            switch (op)
            {
                case DataOperation.Create:
                    result.Add(ManagedAccountAuditEntry.CreatePublicAccountAuditEntry(session, mInstance.Account,
                        string.Format("[user:{0}] joined [group:{1}]",
                        mInstance.Account.Id, mInstance.AccountGroup.Id),
                        string.Format("AccountGroupView.aspx?id={0}", mInstance.AccountGroup.Id)));
                    break;
            }
            return result;
        }
    }
}