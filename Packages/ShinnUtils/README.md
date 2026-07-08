# ShinnUtils

`com.shinn.utils` — a collection of Unity development utilities: helpers, camera
controllers, networking (TCP/UDP/Serial), an object pool, and an event manager.
Demo shaders, materials, sprites, and prefabs ship as the **CommonAssets** sample.

- **Unity:** 6000.0+
- **Author:** [John Tsai](https://github.com/shinn716)
- **License:** MIT (see [LICENSE.md](LICENSE.md); some bundled third-party content is under its own license)

## Installation

Unity Package Manager → **Add package from git URL…**, then paste a URL that
points at the package sub-folder and pins a version tag:

```
https://github.com/shinn716/ShinnUtils.git?path=/Packages/ShinnUtils#v0.4.0
```

Or add it directly to `Packages/manifest.json`:

```json
"com.shinn.utils": "https://github.com/shinn716/ShinnUtils.git?path=/Packages/ShinnUtils#v0.4.0"
```

- `?path=` selects the package folder inside the repo (Unity 2019.3.4+).
- `#v0.4.0` pins a released tag; omit it to track the default branch.

## Assemblies

- `Shinn.Utils` (Runtime)
- `Shinn.Utils.Editor` (Editor)

## Runtime overview

| Area | Types |
|------|-------|
| Utility | `Utils`, `Converter`, `Txt`, `Image`, `Math`, `Date`, `CsvHepler`, `JsonHelper`, `CheckFormat` |
| Camera | `MaxCamera`, `StreetViewCamera` |
| Networking | `TCPClient`, `TCPServer`, `UDPClient`, `UDPServer`, `SerialReceiver` |
| Patterns | `ObjectPool`, `IPooledObject`, `EventManager` |
| Common | `Follower`, `Hover`, `RotateAround`, `RotateSelf`, `SyncTransform`, ... |

> Types live under the `Shinn`, `Shinn.Common`, and `Shinn.Event` namespaces.
> Some legacy files are still in the global namespace and are being migrated.

## Samples

Available through the Package Manager *Samples* tab when installed as a UPM
package: CommonAssets (shaders/materials/prefabs), FirebaseStorage, HDRP Particle
Shaders, ObjectPool/Event/Communication, SimpleDotween, SimpleItween, WebGLBridge,
NewInputModule, Addressable.

## License

MIT for first-party code. Third-party content under `SimpleItween~/Pixelplacement/`
(iTween) is covered by its own license — verify redistribution rights before
publishing.
