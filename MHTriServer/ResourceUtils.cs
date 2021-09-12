﻿using System.IO;
using System.Reflection;

namespace MHTriServer
{
    public static class ResourceUtils
    {
        public static Stream GetResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var l = assembly.GetManifestResourceNames();

            // TODO: Find way to resolve namespace name dynamically
            return assembly.GetManifestResourceStream("MHTriServer.Resources." + name);
        }

        public static byte[] GetResourceBytes(string name)
        {
            using var stream = GetResource(name);
            var bytes = new byte[stream.Length];
            stream.Read(bytes);
            return bytes;
        }
    }
}
