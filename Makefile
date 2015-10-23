NUGET=tools/nuget.exe
NUNIT=packages/NUnit.Runners.*/tools/nunit-console.exe
SLN=Cronofy.sln
TEST_DLLS=build/Cronofy.Test/bin/Debug/Cronofy.Test.dll
GITCOMMIT:=$(shell git rev-parse --verify HEAD)
VERSION:=$(shell cat VERSION)

all: test

clean:
	rm -rf build

full_clean:
	git clean -dfX

mono_version:
	mono --version

install_tools:
	script/nuget-install

set_version:
	mkdir -p build
	echo $(VERSION) > build/VERSION
	echo $(GITCOMMIT) > build/GITCOMMIT
	sed s/%VERSION%/$(VERSION)/ Cronofy.nuspec.template > Cronofy.nuspec
	sed s/%VERSION%/$(VERSION)/ src/Cronofy/Properties/AssemblyVersion.cs.template > src/Cronofy/Properties/AssemblyVersion.cs

build: clean set_version mono_version install_tools
	mono $(NUGET) restore $(SLN)
	xbuild $(SLN)

build_release: build
	xbuild /p:Configuration=Release $(SLN)

test: build
	mkdir -p build/NUnit
	mono $(NUNIT) -result=build/NUnit/TestReport.xml $(TEST_DLLS)

package: test build_release
	mkdir -p artifacts
	mono $(NUGET) pack -Verbosity detailed -OutputDirectory artifacts Cronofy.nuspec
