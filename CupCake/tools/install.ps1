param($installPath, $toolsPath, $package, $project)

$project.Properties.Item("PostBuildEvent").Value = "if `$(ConfigurationName) == Debug ""`$(ProjectDir)CupCake.Debug.exe"" Deploy `$(TargetDir)"
$project.ConfigurationManager.Item("Debug", "Any CPU").Properties.Item("StartAction").Value = 1;
$project.ConfigurationManager.Item("Debug", "Any CPU").Properties.Item("StartProgram").Value = "`$(ProjectDir)CupCake.Debug.exe";
$project.ConfigurationManager.Item("Debug", "Any CPU").Properties.Item("StartArguments").Value = "Debug";

$asms = $package.AssemblyReferences | %{$_.Name}

foreach ($reference in $project.Object.References)
{
    if ($asms -contains $reference.Name + ".dll")
    {
        $reference.CopyLocal = $false;
    }
}