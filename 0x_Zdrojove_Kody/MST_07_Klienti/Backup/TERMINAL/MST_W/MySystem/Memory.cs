using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Fask.MST_W.MySystem
{
    public class Memory
    {
        public struct MEMORYSTATUS
        {
            public UInt32 dwLength;
            public UInt32 dwMemoryLoad;
            public UInt32 dwTotalPhys;
            public UInt32 dwAvailPhys;
            public UInt32 dwTotalPageFile;
            public UInt32 dwAvailPageFile;
            public UInt32 dwTotalVirtual;
            public UInt32 dwAvailVirtual;
        }

        [DllImport("CoreDll.dll")]
        public static extern void GlobalMemoryStatus
        (
            ref MEMORYSTATUS lpBuffer
        );

        [DllImport("CoreDll.dll")]
        public static extern int GetSystemMemoryDivision
        (
            ref UInt32 lpdwStorePages,
            ref UInt32 lpdwRamPages,
            ref UInt32 lpdwPageSize
        );

        public static string Info()
        {
            //GC.Collect();

            UInt32 storePages = 0;
            UInt32 ramPages = 0;
            UInt32 pageSize = 0;
            int res = GetSystemMemoryDivision(ref storePages,
                ref ramPages, ref pageSize);

            // Call the native GlobalMemoryStatus method
            // with the defined structure.
            MEMORYSTATUS memStatus = new MEMORYSTATUS();
            GlobalMemoryStatus(ref memStatus);

            // Use a StringBuilder for the message box string.
            StringBuilder MemoryInfo = new StringBuilder();
            MemoryInfo.Append("Memory Load: "
                + memStatus.dwMemoryLoad.ToString() + "\n");
            MemoryInfo.Append("Total Physical: "
                + memStatus.dwTotalPhys.ToString() + "\n");
            MemoryInfo.Append("Avail Physical: "
                + memStatus.dwAvailPhys.ToString() + "\n");
            MemoryInfo.Append("Total Page File: "
                + memStatus.dwTotalPageFile.ToString() + "\n");
            MemoryInfo.Append("Avail Page File: "
                + memStatus.dwAvailPageFile.ToString() + "\n");
            MemoryInfo.Append("Total Virtual: "
                + memStatus.dwTotalVirtual.ToString() + "\n");
            MemoryInfo.Append("Avail Virtual: "
                + memStatus.dwAvailVirtual.ToString() + "\n");

            // Show the available memory.
            return MemoryInfo.ToString();
        }
    }
}
