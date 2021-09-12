using System;
using System.Linq;
using System.Text;
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
        public void StringMultiply_MultiplyBy5_ReturnRepeatMessages5Times()
        {
            // Call Python function
            var functionResult = fixture.PythonEngine.Operations.Invoke(
                fixture.PythonModule.GetVariable("string_multiply"),
                "Sorry \n",
                5
            );
            Assert.Equal("Sorry \nSorry \nSorry \nSorry \nSorry \n", functionResult);
        }

        [Fact]
        public void StringMultiplyWithOperatorOverloading_MultiplyBy5_ReturnRepeatMessages5Times()
        {
            var repeatMessage = new StringWrapper("Sorry \n") * 5;
            Assert.Equal("Sorry \nSorry \nSorry \nSorry \nSorry \n", repeatMessage);
        }

        public class StringWrapper
        {
            public string Value { get; }
            public int Length { get; }

            public StringWrapper(string value)
            {
                Value = value;
                Length = value.Length;
            }

            public static string operator *(StringWrapper text, int multiplier)
            {
                return Enumerable.Range(1, multiplier).Aggregate(
                     new StringBuilder(multiplier * text.Length),
                     (sb, _) => sb.Append(text.Value),
                     sb => sb.ToString()
                 );

                // String.Concat(Enumerable.Repeat("Hello", 4))
                // If only on character
                // string TenAs = new string ('A', multipler);
            }
        }
    }
}
