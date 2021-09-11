using System;
using System.Reflection;
using Xunit;

namespace IronPythonExamples
{
    // Before running these test cases, we need to register ttranslite.dll with the following command:
    // ps> & "$($Env:SystemRoot)\SysWoW64\regsvr32.exe" ttranslite.dll

    /*
    Currently, you cannot register COM objects in an App Service (Web jobs, Web App, API App).
    This is because Azure Web apps does not provide us a way to access the virtual machine which hosts your web job.
    All Azure Web Apps (as well as Mobile App/Services, WebJobs, and Functions) run in a secure environment called a sandbox.
    Each app runs inside its own sandbox, isolating its execution from other instances on the same machine as well as providing an additional degree of security
    and privacy that would otherwise not be available.
    The sandbox mechanism aims to ensure that each app running on a machine will have a minimum guaranteed level of service;
    furthermore, the runtime limits enforced by the sandbox protects apps from being adversely affected by other resource-intensive apps
    which may be running on the same machine
    Possible solutions would be to use Windows Containers in App Service or use virtual machine
    which would give you full control of the machine and allow you to install whatever you want. Please read this related SO thread for more context.
    */

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
