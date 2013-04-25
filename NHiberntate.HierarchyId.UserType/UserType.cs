//---------------------------------------------------------------------------------
// NHibernate.HierarchyId.UserType, Copyright 2013 roydukkey, 2013-04-25 (Thur, 25 April 2013).
// Dual licensed under the MIT (http://www.roydukkey.com/mit) and
// GPL Version 2 (http://www.roydukkey.com/gpl) licenses.
//---------------------------------------------------------------------------------

namespace NHibernate.HierarchyId
{
	using Microsoft.SqlServer.Types;
	using SqlTypes;
	using System;
	using System.Data;
	using System.Data.SqlTypes;
	using UserTypes;

	/// <summary>
	///		Provides Microsoft.SqlServer.Types.HierarchyId mapping support for NHibernate.
	/// </summary>
	public class UserType : IUserType
	{
		#region Properties

		/// <summary>
		///		The SQL types for the columns mapped by this type.
		/// </summary>
		public SqlType[] SqlTypes
		{
			get { return new[] { NHibernateUtil.String.SqlType }; }
		}
		/// <summary>
		///		The type returned by <c>NullSafeGet()</c>
		/// </summary>
		public Type ReturnedType
		{
			get { return typeof(SqlHierarchyId); }
		}
		/// <summary>
		///		Object of this type are mutable.
		/// </summary>
		public bool IsMutable
		{
			get { return true; }
		}

		#endregion Properties

		#region Methods

		/// <summary>
		///		Compare two instances of the class mapped by this type for persistent "equality". ie. equality of persistent state.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		new public bool Equals(object x, object y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (x == null || y == null) return false;

			return x.Equals(y);
		}
		/// <summary>
		///		Get a hashcode for the instance, consistent with persistence "equality"
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public int GetHashCode(object x)
		{
			return x.GetHashCode();
		}
		/// <summary>
		///		Retrieve an instance of the mapped class from a JDBC resultset.
		/// </summary>
		/// <param name="rs">a IDataReader</param>
		/// <param name="names">column names</param>
		/// <param name="owner">the containing entity</param>
		/// <returns></returns>
		/// <exception cref="HibernateException">HibernateException</exception>
		public object NullSafeGet(IDataReader rs, string[] names, object owner)
		{
			object prop1 = NHibernateUtil.String.NullSafeGet(rs, names[0]);

			if (prop1 == null) return null;

			return SqlHierarchyId.Parse(new SqlString(prop1.ToString()));
		}
		/// <summary>
		///		Write an instance of the mapped class to a prepared statement.
		/// </summary>
		/// <param name="cmd">a IDbCommand</param>
		/// <param name="value">the object to write</param>
		/// <param name="index">command parameter index</param>
		/// <exception cref="HibernateException">HibernateException</exception>
		public void NullSafeSet(IDbCommand cmd, object value, int index)
		{
			if (value == null)
				((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;

			else if (value is SqlHierarchyId)
				((IDataParameter)cmd.Parameters[index]).Value = ((SqlHierarchyId)value).ToString();
		}
		/// <summary>
		///		Return a deep copy of the persistent state, stopping at entities and at collections.
		/// </summary>
		/// <param name="value">generally a collection element or entity field</param>
		/// <returns>a copy</returns>
		public object DeepCopy(object value)
		{
			if (value == null) return null;

			return SqlHierarchyId.Parse(((SqlHierarchyId)value).ToString());
		}
		/// <summary>
		///		During merge, replace the existing (<paramref name="target" />) value in the entity we are merging
		///		to with a new (<paramref name="original" />) value from the detached entity we are merging.
		/// </summary>
		/// <param name="original">the value from the detached entity being merged</param>
		/// <param name="target">the value in the managed entity</param>
		/// <param name="owner">the managed entity</param>
		/// <returns>the value to be merged</returns>
		public object Replace(object original, object target, object owner)
		{
			return DeepCopy(original);
		}
		/// <summary>
		///		Reconstruct an object from the cacheable representation.
		/// </summary>
		/// <param name="cached">the object to be cached</param>
		/// <param name="owner">the owner of the cached object</param>
		/// <returns>a reconstructed object from the cachable representation</returns>
		public object Assemble(object cached, object owner)
		{
			return DeepCopy(cached);
		}
		/// <summary>
		///		Transform the object into its cacheable representation.
		/// </summary>
		/// <param name="value">the object to be cached</param>
		/// <returns>a cacheable representation of the object</returns>
		public object Disassemble(object value)
		{
			return DeepCopy(value);
		}

		#endregion Methods
	}
}