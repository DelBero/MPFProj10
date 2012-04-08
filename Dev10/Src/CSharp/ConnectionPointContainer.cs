﻿/***************************************************************************

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
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using Microsoft.VisualStudio.OLE.Interop;

	[ComVisible(true)]
	public class ConnectionPointContainer : IConnectionPointContainer
	{
		private readonly Dictionary<Guid, IConnectionPoint> connectionPoints;

		protected internal ConnectionPointContainer()
		{
			connectionPoints = new Dictionary<Guid, IConnectionPoint>();
		}

		protected internal void AddEventSource<SinkType>(IEventSource<SinkType> source)
			where SinkType : class
		{
			if(null == source)
			{
				throw new ArgumentNullException("source");
			}
			if(connectionPoints.ContainsKey(typeof(SinkType).GUID))
			{
				throw new ArgumentException("EventSource guid already added to the list of connection points", "source");
			}
			connectionPoints.Add(typeof(SinkType).GUID, new ConnectionPoint<SinkType>(this, source));
		}

		#region IConnectionPointContainer Members
		void IConnectionPointContainer.EnumConnectionPoints(out IEnumConnectionPoints ppEnum)
		{
			ppEnum = new EnumConnectionPoints(connectionPoints.Values);
		}

		void IConnectionPointContainer.FindConnectionPoint(ref Guid riid, out IConnectionPoint ppCP)
		{
			ppCP = connectionPoints[riid];
		}
		#endregion
	}
}
