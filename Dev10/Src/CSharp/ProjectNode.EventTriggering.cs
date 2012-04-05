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

    partial class ProjectNode
    {
        /// <summary>
        /// Flags for specifying which events to stop triggering.
        /// </summary>
        [Flags]
        public enum EventTriggering
        {
            TriggerAll = 0,
            DoNotTriggerHierarchyEvents = 1,
            DoNotTriggerTrackerEvents = 2
        }
	}
}
