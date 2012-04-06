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
	using Microsoft.VisualStudio.Shell.Interop;

	/// <summary>
	/// Defines the event args for the active configuration chnage event.
	/// </summary>
	public class ActiveConfigurationChangedEventArgs : EventArgs
	{
		#region Private fields
		/// <summary>
		/// The hierarchy whose configuration has changed 
		/// </summary>
		private IVsHierarchy hierarchy;
		#endregion

		/// <summary>
		/// Constructs a new event args.
		/// </summary>
		/// <param name="fileName">The hierarchy that has changed its configuration.</param>
		internal ActiveConfigurationChangedEventArgs(IVsHierarchy hierarchy)
		{
			this.hierarchy = hierarchy;
		}

		/// <summary>
		/// The hierarchy whose configuration has changed 
		/// </summary>
		internal IVsHierarchy Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}
	}
}
