# Changelog

All notable changes to this package are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0]

### Removed (breaking)
- Moved all bundled demo assets out of the core package into a new **CommonAssets**
  sample (`CommonAssets~`): shaders (`Runtime/shaders/`), materials, sprites, and
  prefabs. Consumers relying on these must import the CommonAssets sample; scene
  materials referencing them will otherwise show as missing (magenta).
- Deleted the third-party **Ciconia Studio** "Double Sided" shaders (redistribution
  not covered by this package's license).

## [0.3.0]

### Fixed
- `TCPClient`: replaced deprecated `Thread.Abort` with cooperative
  `CancellationToken` shutdown; fixed a busy-loop that spun the CPU when the
  remote peer closed the connection; received messages are now marshalled back
  to the creating (main) thread for safe Unity API access.
- `Utils.GetLocalIP`: removed unreachable code after `return`.
- `Utils.LoadSprite`: guard against a null texture when the file fails to load.
- `Utils.Str2Quaternion`: parse with `InvariantCulture` to avoid locale issues.
- Error paths in `TCPClient`, `TCPServer`, and `SerialReceiver` now use
  `Debug.LogError` / `Debug.LogWarning` instead of `Debug.Log`.

### Changed
- All runtime scripts moved under the `Shinn` namespace (previously 20 files were
  in the global namespace).
- asmdefs renamed to `Shinn.Utils` / `Shinn.Utils.Editor`, `rootNamespace` set to
  `Shinn`, and unused `allowUnsafeCode` disabled on the runtime assembly.
- `package.json`: `license` set to `MIT`, added `unity` compatibility field.
- `Utils`: removed redundant `? true : false`; `Converter.BytesToString` now uses
  `StringBuilder`.
- Added `README.md`, `CHANGELOG.md`, and `LICENSE.md`.

### Added
- EditMode unit tests (`Shinn.Utils.Tests`) covering `Utils` and `Converter`
  pure functions.

## [0.2.0]
- Baseline prior to documented changes.
