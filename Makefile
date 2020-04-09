.PHONY: clean test install release

clean:
	dotnet clean

install:
	@dotnet --version || (echo "Dotnet is not installed, please install Dotnet CLI"; exit 1);
	dotnet restore

test:
	dotnet restore
	dotnet build ./src/SendGrid -c Release
	dotnet test ./tests/SendGrid.Tests/SendGrid.Tests.csproj -c Release -f netcoreapp2.1
	curl -s https://codecov.io/bash > .codecov
	chmod +x .codecov
	./.codecov

release: test
	dotnet pack ./src/SendGrid -c Release
