// ==================================================================================
// Program   : PreventIdle
// Version   : 1.0.0.0
// Author    : Thieren Jochen
// Date      : November 2024
// ----------------------------------------------------------------------------------
// Description:
// PreventIdle is a lightweight tool designed to keep your PC active and prevent it
// from entering sleep or idle states while you wait for tasks to complete.
// It achieves this by utilizing the Windows API function `SetThreadExecutionState`
// to signal system activity and performing minor, imperceptible mouse movements
// to ensure the system remains active.
//
// The program is intentionally designed to operate without requiring administrative
// privileges. This avoids any need for creating log files, modifying system registry
// keys, or other operations that may require elevated permissions.
//
// This software is provided "as is," without any express or implied warranties.
// The author assumes no responsibility for errors, omissions, or any consequences
// arising from the use or misuse of this tool. Users accept full responsibility
// for using it at their own risk.
// ----------------------------------------------------------------------------------
// License:
// PreventIdle is released into the public domain. No fees or licenses are required
// for its use, modification, or distribution. Users are encouraged to adapt the
// code as needed but assume full responsibility for any outcomes. By using this
// software, you agree to these terms and conditions.
// ----------------------------------------------------------------------------------
// Notes:
// - This program was developed as a personal project and is shared for educational
//   and informational purposes. It may not adhere to production-grade standards
//   or industry best practices.
// - Feedback and suggestions for improvement are welcome but not mandatory. Enjoy!
// ==================================================================================

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace PreventIdle.Objects
{
    public static class _PreventIdle
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint SetThreadExecutionState(uint esFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;
        private const uint MOUSEEVENTF_MOVE = 0x0001;

        private static Timer _timer;
        private static bool _isRunning = false;

        public static void StartKeepingAlive()
        {
            if (_isRunning) return; // Prevent starting multiple times

            _isRunning = true;
            _timer = new Timer(_ =>
            {
                SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
                mouse_event(MOUSEEVENTF_MOVE, 1, 1, 0, UIntPtr.Zero);
                mouse_event(MOUSEEVENTF_MOVE, -1, -1, 0, UIntPtr.Zero);
            }, null, 0, 60000); // Runs every minute
        }

        public static void StopKeepingAlive()
        {
            // Check if the timer is not null and still running before attempting to dispose
            if (_timer != null && _isRunning)
            {
                _timer?.Dispose();  // Dispose the timer
                _timer = null;      // Set it to null
                _isRunning = false; // Update the state to false
            }
        }
    }
}