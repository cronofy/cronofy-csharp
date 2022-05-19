NUGET_SOURCE=https://api.nuget.org/v3/index.json
VERSION:=$(shell cat VERSION)

.PHONY: all
all: test

.PHONY: clean
clean:
	rm -rf build/ artifacts/

.PHONY: full_clean
full_clean:
	git clean -dfX

.PHONY: build
build: clean
	mkdir build
	dotnet build -o build/ -p:Version=$(VERSION)

.PHONY: build_release
build_release: clean
	mkdir build
	dotnet build -c Release -o build/ -p:Version=$(VERSION)

.PHONY: test
test:
	dotnet test

.PHONY: package
package: clean test
	mkdir artifacts
	dotnet pack --include-source --include-symbols -c Release -o artifacts/ -p:Version=$(VERSION)

.PHONY: release
release: clean package guard-env-NUGET_API_KEY
	@git diff --exit-code --no-patch || (echo "Cannot release with uncommitted changes"; exit 1)
	git push
	@echo "Publishing artifacts/Cronofy.$(VERSION).nupkg"
	dotnet nuget push artifacts/Cronofy.$(VERSION).nupkg -k $(NUGET_API_KEY) -s $(NUGET_SOURCE)
	git tag rel-$(VERSION)
	git push --tags

guard-env-%:
	@ if [ "${${*}}" == "" ]; then \
		echo "$* must be set"; \
		exit 1; \
	fi

.PHONY: init
init:
	asdf install
