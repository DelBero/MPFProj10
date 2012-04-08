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
	using Microsoft.VisualStudio.OLE.Interop;

	public sealed class DataCacheEntry : IDisposable
	{
		#region fields
		/// <summary>
		/// Defines an object that will be a mutex for this object for synchronizing thread calls.
		/// </summary>
		private static readonly object Mutex = new object();

		private readonly FORMATETC format;

		private SafeGlobalAllocHandle data;

		private readonly DATADIR dataDir;
		#endregion

		#region properties
		public FORMATETC Format
		{
			get
			{
				return this.format;
			}
		}

		public SafeGlobalAllocHandle Data
		{
			get
			{
				return this.data;
			}
		}

		public DATADIR DataDir
		{
			get
			{
				return this.dataDir;
			}
		}

		#endregion

		/// <summary>
		/// The IntPtr is data allocated that should be removed. It is allocated by the ProcessSelectionData method.
		/// </summary>
		internal DataCacheEntry(FORMATETC fmt, SafeGlobalAllocHandle data, DATADIR dir)
		{
			this.format = fmt;
			this.data = data;
			this.dataDir = dir;
		}

		#region Dispose
		/// <summary>
		/// The IDispose interface Dispose method for disposing the object determinastically.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// The method that does the cleanup.
		/// </summary>
		/// <param name="disposing"></param>
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				SafeGlobalAllocHandle handle = this.data;
				if (handle != null)
				{
					handle.Dispose();
					data = null;
				}
			}
		}
		#endregion
	}
}
