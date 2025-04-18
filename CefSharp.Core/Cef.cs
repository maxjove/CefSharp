// Copyright © 2020 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

//NOTE:Classes in the CefSharp.Core namespace have been hidden from intellisnse so users don't use them directly

using System;
using System.Threading.Tasks;
using CefSharp.Enums;
using CefSharp.Internals;

namespace CefSharp
{
    /// <summary>
    /// Global CEF methods are exposed through this class. e.g. CefInitalize maps to Cef.Initialize
    /// CEF API Doc https://magpcss.org/ceforum/apidocs3/projects/(default)/(_globals).html
    /// This class cannot be inherited.
    /// </summary>
    public static class Cef
    {
        /// <summary>
        /// Event is raised when <see cref="Cef.Shutdown"/> is called,
        /// before the shutdown logic is executed.
        /// </summary>
        /// <remarks>
        /// Will be called on the same thread as <see cref="Cef.Shutdown"/>
        /// </remarks>
        public static event EventHandler ShutdownStarted;

        public static TaskFactory UIThreadTaskFactory
        {
            get { return Core.Cef.UIThreadTaskFactory; }
        }
        public static TaskFactory IOThreadTaskFactory
        {
            get { return Core.Cef.IOThreadTaskFactory; }
        }
        public static TaskFactory FileThreadTaskFactory
        {
            get { return Core.Cef.FileThreadTaskFactory; }
        }

        public static void AddDisposable(IDisposable item)
        {
            Core.Cef.AddDisposable(item);
        }

        public static void RemoveDisposable(IDisposable item)
        {
            Core.Cef.RemoveDisposable(item);
        }

        /// <summary>Gets a value that indicates whether CefSharp is initialized.</summary>
        /// <value>
        /// true if CefSharp is initialized; returns false if initialize failed.
        /// null if initialize not yet called.
        /// </value>
        public static bool? IsInitialized
        {
            get { return Core.Cef.IsInitialized; }
        }

        /// <summary>Gets a value that indicates whether CefSharp was shutdown.</summary>
        /// <value>true if CefSharp was shutdown; otherwise, false.</value>
        public static bool IsShutdown
        {
            get { return Core.Cef.IsShutdown; }
        }

        /// <summary>Gets a value that indicates the version of CefSharp currently being used.</summary>
        /// <value>The CefSharp version.</value>
        public static string CefSharpVersion
        {
            get { return Core.Cef.CefSharpVersion; }
        }

        /// <summary>Gets a value that indicates the CEF version currently being used.</summary>
        /// <value>The CEF Version</value>
        public static string CefVersion
        {
            get { return Core.Cef.CefVersion; }
        }

        /// <summary>
        /// API version that will be compiled client-side. The experimental (unversioned)
        /// API is selected by default. Clients can set the CEF_API_VERSION value in
        /// their project configuration to configure an explicit API version. Unlike
        /// the experimental API, explicit API versions are back/forward compatible with
        /// a specific range of CEF versions.
        /// </summary>
        public static int ApiVersion
        {
            get { return Core.Cef.ApiVersion; }
        }
        

        /// <summary>
        /// API hashes for the selected CEF_API_VERSION. API hashes are created for
        /// each version by analyzing CEF header files for C API type definitions. The
        /// hash value will change when header files are modified in a way that may
        /// cause binary incompatibility with other builds.
        /// </summary>
        public static string ApiHashPlatform
        {
            get { return Core.Cef.ApiHashPlatform; }
        }

        /// <summary>Gets a value that indicates the Chromium version currently being used.</summary>
        /// <value>The Chromium version.</value>
        public static string ChromiumVersion
        {
            get { return Core.Cef.ChromiumVersion; }
        }

        /// <summary>
        /// Gets a value that indicates the Git Hash for CEF version currently being used.
        /// </summary>
        /// <value>The Git Commit Hash</value>
        public static string CefCommitHash
        {
            get { return Core.Cef.CefCommitHash; }
        }

        /// <summary>
        /// Configures the CEF API version and returns API hashes for the libcef
        /// library. Defaults to CEF_API_HASH_PLATFORM
        /// </summary>
        /// <param name="version">parameter should be CEF_API_VERSION and any changes to this value will be ignored after the first call to this method.</param>
        /// <returns>
        /// returns API hashes for the libcef library.
        /// The returned string is owned by the library and should not be freed.
        /// </returns>
        public static string ApiHash(int version)
        {
            return ApiHash(version, 0);
        }

