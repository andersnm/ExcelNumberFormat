using System.Xml.Linq;

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

var projectName = "ExcelNumberFormat";
var libraryProject = $"./src/{projectName}/{projectName}.csproj";
var testProject = $"./test/{projectName}.Tests/{projectName}.Tests.csproj";
var outDir = $"./src/{projectName}/bin/{configuration}";

Task("Clean")
    .Does(() =>
{
    CleanDirectory(outDir);
});

Task("Restore").Does(DotNetCoreRestore);

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetCoreBuild(".", new DotNetCoreBuildSettings { Configuration = configuration });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest(testProject, new DotNetCoreTestSettings { Configuration = configuration });
});

Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
{
    DotNetCorePack(libraryProject, new DotNetCorePackSettings { Configuration = configuration });
});

Task("Push")
    .IsDependentOn("Pack")
    .Does(() =>
{
    var doc = XDocument.Load(libraryProject);
    string version = doc.Root.Elements("PropertyGroup").Elements("Version").First().Value;
    string package = $"src/{projectName}/bin/{configuration}/{projectName}.{version}.nupkg";
    NuGetPush(package, new NuGetPushSettings());
});

Task("Default")
    .IsDependentOn("Pack");

RunTarget(target);
