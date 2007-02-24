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
using SnCore.Tools;
using SnCore.Data.Hibernate;

namespace SnCore.Services
{
    public class TransitDistinctPlaceNeighborhood
    {
        private string mName;

        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }

        private int mCount;

        public int Count
        {
            get
            {
                return mCount;
            }
            set
            {
                mCount = value;
            }
        }

        public TransitDistinctPlaceNeighborhood()
        {

        }
    }

    public class TransitPlaceQueryOptions
    {
        public string SortOrder = "Created";
        public bool SortAscending = false;
        public string Country;
        public string State;
        public string City;
        public string Neighborhood;
        public string Name;
        public string Type;
        public bool PicturesOnly = false;
        public int AccountId = 0;

        public override int GetHashCode()
        {
            return PersistentlyHashable.GetHashCode(this);
        }

        public TransitPlaceQueryOptions()
        {
        }

        public string CreateSubQuery()
        {
            StringBuilder b = new StringBuilder();

            if (!string.IsNullOrEmpty(Neighborhood))
            {
                b.Append(b.Length > 0 ? " AND " : " WHERE ");
                b.AppendFormat("Place.Neighborhood.Name = '{0}'", Renderer.SqlEncode(Neighborhood));
            }

            if (!string.IsNullOrEmpty(City))
            {
                b.Append(b.Length > 0 ? " AND " : " WHERE ");
                b.AppendFormat("Place.City.Name = '{0}'", Renderer.SqlEncode(City));
            }

            if (!string.IsNullOrEmpty(Country))
            {
                b.Append(b.Length > 0 ? " AND " : " WHERE ");
                b.AppendFormat("Place.City.Country.Name = '{0}'", Renderer.SqlEncode(Country));
            }

            if (!string.IsNullOrEmpty(State))
            {
                b.Append(b.Length > 0 ? " AND " : " WHERE ");
                b.AppendFormat("Place.City.State.Name = '{0}'", Renderer.SqlEncode(State));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                b.Append(b.Length > 0 ? " AND " : " WHERE ");
                b.AppendFormat("Place.Name LIKE '%{0}%'", Renderer.SqlEncode(Name));
            }

            if (!string.IsNullOrEmpty(Type))
            {
                b.Append(b.Length > 0 ? " AND " : " WHERE ");
                b.AppendFormat("Place.Type.Name = '{0}'", Renderer.SqlEncode(Type));
            }

            if (AccountId != 0)
            {
                b.Append(b.Length > 0 ? " AND " : " WHERE ");
                b.AppendFormat("Place.Account.Id = {0}", AccountId);
            }

            if (PicturesOnly)
            {
                b.Append(b.Length > 0 ? " AND " : " WHERE ");
                b.Append("EXISTS ELEMENTS(Place.PlacePictures)");
            }

            return b.ToString();
        }

        public string CreateCountQuery()
        {
            return CreateSubQuery();
        }

        public string CreateQuery()
        {
            StringBuilder b = new StringBuilder();
            b.Append("SELECT Place FROM Place Place");
            b.Append(CreateSubQuery());
            if (!string.IsNullOrEmpty(SortOrder))
            {
                b.AppendFormat(" ORDER BY Place.{0} {1}", SortOrder, SortAscending ? "ASC" : "DESC");
            }
            return b.ToString();
        }
    };

    [Serializable()]
    public class TransitPlace : TransitService<Place>
    {
        private string mName;

        public string Name
        {
            get
            {

                return mName;
            }
            set
            {
                mName = value;
            }
        }

        private string mType;

        public string Type
        {
            get
            {

                return mType;
            }
            set
            {
                mType = value;
            }
        }

        private string mDescription;

        public string Description
        {
            get
            {

                return mDescription;
            }
            set
            {
                mDescription = value;
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

        private string mStreet;

        public string Street
        {
            get
            {

                return mStreet;
            }
            set
            {
                mStreet = value;
            }
        }

        private string mZip;

        public string Zip
        {
            get
            {

                return mZip;
            }
            set
            {
                mZip = value;
            }
        }

        private string mCrossStreet;

        public string CrossStreet
        {
            get
            {

                return mCrossStreet;
            }
            set
            {
                mCrossStreet = value;
            }
        }

        private string mPhone;

        public string Phone
        {
            get
            {

                return mPhone;
            }
            set
            {
                mPhone = value;
            }
        }

        private string mFax;

        public string Fax
        {
            get
            {

                return mFax;
            }
            set
            {
                mFax = value;
            }
        }

        private string mEmail;

        public string Email
        {
            get
            {

                return mEmail;
            }
            set
            {
                mEmail = value;
            }
        }

        private string mWebsite;

        public string Website
        {
            get
            {

                return mWebsite;
            }
            set
            {
                mWebsite = value;
            }
        }

        private string mNeighborhood;

        public string Neighborhood
        {
            get
            {
                return mNeighborhood;
            }
            set
            {
                mNeighborhood = value;
            }
        }

        private string mCity;

        public string City
        {
            get
            {

                return mCity;
            }
            set
            {
                mCity = value;
            }
        }

        private string mState;

        public string State
        {
            get
            {

                return mState;
            }
            set
            {
                mState = value;
            }
        }

        private string mCountry;

        public string Country
        {
            get
            {

                return mCountry;
            }
            set
            {
                mCountry = value;
            }
        }

        private int mPictureId = 0;

        public int PictureId
        {
            get
            {

                return mPictureId;
            }
            set
            {
                mPictureId = value;
            }
        }

        private int mAccountId = 0;

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

        private bool mCanWrite = false;

        public bool CanWrite
        {
            get
            {
                return mCanWrite;
            }
            set
            {
                mCanWrite = value;
            }
        }

        public TransitPlace()
        {

        }

        public TransitPlace(Place value)
            : base(value)
        {

        }

        public override void SetInstance(Place instance)
        {
            Name = instance.Name;
            Type = instance.Type.Name;
            Description = instance.Description;
            Created = instance.Created;
            Modified = instance.Modified;
            Street = instance.Street;
            Zip = instance.Zip;
            CrossStreet = instance.CrossStreet;
            Phone = instance.Phone;
            Fax = instance.Fax;
            Email = instance.Email;
            Website = instance.Website;
            if (instance.City != null) City = instance.City.Name;
            if (instance.City != null && instance.City.State != null) State = instance.City.State.Name;
            if (instance.City != null && instance.City.Country != null) Country = instance.City.Country.Name;
            if (instance.Neighborhood != null) Neighborhood = instance.Neighborhood.Name;
            PictureId = ManagedService<PlacePicture, TransitPlacePicture>.GetRandomElementId(instance.PlacePictures);
            AccountId = instance.Account.Id;
            base.SetInstance(instance);
        }

        public override Place GetInstance(ISession session, ManagedSecurityContext sec)
        {
            Place instance = base.GetInstance(session, sec);
            instance.Name = this.Name;
            instance.Type = ManagedPlaceType.Find(session, this.Type);
            instance.Description = this.Description;
            instance.Street = this.Street;
            instance.Zip = this.Zip;
            instance.CrossStreet = this.CrossStreet;
            instance.Phone = this.Phone;
            instance.Fax = this.Fax;
            instance.Email = this.Email;
            instance.Website = this.Website;
            if (Id == 0) instance.Account = GetOwner(session, AccountId, sec);
            if (!string.IsNullOrEmpty(City)) instance.City = ManagedCity.FindOrCreate(session, City, State, Country);
            if (!string.IsNullOrEmpty(Neighborhood) && !string.IsNullOrEmpty(City)) instance.Neighborhood = ManagedNeighborhood.FindOrCreate(session, Neighborhood, City, State, Country);
            return instance;
        }
    }

    public class ManagedPlace : ManagedService<Place, TransitPlace>
    {
        public ManagedPlace()
        {

        }

        public ManagedPlace(ISession session)
            : base(session)
        {

        }

        public ManagedPlace(ISession session, int id)
            : base(session, id)
        {

        }

        public ManagedPlace(ISession session, Place value)
            : base(session, value)
        {

        }

        public override TransitPlace GetTransitInstance(ManagedSecurityContext sec)
        {
            TransitPlace instance = base.GetTransitInstance(sec);
            if (sec.Account != null) instance.CanWrite = CanWrite(sec.Account.Id);
            return instance;
        }

        public override void Delete(ManagedSecurityContext sec)
        {
            ManagedDiscussion.FindAndDelete(
                Session, mInstance.Account.Id, ManagedDiscussion.PlaceDiscussion, mInstance.Id, sec);

            foreach (PlacePicture pic in Collection<PlacePicture>.GetSafeCollection(mInstance.PlacePictures))
            {
                new ManagedPlacePicture(Session, pic).Delete(sec);
            }

            foreach (AccountEvent evt in Collection<AccountEvent>.GetSafeCollection(mInstance.AccountEvents))
            {
                new ManagedAccountEvent(Session, evt).Delete(sec);
            }

            Session.Delete(string.Format("FROM AccountPlace f WHERE f.Place.Id = {0}", Id));
            Session.Delete(string.Format("FROM AccountPlaceRequest f WHERE f.Place.Id = {0}", Id));
            Session.Delete(string.Format("FROM AccountPlaceFavorite f WHERE f.Place.Id = {0}", Id));
            Session.Delete(string.Format("FROM PlaceQueueItem q WHERE q.Place.Id = {0}", Id));
            ManagedFeature.Delete(Session, "Place", Id);
            base.Delete(sec);
        }

        public bool CanWrite(int accountid)
        {
            // person suggesting a place can write
            if (mInstance.Account.Id == accountid)
                return true;

            if (mInstance.AccountPlaces == null)
                return false;

            foreach (AccountPlace ap in Collection<AccountPlace>.GetSafeCollection(mInstance.AccountPlaces))
            {
                if (ap.Account.Id == accountid && ap.Type.CanWrite)
                {
                    return true;
                }
            }

            return false;
        }

        public void MigrateToAccount(Account newowner, ManagedSecurityContext sec)
        {
            // migrate pictures
            IList pictures = Session.CreateCriteria(typeof(PlacePicture))
                .Add(Expression.Eq("Account.Id", mInstance.Account.Id))
                .Add(Expression.Eq("Place.Id", mInstance.Id))
                .List();

            foreach (PlacePicture pp in pictures)
            {
                ManagedPlacePicture mpp = new ManagedPlacePicture(Session, pp);
                mpp.MigrateToAccount(newowner);
            }

            // migrate review discussion
            Discussion d = ManagedDiscussion.Find(
                Session, mInstance.Account.Id, ManagedDiscussion.PlaceDiscussion, mInstance.Id, sec);

            if (d != null)
            {
                ManagedDiscussion md = new ManagedDiscussion(Session, d);
                md.MigrateToAccount(newowner);
            }

            mInstance.Account = newowner;
            Session.Save(mInstance);
        }

        protected override void Save(ManagedSecurityContext sec)
        {
            mInstance.Modified = DateTime.UtcNow;
            if (mInstance.Id == 0) mInstance.Created = mInstance.Modified;
            base.Save(sec);
        }

        public override ACL GetACL()
        {
            ACL acl = base.GetACL();
            acl.Add(new ACLEveryoneAllowRetrieve());
            acl.Add(new ACLAuthenticatedAllowCreate());
            acl.Add(new ACLAccount(mInstance.Account, DataOperation.All));
            foreach (AccountPlace relationship in Collection<AccountPlace>.GetSafeCollection(mInstance.AccountPlaces))
            {
                acl.Add(new ACLAccount(relationship.Account,
                    relationship.Type.CanWrite ? DataOperation.Update : DataOperation.Retreive));
            }
            return acl;
        }

        protected override void Check(TransitPlace t_instance, ManagedSecurityContext sec)
        {
            base.Check(t_instance, sec);
            if (t_instance.Id == 0) sec.CheckVerifiedEmail();
        }
    }
}
