﻿
// GaGa.
// A simple radio player running on the Windows notification area.


using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace GaGa
{
    /// <summary>
    /// Stand-alone utilities.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// GetLastWriteTime() returns a DateTime equivalent to this
        /// when a file is not found.
        ///
        /// MSDN:
        /// If the file described in the path parameter does not exist
        /// this method returns 12:00 midnight, January 1, 1601 A.D. (C.E.)
        /// Coordinated Universal Time (UTC), adjusted to local time.
        /// </summary>
        public static readonly Int64 fileNotFoundUtc = new DateTime(1601, 1, 1).ToFileTimeUtc();

        /// <summary>
        /// Load an Icon from an embedded resource.
        /// </summary>
        /// <param name="resource">
        /// Resource path as a string, including namespace.
        /// </param>
        public static Icon LoadIconFromResource(String resource)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resource))
            {
                return new Icon(stream);
            }
        }

        /// <summary>
        /// Copy an embedded resource to the local filesystem.
        /// </summary>
        /// <param name="resource">
        /// Resource path as a string, including namespace.
        /// </param>
        /// <param name="filepath">
        /// Destination path as a string.
        /// </param>
        public static void CopyResource(String resource, String filepath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resource))
            {
                using (FileStream target = new FileStream(filepath,
                    FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(target);
                }
            }
        }

        /// <summary>
        /// Iterate all lines from an UTF8-encoded text file.
        /// </summary>
        /// <param name="filepath">File path as a string.</param>
        public static IEnumerable<String> ReadLineByLine(String filepath)
        {
            String line;
            using (StreamReader reader = File.OpenText(filepath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        /// <summary>
        /// Get the directory path where the executable that started the
        /// application can be found.
        /// </summary>
        public static String ApplicationDirectory()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        /// <summary>
        /// Show a MessageBox with Yes and No buttons. Return a Boolean
        /// determining whether Yes (true) or No (false) was clicked.
        /// </summary>
        /// <param name="text">MessageBox text.</param>
        /// <param name="caption">MessageBox caption.</param>
        public static Boolean MessageBoxYesNo(String text, String caption)
        {
            DialogResult result;
            result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo);
            return result == DialogResult.OK;
        }
    }
}

