cd /d %~dp0
if /i "%PROCESSOR_IDENTIFIER:~0,3%"=="X86" (
	echo system is x86
	
	) else (
		echo system is x64
		regsvr32 %windir%\system32\zkemkeeper.dll -u
		del %windir%\system32\commpro.dll
		del %windir%\system32\comms.dll
		del %windir%\system32\rscagent.dll
		del %windir%\system32\rscomm.dll
		del %windir%\system32\tcpcomm.dll
		del %windir%\system32\usbcomm.dll
		del %windir%\system32\usbstd.dll
		del %windir%\system32\zkemkeeper.dll
		del %windir%\system32\zkemsdk.dll
		del %windir%\system32\plcommpro.dll
		del %windir%\system32\plcomms.dll
		del %windir%\system32\plrscagent.dll
		del %windir%\system32\plrscomm.dll
		del %windir%\system32\pltcpcomm.dll
		del %windir%\system32\plusbcomm.dll
		del %windir%\system32\ZKCommuCryptoClient.dll
		del %windir%\system32\libareacode.dll
	)
pause