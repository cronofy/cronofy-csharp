NUGET=tools/nuget.exe
NUNIT=packages/NUnit.Runners.*/tools/nunit-console.exe
SLN=Cronofy.sln
TEST_DLLS=build/Cronofy.Test/bin/Debug/Cronofy.Test.dll

all: test

clean:
	rm -rf build

full_clean:
	git clean -dfX

install_tools:
	script/nuget-install

build: clean install_tools
	mono $(NUGET) restore $(SLN)
	xbuild $(SLN)

test: build
	mono $(NUNIT) $(TEST_DLLS)

