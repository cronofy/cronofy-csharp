all: test

clean:
	rm -rf build

build: clean
	xbuild Cronofy.sln

test: build
	mono packages/NUnit.Runners.*/tools/nunit-console.exe build/Cronofy.Test/bin/Debug/Cronofy.Test.dll

