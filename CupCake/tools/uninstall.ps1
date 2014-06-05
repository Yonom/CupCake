param($installPath, $toolsPath, $package, $project)

$project.Properties.Item("PostBuildEvent").Value = ""
$project.ConfigurationManager.Item("Debug", "Any CPU").Properties.Item("StartAction").Value = 0;
$project.ConfigurationManager.Item("Debug", "Any CPU").Properties.Item("StartProgram").Value = "";
$project.ConfigurationManager.Item("Debug", "Any CPU").Properties.Item("StartArguments").Value = "";