        /// <summary>
        /// Configures the CEF API version and returns API hashes for the libcef
        /// library.  The entry parameter describes which hash value will be returned:
        ///
        /// 0 - CEF_API_HASH_PLATFORM
        /// 1 - CEF_API_HASH_UNIVERSAL (deprecated, same as CEF_API_HASH_PLATFORM)
        /// 2 - CEF_COMMIT_HASH (from cef_version.h)
        ///
        /// </summary>
        /// <param name="version">parameter should be CEF_API_VERSION and any changes to this value will be ignored after the first call to this method.</param>
        /// <param name="entry">The entry parameter describes which hash value will be returned:</param>
        /// <returns>
        /// returns API hashes for the libcef library.
        /// The returned string is owned by the library and should not be freed.
        /// </returns>
        public static string ApiHash(int version, int entry)
        {
            return Core.Cef.ApiHash(version, entry);
        }

        /// <summary>
        /// Parse the specified url into its component parts.
        /// Uses a GURL to parse the Url. GURL is Google's URL parsing library.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Returns null if the URL is empty or invalid.</returns>
        public static UrlParts ParseUrl(string url)
        {
            return Core.Cef.ParseUrl(url);
        }

        /// <summary>
        /// Initializes CefSharp with user-provided settings.
        /// It's important to note that Initialize and Shutdown <strong>MUST</strong> be called on your main
        /// application thread (typically the UI thread). If you call them on different
        /// threads, your application will hang. See the documentation for Cef.Shutdown() for more details.
        /// </summary>
        /// <param name="settings">CefSharp configuration settings.</param>
        /// <returns>true if successful; otherwise, false.</returns>
        public static bool Initialize(CefSettingsBase settings)
        {
            using (settings)
            {
                return Core.Cef.Initialize(settings.settings);
            }
        }

        /// <summary>
        /// Initializes CefSharp with user-provided settings.
        /// It's important to note that Initialize/Shutdown <strong>MUST</strong> be called on your main
        /// application thread (typically the UI thread). If you call them on different
        /// threads, your application will hang. See the documentation for Cef.Shutdown() for more details.
        /// </summary>
        /// <param name="settings">CefSharp configuration settings.</param>
        /// <param name="performDependencyCheck">Check that all relevant dependencies available, throws exception if any are missing</param>
        /// <returns>true if successful; otherwise, false.</returns>
        public static bool Initialize(CefSettingsBase settings, bool performDependencyCheck)
        {
            using (settings)
            {
                return Core.Cef.Initialize(settings.settings, performDependencyCheck);
            }
        }

        /// <summary>
        /// Initializes CefSharp with user-provided settings.
        /// It's important to note that Initialize/Shutdown <strong>MUST</strong> be called on your main
        /// application thread (typically the UI thread). If you call them on different
        /// threads, your application will hang. See the documentation for Cef.Shutdown() for more details.
        /// </summary>
        /// <param name="settings">CefSharp configuration settings.</param>
        /// <param name="performDependencyCheck">Check that all relevant dependencies available, throws exception if any are missing</param>
        /// <param name="browserProcessHandler">The handler for functionality specific to the browser process. Null if you don't wish to handle these events</param>
        /// <returns>true if successful; otherwise, false.</returns>
        public static bool Initialize(CefSettingsBase settings, bool performDependencyCheck, IBrowserProcessHandler browserProcessHandler)
        {
            using (settings)
            {
                return Core.Cef.Initialize(settings.settings, performDependencyCheck, browserProcessHandler);
            }
        }

