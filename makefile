all: clean
	@cp thirdparty/osx/*.dylib .
	@cp thirdparty/win/*.dll .
	@cp thirdparty/*.config .
	@xbuild /p:TreatWarningsAsErrors="true" /nologo

clean:
	@rm -rf *.exe
	@rm -rf *.mdb
	@rm -rf *.dll
	@rm -rf *.dylib
	@rm -rf *.config

