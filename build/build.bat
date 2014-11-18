

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild" ..\Kooboo_CommonLibraries.sln /t:rebuild /l:FileLogger,Microsoft.Build.Engine;logfile=Publish.log; /p:VisualStudioVersion=12.0 /p:Configuration=Release

rd Bin
md Bin

copy ..\Kooboo.Common\bin\Release\Kooboo.Common.* Bin\

copy ..\Kooboo.Common.Data\bin\Release\Kooboo.Common.Data.* Bin\

copy ..\Kooboo.Common.Web\bin\Release\Kooboo.Common.Web.* Bin\

copy ..\Kooboo.Common.Windows\bin\Release\Kooboo.Common.Windows.* Bin\

copy ..\Kooboo.Common.ObjectContainer.Ninject\bin\Release\Kooboo.Common.ObjectContainer.Ninject.* Bin\

copy ..\Kooboo.Common.ObjectContainer.Ninject\bin\Release\Ninject.* Bin\


@pause