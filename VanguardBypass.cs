using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript.VanguardBypass
{
    class VanguardBypass
    {
        private readonly IntPtr _vanguardModule;
        private readonly IntPtr _vanguardFunction;

        public VanguardBypass()
        {
            // Obtenir le module et les pointeurs de fonction Vanguard
            _vanguardModule = GetModuleAddress("Vanguard.dll");
            _vanguardFunction = GetProcAddress(_vanguardModule, "VanguardFunction");

            // hook la fonction Vanguard pour la contourner
            Hook(_vanguardFunction, BypassVanguard);
        }

        private IntPtr GetModuleAddress(string moduleName)
        {
            // Obtenir l'adresse du module en utilisant l'API Windows
            return GetModuleHandle(moduleName);
        }

        private IntPtr GetProcAddress(IntPtr module, string functionName)
        {
            // Obtenir l'adresse de la fonction en utilisant l'API Windows
            return GetProcAddress(module, functionName);
        }

        private void Hook(IntPtr function, Delegate callback)
        {
            // hook la fonction en utilisant un detour
            DetourAttach(function, callback);
        }

        private void BypassVanguard(IntPtr args)
        {
            // Contourner Vanguard en renvoyant un résultat factice
            return 0;
        }
    }
}