        /// <summary>
        /// Initializes CefSharp with user-provided settings.
        /// It's important to note that Initialize/Shutdown <strong>MUST</strong> be called on your main
        /// application thread (typically the UI thread). If you call them on different
        /// threads, your application will hang. See the documentation for Cef.Shutdown() for more details.
        /// </summary>
        /// <param name="settings">CefSharp configuration settings.</param>
        /// <param name="performDependencyCheck">Check that all relevant dependencies available, throws exception if any are missing</param>
        /// <param name="cefApp">Implement this interface to provide handler implementations. Null if you don't wish to handle these events</param>
        /// <returns>true if successful; otherwise, false.</returns>
        public static bool Initialize(CefSettingsBase settings, bool performDependencyCheck, IApp cefApp)
        {
            using (settings)
            {
                return Core.Cef.Initialize(settings.settings, performDependencyCheck, cefApp);
            }
        }

        /// <summary>
        /// Initializes CefSharp with user-provided settings. This method allows you to wait for
        /// <see cref="IBrowserProcessHandler.OnContextInitialized"/> to be called before continuing. 
        /// It's important to note that Initialize and Shutdown <strong>MUST</strong> be called on your main
        /// application thread (typically the UI thread). If you call them on different
        /// threads, your application will hang. See the documentation for Cef.Shutdown() for more details.
        /// </summary>
        /// <param name="settings">CefSharp configuration settings.</param>
        /// <param name="performDependencyCheck">Check that all relevant dependencies available, throws exception if any are missing</param>
        /// <param name="browserProcessHandler">The handler for functionality specific to the browser process. Null if you don't wish to handle these events</param>
        /// <returns>returns a Task that can be awaited. true if successful; otherwise, false. If false check the log file for possible errors</returns>
        /// <remarks>
        /// If successful then the Task will be completed successfully when <see cref="IBrowserProcessHandler.OnContextInitialized"/> is called.
        /// If successful then the continuation will happen syncrionously on the CEF UI thread.
        /// </remarks>
        public static Task<bool> InitializeAsync(CefSettingsBase settings, bool performDependencyCheck = true, IBrowserProcessHandler browserProcessHandler = null)
        {
            using (settings)
            {
                try
                {
                    //Ignore the result, the Task will be set in Core.Cef.Initialze
                    Core.Cef.Initialize(settings.settings, performDependencyCheck, browserProcessHandler);
                }
                catch (Exception ex)
                {
                    GlobalContextInitialized.SetException(ex);
                }
            }

            return GlobalContextInitialized.Task;
        }

        /// <summary>
        /// Run the CEF message loop. Use this function instead of an application-
        /// provided message loop to get the best balance between performance and CPU
        /// usage. This function should only be called on the main application thread and
        /// only if Cef.Initialize() is called with a
        /// CefSettings.MultiThreadedMessageLoop value of false. This function will
        /// block until a quit message is received by the system.
        /// </summary>
        public static void RunMessageLoop()
        {
            Core.Cef.RunMessageLoop();
        }

        /// <summary>
        /// Quit the CEF message loop that was started by calling Cef.RunMessageLoop().
        /// This function should only be called on the main application thread and only
        /// if Cef.RunMessageLoop() was used.
        /// </summary>
        public static void QuitMessageLoop()
        {
            Core.Cef.QuitMessageLoop();
        }

        /// <summary>
        /// Perform a single iteration of CEF message loop processing.This function is
        /// provided for cases where the CEF message loop must be integrated into an
        /// existing application message loop. Use of this function is not recommended
        /// for most users; use CefSettings.MultiThreadedMessageLoop if possible (the default).
        /// When using this function care must be taken to balance performance
        /// against excessive CPU usage. It is recommended to enable the
        /// CefSettings.ExternalMessagePump option when using
        /// this function so that IBrowserProcessHandler.OnScheduleMessagePumpWork()
        /// callbacks can facilitate the scheduling process. This function should only be
        /// called on the main application thread and only if Cef.Initialize() is called
        /// with a CefSettings.MultiThreadedMessageLoop value of false. This function
        /// will not block.
        /// </summary>
        public static void DoMessageLoopWork()
        {
            Core.Cef.DoMessageLoopWork();
        }

        /// <summary>
        /// This function should be called from the application entry point function to execute a secondary process.
        /// It can be used to run secondary processes from the browser client executable (default behavior) or
        /// from a separate executable specified by the CefSettings.browser_subprocess_path value.
        /// If called for the browser process (identified by no "type" command-line value) it will return immediately with a value of -1.
        /// If called for a recognized secondary process it will block until the process should exit and then return the process exit code.
        /// The |application| parameter may be empty. The |windows_sandbox_info| parameter is only used on Windows and may be NULL (see cef_sandbox_win.h for details). 
        /// </summary>
        public static int ExecuteProcess()
        {
            return Core.Cef.ExecuteProcess();
        }

