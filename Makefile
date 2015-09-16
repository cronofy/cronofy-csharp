NUGET=tools/nuget.exe
NUNIT=packages/NUnit.Runners.*/tools/nunit-console.exe
SLN=Cronofy.sln
TEST_DLLS=build/Cronofy.Test/bin/Debug/Cronofy.Test.dll

all: test

clean:
	rm -rf build

full_clean:
	git clean -dfX

mono_version:
	mono --version

install_tools:
	script/nuget-install

build: clean mono_version install_tools
	mono $(NUGET) restore $(SLN)
	xbuild $(SLN)

test: build
	mkdir -p build/NUnit
	mono $(NUNIT) -result=build/NUnit/TestReport.xml $(TEST_DLLS)
