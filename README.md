NHiberntate HierarchyId UserType v1.0.0
==========================================

**Download** [Solution](//github.com/roydukkey/NHiberntate.HierarchyId.UserType/archive/master.zip) | [Assembly](//github.com/roydukkey/NHiberntate.HierarchyId.UserType/blob/master/Release/NHiberntate.HierarchyId.UserType-v1.0.0.0.zip?raw=true)

Mapping
----------

~~~ xml
<?xml version="1.0" encoding="utf-8" ?>
<hibernate-mapping xmlns="urn:nhibernate-mapping-2.2" assembly="DataLayer" namespace="NHibernate.Map">
	<class name="NHibernate.Map.OrganizationUnit, DataLayer" table="`orgunit`">

		<property name="HierarchyId" column="`ou_hid`" type="NHibernate.HierarchyId.UserType, NHibernate.HierarchyId.UserType" />
		...

	</class>
</hibernate-mapping>
~~~

Object with SqlHierarchyId
-----------------------------

~~~ c#
namespace NHibernate.Map
{
	using Microsoft.SqlServer.Types;

	public class OrganizationUnit
	{
		#region Fields

		private SqlHierarchyId _hierarchyId;
		...

		#endregion Fields

		#region Properties

		public virtual SqlHierarchyId HierarchyId
		{
			get { return _hierarchyId; }
			set { _hierarchyId = value; }
		}
		...

		#endregion Properties
	}
}
~~~
