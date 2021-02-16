using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.Deployment;
using Microsoft.SqlServer.Dac.Model;

namespace AgileSqlClub.SqlPackageFilter.Filter
{
    class ModifyDatabaseCheckStep
    {
        public bool IsDatabaseModify { get; }

        public ObjectIdentifier SettingName { get; private set; }

        public ModifyDatabaseCheckStep(DeploymentStep step)
        {
            var scriptStep = step as DeploymentScriptStep;

            IsDatabaseModify = false;

            var scriptLines = scriptStep?.GenerateTSQL();
            var script = scriptLines != null && scriptLines.Count > 0 ? scriptLines[0] : "";

            if (string.IsNullOrWhiteSpace(script))
            {
                StringWriter sw = new StringWriter();
                step.GenerateBatchScript(sw);
                Console.WriteLine("**********");
                Console.WriteLine($"Batch: {sw}");
                Console.WriteLine("**********");

                script = sw.ToString();

                var match = Regex.Match(script, @"BEGIN[\s\S]*?ALTER\s*DATABASE\s*\[.*\][\s\S]*?SET\s*RECOVERY\s*.*");

                IsDatabaseModify = match.Success;
                SettingName = match.Success ? ObjectIdentifierFactory.Create("DatabaseOptions.RECOVERY") : null;
            }
        }
    }
}
