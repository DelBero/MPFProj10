/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.Reflection;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.VisualStudio.Project.UnitTests
{
	public static class ProjectEventsUtilities
	{
		private static ConstructorInfo afterProjectFileOpenedEventArgsCtr;
		public static ProjectFileOpenedEventArgs CreateAfterProjectFileOpenedEventArgs(bool added)
		{
			if(null == afterProjectFileOpenedEventArgsCtr)
			{
				//afterProjectFileOpenedEventArgsCtr = typeof(AfterProjectFileOpenedEventArgs).GetConstructor(new Type[] { typeof(bool) });
				afterProjectFileOpenedEventArgsCtr = typeof(ProjectFileOpenedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(bool) }, null);
			}
			return afterProjectFileOpenedEventArgsCtr.Invoke(new object[] { added }) as ProjectFileOpenedEventArgs;
		}

		private static ConstructorInfo beforeProjectFileClosedEventArgsCtr;
		public static ProjectFileClosingEventArgs CreateBeforeProjectFileClosedEventArgs(bool removed)
		{
			if(null == beforeProjectFileClosedEventArgsCtr)
			{
				beforeProjectFileClosedEventArgsCtr = typeof(ProjectFileClosingEventArgs).GetConstructor(new Type[] { typeof(bool) });
			}
			return beforeProjectFileClosedEventArgsCtr.Invoke(new object[] { removed }) as ProjectFileClosingEventArgs;
		}
	}

	[TestClass]
	public class ProjectEventsTest
	{
		private class ProjectEventsSource : IProjectEvents, IDisposable
		{
			public enum ProjectEventsSinkType
			{
				AfterOpened,
				BeforeClosed,
				AnyEvent
			}
			public event EventHandler<ProjectFileOpenedEventArgs> ProjectFileOpened;
			public event EventHandler<ProjectFileClosingEventArgs> ProjectFileClosing;

			public void SignalOpenStatus(bool isOpened)
			{
				if(isOpened)
				{
					if(null != ProjectFileOpened)
					{
						ProjectFileOpened(this, ProjectEventsUtilities.CreateAfterProjectFileOpenedEventArgs(true));
					}
				}
				else
				{
					if(null != ProjectFileClosing)
					{
						ProjectFileClosing(this, ProjectEventsUtilities.CreateBeforeProjectFileClosedEventArgs(true));
					}
				}
			}

			public bool IsSinkRegister(ProjectEventsSinkType sinkType)
			{
				if(ProjectEventsSinkType.AfterOpened == sinkType)
				{
					return (null != ProjectFileOpened);
				}
				if(ProjectEventsSinkType.BeforeClosed == sinkType)
				{
					return (null != ProjectFileClosing);
				}
				return (null != ProjectFileOpened) || (null != ProjectFileClosing);
			}

			public void Dispose()
			{
				Assert.IsFalse(IsSinkRegister(ProjectEventsSinkType.AnyEvent), "ProjectEvents sink registered at shutdown.");
			}
		}

		private static FieldInfo projectOpened;
		private static bool IsProjectOpened(ProjectNode project)
		{
			if(null == projectOpened)
			{
				projectOpened = typeof(VisualStudio.Project.ProjectNode).GetField("projectOpened", BindingFlags.Instance | BindingFlags.NonPublic);
			}
			return (bool)projectOpened.GetValue(project);
		}

		[TestMethod]
		public void SetOpenStatusTest()
		{
			using(ProjectEventsSource eventSource = new ProjectEventsSource())
			{
				ProjectTestClass project = new ProjectTestClass(new ProjectTestPackage());
				IProjectEventsProvider eventProvider = project as IProjectEventsProvider;
				Assert.IsNotNull(eventProvider, "Project class does not implements IProjectEventsProvider.");
				Assert.IsFalse(IsProjectOpened(project), "Project is opened right after its creation.");
				eventProvider.ProjectEventsProvider = eventSource;
				eventSource.SignalOpenStatus(true);
				Assert.IsTrue(IsProjectOpened(project), "Project is not opened after the AfterProjectFileOpened is signaled.");
				project.Close();
			}
		}

		[TestMethod]
		public void SetMultipleSource()
		{
			using(ProjectEventsSource firstSource = new ProjectEventsSource())
			{
				using(ProjectEventsSource secondSource = new ProjectEventsSource())
				{
					ProjectTestClass project = new ProjectTestClass(new ProjectTestPackage());
					IProjectEventsProvider eventProvider = project as IProjectEventsProvider;
					Assert.IsNotNull(eventProvider, "Project class does not implements IProjectEventsProvider.");
					eventProvider.ProjectEventsProvider = firstSource;
					eventProvider.ProjectEventsProvider = secondSource;
					Assert.IsFalse(IsProjectOpened(project));
					firstSource.SignalOpenStatus(true);
					Assert.IsFalse(IsProjectOpened(project));
					secondSource.SignalOpenStatus(true);
					Assert.IsTrue(IsProjectOpened(project));
					project.Close();
				}
			}
		}

		[TestMethod]
		public void SetNullSource()
		{
			ProjectTestClass project = new ProjectTestClass(new ProjectTestPackage());
			IProjectEventsProvider eventProvider = project as IProjectEventsProvider;
			Assert.IsNotNull(eventProvider, "Project class does not implements IProjectEventsProvider.");
			eventProvider.ProjectEventsProvider = null;
			project.Close();
		}
	}
}