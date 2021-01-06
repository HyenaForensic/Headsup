using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Headup
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcxMDExQDMxMzgyZTM0MmUzMGJUbWZJaWV1QzQ0Y0poU1pqR0pHamJUVzdjbldhZXlIZGQ2bUhhcXRtaEU9;MzcxMDEyQDMxMzgyZTM0MmUzMGNoN2ZnODNwSHJkNzhNTUdOVHJjbFVNRmhtVTkxNHJBQ1NCMmVSR1NmbG89;MzcxMDEzQDMxMzgyZTM0MmUzMGxPWWRKOG9JZEdEUWRiNTMrcGFXWDlQQnhJVlVBdFlkYXgrSjNQZXpuN289;MzcxMDE0QDMxMzgyZTM0MmUzMEJIcVFjQnpmTmMvOG9sTEZLbHBqcWxvNUFBR3lVK0tJK2tXT2VsaVpHUWs9;MzcxMDE1QDMxMzgyZTM0MmUzMFUrbStMM0NPU2gyODRndHBEU2J2VTVOMThLS2ZwcEN1V0F1KytySEx3U289;MzcxMDE2QDMxMzgyZTM0MmUzMFhxTmRBeFVMSnljbmJPRmFnaFp0elIxdW02SCtHS1owSHI2Rll1ZmpFaWM9;MzcxMDE3QDMxMzgyZTM0MmUzMFBOb1ZVMzVPSG5jeENOODJBcFdSMGk2ZGRIUjlPNzdLQlN1M3JMaDdRYk09;MzcxMDE4QDMxMzgyZTM0MmUzMFlXUnlzVGZpeEtTZjdQcVdteENMYWxQWFRNWlNZbVRKK2FCd3BVN0NRWXM9;MzcxMDE5QDMxMzgyZTM0MmUzME9NaDVjczd6eVhDekFBQkMrMkZ6QU5MNTNFOXBZeHdxbW5yVlZLWHdRUVU9;MzcxMDIwQDMxMzgyZTM0MmUzMGJqbzFzUXZqTFJHR3c2M0dtY3hTWGYvWEtEeStLdUFnV1Z3K1Z6UE9BZFk9");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
            //Application.Run(new Form1()); 
        }
    }
}
