using System;
    ///--------------------------------------------------------------------------------
    ///<summary>
    ///Persistent domain entity class representing 'AccountBlog' entities.
    ///</summary>
    ///<remarks>
    ///
    ///Mapping information:
    ///This class maps to the 'AccountBlog' table in the data source.
    ///</remarks>
    ///--------------------------------------------------------------------------------
    public class AccountBlog
    {
#region " : Generated Code Region "
        //Private field variables

        //Holds property values
        private System.Int32 m_Id;
        private Account m_Account;
        private System.Collections.IList m_AccountBlogAuthors;
        private System.Collections.IList m_AccountBlogPosts;
        private System.DateTime m_Created;
        private System.String m_Description;
        private System.String m_Name;
        private System.DateTime m_Updated;

        //Public properties
        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent primitive identity property.
        ///</summary>
        ///<remarks>
        ///This property is an identity property.
        ///The identity index for this property is '0'.
        ///This property accepts values of the type 'System.Int32'.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Id' that holds the value for this property is 'PrivateAccess'.
        ///
        ///Mapping information:
        ///The property maps to the column 'AccountBlog_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public  System.Int32 Id
        {
            get
            {
                return m_Id;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent one-many reference property.
        ///</summary>
        ///<remarks>
        ///This property accepts references to objects of the type 'Account'.
        ///This property is part of a 'OneToMany' relationship.
        ///The inverse property for this property is 'Account.AccountBlogs'.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Account' that holds the value for this property is 'PrivateAccess'.
        ///
        ///Mapping information:
        ///The property maps to the column 'Account_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public  Account Account
        {
            get
            {
                return m_Account;
            }
            set
            {
                m_Account = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent many-one reference property.
        ///</summary>
        ///<remarks>
        ///This property accepts multiple references to objects of the type 'AccountBlogAuthor'.
        ///This property is part of a 'ManyToOne' relationship.
        ///The data type for this property is 'System.Collections.IList'.
        ///The inverse property for this property is 'AccountBlogAuthor.AccountBlog'.
        ///This property inherits its mapping information from its inverse property.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_AccountBlogAuthors' that holds the value for this property is 'PrivateAccess'.
        ///This property is marked as Read-Only.
        ///
        ///Mapping information:
        ///This class maps to the 'AccountBlogAuthor' table in the data source.
        ///The property maps to the identity column 'AccountBlog_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public  System.Collections.IList AccountBlogAuthors
        {
            get
            {
                return m_AccountBlogAuthors;
            }
            set
            {
                m_AccountBlogAuthors = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent many-one reference property.
        ///</summary>
        ///<remarks>
        ///This property accepts multiple references to objects of the type 'AccountBlogPost'.
        ///This property is part of a 'ManyToOne' relationship.
        ///The data type for this property is 'System.Collections.IList'.
        ///The inverse property for this property is 'AccountBlogPost.AccountBlog'.
        ///This property inherits its mapping information from its inverse property.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_AccountBlogPosts' that holds the value for this property is 'PrivateAccess'.
        ///This property is marked as Read-Only.
        ///
        ///Mapping information:
        ///This class maps to the 'AccountBlogPost' table in the data source.
        ///The property maps to the identity column 'AccountBlog_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public  System.Collections.IList AccountBlogPosts
        {
            get
            {
                return m_AccountBlogPosts;
            }
            set
            {
                m_AccountBlogPosts = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent primitive property.
        ///</summary>
        ///<remarks>
        ///This property accepts values of the type 'System.DateTime'.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Created' that holds the value for this property is 'PrivateAccess'.
        ///
        ///Mapping information:
        ///The property maps to the column 'Created' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public  System.DateTime Created
        {
            get
            {
                return m_Created;
            }
            set
            {
                m_Created = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent primitive property.
        ///</summary>
        ///<remarks>
        ///This property accepts values of the type 'System.String'.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Description' that holds the value for this property is 'PrivateAccess'.
        ///
        ///Mapping information:
        ///The property maps to the column 'Description' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public  System.String Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent primitive property.
        ///</summary>
        ///<remarks>
        ///This property accepts values of the type 'System.String'.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Name' that holds the value for this property is 'PrivateAccess'.
        ///
        ///Mapping information:
        ///The property maps to the column 'Name' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public  System.String Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent primitive property.
        ///</summary>
        ///<remarks>
        ///This property accepts values of the type 'System.DateTime'.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Updated' that holds the value for this property is 'PrivateAccess'.
        ///
        ///Mapping information:
        ///The property maps to the column 'Updated' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public  System.DateTime Updated
        {
            get
            {
                return m_Updated;
            }
            set
            {
                m_Updated = value;
            }
        }

#endregion //: Generated Code Region

        //Add your synchronized custom code here:
#region " : Synchronized Custom Code Region "
#endregion //: Synchronized Custom Code Region

        //Add your unsynchronized custom code here:
#region " : Unsynchronized Custom Code Region "



#endregion //: Unsynchronized Custom Code Region

    }