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

	partial class HierarchyNode
	{
		/// <summary>
		/// DropEffect as defined in oleidl.h
		/// </summary>
		[Flags]
		public enum DropEffect
		{
			None,
			Copy = 1,
			Move = 2,
			Link = 4
		}
	}
}
