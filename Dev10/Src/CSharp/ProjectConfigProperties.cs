/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.Runtime.InteropServices;
using _PersistStorageType = Microsoft.VisualStudio.Shell.Interop._PersistStorageType;

namespace Microsoft.VisualStudio.Project
{
	/// <summary>
	/// Implements the configuration dependent properties interface
	/// </summary>
	[CLSCompliant(false)]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class ProjectConfigProperties : IProjectConfigProperties
	{
		private readonly ProjectConfig _projectConfig;

		public ProjectConfigProperties(ProjectConfig projectConfig)
		{
            if (projectConfig == null)
                throw new ArgumentNullException("projectConfig");

			this._projectConfig = projectConfig;
		}

		#region IProjectConfigProperties Members

		public virtual string OutputPath
		{
			get
			{
				return this._projectConfig.GetConfigurationProperty(BuildPropertyPageTag.OutputPath.ToString(), _PersistStorageType.PST_PROJECT_FILE, true);
			}

			set
			{
				this._projectConfig.SetConfigurationProperty(BuildPropertyPageTag.OutputPath.ToString(), _PersistStorageType.PST_PROJECT_FILE, value);
			}
		}

		#endregion
	}
}
