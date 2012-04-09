/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

namespace Microsoft.VisualStudio.Project
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Globalization;

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class LocDisplayNameAttribute : DisplayNameAttribute
	{
		#region fields
		private readonly string name;
		#endregion

		#region ctors
		public LocDisplayNameAttribute(string name)
		{
			this.name = name;
		}
		#endregion

		#region properties
		public string Name
		{
			get
			{
				return name;
			}
		}

		public override string DisplayName
		{
			get
			{
				string result = SR.GetString(this.name, CultureInfo.CurrentUICulture);
				if(result == null)
				{
					Debug.Assert(false, "String resource '" + this.name + "' is missing");
					result = this.name;
				}
				return result;
			}
		}
		#endregion
	}
}
