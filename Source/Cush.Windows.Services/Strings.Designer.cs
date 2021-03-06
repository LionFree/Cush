﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cush.Windows.Services {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cush.Windows.Services.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sorry, I did not understand that input! Try again..
        /// </summary>
        internal static string DEBUG_BadKey {
            get {
                return ResourceManager.GetString("DEBUG_BadKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cush.
        /// </summary>
        internal static string DEBUG_Cush {
            get {
                return ResourceManager.GetString("DEBUG_Cush", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to   {0}.
        /// </summary>
        internal static string DEBUG_EndpointHeader {
            get {
                return ResourceManager.GetString("DEBUG_EndpointHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Endpoints:.
        /// </summary>
        internal static string DEBUG_EndpointHeaderPlural {
            get {
                return ResourceManager.GetString("DEBUG_EndpointHeaderPlural", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Endpoint:.
        /// </summary>
        internal static string DEBUG_EndpointHeaderSingular {
            get {
                return ResourceManager.GetString("DEBUG_EndpointHeaderSingular", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter [P]ause, [R]esume, or [Q]uit :.
        /// </summary>
        internal static string DEBUG_EnterPauseResumeOrQuit {
            get {
                return ResourceManager.GetString("DEBUG_EnterPauseResumeOrQuit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pausing {0}..
        /// </summary>
        internal static string DEBUG_PausingService {
            get {
                return ResourceManager.GetString("DEBUG_PausingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} resuming..
        /// </summary>
        internal static string DEBUG_ResumingService {
            get {
                return ResourceManager.GetString("DEBUG_ResumingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} :built {1}.
        /// </summary>
        internal static string DEBUG_ServiceNameAndBuildDate {
            get {
                return ResourceManager.GetString("DEBUG_ServiceNameAndBuildDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service started..
        /// </summary>
        internal static string DEBUG_ServiceStarted {
            get {
                return ResourceManager.GetString("DEBUG_ServiceStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} stopped.  Shutting down..
        /// </summary>
        internal static string DEBUG_ServiceStopped {
            get {
                return ResourceManager.GetString("DEBUG_ServiceStopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} stopping....
        /// </summary>
        internal static string DEBUG_ServiceStopping {
            get {
                return ResourceManager.GetString("DEBUG_ServiceStopping", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} starting..
        /// </summary>
        internal static string DEBUG_StartingService {
            get {
                return ResourceManager.GetString("DEBUG_StartingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot open the service host for {0}..
        /// </summary>
        internal static string Error_CannotOpenHost {
            get {
                return ResourceManager.GetString("Error_CannotOpenHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot open the service host for {0}.  The address reports access denied..
        /// </summary>
        internal static string Error_CannotOpenHost_AccessDenied {
            get {
                return ResourceManager.GetString("Error_CannotOpenHost_AccessDenied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot open the service host for {0}.  The address is already in use..
        /// </summary>
        internal static string Error_CannotOpenHost_AddressInUse {
            get {
                return ResourceManager.GetString("Error_CannotOpenHost_AddressInUse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot find existing service in registry..
        /// </summary>
        internal static string EXCEPTION_CannotFindServiceInRegistry {
            get {
                return ResourceManager.GetString("EXCEPTION_CannotFindServiceInRegistry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXCEPTION: {0}.
        /// </summary>
        internal static string EXCEPTION_HeaderWithMessage {
            get {
                return ResourceManager.GetString("EXCEPTION_HeaderWithMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service description needs type or object..
        /// </summary>
        internal static string EXCEPTION_ServiceDescriptionNeedsTypeOrObject {
            get {
                return ResourceManager.GetString("EXCEPTION_ServiceDescriptionNeedsTypeOrObject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Types deriving from the WindowsService class (such as {0}) must be decorated with a WindowsServiceAttribute..
        /// </summary>
        internal static string EXCEPTION_ServiceMustBeMarkedWithAttribute {
            get {
                return ResourceManager.GetString("EXCEPTION_ServiceMustBeMarkedWithAttribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Done..
        /// </summary>
        internal static string INFO_Done {
            get {
                return ResourceManager.GetString("INFO_Done", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SMTP Client: Email Sent..
        /// </summary>
        internal static string INFO_EmailSent {
            get {
                return ResourceManager.GetString("INFO_EmailSent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Installing &apos;{0}&apos; service...  .
        /// </summary>
        internal static string INFO_InstallingWindowsService {
            get {
                return ResourceManager.GetString("INFO_InstallingWindowsService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Paused. Attach debugger to process, then press Enter to continue..
        /// </summary>
        internal static string INFO_PausedForDebugger {
            get {
                return ResourceManager.GetString("INFO_PausedForDebugger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} service is already running..
        /// </summary>
        internal static string INFO_ServiceAlreadyRunning {
            get {
                return ResourceManager.GetString("INFO_ServiceAlreadyRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} service is not started..
        /// </summary>
        internal static string INFO_ServiceNotStarted {
            get {
                return ResourceManager.GetString("INFO_ServiceNotStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} paused..
        /// </summary>
        internal static string INFO_ServicePaused {
            get {
                return ResourceManager.GetString("INFO_ServicePaused", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} resumed..
        /// </summary>
        internal static string INFO_ServiceResumed {
            get {
                return ResourceManager.GetString("INFO_ServiceResumed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} service is {1}..
        /// </summary>
        internal static string INFO_ServiceStatus {
            get {
                return ResourceManager.GetString("INFO_ServiceStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service stopped..
        /// </summary>
        internal static string INFO_ServiceStopped {
            get {
                return ResourceManager.GetString("INFO_ServiceStopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Starting service {0} as a background service.  .
        /// </summary>
        internal static string INFO_StartingBackgroundService {
            get {
                return ResourceManager.GetString("INFO_StartingBackgroundService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Starting {0} service...  .
        /// </summary>
        internal static string INFO_StartingService {
            get {
                return ResourceManager.GetString("INFO_StartingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stopping {0} service...  .
        /// </summary>
        internal static string INFO_StoppingService {
            get {
                return ResourceManager.GetString("INFO_StoppingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Uninstalling &apos;{0}&apos; service...  .
        /// </summary>
        internal static string INFO_UninstallingWindowsService {
            get {
                return ResourceManager.GetString("INFO_UninstallingWindowsService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to P.
        /// </summary>
        internal static string KEY_Pause {
            get {
                return ResourceManager.GetString("KEY_Pause", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Q.
        /// </summary>
        internal static string KEY_Quit {
            get {
                return ResourceManager.GetString("KEY_Quit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to R.
        /// </summary>
        internal static string KEY_Resume {
            get {
                return ResourceManager.GetString("KEY_Resume", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to N.
        /// </summary>
        internal static string Noun_NoKey {
            get {
                return ResourceManager.GetString("Noun_NoKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Y.
        /// </summary>
        internal static string Noun_YesKey {
            get {
                return ResourceManager.GetString("Noun_YesKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot stop service..
        /// </summary>
        internal static string WARN_CannotStopService {
            get {
                return ResourceManager.GetString("WARN_CannotStopService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Install this version anyway [{0}/{1}]?  .
        /// </summary>
        internal static string Warn_InstallAnyway {
            get {
                return ResourceManager.GetString("Warn_InstallAnyway", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Installed version is newer..
        /// </summary>
        internal static string Warn_InstalledVersionIsNewer {
            get {
                return ResourceManager.GetString("Warn_InstalledVersionIsNewer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} service is already installed..
        /// </summary>
        internal static string WARN_ServiceAlreadyInstalled {
            get {
                return ResourceManager.GetString("WARN_ServiceAlreadyInstalled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} service is not installed..
        /// </summary>
        internal static string WARN_ServiceNotInstalled {
            get {
                return ResourceManager.GetString("WARN_ServiceNotInstalled", resourceCulture);
            }
        }
    }
}
