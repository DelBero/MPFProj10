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
	using System.Runtime.InteropServices;

	/// <summary>
	/// Class used to identify a source of events of type SinkType.
	/// </summary>
	[ComVisible(false)]
	public interface IEventSource<SinkType>
		where SinkType : class
	{
		void OnSinkAdded(SinkType sink);
		void OnSinkRemoved(SinkType sink);
	}
}
