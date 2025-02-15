﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Sharpmake;

namespace SharpmakeGen
{
    [Generate]
    public class SharpmakeApplicationProject : Common.SharpmakeBaseProject
    {
        public SharpmakeApplicationProject()
            : base(generateXmlDoc: false)
        {
            Name = "Sharpmake.Application";
            ApplicationManifest = "app.manifest";

            NoneFiles.Add(Path.Combine(Globals.AbsoluteRootPath, "Sharpmake.Main.sharpmake.cs"));

            CustomProperties.Add("ServerGarbageCollection", "true");
        }

        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.ProjectPath = @"[project.SourceRootPath]";

            conf.Output = Configuration.OutputType.DotNetConsoleApp;

            conf.Options.Add(Options.CSharp.AutoGenerateBindingRedirects.Enabled);

            conf.ReferencesByName.Add("System.Windows.Forms");

            conf.AddPrivateDependency<SharpmakeProject>(target);
            conf.AddPrivateDependency<SharpmakeGeneratorsProject>(target);
            conf.AddPrivateDependency<Platforms.CommonPlatformsProject>(target);
        }
    }
}
