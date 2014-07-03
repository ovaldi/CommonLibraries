del *.nupkg
nuget pack ..\Kooboo.Common\Kooboo.Common.csproj -Prop Configuration=Release
nuget pack ..\Kooboo.Common.Data\Kooboo.Common.Data.csproj -Prop Configuration=Release
nuget pack ..\Kooboo.Common.Web\Kooboo.Common.Web.csproj -Prop Configuration=Release
nuget pack ..\Kooboo.Common.Windows\Kooboo.Common.Windows.csproj -Prop Configuration=Release

@pause