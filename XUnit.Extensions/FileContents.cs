using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace XUnit.Core
{
    public class FileContentsAttribute : DataAttribute
    {
        private static string BaseFolder = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string FileName;

        public FileContentsAttribute(string fileName)
        {
            FileName = fileName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { File.ReadAllText(Path.Combine(BaseFolder, FileName)) };
        }
    }
}