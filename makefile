XBFLAGS = /p:TreatWarningsAsErrors="true" /nologo /verbosity:quiet
ROOT := $(shell pwd)

all: clean
	@printf "Copying native libs..."
	@cp thirdparty/osx/*.dylib .
	@cp thirdparty/*.config .
	@printf " done\n"
	@cd lib/fna/ \
		&& printf "Building fna..." \
		&& xbuild $(XBFLAGS) \
		&& printf " done\n" \
		&& printf "Copying FNA.dll..." \
		&& cp bin/Debug/FNA.dll $(ROOT)/thirdparty \
		&& printf " done\n" \
		&& cd $(ROOT)
	@printf "Building..."
	@xbuild $(XBFLAGS)
	@printf " done\n"

subs:
	@printf "Updating submodules..."
	@git submodule update
	@cp lib/fna/bin/Debug/FNA.dll $(ROOT)/thirdparty
	@printf " done\n"

clean:
	@printf "Cleaning..."
	@rm -rf *.exe
	@rm -rf *.mdb
	@rm -rf *.dll
	@rm -rf *.dylib
	@rm -rf *.config
	@printf " done\n"

run:
	@mono --debug game.exe

