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