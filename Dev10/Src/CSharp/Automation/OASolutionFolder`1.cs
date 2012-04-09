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
	using System.Diagnostics;
	using System.Globalization;
	using System.Runtime.InteropServices;
	using Microsoft.VisualStudio;
	using Microsoft.VisualStudio.Shell.Interop;

	[ComVisible(true), CLSCompliant(false)]
	public class OASolutionFolder<T> : EnvDTE80.SolutionFolder
		where T : HierarchyNode
	{
		private readonly T node;
		private bool hidden;

		public OASolutionFolder(T associatedNode)
		{
			if(associatedNode == null)
			{
				throw new ArgumentNullException("associatedNode");
			}

			Debug.Assert(associatedNode.ProjectManager is ProjectContainerNode, "Expecting obejct of type" + typeof(ProjectContainerNode).Name);

			if(!(associatedNode.ProjectManager is ProjectContainerNode))
				throw new ArgumentException(SR.GetString(SR.InvalidParameter, CultureInfo.CurrentUICulture), "associatedNode");

			this.node = associatedNode;
		}


		#region SolutionFolder Members

		public virtual EnvDTE.Project AddFromFile(string fileName)
		{
			ProjectContainerNode projectContainer = (ProjectContainerNode)this.node.ProjectManager;
			ProjectElement newElement = new ProjectElement(projectContainer, fileName, ProjectFileConstants.SubProject);
			NestedProjectNode newNode = projectContainer.AddExistingNestedProject(newElement, __VSCREATEPROJFLAGS.CPF_NOTINSLNEXPLR | __VSCREATEPROJFLAGS.CPF_SILENT | __VSCREATEPROJFLAGS.CPF_OPENFILE);
			if(newNode == null)
				return null;
			// Now that the sub project was created, get its extensibility object so we can return it
			object newProject = null;
			if(ErrorHandler.Succeeded(newNode.NestedHierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_ExtObject, out newProject)))
				return newProject as EnvDTE.Project;
			else
				return null;
		}

		public virtual EnvDTE.Project AddFromTemplate(string fileName, string destination, string projectName)
		{
			bool isVSTemplate = Utilities.IsTemplateFile(fileName);

			NestedProjectNode newNode = null;
			if(isVSTemplate)
			{
				// Get the wizard to run, we will get called again and use the alternate code path
				ProjectElement newElement = new ProjectElement(this.node.ProjectManager, System.IO.Path.Combine(destination, projectName), ProjectFileConstants.SubProject);
				newElement.SetMetadata(ProjectFileConstants.Template, fileName);
				((ProjectContainerNode)this.node.ProjectManager).RunVSTemplateWizard(newElement, false);
			}
			else
			{
				if((String.IsNullOrEmpty(System.IO.Path.GetExtension(projectName))))
				{
					string targetExtension = System.IO.Path.GetExtension(fileName);
					projectName = System.IO.Path.ChangeExtension(projectName, targetExtension);
				}

				ProjectContainerNode projectContainer = (ProjectContainerNode)this.node.ProjectManager;
				newNode = projectContainer.AddNestedProjectFromTemplate(fileName, destination, projectName, null, __VSCREATEPROJFLAGS.CPF_NOTINSLNEXPLR | __VSCREATEPROJFLAGS.CPF_SILENT | __VSCREATEPROJFLAGS.CPF_CLONEFILE);
			}
			if(newNode == null)
				return null;

			// Now that the sub project was created, get its extensibility object so we can return it
			object newProject = null;
			if(ErrorHandler.Succeeded(newNode.NestedHierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_ExtObject, out newProject)))
				return newProject as EnvDTE.Project;
			else
				return null;
		}

		public virtual EnvDTE.Project AddSolutionFolder(string Name)
		{
			throw new NotImplementedException();
		}

		public virtual EnvDTE.Project Parent
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public virtual bool Hidden
		{
			get
			{
				return hidden;
			}
			set
			{
				hidden = value;
			}
		}

		public virtual EnvDTE.DTE DTE
		{
			get
			{
				return (EnvDTE.DTE)this.node.ProjectManager.Site.GetService(typeof(EnvDTE.DTE));
			}
		}

		#endregion
	}

}
