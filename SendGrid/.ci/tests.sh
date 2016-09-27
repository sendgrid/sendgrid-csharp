#!/bin/sh -x

mono --runtime=v4.0 .nuget/NuGet.exe install NUnit.Runners -Version 2.6.1 -o packages
mono --runtime=v4.0 packages/NUnit.Runners.2.6.1/tools/nunit-console.exe -noxml -nodots -labels -stoponerror Tests/bin/BuildNet45/Tests.dll
exit $?
