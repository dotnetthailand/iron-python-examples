using System;
using System.IO;
using System.Reflection;
using IronPython.Hosting;
using Xunit;

namespace IronPythonExamples
{
    public class IronPythonClassLibraryTest
    {
        // https://www.py4u.net/discuss/736777
        [Fact]
        public void Sum_ValidInput_ReturnCorectValue()
        {
            var pyEngine = Python.CreateEngine();
            // Add IronPyhon library path if your IronPython dll includes Python standard module, e.g. os
            // pyEngine.SetSearchPaths(new[] { @"C:\Program Files\IronPython 3.4\Lib" });

            var assembly = Assembly.LoadFile(Path.GetFullPath("Math.dll"));
            pyEngine.Runtime.LoadAssembly(assembly);
            var mathModule = pyEngine.Runtime.ImportModule("Math");

            // Call Python function
            var functionResult = pyEngine.Operations.Invoke(mathModule.GetVariable("sum_function"), 1, 2);

            // Get the Python class
            var mathClass = pyEngine.Operations.Invoke(mathModule.GetVariable("Math"));
            // Create a callable function to 'sum'
            var sumMethod = pyEngine.Operations.GetMember<Func<int, int, int>>(mathClass, "sum_method");
            // Call python method
            var methodResult = sumMethod(1, 2);

            Assert.Equal(3, functionResult);
            Assert.Equal(3, methodResult);
        }
    }
}