        /// <summary>Add an entry to the cross-origin whitelist.</summary>
        /// <param name="sourceOrigin">The origin allowed to be accessed by the target protocol/domain.</param>
        /// <param name="targetProtocol">The target protocol allowed to access the source origin.</param>
        /// <param name="targetDomain">The optional target domain allowed to access the source origin.</param>
        /// <param name="allowTargetSubdomains">If set to true would allow a blah.example.com if the 
        ///     <paramref name="targetDomain"/> was set to example.com
        /// </param>
        /// <returns>Returns false if is invalid or the whitelist cannot be accessed.</returns>
        /// <remarks>
        /// The same-origin policy restricts how scripts hosted from different origins
        /// (scheme + domain + port) can communicate. By default, scripts can only access
        /// resources with the same origin. Scripts hosted on the HTTP and HTTPS schemes
        /// (but no other schemes) can use the "Access-Control-Allow-Origin" header to
        /// allow cross-origin requests. For example, https://source.example.com can make
        /// XMLHttpRequest requests on http://target.example.com if the
        /// http://target.example.com request returns an "Access-Control-Allow-Origin:
        /// https://source.example.com" response header.
        ///
        /// Scripts in separate frames or iframes and hosted from the same protocol and
        /// domain suffix can execute cross-origin JavaScript if both pages set the
        /// document.domain value to the same domain suffix. For example,
        /// scheme://foo.example.com and scheme://bar.example.com can communicate using
        /// JavaScript if both domains set document.domain="example.com".
        ///
        /// This method is used to allow access to origins that would otherwise violate
        /// the same-origin policy. Scripts hosted underneath the fully qualified
        /// <paramref name="sourceOrigin"/> URL (like http://www.example.com) will be allowed access to
        /// all resources hosted on the specified <paramref name="targetProtocol"/> and <paramref name="targetDomain"/>.
        /// If <paramref name="targetDomain"/> is non-empty and <paramref name="allowTargetSubdomains"/> if false only
        /// exact domain matches will be allowed. If <paramref name="targetDomain"/> contains a top-
        /// level domain component (like "example.com") and <paramref name="allowTargetSubdomains"/> is
        /// true sub-domain matches will be allowed. If <paramref name="targetDomain"/> is empty and
        /// <paramref name="allowTargetSubdomains"/> if true all domains and IP addresses will be
        /// allowed.
        ///
        /// This method cannot be used to bypass the restrictions on local or display
        /// isolated schemes. See the comments on <see cref="CefCustomScheme"/> for more
        /// information.
        ///
        /// This function may be called on any thread. Returns false if <paramref name="sourceOrigin"/>
        /// is invalid or the whitelist cannot be accessed.
        /// </remarks>
        public static bool AddCrossOriginWhitelistEntry(
            string sourceOrigin,
            string targetProtocol,
            string targetDomain,
            bool allowTargetSubdomains)
        {
            return Core.Cef.AddCrossOriginWhitelistEntry(
                sourceOrigin,
                targetProtocol,
                targetDomain,
                allowTargetSubdomains);
        }

        /// <summary>Remove entry from cross-origin whitelist</summary>
        /// <param name="sourceOrigin">The origin allowed to be accessed by the target protocol/domain.</param>
        /// <param name="targetProtocol">The target protocol allowed to access the source origin.</param>
        /// <param name="targetDomain">The optional target domain allowed to access the source origin.</param>
        /// <param name="allowTargetSubdomains">If set to true would allow a blah.example.com if the 
        ///     <paramref name="targetDomain"/> was set to example.com
        /// </param>
        /// <remarks>
        /// Remove an entry from the cross-origin access whitelist. Returns false if
        /// <paramref name="sourceOrigin"/> is invalid or the whitelist cannot be accessed.
        /// </remarks>
        public static bool RemoveCrossOriginWhitelistEntry(string sourceOrigin,
            string targetProtocol,
            string targetDomain,
            bool allowTargetSubdomains)

