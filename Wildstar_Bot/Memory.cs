using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace Wildstar_Bot
{
    public class Memory
    {
        #region DllImport_Structs_Constants

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
          int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress,
          byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        const int PROCESS_WM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        //----------------------DebugPriv---------------------
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle,
            UInt32 DesiredAccess, out IntPtr TokenHandle);

        static uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        static uint STANDARD_RIGHTS_READ = 0x00020000;
        static uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        static uint TOKEN_DUPLICATE = 0x0002;
        static uint TOKEN_IMPERSONATE = 0x0004;
        static uint TOKEN_QUERY = 0x0008;
        static uint TOKEN_QUERY_SOURCE = 0x0010;
        static uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        static uint TOKEN_ADJUST_GROUPS = 0x0040;
        static uint TOKEN_ADJUST_DEFAULT = 0x0080;
        static uint TOKEN_ADJUST_SESSIONID = 0x0100;
        static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        static uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
            TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
            TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
            TOKEN_ADJUST_SESSIONID);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
            out LUID lpLuid);

        const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
        const string SE_AUDIT_NAME = "SeAuditPrivilege";
        const string SE_BACKUP_NAME = "SeBackupPrivilege";
        const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
        const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
        const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
        const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
        const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";
        const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
        const string SE_DEBUG_NAME = "SeDebugPrivilege";
        const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
        const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";
        const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
        const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
        const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";
        const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
        const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
        const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
        const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
        const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
        const string SE_RELABEL_NAME = "SeRelabelPrivilege";
        const string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";
        const string SE_RESTORE_NAME = "SeRestorePrivilege";
        const string SE_SECURITY_NAME = "SeSecurityPrivilege";
        const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";
        const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
        const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
        const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";
        const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
        const string SE_TCB_NAME = "SeTcbPrivilege";
        const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";
        const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";
        const string SE_UNDOCK_NAME = "SeUndockPrivilege";
        const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";

        [StructLayout(LayoutKind.Sequential)]
        struct LUID
        {
            UInt32 LowPart;
            Int32 HighPart;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hHandle);

        const UInt32 SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
        const UInt32 SE_PRIVILEGE_REMOVED = 0x00000004;
        const UInt32 SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;

        [StructLayout(LayoutKind.Sequential)]
        struct TOKEN_PRIVILEGES
        {
           public UInt32 PrivilegeCount;
           public LUID Luid;
           public UInt32 Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
           [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
           ref TOKEN_PRIVILEGES NewState,
           UInt32 Zero,
           IntPtr Null1,
           IntPtr Null2);
        #endregion

        int pID;
        Process process;

        public Memory(int pID){
            this.pID = pID;
            process = Process.GetProcessById(pID);
        }

        public String ReadMemory(Int32 address)
        {
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, pID);
            int bytesRead = 0;
            byte[] buffer = new byte[30];
            ReadProcessMemory((int)processHandle, address, buffer, buffer.Length, ref bytesRead);
            return Encoding.Unicode.GetString(buffer);
        }

        public void WrtieMemory(Int32 address, int value)
        {
            IntPtr processHandle = OpenProcess(0x1F0FFF, false, pID);
            int bytesWritten = 0;
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory((int)processHandle, address, buffer, buffer.Length, ref bytesWritten);
        }

        public void SetDebugPriveleg()
        {
            IntPtr hToken;
            LUID luidSEDebugNameValue;
            TOKEN_PRIVILEGES tkpPrivileges;

            if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken))
            {
                Console.WriteLine("OpenProcessToken() failed, error = {0} . SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
                return;
            }
            else
            {
                Console.WriteLine("OpenProcessToken() successfully");
            }

            if (!LookupPrivilegeValue(null, SE_DEBUG_NAME, out luidSEDebugNameValue))
            {
                Console.WriteLine("LookupPrivilegeValue() failed, error = {0} .SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
                CloseHandle(hToken);
                return;
            }
            else
            {
                Console.WriteLine("LookupPrivilegeValue() successfully");
            }

            tkpPrivileges.PrivilegeCount = 1;
            tkpPrivileges.Luid = luidSEDebugNameValue;
            tkpPrivileges.Attributes = SE_PRIVILEGE_ENABLED;

            if (!AdjustTokenPrivileges(hToken, false, ref tkpPrivileges, 0, IntPtr.Zero, IntPtr.Zero))
            {
                Console.WriteLine("LookupPrivilegeValue() failed, error = {0} .SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
            }
            else
            {
                Console.WriteLine("SeDebugPrivilege is now available");
            }
            CloseHandle(hToken);
            Console.ReadLine();
        }
    }
}
