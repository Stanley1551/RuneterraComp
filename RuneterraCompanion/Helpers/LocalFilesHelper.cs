using RuneterraCompanion.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RuneterraCompanion.Helpers
{
    internal static class LocalFilesHelper
    {
        internal static bool IsDownloadNeeded(string currentDirectory)
        {
            if (!CheckPath(Constants.assetsDirectoryName, currentDirectory))
            {
                return true;
            }

            if (!CheckPath(Constants.cardImgPath, currentDirectory))
            {
                return true;
            }

            if (!CheckPath(Constants.cardThumbnailPath, currentDirectory))
            {
                return true;
            }

            if (Directory.GetFiles(Path.Combine(currentDirectory, Constants.cardThumbnailPath)).Length < 100
                || Directory.GetFiles(Path.Combine(currentDirectory, Constants.cardThumbnailPath)).Length < 100)
            {
                return true;
            }

            return false;
        }

        private static bool CheckPath(string path, string currentDirectory)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            if (Directory.GetFiles(Path.Combine(currentDirectory, path)).Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
