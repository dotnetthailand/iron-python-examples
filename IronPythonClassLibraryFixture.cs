using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;
using System.Reflection;

namespace IronPythonExamples
{
    public class IronPythonClassLibraryFixture
    {
        public ScriptEngine PythonEngine { get; private set; }
        public ScriptScope PythonModule { get; private set; }

        public IronPythonClassLibraryFixture()
        {
            PythonEngine = Python.CreateEngine();
            // Add IronPyhon library path if your IronPython dll includes Python standard module, e.g. os
            // pyEngine.SetSearchPaths(new[] { @"C:\Program Files\IronPython 3.4\Lib" });

            var assembly = Assembly.LoadFile(Path.GetFullPath("Math.dll"));
            PythonEngine.Runtime.LoadAssembly(assembly);
            PythonModule = PythonEngine.Runtime.ImportModule("Math");
        }
    }
}
