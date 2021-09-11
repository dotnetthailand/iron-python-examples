using System;
using System.Reflection;
using Xunit;

namespace IronPythonExamples
{
    // Before running these test cases, we need to register ttranslite.dll with the following command:
    // ps> & "$($Env:SystemRoot)\SysWoW64\regsvr32.exe" ttranslite.dll
    public class ComInteropTest : IClassFixture<ComInteropFixture>
    {
        private readonly ComInteropFixture fixture;
        public ComInteropTest(ComInteropFixture fixture) => this.fixture = fixture;

        [Theory]
        [InlineData("สวัสดี", "sawatdi")]
        [InlineData("นะจ๊ะ", "na cha")]
        [InlineData("ไม่รู้", "mai ru")]
        public void ToRoman_ValidInput_ReturnCorrectResult(string inputThaiText, string expectedRomanizedText) =>
            Assert.Equal(expectedRomanizedText, ToRoman(inputThaiText));

        [Theory]
        [InlineData("สวัสดี", "sawatdi")]
        [InlineData("นะจ๊ะ", "na cha")]
        [InlineData("ไม่รู้", "mai ru")]
        public void ToRomanWithDynamicLateBinding_ValidInput_ReturnCorrectResult(string inputThaiText, string expectedRomanizedText)
        {
            var prpN = "general";
            var oneword = "sent";
            var outType = "roman";
            var obj = fixture.DynamicLateBindingTtranslite.Roman(inputThaiText, prpN, oneword, outType);
            var outText = Convert.ToString(obj);
            var strArray = outText.Split(new string[] { "#|#" }, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(expectedRomanizedText, strArray[0]);
        }

        private string ToRoman(string inputText)
        {
            var prpN = "general";
            var oneword = "sent";
            var outType = "roman";
            var arguments = new object[] {
                inputText,
                prpN,
                oneword,
                outType
            };

            //var flag = new[] { true, true, true, true };
            //var obj = NewLateBinding.LateGet(fixture.NormalLateBindingTtranslite, null, "Roman", arguments, null, null, flag);

            var obj = fixture.NormalLateBindingTtranslite.GetType().InvokeMember(
                "Roman",
                BindingFlags.InvokeMethod,
                null,
                fixture.NormalLateBindingTtranslite,
                arguments
            );

            var outText = Convert.ToString(obj);
            var strArray = outText.Split(new string[] { "#|#" }, StringSplitOptions.RemoveEmptyEntries);
            var text = strArray[0];
            return text;
        }
    }
}