        {
            return Core.Cef.RemoveCrossOriginWhitelistEntry(
                sourceOrigin,
                targetProtocol,
                targetDomain,
                allowTargetSubdomains);
        }

        /// <summary>Remove all entries from the cross-origin access whitelist.</summary>
        /// <remarks>
        /// Remove all entries from the cross-origin access whitelist. Returns false if
        /// the whitelist cannot be accessed.
        /// </remarks>
        public static bool ClearCrossOriginWhitelist()
        {
            return Core.Cef.ClearCrossOriginWhitelist();
        }

        /// <summary>
        /// Returns the global cookie manager. By default data will be stored at CefSettings.CachePath if specified or in memory otherwise.
        /// Using this method is equivalent to calling Cef.GetGlobalRequestContext().GetCookieManager()
        /// The cookie managers storage is created in an async fashion, whilst this method may return a cookie manager instance,
        /// there may be a short delay before you can Get/Write cookies.
        /// To be sure the cookie manager has been initialized use one of the following
        /// - Access the ICookieManager after ICompletionCallback.OnComplete has been called
        /// - Access the ICookieManager instance in IBrowserProcessHandler.OnContextInitialized.
        /// - Use the ChromiumWebBrowser BrowserInitialized (OffScreen) or IsBrowserInitializedChanged (WinForms/WPF) events.
        /// </summary>
        /// <param name="callback">If non-NULL it will be executed asynchronously on the CEF UI thread after the manager's storage has been initialized.</param>
        /// <returns>A the global cookie manager or null if the RequestContext has not yet been initialized.</returns>
        public static ICookieManager GetGlobalCookieManager(ICompletionCallback callback = null)
        {
            return Core.Cef.GetGlobalCookieManager(callback);
        }

        /// <summary>
        /// This function can optionally be called on the main application thread after
        /// CefInitialize to retrieve the initialization exit code. When CefInitialize
        /// returns true the exit code will be 0 (<see cref="ResultCode.NormalExit"/>).
        /// Otherwise, see <see cref="ResultCode"/> for possible exit code values including
        /// browser process initialization errors and normal early exit conditions
        /// (such as <see cref="ResultCode.NormalExitProcessNotified"/>  for process singleton relaunch behavior).
        /// </summary>
        public static ResultCode GetExitCode()
        {
            return Core.Cef.GetExitCode();
        }

        /// <summary>
        /// Called prior to calling Cef.Shutdown, this disposes of any remaining
        /// ChromiumWebBrowser instances. In WPF this is used from Dispatcher.ShutdownStarted
        /// to release the unmanaged resources held by the ChromiumWebBrowser instances.
        /// Generally speaking you don't need to call this yourself.
        /// </summary>
        public static void PreShutdown()
        {
            Core.Cef.PreShutdown();
        }

        /// <summary>
        /// Shuts down CefSharp and the underlying CEF infrastructure. This method is safe to call multiple times; it will only
        /// shut down CEF on the first call (all subsequent calls will be ignored).
        /// This method should be called on the main application thread to shut down the CEF browser process before the application exits. 
        /// If you are Using CefSharp.OffScreen then you must call this explicitly before your application exits or it will hang.
        /// This method must be called on the same thread as Initialize. If you don't call Shutdown explicitly then CefSharp.Wpf and CefSharp.WinForms
        /// versions will do their best to call Shutdown for you, if your application is having trouble closing then call thus explicitly.
        /// </summary>
        public static void Shutdown()
        {
            ShutdownStarted?.Invoke(null, EventArgs.Empty);
            ShutdownStarted = null;

            Core.Cef.Shutdown();
        }

        /// <summary>
        /// This method should only be used by advanced users, if you're unsure then use Cef.Shutdown().
        /// This function should be called on the main application thread to shut down
        /// the CEF browser process before the application exits. This method simply obtains a lock
        /// and calls the native CefShutdown method, only IsInitialized is checked. All ChromiumWebBrowser
        /// instances MUST be Disposed of before calling this method. If calling this method results in a crash
        /// or hangs then you're likely hanging on to some unmanaged resources or haven't closed all of your browser
        /// instances
        /// </summary>
        public static void ShutdownWithoutChecks()
        {
            ShutdownStarted?.Invoke(null, EventArgs.Empty);
            ShutdownStarted = null;

            Core.Cef.ShutdownWithoutChecks();
        }

