﻿using System;
using System.Configuration;
using System.IO;
using NUnit.Framework;

namespace AgileSqlClub.SqlPackageFilter.IntegrationTests
{
    [TestFixture]
    class XmlMultiFilterTests
    {
        private readonly SqlGateway _gateway = new SqlGateway(new AppSettingsReader().GetValue("ConnectionString", typeof(string)) as string);

        //[Test]
        
        //public void Negative_Filter_Excludes_Everything_Except_Specified_Schema_With_Additional_IgnoreSecurity_Also_Leaving_Security()
        //{
        //    _gateway.RunQuery("IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'func') exec sp_executesql N'CREATE SCHEMA func';");
        //    _gateway.RunQuery("IF object_id('func.funky') is null exec sp_executesql N'CREATE FUNCTION func.funky() RETURNS INT AS  BEGIN  	RETURN 1;	 END';");

        //    _gateway.RunQuery("IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'blah') exec sp_executesql N'CREATE SCHEMA blah';");
        //    _gateway.RunQuery("IF object_id('blah.funky_chicken') is null exec sp_executesql N'CREATE FUNCTION blah.funky_chicken() RETURNS INT AS  BEGIN  	RETURN 1;	 END';");
        //    _gateway.RunQuery("IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE name = 'fred')	CREATE USER fred WITHOUT LOGIN;");

        //    var count = _gateway.GetInt("SELECT COUNT(*) FROM sys.objects where name = 'funky';");
        //    Assert.AreEqual(1, count);

        //    count = _gateway.GetInt("SELECT COUNT(*) FROM sys.objects where name = 'funky_chicken';");
        //    Assert.AreEqual(1, count);

        //    count = _gateway.GetInt("SELECT COUNT(*) FROM sys.database_principals WHERE name = 'fred';");
        //    Assert.AreEqual(1, count);


        //    var xmlDefinitionFileText =
        //        "<Filters><Filter Operation=\"Keep\" Type=\"Schema\" MatchType=\"DoesNotMatch\">func</Filter>" +
        //        "<Filter Operation=\"Ignore\" Type=\"Security\" MatchType=\"DoesMatch\"/></Filters>";

        //    var xmlFile = Path.Combine(Environment.CurrentDirectory,
        //        "XmlMultiFilterTestsNegative_Filter_Excludes_Everything_Except_Specified_Schema_With_Additional_IgnoreSecurity_Also_Leaving_Security.xml");
        //    if(File.Exists(xmlFile))
        //        File.Delete(xmlFile);

        //    var sr = new StreamWriter(xmlFile); sr.Write(xmlDefinitionFileText);
        //    sr.Dispose();

        //    var args = string.Format(
        //        $"/Action:Publish /TargetServerName:{_gateway.dbServer} /SourceFile:{Path.Combine(TestContext.CurrentContext.TestDirectory, "Dacpac.Dacpac")} /p:AdditionalDeploymentContributors=AgileSqlClub.DeploymentFilterContributor " +
        //        $" /TargetDatabaseName:{_gateway.dbName} /p:DropObjectsNotInSource=True " +
        //        "/p:AdditionalDeploymentContributorArguments=SqlPackageLogging=Info;\"SqlPackageXmlFilterFile={0}\" /p:AllowIncompatiblePlatform=true", xmlFile);

        //    var proc = new ProcessGateway( Path.Combine(TestContext.CurrentContext.TestDirectory,   "SqlPackage.exe\\SqlPackage.exe"), args);
        //    proc.Run();
        //    proc.WasDeploySuccess();

        //    count = _gateway.GetInt("SELECT COUNT(*) FROM sys.objects where name = 'funky';");
        //    Assert.AreEqual(0, count, proc.Messages);

        //    count = _gateway.GetInt("SELECT COUNT(*) FROM sys.objects where name = 'funky_chicken';");
        //    Assert.AreEqual(1, count, proc.Messages);

        //    count = _gateway.GetInt("SELECT COUNT(*) FROM sys.database_principals WHERE name = 'fred';");
        //    Assert.AreEqual(1, count, proc.Messages);

        //}
    }
}
