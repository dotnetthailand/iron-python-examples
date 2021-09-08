using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Xunit;

namespace IronPythonExamples
{
    // Before running these test cases, we need to register ttranslite.dll with the following command:
    // ps> & "$($Env:SystemRoot)\SysWoW64\regsvr32.exe" ttranslite.dll
    public class ComInteropTest:IClassFixture<TestFixture>
    {
        private readonly TestFixture testFixture;
        public ComInteropTest(TestFixture testFixture) => this.testFixture = testFixture;
       
        [Theory]
        [InlineData("สวัสดี", "sawatdi")]
        [InlineData("นะจ๊ะ", "na cha")]
        [InlineData("ไม่รู้", "mai ru")]
        public void ToRoman_ValidInput_ReturnCorrectResult(string inputThaiText, string expectedRomanizedText) =>
            Assert.Equal(expectedRomanizedText, ToRoman(inputThaiText));

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

            var flag = new [] { true, true, true, true };
            var obj = NewLateBinding.LateGet(testFixture.Ttranslite, null, "Roman", arguments, null, null, flag);
            var outText = Conversions.ToString(obj);
            var strArray = Strings.Split(outText, "#|#");
            var text = strArray[0];
            return text;
        }
    }
}
