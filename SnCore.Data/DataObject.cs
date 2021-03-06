﻿using System;
    ///--------------------------------------------------------------------------------
    ///<summary>
    ///Persistent domain entity class representing 'DataObject' entities.
    ///</summary>
    ///<remarks>
    ///
    ///Mapping information:
    ///This class maps to the 'DataObject' table in the data source.
    ///</remarks>
    ///--------------------------------------------------------------------------------
    public class DataObject: IDbObject
    {
#region " Generated Code Region "

        private System.Int32 m_Id;
        private System.Collections.Generic.IList<Feature> m_Features;
        private System.String m_Name;
        private System.Collections.Generic.IList<Reminder> m_Reminders;
        private System.Collections.Generic.IList<MadLibInstance> m_MadLibInstances;
        private System.Collections.Generic.IList<AccountQuota> m_AccountQuotas;
        private System.Collections.Generic.IList<Discussion> m_Discussions;

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
        ///The property maps to the column 'DataObject_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public virtual System.Int32 Id
        {
            get
            {
                return m_Id;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent many-one reference property.
        ///</summary>
        ///<remarks>
        ///This property accepts multiple references to objects of the type 'Feature'.
        ///This property is part of a 'ManyToOne' relationship.
        ///The data type for this property is 'System.Collections.IList'.
        ///The inverse property for this property is 'Feature.DataObject'.
        ///This property inherits its mapping information from its inverse property.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Features' that holds the value for this property is 'PrivateAccess'.
        ///This property is marked as Read-Only.
        ///This property is marked as slave.
        ///
        ///Mapping information:
        ///This class maps to the 'Feature' table in the data source.
        ///The property maps to the identity column 'DataObject_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public virtual System.Collections.Generic.IList<Feature> Features
        {
            get
            {
                return m_Features;
            }
            set
            {
                m_Features = value;
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
        public virtual System.String Name
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
        ///Persistent many-one reference property.
        ///</summary>
        ///<remarks>
        ///This property accepts multiple references to objects of the type 'Reminder'.
        ///This property is part of a 'ManyToOne' relationship.
        ///The data type for this property is 'System.Collections.IList'.
        ///The inverse property for this property is 'Reminder.DataObject'.
        ///This property inherits its mapping information from its inverse property.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Reminders' that holds the value for this property is 'PrivateAccess'.
        ///This property is marked as Read-Only.
        ///This property is marked as slave.
        ///
        ///Mapping information:
        ///This class maps to the 'Reminder' table in the data source.
        ///The property maps to the identity column 'DataObject_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public virtual System.Collections.Generic.IList<Reminder> Reminders
        {
            get
            {
                return m_Reminders;
            }
            set
            {
                m_Reminders = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent many-one reference property.
        ///</summary>
        ///<remarks>
        ///This property accepts multiple references to objects of the type 'MadLibInstance'.
        ///This property is part of a 'ManyToOne' relationship.
        ///The data type for this property is 'System.Collections.IList'.
        ///The inverse property for this property is 'MadLibInstance.DataObject'.
        ///This property inherits its mapping information from its inverse property.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_MadLibInstances' that holds the value for this property is 'PrivateAccess'.
        ///This property is marked as slave.
        ///
        ///Mapping information:
        ///This class maps to the 'MadLibInstance' table in the data source.
        ///The property maps to the identity column 'DataObject_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public virtual System.Collections.Generic.IList<MadLibInstance> MadLibInstances
        {
            get
            {
                return m_MadLibInstances;
            }
            set
            {
                m_MadLibInstances = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent many-one reference property.
        ///</summary>
        ///<remarks>
        ///This property accepts multiple references to objects of the type 'AccountQuota'.
        ///This property is part of a 'ManyToOne' relationship.
        ///The data type for this property is 'System.Collections.IList'.
        ///The inverse property for this property is 'AccountQuota.DataObject'.
        ///This property inherits its mapping information from its inverse property.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_AccountQuotas' that holds the value for this property is 'PrivateAccess'.
        ///This property is marked as slave.
        ///
        ///Mapping information:
        ///This class maps to the 'AccountQuota' table in the data source.
        ///The property maps to the identity column 'DataObject_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public virtual System.Collections.Generic.IList<AccountQuota> AccountQuotas
        {
            get
            {
                return m_AccountQuotas;
            }
            set
            {
                m_AccountQuotas = value;
            }
        }

        ///--------------------------------------------------------------------------------
        ///<summary>
        ///Persistent many-one reference property.
        ///</summary>
        ///<remarks>
        ///This property accepts multiple references to objects of the type 'Discussion'.
        ///This property is part of a 'ManyToOne' relationship.
        ///The data type for this property is 'System.Collections.IList'.
        ///The inverse property for this property is 'Discussion.DataObject'.
        ///This property inherits its mapping information from its inverse property.
        ///The accessibility level for this property is 'PublicAccess'.
        ///The accessibility level for the field 'm_Discussions' that holds the value for this property is 'PrivateAccess'.
        ///This property is marked as slave.
        ///
        ///Mapping information:
        ///This class maps to the 'Discussion' table in the data source.
        ///The property maps to the identity column 'DataObject_Id' in the data source.
        ///</remarks>
        ///--------------------------------------------------------------------------------
        public virtual System.Collections.Generic.IList<Discussion> Discussions
        {
            get
            {
                return m_Discussions;
            }
            set
            {
                m_Discussions = value;
            }
        }

#endregion //Generated Code Region

#region " Synchronized Custom Code Region "
#endregion //Synchronized Custom Code Region

#region " Unsynchronized Custom Code Region "



#endregion //Unsynchronized Custom Code Region

    }
