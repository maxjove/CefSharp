// Copyright © 2010 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

#pragma once

#include "Stdafx.h"

using namespace System::Collections::Generic;
using namespace System::IO;

namespace CefSharp
{
    namespace Core
    {
        /// <summary>
        /// Initialization settings. Many of these and other settings can also configured using command-line switches.
        /// WPF/WinForms/OffScreen each have their own CefSettings implementation that sets
        /// relevant settings e.g. OffScreen starts with audio muted.
        /// </summary>
        [System::ComponentModel::EditorBrowsableAttribute(System::ComponentModel::EditorBrowsableState::Never)]
        public ref class CefSettingsBase sealed
        {
        private:
            /// <summary>
            /// Command Line Arguments Dictionary. 
            /// </summary>
            CommandLineArgDictionary^ _cefCommandLineArgs;

        internal:
            /// <summary>
            /// CefSettings unmanaged pointer
            /// </summary>
            ::CefSettings* _cefSettings;
            /// <summary>
            /// CefCustomScheme collection
            /// </summary>
            List<CefCustomScheme^>^ _cefCustomSchemes;

        public:
            /// <summary>
            /// Default Constructor.
            /// </summary>
            CefSettingsBase() : _cefSettings(new ::CefSettings())
            {
                _cefSettings->multi_threaded_message_loop = true;
                _cefSettings->no_sandbox = true;
                BrowserSubprocessPath = Path::Combine(Path::GetDirectoryName(CefSettingsBase::typeid->Assembly->Location), "CefSharp.BrowserSubprocess.exe");
                _cefCustomSchemes = gcnew List<CefCustomScheme^>();
                _cefCommandLineArgs = gcnew CommandLineArgDictionary();
            }

            /// <summary>
            /// Finalizer.
            /// </summary>
            !CefSettingsBase()
            {
                _cefSettings = nullptr;
            }

            /// <summary>
            /// Destructor.
            /// </summary>
            ~CefSettingsBase()
            {
                this->!CefSettingsBase();
            }

            /// <summary>
            /// Add Customs schemes to this collection.
            /// </summary>
            property IEnumerable<CefCustomScheme^>^ CefCustomSchemes
            {
                IEnumerable<CefCustomScheme^>^ get() { return _cefCustomSchemes; }
            }

            /// <summary>
            /// Add custom command line argumens to this collection, they will be added in OnBeforeCommandLineProcessing. The
            /// CefSettings.CommandLineArgsDisabled value can be used to start with an empty command-line object. Any values specified in
            /// CefSettings that equate to command-line arguments will be set before this method is called.
            /// </summary>
            property CommandLineArgDictionary^ CefCommandLineArgs
            {
                CommandLineArgDictionary^ get() { return _cefCommandLineArgs; }
            }

            /// <summary>
            /// Set to true to disable configuration of browser process features using standard CEF and Chromium command-line arguments.
            /// Configuration can still be specified using CEF data structures or by adding to CefCommandLineArgs.
            /// </summary>
            property bool CommandLineArgsDisabled
            {
                bool get() { return _cefSettings->command_line_args_disabled == 1; }
                void set(bool value) { _cefSettings->command_line_args_disabled = value; }
            }

            /// <summary>
            /// Set to true to control browser process main (UI) thread message pump scheduling via the
            /// IBrowserProcessHandler.OnScheduleMessagePumpWork callback. This option is recommended for use in combination with the
            /// Cef.DoMessageLoopWork() function in cases where the CEF message loop must be integrated into an existing application message
            /// loop (see additional comments and warnings on Cef.DoMessageLoopWork). Enabling this option is not recommended for most users;
            /// leave this option disabled and use either MultiThreadedMessageLoop (the default) if possible.
            /// </summary>
            property bool ExternalMessagePump
            {
                bool get() { return _cefSettings->external_message_pump == 1; }
                void set(bool value) { _cefSettings->external_message_pump = value; }
            }

            /// <summary>
            /// Set to true to have the browser process message loop run in a separate thread. If false than the CefDoMessageLoopWork()
            /// function must be called from your application message loop. This option is only supported on Windows. The default value is
            /// true.
            /// </summary>
            property bool MultiThreadedMessageLoop
            {
                bool get() { return _cefSettings->multi_threaded_message_loop == 1; }
                void set(bool value) { _cefSettings->multi_threaded_message_loop = value; }
            }

            /// <summary>
            /// The path to a separate executable that will be launched for sub-processes. By default the browser process executable is used.
            /// See the comments on Cef.ExecuteProcess() for details. If this value is non-empty then it must be an absolute path.
            /// Also configurable using the "browser-subprocess-path" command-line switch.
            /// Defaults to using the provided CefSharp.BrowserSubprocess.exe instance
            /// </summary>
            property String^ BrowserSubprocessPath
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->browser_subprocess_path); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->browser_subprocess_path, value); }
            }

            /// <summary>
            /// The location where data for the global browser cache will be stored on disk. In this value is non-empty then it must be
            /// an absolute path that is must be either equal to or a child directory of CefSettings.RootCachePath (if RootCachePath is
            /// empty it will default to this value). If the value is empty then browsers will be created in "incognito mode" where
            /// in-memory caches are used for storage and no data is persisted to disk. HTML5 databases such as localStorage will only
            /// persist across sessions if a cache path is specified. Can be overridden for individual RequestContext instances via the
            /// RequestContextSettings.CachePath value.
            /// </summary>
            property String^ CachePath
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->cache_path); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->cache_path, value); }
            }

            /// <summary>
            /// The root directory for installation-specific data and the parent directory for profile-specific data.
            /// All CachePath and RequestContextSettings.CachePath" values must have this parent directory
            /// in common. If this value is empty and <see cref="CachePath"> is non-empty then it will default to the
            /// CachePath value. Any non-empty value must be an absolute path. If both values are empty then
            /// the default platform-specific directory will be used ("AppData\Local\CEF\User Data" directory under the user
            /// profile directory on Windows).
            ///
            /// **Use of the default directory is not recommended in production applications(see below).**
            /// 
            /// Multiple application instances writing to the same RootCachePath directory could result in data corruption.
            /// A process singleton lock based on the RootCachePath value is therefore used to protect against this.
            /// This singleton behavior applies to all CEF-based applications using version 120 or newer.
            /// You should customize RootCachePath for your application and implement IBrowserProcessHandler.OnAlreadyRunningAppRelaunch,
            /// which will then be called on any app relaunch with the same RootCachePath value.
            ///
            /// Failure to set the RootCachePath value correctly may result in startup crashes or other unexpected behaviors
            /// </summary>
            property String^ RootCachePath
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->root_cache_path); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->root_cache_path, value); }
            }

            /// <summary>
            /// The locale string that will be passed to WebKit. If empty the default locale of "en-US" will be used. Also configurable using
            /// the "lang" command-line switch.
            /// </summary>
            property String^ Locale
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->locale); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->locale, value); }
            }

            /// <summary>
            /// The fully qualified path for the locales directory. If this value is empty the locales directory must be located in the
            /// module directory. If this value is non-empty then it must be an absolute path. Also configurable using the "locales-dir-path"
            /// command-line switch.
            /// </summary>
            property String^ LocalesDirPath
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->locales_dir_path); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->locales_dir_path, value); }
            }

            /// <summary>
            /// The fully qualified path for the resources directory. If this value is empty the cef.pak and/or devtools_resources.pak files
            /// must be located in the module directory. Also configurable using the "resources-dir-path" command-line switch.
            /// </summary>
            property String^ ResourcesDirPath
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->resources_dir_path); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->resources_dir_path, value); }
            }

            /// <summary>
            /// The directory and file name to use for the debug log. If empty a default log file name and location will be used. On Windows
            /// a "debug.log" file will be written in the main executable directory. Also configurable using the"log-file" command- line
            /// switch.
            /// </summary>
            property String^ LogFile
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->log_file); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->log_file, value); }
            }

            /// <summary>
            /// The log severity. Only messages of this severity level or higher will be logged. When set to
            /// <see cref="CefSharp::LogSeverity::Disable"/> no messages will be written to the log file, but Fatal messages will still be
            /// output to stderr. Also configurable using the "log-severity" command-line switch with a value of "verbose", "info", "warning",
            /// "error", "fatal", "error-report" or "disable".
            /// </summary>
            property CefSharp::LogSeverity LogSeverity
            {
                CefSharp::LogSeverity get() { return (CefSharp::LogSeverity)_cefSettings->log_severity; }
                void set(CefSharp::LogSeverity value) { _cefSettings->log_severity = (cef_log_severity_t)value; }
            }

            /// <summary>
            /// Custom flags that will be used when initializing the V8 JavaScript engine. The consequences of using custom flags may not be
            /// well tested. Also configurable using the "js-flags" command-line switch.
            /// </summary>
            property String^ JavascriptFlags
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->javascript_flags); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->javascript_flags, value); }
            }

            /// <summary>
            /// Value that will be inserted as the product portion of the default User-Agent string. If empty the Chromium product version
            /// will be used. If UserAgent is specified this value will be ignored. Also configurable using the "user-agent-product" command-
            /// line switch.
            /// </summary>
            property String^ UserAgentProduct
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->user_agent_product); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->user_agent_product, value); }
            }

            /// <summary>
            /// Set to a value between 1024 and 65535 to enable remote debugging on the specified port. For example, if 8080 is specified the
            /// remote debugging URL will be http://localhost:8080. CEF can be remotely debugged from any CEF or Chrome browser window. Also
            /// configurable using the "remote-debugging-port" command-line switch.
            /// </summary>
            property int RemoteDebuggingPort
            {
                int get() { return _cefSettings->remote_debugging_port; }
                void set(int value) { _cefSettings->remote_debugging_port = value; }
            }

            /// <summary>
            /// The number of stack trace frames to capture for uncaught exceptions. Specify a positive value to enable the
            /// CefRenderProcessHandler:: OnUncaughtException() callback. Specify 0 (default value) and OnUncaughtException() will not be
            /// called. Also configurable using the "uncaught-exception-stack-size" command-line switch.
            /// </summary>
            property int UncaughtExceptionStackSize
            {
                int get() { return _cefSettings->uncaught_exception_stack_size; }
                void set(int value) { _cefSettings->uncaught_exception_stack_size = value; }
            }

            /// <summary>
            /// Value that will be returned as the User-Agent HTTP header. If empty the default User-Agent string will be used. Also
            /// configurable using the "user-agent" command-line switch.
            /// </summary>
            property String^ UserAgent
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->user_agent); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->user_agent, value); }
            }

            /// <summary>
            /// Set to true (1) to enable windowless (off-screen) rendering support. Do not enable this value if the application does not use
            /// windowless rendering as it may reduce rendering performance on some systems.
            /// </summary>
            property bool WindowlessRenderingEnabled
            {
                bool get() { return _cefSettings->windowless_rendering_enabled == 1; }
                void set(bool value) { _cefSettings->windowless_rendering_enabled = value; }
            }

            /// <summary>
            /// The directory where data for the global browser cache will be stored on disk.
            /// If this value is non-empty then it must be an absolute path that is either equal to or a child directory
            /// of RootCachePath. If this value is empty then browsers will be created in "incognito mode"
            /// where in-memory caches are used for storage and no profile-specific data is persisted to disk
            /// (installation-specific data will still be persisted in RootCachePath). HTML5 databases
            /// such as localStorage will only persist across sessions if a cache path is specified.
            /// Can be overridden for individual RequestContext instances via the RequestContextSettings.CachePath value.
            /// Any child directory value will be ignored and the "default" profile (also a child directory) will be used
            /// instead.
            /// </summary>
            property bool PersistSessionCookies
            {
                bool get() { return _cefSettings->persist_session_cookies == 1; }
                void set(bool value) { _cefSettings->persist_session_cookies = value; }
            }

            /// <summary>
            /// Comma delimited ordered list of language codes without any whitespace that will be used in the "Accept-Language" HTTP header.
            /// May be set globally using the CefSettings.AcceptLanguageList value. If both values are empty then "en-US,en" will be used.
            /// 
            /// </summary>
            property String^ AcceptLanguageList
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->accept_language_list); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->accept_language_list, value); }
            }

            /// <summary>
            /// Background color used for the browser before a document is loaded and when no document color is specified. The alpha
            /// component must be either fully opaque (0xFF) or fully transparent (0x00). If the alpha component is fully opaque then the RGB
            /// components will be used as the background color. If the alpha component is fully transparent for a WinForms browser then the
            /// default value of opaque white be used. If the alpha component is fully transparent for a windowless (WPF/OffScreen) browser
            /// then transparent painting will be enabled.
            /// </summary>
            property uint32_t BackgroundColor
            {
                uint32_t get() { return _cefSettings->background_color; }
                void set(uint32_t value) { _cefSettings->background_color = value; }
            }

            /// <summary>
            /// Comma delimited list of schemes supported by the associated
            /// ICookieManager. If CookieableSchemesExcludeDefaults is false the
            /// default schemes ("http", "https", "ws" and "wss") will also be supported.
            /// Specifying a CookieableSchemesList value and setting
            /// CookieableSchemesExcludeDefaults to true will disable all loading
            /// and saving of cookies for this manager. Can be overridden
            /// for individual RequestContext instances via the
            /// RequestContextSettings.CookieableSchemesList and
            /// RequestContextSettings.CookieableSchemesExcludeDefaults values.
            /// </summary>
            property String^ CookieableSchemesList
            {
                String^ get() { return StringUtils::ToClr(_cefSettings->cookieable_schemes_list); }
                void set(String^ value) { StringUtils::AssignNativeFromClr(_cefSettings->cookieable_schemes_list, value); }
            }
            
            /// <summary>
            /// If CookieableSchemesExcludeDefaults is false the
            /// default schemes ("http", "https", "ws" and "wss") will also be supported.
            /// Specifying a CookieableSchemesList value and setting
            /// CookieableSchemesExcludeDefaults to true will disable all loading
            /// and saving of cookies for this manager. Can be overridden
            /// for individual RequestContext instances via the
            /// RequestContextSettings.CookieableSchemesList and
            /// RequestContextSettings.CookieableSchemesExcludeDefaults values.
            /// </summary>
            property bool CookieableSchemesExcludeDefaults
            {
                bool get() { return _cefSettings->cookieable_schemes_exclude_defaults == 1; }
                void set(bool value) { _cefSettings->cookieable_schemes_exclude_defaults = value; }
            }

            /// <summary>
            /// Registers a custom scheme using the provided settings.
            /// </summary>
            /// <param name="cefCustomScheme">The CefCustomScheme which provides the details about the scheme.</param>
            void RegisterScheme(CefCustomScheme^ cefCustomScheme)
            {
                //Scheme names are converted to lowercase
                cefCustomScheme->SchemeName = cefCustomScheme->SchemeName->ToLower();

                _cefCustomSchemes->Add(cefCustomScheme);
            }
        };
    }
}