        /// <summary>
        /// Clear all scheme handler factories registered with the global request context.
        /// Returns false on error. This function may be called on any thread in the browser process.
        /// Using this function is equivalent to calling Cef.GetGlobalRequestContext().ClearSchemeHandlerFactories().
        /// </summary>
        /// <returns>Returns false on error.</returns>
        public static bool ClearSchemeHandlerFactories()
        {
            return Core.Cef.ClearSchemeHandlerFactories();
        }

        /// <summary>
        /// Returns true if called on the specified CEF thread.
        /// </summary>
        /// <returns>Returns true if called on the specified thread.</returns>
        public static bool CurrentlyOnThread(CefThreadIds threadId)
        {
            return Core.Cef.CurrentlyOnThread(threadId);
        }

        /// <summary>
        /// Gets the Global Request Context. Make sure to Dispose of this object when finished.
        /// The earlier possible place to access the IRequestContext is in IBrowserProcessHandler.OnContextInitialized.
        /// Alternative use the ChromiumWebBrowser BrowserInitialized (OffScreen) or IsBrowserInitializedChanged (WinForms/WPF) events.
        /// </summary>
        /// <returns>Returns the global request context or null if the RequestContext has not been initialized yet.</returns>
        public static IRequestContext GetGlobalRequestContext()
        {
            return Core.Cef.GetGlobalRequestContext();
        }

        /// <summary>
        /// Helper function (wrapper around the CefColorSetARGB macro) which combines
        /// the 4 color components into an uint32 for use with BackgroundColor property
        /// </summary>
        /// <param name="a">Alpha</param>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <returns>Returns the color.</returns>
        public static UInt32 ColorSetARGB(UInt32 a, UInt32 r, UInt32 g, UInt32 b)
        {
            return Core.Cef.ColorSetARGB(a, r, g, b);
        }

