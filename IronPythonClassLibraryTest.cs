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
            var assembly = Assembly.LoadFile(Path.GetFullPath("Math.dll"));
            pyEngine.Runtime.LoadAssembly(assembly);
            var mathModule = pyEngine.Runtime.ImportModule("Math");

            // Get the Python class
            var mathClass = pyEngine.Operations.Invoke(mathModule.GetVariable("Math"));
            // create a callable function to 'sum'
            var sum = pyEngine.Operations.GetMember<Func<int, int, int>>(mathClass, "sum");

            Assert.Equal(3, sum(1, 2));
        }
    }
}
