using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace DecodeText
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.3", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidDecodeTextPkgString)]
    public sealed class DecodeTextPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public DecodeTextPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (mcs != null)
            {
                mcs.AddCommand(
                    new MenuCommand(
                        HandleDecodeHTMLMenuItemEvent,
                        new CommandID(
                            GuidList.guidDecodeTextCmdSet,
                            (int)PkgCmdIDList.cmdidDecodeHTMLCharacterEntities)));

                mcs.AddCommand(
                    new MenuCommand(
                        HandleDecodeURLMenuItemEvent,
                        new CommandID(
                            GuidList.guidDecodeTextCmdSet,
                            (int)PkgCmdIDList.cmdidDecodeURLEscapeSequences)));
            }
        }
        #endregion

        /// <summary>
        /// This function is a callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        void HandleDecodeHTMLMenuItemEvent(object sender, EventArgs e)
        {
            ReplaceSelectedText(System.Net.WebUtility.HtmlDecode);
        }

        void HandleDecodeURLMenuItemEvent(object x, EventArgs y)
        {
            ReplaceSelectedText(System.Net.WebUtility.UrlDecode);
        }

        void ReplaceSelectedText(
            Func<string, string> decodeString)
        {
            DTE2 dte = (DTE2)GetService(typeof(DTE));

            if (dte == null ||
                dte.ActiveDocument == null ||
                dte.ActiveDocument.Selection == null)
            {
                return;
            }

            TextSelection theSelectedText = dte.ActiveDocument.Selection as TextSelection;

            if (theSelectedText == null)
            {
                return;
            }

            var decodedText = decodeString(theSelectedText.Text);

            try
            {
                theSelectedText.Insert(decodedText, (int)vsInsertFlags.vsInsertFlagsContainNewText);
            }
            catch (Exception theException)
            {
                Debug.WriteLine(theException.ToString());
            }
        }

    }
}
