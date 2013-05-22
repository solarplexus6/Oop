﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.18033
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="C:\\Users\\solarplexus\\AppData\\Local\\Temp\\Database1.sdf")]
public partial class Database1Context : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertNumber(Number instance);
  partial void UpdateNumber(Number instance);
  partial void DeleteNumber(Number instance);
  #endregion
	
	public Database1Context(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Database1Context(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Database1Context(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Database1Context(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<Number> Numbers
	{
		get
		{
			return this.GetTable<Number>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="Numbers")]
public partial class Number : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private System.Guid _Id;
	
	private int _Number1;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnNumber1Changing(int value);
    partial void OnNumber1Changed();
    #endregion
	
	public Number()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
	public System.Guid Id
	{
		get
		{
			return this._Id;
		}
		set
		{
			if ((this._Id != value))
			{
				this.OnIdChanging(value);
				this.SendPropertyChanging();
				this._Id = value;
				this.SendPropertyChanged("Id");
				this.OnIdChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Number", Storage="_Number1", DbType="Int NOT NULL")]
	public int Number1
	{
		get
		{
			return this._Number1;
		}
		set
		{
			if ((this._Number1 != value))
			{
				this.OnNumber1Changing(value);
				this.SendPropertyChanging();
				this._Number1 = value;
				this.SendPropertyChanged("Number1");
				this.OnNumber1Changed();
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
#pragma warning restore 1591