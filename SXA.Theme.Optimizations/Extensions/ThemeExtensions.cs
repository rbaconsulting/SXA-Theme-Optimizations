﻿using Microsoft.Extensions.DependencyInjection;
using Sitecore.Configuration;
using Sitecore.DependencyInjection;
using Sitecore.XA.Foundation.Theming;
using SXA.Theme.Optimizations.Constants;
using System.IO;
using System.Web;

namespace SXA.Theme.Optimizations.Extensions
{
    public static class ThemeExtensions
    {
        /// <summary>
        /// Returns the script src for SXA's Theme Optimization script.
        /// </summary>
        /// <returns>A string for a html script tag's src attribute.</returns>
        public static string GetSXAThemeOptimizationsScript()
        {
            var themingContext = ServiceLocator.ServiceProvider.GetService<IThemingContext>();
            var scriptUrl = string.Format(FileNames.NewlyOptimizedMin, themingContext?.ThemeItem?.Name.Replace(" ", "-").ToLower(), themingContext?.ThemeItem?.Database?.Name.ToLower());

            if (Settings.GetBoolSetting(SitecoreSettings.AlwaysIncludeServerUrl, false))
            {
                scriptUrl = Settings.GetSetting(SitecoreSettings.MediaLinkServerUrl, string.Empty) + scriptUrl;
            }

            if (Settings.GetBoolSetting(SitecoreSettings.AlwaysAppendRevision, false))
            {
                var updatedDate = File.GetLastWriteTimeUtc($"{HttpRuntime.AppDomainAppPath}{scriptUrl.TrimStart('/').Replace("/", "\\")}");

                scriptUrl = $"{scriptUrl}?rev={updatedDate:MMddHHmmss}";
            }

            return scriptUrl;
        }
    }
}