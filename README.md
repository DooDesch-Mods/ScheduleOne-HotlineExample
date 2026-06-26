# Hotline Example - the modder API for Hotline

> 🛟 **Need help or found a bug?** Get support at [support.doodesch.de](https://support.doodesch.de).

A minimal example mod showing how to give your Schedule I mod a panel in the
[Hotline](https://github.com/DooDesch-Mods/ScheduleOne-Hotline) overlay - a text readout, an action button, a
toggle, an optional central hotkey and a log - with a single fluent call. Every call is a zero-overhead no-op
when Hotline is not installed, so there is no hard dependency.

## What's here

- `Hotline.Api/` - the modder shim. Reference `Hotline.Api.dll`, OR (recommended) drop the single `Hotline.cs`
  into your mod and compile it in - no extra DLL to ship.
- `HotlineExample/` - the example mod (`Core.cs`): registers a panel and demonstrates the full surface.

## The 10-second version

```csharp
using Hotline.Api;

Hud.RegisterPanel("MyMod", "My Mod")
   .Text(() => "queue = " + _queue.Count)                // a free, multi-line readout
   .Action("Reload", Reload)                             // a button (replaces a debug hotkey)
   .Toggle("Verbose", () => _verbose, v => _verbose = v) // an on/off control
   .Hotkey("Reload", HotlineKey.F8, Reload)              // an optional central hotkey
   .Log();                                               // a log channel
```

That is it - no `OnGUI`, no uGUI, no per-mod hotkey. Hotline draws the window; the player presses its master
key (F6) to open the overlay and your panel is there alongside every other mod's. See
`HotlineExample/Core.cs` for the working version, and the
[Hotline wiki](https://github.com/DooDesch-Mods/ScheduleOne-Hotline/wiki) for the full API.

## Integration

- **Copy-in source (recommended):** drop `Hotline.Api/Hotline.cs` into your mod project and compile it in.
  Nothing extra to ship; it binds to the running Hotline host by reflection and is a no-op when Hotline is
  absent.
- **Reference the DLL:** reference `Hotline.Api.dll`.

Either way it works regardless of load order (registrations queue until the host is up) and adds no hard
dependency. If your mod already polls a raw function key, Hotline also auto-detects it and adds it as a button
to your panel - but registering a panel like this is the clean way.

## License

MIT - see [LICENSE.md](LICENSE.md). Built by DooDesch.
