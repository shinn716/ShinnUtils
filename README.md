# ShinnUtils

A collection of Unity development utilities — helpers, camera controllers,
networking (TCP/UDP/Serial), an object pool, and an event manager. Demo
shaders/materials/prefabs ship as the **CommonAssets** sample.

- **Unity:** 6000.0+ · **License:** MIT
- Full docs: [Packages/ShinnUtils/README.md](Packages/ShinnUtils/README.md)

## Install

Unity Package Manager → **Add package from git URL…**:

```
https://github.com/shinn716/ShinnUtils.git?path=/Packages/ShinnUtils#v1.0.0
```

Omit `#v1.0.0` to track the default branch. `?path=` requires Unity 2019.3.4+.

### Newtonsoft.Json (only needed by some samples)

Unity 6 provides it via the built-in **`com.unity.nuget.newtonsoft-json`** package
(install from Package Manager). For older Unity versions, add:

```
https://github.com/jilleJr/Newtonsoft.Json-for-Unity.git#upm
```
