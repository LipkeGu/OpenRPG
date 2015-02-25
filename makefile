all:
	@cp thirdparty/*.dylib .
	@cp thirdparty/*.config .
	@xbuild /p:TreatWarningsAsErrors="true" /noconsolelogger /nologo

clean:
	@rm -rf *.exe
	@rm -rf *.mdb
	@rm -rf *.dll
	@rm -rf *.dylib
	@rm -rf *.config

