using MelonLoader;
using Hotline.Api;   // Hud, HotlineKey

[assembly: MelonInfo(typeof(HotlineExample.Core), "HotlineExample", "1.0.0", "DooDesch", "https://github.com/DooDesch-Mods/ScheduleOne-HotlineExample")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace HotlineExample
{
    /// <summary>
    /// Minimal demonstration of the Hotline framework's modder API. Register a panel and you get a ready-made,
    /// toggleable, draggable window in the unified Hotline overlay - a text readout, an action button, an on/off
    /// toggle, an optional central hotkey and a log channel - all drawn by Hotline. Every call is a zero-overhead
    /// no-op when Hotline is not installed (registrations queue until the host binds), so this mod has NO hard
    /// dependency on Hotline.
    /// </summary>
    public sealed class Core : MelonMod
    {
        private int _tick;
        private bool _verbose;

        public override void OnInitializeMelon()
        {
            Hud.RegisterPanel("HotlineExample", "Hotline Example")
               .Text(() => "tick = " + _tick)                                          // a free, multi-line readout
               .Action("Say hi", () => LoggerInstance.Msg("hi from the panel button")) // a button (replaces a debug hotkey)
               .Toggle("Verbose", () => _verbose, v => _verbose = v)                   // an on/off control
               .Hotkey("Say hi", HotlineKey.F8, () => LoggerInstance.Msg("hi via the F8 hotkey")) // optional central key
               .Log();                                                                 // show this panel's log channel

            LoggerInstance.Msg("HotlineExample loaded - registered a Hotline panel (no-op if Hotline is absent).");
        }

        public override void OnUpdate()
        {
            _tick++;
            // Send a log line to this panel's channel (and Hotline's combined timeline) while verbose is on.
            if (_verbose && _tick % 300 == 0)
                Hud.Log("HotlineExample", "tick reached " + _tick);
        }
    }
}
