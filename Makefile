.PHONY: clean install test test-integ test-docker release

clean:
	dotnet clean

install:
	@dotnet --version || (echo "Dotnet is not installed, please install Dotnet CLI"; exit 1);

test:
	dotnet build -c Release
	dotnet test -c Release
	curl -s https://codecov.io/bash > .codecov
	chmod +x .codecov
	./.codecov

test-integ: test

test-docker:
	curl -s https://raw.githubusercontent.com/sendgrid/sendgrid-oai/master/prism/prism.sh | bash

release:
	dotnet pack -c Release
