using System;
using System.Reflection;

namespace IronPythonExamples
{
    public class Thai2RomComInteropFixture
    {
        private const string progId = "ThaiTranslite";
        private readonly string workingDirectory = AppContext.BaseDirectory;

        public object NormalLateBindingTtranslite { get; private set; }
        public dynamic DynamicLateBindingTtranslite { get; private set; }

        public Thai2RomComInteropFixture()
        {
            // From visual basic
            // NormalLateBindingTtranslite = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("ThaiTranslite"));
            // NewLateBinding.LateCall(NormalLateBindingTtranslite, null, "Initial", new object[] { workingDirectory }, null, null, null, true);

            // CSLI 72CF0702-A3CA-434D-BC94-16DC50D52350
            var objType = Type.GetTypeFromProgID(progId);
            NormalLateBindingTtranslite = Activator.CreateInstance(objType);
            NormalLateBindingTtranslite.GetType().InvokeMember(
                "Initial",
                BindingFlags.InvokeMethod,
                null,
                NormalLateBindingTtranslite,
                new[] { workingDirectory }
            );

            DynamicLateBindingTtranslite = Activator.CreateInstance(objType);
            DynamicLateBindingTtranslite.Initial(workingDirectory);
        }
    }
}

