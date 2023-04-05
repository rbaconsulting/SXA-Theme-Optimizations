﻿using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Pipelines;
using SXA.Theme.Optimizations.Core.Constants;
using System;

namespace SXA.Theme.Optimizations.Pipelines
{
	/// <summary>
	/// Optimizes the scripts on server startup for the Content Managment and Standalone server roles only.
	/// </summary>
	public class Initialize
	{
		public void Process(PipelineArgs args)
		{
			try
			{
                Event.RaiseEvent(CustomEvents.OptimizeScripts);
            }
            catch (Exception e)
			{
				Log.Error(string.Format(LogMessages.Error.StartupEvent, CustomEvents.OptimizeScripts, e.Message), e, this);
			}
		}
    }
}