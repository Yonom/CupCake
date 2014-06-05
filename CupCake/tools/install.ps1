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


[xml] $prjXml = Get-Content $project.FullName
foreach($PropertyGroup in $prjXml.project.ChildNodes)
{
	if($PropertyGroup.StartAction -ne $null)
	{
		exit
	}
}

$propertyGroupElement = $prjXml.CreateElement("PropertyGroup", $prjXml.Project.GetAttribute("xmlns"));

$startActionElement = $prjXml.CreateElement("StartAction", $prjXml.Project.GetAttribute("xmlns"));
$propertyGroupElement.AppendChild($startActionElement)
$propertyGroupElement.StartAction = "Program"

$startProgramElement = $prjXml.CreateElement("StartProgram", $prjXml.Project.GetAttribute("xmlns"));
$propertyGroupElement.AppendChild($startProgramElement)
$propertyGroupElement.StartProgram = "`$(MSBuildProjectDirectory)\CupCake.Debug.exe"

$startArgumentsElement = $prjXml.CreateElement("StartArguments", $prjXml.Project.GetAttribute("xmlns"));
$propertyGroupElement.AppendChild($startArgumentsElement)
$propertyGroupElement.StartArguments = "Debug"

$prjXml.project.AppendChild($propertyGroupElement);
$writerSettings = new-object System.Xml.XmlWriterSettings
$writerSettings.OmitXmlDeclaration = $false
$writerSettings.NewLineOnAttributes = $false
$writerSettings.Indent = $true
$projectFilePath = Resolve-Path -Path $project.FullName
$writer = [System.Xml.XmlWriter]::Create($projectFilePath, $writerSettings)
$prjXml.WriteTo($writer)
$writer.Flush()
$writer.Close()