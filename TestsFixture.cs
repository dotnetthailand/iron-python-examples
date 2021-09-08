using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.CompilerServices;

namespace IronPythonExamples
{
    public class TestFixture : IDisposable
    {
        public object Ttranslite { get; private set; }

        public TestFixture()
        {
            var workingDirectory = AppContext.BaseDirectory;
            Ttranslite = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("ThaiTranslite"));
            NewLateBinding.LateCall(Ttranslite, null, "Initial", new object[] { workingDirectory }, null, null, null, true);
        }

        public void Dispose()
        {
        }
    }
}
