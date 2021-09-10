using System;
using Xunit;

namespace IronPythonExamples
{
    public class IronPythonClassLibraryTest : IClassFixture<IronPythonClassLibraryFixture>
    {
        private readonly IronPythonClassLibraryFixture fixture;
        public IronPythonClassLibraryTest(IronPythonClassLibraryFixture fixture) => this.fixture = fixture;

        // https://www.py4u.net/discuss/736777
        [Fact]
        public void Sum_ValidInput_ReturnCorectValue()
        {
            // Call Python function
            var functionResult = fixture.PythonEngine.Operations.Invoke(fixture.PythonModule.GetVariable("sum_function"), 1, 2);

            // Get the Python class
            var mathClass = fixture.PythonEngine.Operations.Invoke(fixture.PythonModule.GetVariable("Math"));
            // Create a callable function to 'sum'
            var sumMethod = fixture.PythonEngine.Operations.GetMember<Func<int, int, int>>(mathClass, "sum_method");
            // Call python method
            var methodResult = sumMethod(1, 2);

            Assert.Equal(3, functionResult);
            Assert.Equal(3, methodResult);
        }

        [Fact]
        public void StringMultiply_5Times_ReturnMessage5Times()
        {
            // Call Python function
            var functionResult = fixture.PythonEngine.Operations.Invoke(
                fixture.PythonModule.GetVariable("string_multiply"),
                "Sorry \n",
                5
            );
            Assert.Equal("Sorry \nSorry \nSorry \nSorry \nSorry \n", functionResult);
        }
    }
}
