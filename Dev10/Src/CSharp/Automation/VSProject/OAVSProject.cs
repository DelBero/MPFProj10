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
	using System.Diagnostics.CodeAnalysis;
	using System.Runtime.InteropServices;
	using EnvDTE;
	using VSLangProj;

	/// <summary>
	/// Represents an automation friendly version of a language-specific project.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OAVS")]
	[ComVisible(true), CLSCompliant(false)]
	public class OAVSProject : VSProject
	{
		private ProjectNode project;
		private OAVSProjectEvents events;

		public OAVSProject(ProjectNode project)
		{
			this.project = project;
		}

		#region VSProject Members

		public virtual ProjectItem AddWebReference(string bstrUrl)
		{
			throw new NotImplementedException();
		}

		public virtual BuildManager BuildManager
		{
			get
			{
				return new OABuildManager(this.project);
			}
		}

		public virtual void CopyProject(string bstrDestFolder, string bstrDestUNCPath, prjCopyProjectOption copyProjectOption, string bstrUsername, string bstrPassword)
		{
			throw new NotImplementedException();
		}

		public virtual ProjectItem CreateWebReferencesFolder()
		{
			throw new NotImplementedException();
		}

		public virtual DTE DTE
		{
			get
			{
				return (EnvDTE.DTE)this.project.Site.GetService(typeof(EnvDTE.DTE));
			}
		}

		public virtual VSProjectEvents Events
		{
			get
			{
				if(events == null)
					events = new OAVSProjectEvents(this);
				return events;
			}
		}

		public virtual void Exec(prjExecCommand command, int bSuppressUI, object varIn, out object pVarOut)
		{
			throw new NotImplementedException();
		}

		public virtual void GenerateKeyPairFiles(string strPublicPrivateFile, string strPublicOnlyFile)
		{
			throw new NotImplementedException();
		}

		public virtual string GetUniqueFilename(object pDispatch, string bstrRoot, string bstrDesiredExt)
		{
			throw new NotImplementedException();
		}

		public virtual Imports Imports
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public virtual EnvDTE.Project Project
		{
			get
			{
				return this.project.GetAutomationObject() as EnvDTE.Project;
			}
		}

		public virtual References References
		{
			get
			{
				ReferenceContainerNode references = project.GetReferenceContainer() as ReferenceContainerNode;
				if(null == references)
				{
					return null;
				}
				return references.Object as References;
			}
		}

		public virtual void Refresh()
		{
			throw new NotImplementedException();
		}

		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public virtual string TemplatePath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public virtual ProjectItem WebReferencesFolder
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public virtual bool WorkOffline
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
