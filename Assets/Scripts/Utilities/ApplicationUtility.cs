using System;
using System.IO;
using UnityEngine;

namespace Tempname
{
    public static partial class ApplicationUtility
    {
        public static readonly string DataPath;

        static ApplicationUtility()
        {
            if (GetCommandLineArgument("-dataPath", out string dataPath))
            {
                DataPath = dataPath;
            }
            else
            {
                if (Application.isEditor || Application.isMobilePlatform == false)
                {
                    DataPath = Application.dataPath + "/..";
                }
                else
                {
                    DataPath = Application.persistentDataPath;
                }
            }
        }

        // PUBLIC METHODS

        public static string GetFilePath(string fileName)
        {
            return Path.Combine(DataPath, fileName);
        }

        public static int GetTimeID(int splitHours, int splitMinutes, int splitSeconds)
        {
            var time = System.DateTime.Now;
            var timeID = (time.Hour * 60 + time.Minute) * 60 + time.Second;

            var denominator = (splitHours * 60 + splitMinutes) * 60 + splitSeconds;
            if (denominator > 0)
            {
                timeID /= denominator;
            }

            return timeID;
        }

        public static bool HasCommandLineArgument(string name)
        {
            var arguments = Environment.GetCommandLineArgs();
            for (var i = 0; i < arguments.Length; ++i)
            {
                if (arguments[i] == name)
                    return true;
            }

            return false;
        }

        public static bool GetCommandLineArgument(string name, out string argument)
        {
            var arguments = Environment.GetCommandLineArgs();
            for (var i = 0; i < arguments.Length; ++i)
            {
                if (arguments[i] == name && arguments.Length > (i + 1))
                {
                    argument = arguments[i + 1];
                    return true;
                }
            }

            argument = default;
            return false;
        }

        public static bool GetCommandLineArgument(string name, out int argument)
        {
            var arguments = Environment.GetCommandLineArgs();
            for (var i = 0; i < arguments.Length; ++i)
            {
                if (arguments[i] == name && arguments.Length > (i + 1) &&
                    int.TryParse(arguments[i + 1], out var parsedArgument))
                {
                    argument = parsedArgument;
                    return true;
                }
            }

            argument = default;
            return false;
        }
    }
}