        /// <summary>
        /// Crash reporting is configured using an INI-style config file named
        /// crash_reporter.cfg. This file must be placed next to
        /// the main application executable. File contents are as follows:
        ///
        ///  # Comments start with a hash character and must be on their own line.
        ///
        ///  [Config]
        ///  ProductName=&lt;Value of the &quot;prod&quot; crash key; defaults to &quot;cef&quot;&gt;
        ///  ProductVersion=&lt;Value of the &quot;ver&quot; crash key; defaults to the CEF version&gt;
        ///  AppName=&lt;Windows only; App-specific folder name component for storing crash
        ///           information; default to &quot;CEF&quot;&gt;
        ///  ExternalHandler=&lt;Windows only; Name of the external handler exe to use
        ///                   instead of re-launching the main exe; default to empty>
        ///  ServerURL=&lt;crash server URL; default to empty&gt;
        ///  RateLimitEnabled=&lt;True if uploads should be rate limited; default to true&gt;
        ///  MaxUploadsPerDay=&lt;Max uploads per 24 hours, used if rate limit is enabled;
        ///                    default to 5&gt;
        ///  MaxDatabaseSizeInMb=&lt;Total crash report disk usage greater than this value
        ///                       will cause older reports to be deleted; default to 20&gt;
        ///  MaxDatabaseAgeInDays=&lt;Crash reports older than this value will be deleted;
        ///                        default to 5&gt;
        ///
        ///  [CrashKeys]
        ///  my_key1=&lt;small|medium|large&gt;
        ///  my_key2=&lt;small|medium|large&gt;
        ///
        /// Config section:
        ///
        /// If &quot;ProductName&quot; and/or &quot;ProductVersion&quot; are set then the specified values
        /// will be included in the crash dump metadata. 
        ///
        /// If &quot;AppName&quot; is set on Windows then crash report information (metrics,
        /// database and dumps) will be stored locally on disk under the
        /// &quot;C:\Users\[CurrentUser]\AppData\Local\[AppName]\User Data&quot; folder. 
        ///
        /// If &quot;ExternalHandler&quot; is set on Windows then the specified exe will be
        /// launched as the crashpad-handler instead of re-launching the main process
        /// exe. The value can be an absolute path or a path relative to the main exe
        /// directory. 
        ///
        /// If &quot;ServerURL&quot; is set then crashes will be uploaded as a multi-part POST
        /// request to the specified URL. Otherwise, reports will only be stored locally
        /// on disk.
        ///
        /// If &quot;RateLimitEnabled&quot; is set to true then crash report uploads will be rate
        /// limited as follows:
        ///  1. If &quot;MaxUploadsPerDay&quot; is set to a positive value then at most the
        ///     specified number of crashes will be uploaded in each 24 hour period.
        ///  2. If crash upload fails due to a network or server error then an
        ///     incremental backoff delay up to a maximum of 24 hours will be applied for
        ///     retries.
        ///  3. If a backoff delay is applied and &quot;MaxUploadsPerDay&quot; is > 1 then the
        ///     &quot;MaxUploadsPerDay&quot; value will be reduced to 1 until the client is
        ///     restarted. This helps to avoid an upload flood when the network or
        ///     server error is resolved.
        ///
        /// If &quot;MaxDatabaseSizeInMb&quot; is set to a positive value then crash report storage
        /// on disk will be limited to that size in megabytes. For example, on Windows
        /// each dump is about 600KB so a &quot;MaxDatabaseSizeInMb&quot; value of 20 equates to
        /// about 34 crash reports stored on disk.
        ///
        /// If &quot;MaxDatabaseAgeInDays&quot; is set to a positive value then crash reports older
        /// than the specified age in days will be deleted.
        ///
        /// CrashKeys section:
        ///
        /// Any number of crash keys can be specified for use by the application. Crash
        /// key values will be truncated based on the specified size (small = 63 bytes,
        /// medium = 252 bytes, large = 1008 bytes). The value of crash keys can be set
        /// from any thread or process using the Cef.SetCrashKeyValue function. These
        /// key/value pairs will be sent to the crash server along with the crash dump
        /// file. Medium and large values will be chunked for submission. For example,
        /// if your key is named &quot;mykey&quot; then the value will be broken into ordered
        /// chunks and submitted using keys named &quot;mykey-1&quot;, &quot;mykey-2&quot;, etc.
        /// </summary>
        /// <returns>Returns true if crash reporting is enabled.</returns>
        public static bool CrashReportingEnabled
        {
            get { return Core.Cef.CrashReportingEnabled; }
        }

        /// <summary>
        /// Sets or clears a specific key-value pair from the crash metadata.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void SetCrashKeyValue(string key, string value)
        {
            Core.Cef.SetCrashKeyValue(key, value);
        }

        /// <summary>
        /// Gets the current log level.
        /// When <see cref="CefSettingsBase.LogSeverity"/> is set to <see cref="LogSeverity.Disable"/> then
        /// no messages will be written to the log file, but FATAL messages will still be output to stderr.
        /// When logging is disabled this method will return <see cref="LogSeverity.Fatal"/>.
        /// </summary>
        /// <returns>Current Log Level</returns>
        public static LogSeverity GetMinLogLevel()
        {
            var severity = Core.Cef.GetMinLogLevel();

            //Manually convert the int into the enum
            //Values don't match (this is a difference in CEF/Chromium) implementation
            //we need to deal with it manually,
            //https://github.com/chromiumembedded/cef/blob/2a64387259cf14412e24c3267c8a1eb3b99a54e3/include/base/cef_logging.h#L186
            //const LogSeverity LOG_VERBOSE = -1;
            //const LogSeverity LOG_INFO = 0;
            //const LogSeverity LOG_WARNING = 1;
            //const LogSeverity LOG_ERROR = 2;
            //const LogSeverity LOG_FATAL = 3;

            if (severity == -1)
            {
                return LogSeverity.Verbose;
            }

            if (severity == 0)
            {
                return LogSeverity.Info;
            }

            if (severity == 1)
            {
                return LogSeverity.Warning;
            }

            if (severity == 2)
            {
                return LogSeverity.Error;
            }

            if (severity == 3)
            {
                return LogSeverity.Fatal;
            }

            //No matching type, return the integer value as enum
            return (LogSeverity)severity;
        }

