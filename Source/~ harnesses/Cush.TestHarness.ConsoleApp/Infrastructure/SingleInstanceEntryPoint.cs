﻿using System;
using Cush.Windows.SingleInstance;

namespace Cush.TestHarness.ConsoleApp.Infrastructure
{
    public static class SingleInstanceEntryPoint
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Program.ComposeObjectGraph().StartSingleInstance(args);
        }
    }
}