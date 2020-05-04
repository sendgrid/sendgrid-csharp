.PHONY: clean install test test-integ test-docker release

clean:
	dotnet clean

install:
	@dotnet --version || (echo "Dotnet is not installed, please install Dotnet CLI"; exit 1);
	dotnet restore

test:
	dotnet build ./src/SendGrid -c Release -f netstandard1.3
	dotnet build ./src/SendGrid -c Release -f netstandard2.0
	dotnet test ./tests/SendGrid.Tests/SendGrid.Tests.csproj -c Release -f netcoreapp2.1
	curl -s https://codecov.io/bash > .codecov
	chmod +x .codecov
	./.codecov

test-integ: test

test-docker:
	curl -s https://raw.githubusercontent.com/sendgrid/sendgrid-oai/master/prism/prism.sh | bash

release:
	dotnet build ./src/SendGrid -c Release
	dotnet pack ./src/SendGrid -c Release
