@echo off
title MediCare Hospital Management System
echo ======================================================================
echo Starting MediCare Hospital Management System...
echo ======================================================================
cd /d "%~dp0MEDICARE HOSPITAL MANGAMENT"
dotnet run --no-build
if %ERRORLEVEL% NEQ 0 (
    echo Building and starting...
    dotnet run
)
