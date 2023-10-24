@echo off
Setlocal

set logfile=build.log
set target=BuildAll
set config=Debug
set platform=Any CPU
color 07

if "%1" == "" goto build
set target=%1

if "%2" == "" goto build
set config=%2

if "%~3" == "" goto build
set platform=%~3

:build
@echo(
@echo Target=%target%, Configuration=%config%, Platform=%platform%
@echo(
@echo Building targets - this will pause at the end only if there are errors
@echo(
@echo msbuild OnlinePharmacy.msbuild /t:%target% /verbosity:minimal /consoleloggerparameters:NoItemAndPropertyList /p:Configuration=%config% /p:Platform="%platform%" /p:SolutionPath=%CD% /p:LogFile=%logfile% /p:RunCodeAnalysis=Never /logger:FileLogger,Microsoft.Build.Engine;logfile=%logfile% /maxcpucount
@echo(
msbuild OnlinePharmacy.msbuild /t:%target% /verbosity:minimal /consoleloggerparameters:NoItemAndPropertyList /p:Configuration=%config% /p:Platform="%platform%" /p:SolutionPath=%CD% /p:LogFile=%logfile% /p:RunCodeAnalysis=Never /logger:FileLogger,Microsoft.Build.Engine;logfile=%logfile% /maxcpucount

if %ErrorLevel%==0 goto completeBuild

:error
@echo(
@echo Build errors, please see logfile: %CD%\%logfile%
goto :end

:completeBuild
@echo(
@echo Build Complete

:end
color 
