﻿using System;

namespace Cush.TestHarness.WinService.Infrastructure
{
    public static class SingleInstanceEntryPoint
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            var inst = new ServiceInstaller();

            Engine.ComposeObjectGraph().Start(args);
        }
    }
}