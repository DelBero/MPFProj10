/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

namespace Microsoft.VisualStudio.Project.Automation
{
	using System;
	using System.Runtime.InteropServices;

	/// <summary>
	/// This object defines a so called null object that is returned as instead of null. This is because callers in VSCore usually crash if a null propery is returned for them.
	/// </summary>
	[CLSCompliant(false), ComVisible(true)]
	public class OANullProperty : EnvDTE.Property
	{
		private readonly OAProperties _parent;

		public OANullProperty(OAProperties parent)
		{
			if (parent == null)
				throw new ArgumentNullException("parent");

			this._parent = parent;
		}

		#region EnvDTE.Property

		public object Application
		{
			get
			{
				return String.Empty;
			}
		}

		public EnvDTE.Properties Collection
		{
			get
			{
				//todo: EnvDTE.Property.Collection
				return this._parent;
			}
		}

		public EnvDTE.DTE DTE
		{
			get
			{
				return null;
			}
		}

		public object get_IndexedValue(object index1, object index2, object index3, object index4)
		{
			return String.Empty;
		}

		public void let_Value(object value)
		{
			//todo: let_Value
		}

		public string Name
		{
			get
			{
				return String.Empty;
			}
		}

		public short NumIndices
		{
			get
			{
				return 0;
			}
		}

		public object Object
		{
			get
			{
				return this._parent.Target;
			}

			set
			{
			}
		}

		public EnvDTE.Properties Parent
		{
			get
			{
				return this._parent;
			}
		}

		public void set_IndexedValue(object index1, object index2, object index3, object index4, object value)
		{

		}

		public object Value
		{
			get
			{
				return String.Empty;
			}

			set
			{
			}
		}
		#endregion
	}
}
