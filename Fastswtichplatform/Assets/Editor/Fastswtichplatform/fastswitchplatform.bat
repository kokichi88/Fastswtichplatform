@echo off
 
set PLATFORM_DIR=Library_%2
set UNITY_PATH=%1
REM Change to directory the bat file is located in.  This is needed when run as admin.
cd %UNITY_PATH%
 
REM Make sure Unity not running
tasklist /FI "IMAGENAME eq Unity.exe" 2>NUL | find /I /N "Unity.exe">NUL
if "%ERRORLEVEL%"=="0" (
    taskkill /F /IM Unity.exe 
)
REM delete any existing library
rmdir /S /Q Library

REM Make platform dir
IF EXIST %PLATFORM_DIR% GOTO MAKELINK
mkdir %PLATFORM_DIR%

:MAKELINK
REM Make symbolic link to platform dir...
mklink /D Library %PLATFORM_DIR%

    
%3%
@pause