        /// <summary>
        /// Returns the mime type for the specified file extension or an empty string if unknown.
        /// </summary>
        /// <param name="extension">file extension</param>
        /// <returns>Returns the mime type for the specified file extension or an empty string if unknown.</returns>
        public static string GetMimeType(string extension)
        {
            return Core.Cef.GetMimeType(extension);
        }

        /// <summary>
        /// WaitForBrowsersToClose is not enabled by default, call this method
        /// before Cef.Initialize to enable. If you aren't calling Cef.Initialize
        /// explicitly then this should be called before creating your first
        /// ChromiumWebBrowser instance.
        /// </summary>
        public static void EnableWaitForBrowsersToClose()
        {
            Core.Cef.EnableWaitForBrowsersToClose();
        }

        /// <summary>
        /// Helper method to ensure all ChromiumWebBrowser instances have been
        /// closed/disposed, should be called before Cef.Shutdown.
        /// Disposes all remaining ChromiumWebBrowser instances
        /// then waits for CEF to release its remaining CefBrowser instances.
        /// Finally a small delay of 50ms to allow for CEF to finish it's cleanup.
        /// Should only be called when MultiThreadedMessageLoop = true;
        /// (Hasn't been tested when when CEF integrates into main message loop).
        /// </summary>
        public static void WaitForBrowsersToClose()
        {
            Core.Cef.WaitForBrowsersToClose();
        }


        /// <summary>
        /// Helper method to ensure all ChromiumWebBrowser instances have been
        /// closed/disposed, should be called before Cef.Shutdown.
        /// Disposes all remaining ChromiumWebBrowser instances
        /// then waits for CEF to release its remaining CefBrowser instances.
        /// Finally a small delay of 50ms to allow for CEF to finish it's cleanup.
        /// Should only be called when MultiThreadedMessageLoop = true;
        /// (Hasn't been tested when when CEF integrates into main message loop).
        /// </summary>
        /// <param name="timeoutInMiliseconds">The timeout in miliseconds.</param>
        public static void WaitForBrowsersToClose(int timeoutInMiliseconds)
        {
            Core.Cef.WaitForBrowsersToClose(timeoutInMiliseconds);
        }

        /// <summary>
        /// Post an action for delayed execution on the specified thread.
        /// </summary>
        /// <param name="threadId">thread id</param>
        /// <param name="action">action to execute</param>
        /// <param name="delayInMs">delay in ms</param>
        /// <returns>bool</returns>
        public static bool PostDelayedAction(CefThreadIds threadId, Action action, int delayInMs)
        {
            return Core.Cef.PostDelayedAction(threadId, action, delayInMs);
        }

        /// <summary>
        /// Post an action for execution on the specified thread.
        /// </summary>
        /// <param name="threadId">thread id</param>
        /// <param name="action">action to execute</param>
        /// <returns>bool</returns>
        public static bool PostAction(CefThreadIds threadId, Action action)
        {
            return Core.Cef.PostAction(threadId, action);
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than, the Windows 10 version.
        /// Applications not manifested for Windows 10 return false, even if the current operating system version is Windows 10.
        /// To manifest your applications for Windows 10, see https://learn.microsoft.com/en-us/windows/win32/sysinfo/targeting-your-application-at-windows-8-1.
        /// </summary>
        /// <returns>True if the current OS version matches, or is greater than, the Windows 10 version; otherwise, false.</returns>
        public static bool IsWindows10OrGreater()
        {
            return Core.Cef.IsWindows10OrGreaterEx();
        }

        /// <summary>
        /// If the current OS version matches, or is greater than, the Windows 10 version then this method does nothing.
        /// Applications not manifested for Windows 10 will throw an <see cref="ApplicationException"/>, even if the current operating system version is Windows 10.
        /// To manifest your applications for Windows 10, see https://learn.microsoft.com/en-us/windows/win32/sysinfo/targeting-your-application-at-windows-8-1.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public static void AssertIsWindows10OrGreater()
        {
            if (!IsWindows10OrGreater())
                throw new ApplicationException("Current OS version is less than Windows 10. Applications not manifested for Windows 10 throw this exception, even if the current operating system version is Windows 10. To manifest your applications for Windows 10, see https://learn.microsoft.com/en-us/windows/win32/sysinfo/targeting-your-application-at-windows-8-1.");
        }
    }
}
