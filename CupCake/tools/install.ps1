param($installPath, $toolsPath, $package, $project)

$project.Properties.Item("PostBuildEvent").Value = "if `$(ConfigurationName) == Debug ""`$(ProjectDir)CupCake.Debug.exe"" Deploy `$(TargetDir)"

$asms = $package.AssemblyReferences | %{$_.Name}

foreach ($reference in $project.Object.References)
{
    if ($asms -contains $reference.Name + ".dll")
    {
        $reference.CopyLocal = $false;
    }
}

$project.Properties | where { $_.Name -eq "StartAction" } | foreach { $_.Value = "Program" }
$project.Properties | where { $_.Name -eq "StartProgram" } | foreach { $_.Value = "$(MSBuildProjectDirectory)\CupCake.Debug.exe" }
$project.Properties | where { $_.Name -eq "StartArguments" } | foreach { $_.Value = "Debug" }