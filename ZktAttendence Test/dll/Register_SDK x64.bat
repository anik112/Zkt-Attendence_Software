cd /d %~dp0
if /i "%PROCESSOR_IDENTIFIER:~0,3%"=="X86" (
	echo system is x86
	
	) else (
		echo system is x64
		copy .\*.dll %windir%\system32\
		regsvr32 %windir%\system32\zkemkeeper.dll
	)
pause