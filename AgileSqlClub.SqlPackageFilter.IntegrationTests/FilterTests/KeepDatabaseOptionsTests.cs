using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AgileSqlClub.SqlPackageFilter.IntegrationTests.FilterTests
{
    [TestFixture]
    public class KeepDatabaseOptionsTests
    {
        private readonly SqlGateway _gateway = new SqlGateway(new AppSettingsReader().GetValue("ConnectionString", typeof(string)) as string);

        [Test]
        public void Database_Options_Are_Normally_Changed()
        {

            _gateway.RunQuery($"IF NOT EXISTS(SELECT recovery_model_desc FROM sys.databases where name = '{_gateway.dbName}' and recovery_model_desc = 'SIMPLE') ALTER DATABASE[{_gateway.dbName}] SET RECOVERY SIMPLE WITH NO_WAIT;");

            var args = $"/Action:Publish /TargetServerName:{_gateway.dbServer} /SourceFile:{Path.Combine(TestContext.CurrentContext.TestDirectory, "Dacpac.Dacpac")} " +
                $"/p:AdditionalDeploymentContributors=AgileSqlClub.DeploymentFilterContributor " +
                $"/TargetDatabaseName:{_gateway.dbName} /p:DropObjectsNotInSource=True /p:AllowIncompatiblePlatform=true";

            var proc = new ProcessGateway(Path.Combine(TestContext.CurrentContext.TestDirectory, "SqlPackage.exe\\SqlPackage.exe"), args);
            proc.Run();
            proc.WasDeploySuccess();

            var recoveryType = _gateway.GetValue<string>($"SELECT recovery_model_desc FROM sys.databases where name = '{_gateway.dbName}'");

            Assert.AreEqual("FULL", recoveryType, proc.Messages);

        }

        [Test]
        public void Database_Options_Are_Skipped_When_KeepType()
        {

            _gateway.RunQuery($"IF NOT EXISTS(SELECT recovery_model_desc FROM sys.databases where name = '{_gateway.dbName}' and recovery_model_desc = 'SIMPLE') ALTER DATABASE[{_gateway.dbName}] SET RECOVERY SIMPLE WITH NO_WAIT;");

            var args = $"/Action:Publish /TargetServerName:{_gateway.dbServer} /SourceFile:{Path.Combine(TestContext.CurrentContext.TestDirectory, "Dacpac.Dacpac")} " +
                       $"/p:AdditionalDeploymentContributors=AgileSqlClub.DeploymentFilterContributor /p:AdditionalDeploymentContributorArguments=\"SqlPackageFilter=KeepType(DatabaseOptions.RECOVERY)\" " +
                       $"/TargetDatabaseName:{_gateway.dbName} /p:DropObjectsNotInSource=True /p:AllowIncompatiblePlatform=true";

            var proc = new ProcessGateway(Path.Combine(TestContext.CurrentContext.TestDirectory, "SqlPackage.exe\\SqlPackage.exe"), args);
            proc.Run();
            proc.WasDeploySuccess();

            var recoveryType = _gateway.GetValue<string>($"SELECT recovery_model_desc FROM sys.databases where name = '{_gateway.dbName}'");

            Assert.AreEqual("SIMPLE", recoveryType, proc.Messages);

        }
    }
}
