platform=anycpu
projName=Project1
outDir=$(projName)\bin
globalUsingsFile = GlobalUsings.cs

# add references based on VS 2022 solution
sysDir=C:\Program Files\dotnet\shared\Microsoft.NETCore.App\6.0.14
nugetDir=C:\Users\Kenneth\.nuget\packages
version=7.0.0\lib\net6.0
libnames=microsoft.extensions.logging.console\$(version)\Microsoft.Extensions.Logging.Console.dll
libnames=$(libnames),microsoft.extensions.dependencyinjection.abstractions\$(version)\Microsoft.Extensions.DependencyInjection.Abstractions.dll
libnames=$(libnames),Microsoft.Extensions.Logging.Abstractions\$(version)\Microsoft.Extensions.Logging.Abstractions.dll
libnames=$(libnames),microsoft.extensions.logging.Configuration\$(version)\Microsoft.Extensions.Logging.Configuration.dll
libnames=$(libnames),microsoft.extensions.logging\$(version)\Microsoft.Extensions.Logging.dll
libnames=$(libnames),microsoft.extensions.Options\$(version)\Microsoft.Extensions.Options.dll
libnames=$(libnames),System.Runtime.CompilerServices.Unsafe\6.0.0\lib\net6.0\System.Runtime.CompilerServices.Unsafe.dll
libnames=$(libnames),System.Text.Json\$(version)\System.Text.Json.dll

libnames=$(libnames),Microsoft.Extensions.Configuration\$(version)\Microsoft.Extensions.Configuration.dll
libnames=$(libnames),Microsoft.Extensions.Configuration.Binder\$(version)\Microsoft.Extensions.Configuration.Binder.dll
libnames=$(libnames),microsoft.extensions.dependencyinjection\$(version)\Microsoft.Extensions.DependencyInjection.dll
libnames=$(libnames),microsoft.extensions.Options.ConfigurationExtensions\$(version)\Microsoft.Extensions.Options.ConfigurationExtensions.dll
libnames=$(libnames),microsoft.extensions.Primitives\$(version)\Microsoft.Extensions.Primitives.dll
libnames=$(libnames),System.Text.Encodings.Web\$(version)\System.Text.Encodings.Web.dll
libnames=$(libnames),System.Runtime.dll

# Microsoft.Extensions.Configuration.Abstractions.dll
# libnames=$(libnames),Microsoft.Extensions.Configuration.Binder.dll
# libnames=$(libnames),Microsoft.Extensions.Configuration.dll
# libnames=$(libnames),Microsoft.Extensions.DependencyInjection.Abstractions.dll
# libnames=$(libnames),Microsoft.Extensions.DependencyInjection.dll
# libnames=$(libnames),Microsoft.Extensions.Logging.Configuration.dll
# libnames=$(libnames),Microsoft.Extensions.Logging.dll
# libnames=$(libnames),Microsoft.Extensions.Options.ConfigurationExtensions.dll
# libnames=$(libnames),Microsoft.Extensions.Options.dll
# libnames=$(libnames),Microsoft.Extensions.Primitives.dll
#libnames=$(libnames),System.Text.Encodings.Web.dll
# targets
Test:
	@echo Sources : $(codeFiles)
	@echo Platform : $(platform)
	@echo Nuget Dir : $(nugetDir)
	@echo Nuget Dlls : $(libnames)
ConsoleAppTest: MakeBinDir $(codeFiles)
	@csc -t:exe -platform:$(platform)\
		-o+ -generatedfilesout:"$(outDir)" /out:"$(outDir)\$(platform)_$(projName).exe"\
		-lib:$(nugetDir),"$(sysDir)" -r:$(libnames)\
		$(codeFiles) $(globalUsingsFile)
ConsoleApp: MakeBinDir $(codeFiles)
	@csc -t:exe -platform:$(platform) -o+ -generatedfilesout:"$(outDir)" /out:"$(outDir)\$(platform)_$(projName).exe"\
		-lib:"$(libSearchDir)","$(sysLibDir)" -r:$(libnames)\
		$(codeFiles) $(globalUsingsFile)
	@copy /Y "$(libSearchDir)\Microsoft.Extensions.*dll" "$(outDir)"
	@copy /Y "$(sysLibDir)\System.Runtime.dll" "$(outDir)"
ClassLib-Dbg: MakeBinDir $(codeFiles)
	csc -t:library -platform:$(platform) -debug+ -generatedfilesout:"$(outDir)" /out:"$(outDir)\$(platform)_debug_$(projName).dll"\
		$(codeFiles) $(globalUsingsFile)
ClassLib: MakeBinDir $(codeFiles)
	csc -t:library -platform:$(platform) -o+ -generatedfilesout:"$(outDir)" /out:"$(outDir)\$(platform)_$(projName).dll"\
		$(codeFiles) $(globalUsingsFile)
MakeBinDir:
	if not exist "$(outDir)" mkdir "$(outDir)"
Clean-All:
	@for /f "tokens=*" %f in ('dir /a:d /b') do (\
		@dir /b "%f\bin" 2>nul &&\
		@rmdir /S /Q "%f\bin"\
	)
Clean: $(projName)
	@dir /b "$(projName)\bin" && rmdir /S /Q "$(projName)\bin"
