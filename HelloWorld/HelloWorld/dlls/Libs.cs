using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.dlls
{
    class Libs
    {

        public byte[] pcdll = new byte[20001];
        public byte[] Readbuffer = new byte[513];

        // Open Com Port
        [System.Runtime.InteropServices.DllImport("TSMCOM32.DLL")]
            public static extern int TSMOpenComm(string port, long p1, byte p2, long p3, long p4, long p5, long p6, ref byte pcdll);

            // Close Com Port
            [System.Runtime.InteropServices.DllImport("TSMCOM32.DLL")]
            public static extern int TSMCloseComm(ref byte pcdll);

            [System.Runtime.InteropServices.DllImport("TSMCOM32.DLL")]
            public static extern int TSMSetRespondPeriod(ref byte pcdll, int iPeriod);

            // Set TimeOut
            [System.Runtime.InteropServices.DllImport("TSMCOM32.DLL")]
            public static extern int TSMSetTimeout(ref byte pcdll, int iTimeout);

            // Clean Data
            [System.Runtime.InteropServices.DllImport("TSMCOM32.DLL")]
            public static extern int TSM_CLMSP04(ref byte pcdll, long nNodeId);

            // Console inquiry working node for link status
            [System.Runtime.InteropServices.DllImport("TSMCOM32.DLL")]
            public static extern int TSM_ENQND05(ref byte pcdll, long nNodeId);


        public void FN_opencom(string pscom, long psbaud)
        {
            // Get COM PORT
            long lbaud;

            lbaud = psbaud;
            int Com_check = TSMOpenComm(pscom, lbaud, 110, 8, 1, 2048, 2048, ref pcdll[1]);
            Console.WriteLine(Com_check);
            Console.WriteLine(">> "+pcdll[1]);
        }


    }
}
