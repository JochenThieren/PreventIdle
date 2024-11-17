// ==================================================================================
// Program   : PreventIdle
// Version   : 1.0.0.0
// Author    : Thieren Jochen
// Date      : November 2024
// ----------------------------------------------------------------------------------
// Description:
// PreventIdle is designed to [briefly describe what the program does, if desired].
// This software is provided "as is," without any express or implied warranties.
// The author disclaims all liability for any errors, omissions, or damages arising
// from the use, misuse, or inability to use this code, whether foreseeable or not.
// ----------------------------------------------------------------------------------
// License:
// This code is released into the public domain. No fees or licenses are required for
// its use, modification, or distribution. Users are encouraged to adapt it as needed
// but assume all responsibility for any outcomes. By using this software, you accept
// these terms and agree to use it entirely at your own risk.
// ----------------------------------------------------------------------------------
// Notes:
// - This program was developed as a personal project and is shared for informational
//   or educational purposes. It may not meet production standards or best practices.
// - Feedback and improvements are welcome but not expected. Enjoy!
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