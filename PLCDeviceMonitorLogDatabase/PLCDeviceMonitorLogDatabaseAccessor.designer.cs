﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PLCDeviceMonitorLogDatabase
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PLCDeviceMonitorLogDatabase")]
	public partial class PLCDeviceMonitorLogDatabaseAccessorDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertLogEvent(LogEvent instance);
    partial void UpdateLogEvent(LogEvent instance);
    partial void DeleteLogEvent(LogEvent instance);
    #endregion
		
		public PLCDeviceMonitorLogDatabaseAccessorDataContext() : 
				base(global::PLCDeviceMonitorLogDatabase.Properties.Settings.Default.PLCDeviceMonitorLogDatabaseConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PLCDeviceMonitorLogDatabaseAccessorDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PLCDeviceMonitorLogDatabaseAccessorDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PLCDeviceMonitorLogDatabaseAccessorDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PLCDeviceMonitorLogDatabaseAccessorDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<LogEvent> LogEvents
		{
			get
			{
				return this.GetTable<LogEvent>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LogEvent")]
	public partial class LogEvent : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID;
		
		private System.DateTime _Time;
		
		private string _Level;
		
		private string _ThreadName;
		
		private string _Msg;
		
		private string _Exception;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(long value);
    partial void OnIDChanged();
    partial void OnTimeChanging(System.DateTime value);
    partial void OnTimeChanged();
    partial void OnLevelChanging(string value);
    partial void OnLevelChanged();
    partial void OnThreadNameChanging(string value);
    partial void OnThreadNameChanged();
    partial void OnMsgChanging(string value);
    partial void OnMsgChanged();
    partial void OnExceptionChanging(string value);
    partial void OnExceptionChanged();
    #endregion
		
		public LogEvent()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Time", DbType="DateTime NOT NULL")]
		public System.DateTime Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				if ((this._Time != value))
				{
					this.OnTimeChanging(value);
					this.SendPropertyChanging();
					this._Time = value;
					this.SendPropertyChanged("Time");
					this.OnTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Level]", Storage="_Level", DbType="NChar(8) NOT NULL", CanBeNull=false)]
		public string Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if ((this._Level != value))
				{
					this.OnLevelChanging(value);
					this.SendPropertyChanging();
					this._Level = value;
					this.SendPropertyChanged("Level");
					this.OnLevelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ThreadName", DbType="NChar(16) NOT NULL", CanBeNull=false)]
		public string ThreadName
		{
			get
			{
				return this._ThreadName;
			}
			set
			{
				if ((this._ThreadName != value))
				{
					this.OnThreadNameChanging(value);
					this.SendPropertyChanging();
					this._ThreadName = value;
					this.SendPropertyChanged("ThreadName");
					this.OnThreadNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Msg", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Msg
		{
			get
			{
				return this._Msg;
			}
			set
			{
				if ((this._Msg != value))
				{
					this.OnMsgChanging(value);
					this.SendPropertyChanging();
					this._Msg = value;
					this.SendPropertyChanged("Msg");
					this.OnMsgChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Exception", DbType="NVarChar(MAX)")]
		public string Exception
		{
			get
			{
				return this._Exception;
			}
			set
			{
				if ((this._Exception != value))
				{
					this.OnExceptionChanging(value);
					this.SendPropertyChanging();
					this._Exception = value;
					this.SendPropertyChanged("Exception");
					this.OnExceptionChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
