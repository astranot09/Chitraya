# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] — 2026-04-29

### Changed
- **Namespace**: Migrated from `SplashArt.RePivot` to `io.splashart.RePivot` for
  reverse-DNS consistency across SplashArt Toolbox products.
- **Folder structure**: Moved from `Assets/Plugins/SplashArtToolbox/RePivot/` to
  `Assets/RePivot/` (standalone root folder, no external dependencies).
- **Assembly definitions**: Renamed to `io.splashart.RePivot.Editor` and
  `io.splashart.RePivot.Tests.Editor`.
- **UI styling**: Switched from custom SplashArt USS theme to Unity default editor
  theme with minimal layout-only USS. Now fully compatible with both dark and light
  editor themes without color overrides.
- **Pivot presets**: Replaced the 9-point grid with a category-based 3D picker
  for vertices, edge midpoints, face centers, and bounds center.
- **Anchor API**: Replaced the UI-facing `BoundsAnchor` model with `Anchor3D`,
  `AxisSnap`, and `AnchorKind` for explicit three-axis bounds anchoring.

### Added
- Welcome page (`RePivot Welcome.asset`) with auto-show on first import via
  `[InitializeOnLoad]` + `SessionState`.
- Offline PDF documentation (`Documentation/RePivot-Documentation.pdf`) and
  "Open Documentation" link in the welcome page.
- `RPReadme.cs`, `RPReadmeEditor.cs`, `RPDocHelper.cs` — welcome page infrastructure.
- `PivotAdjusterWindow.uss` — minimal layout-only stylesheet for the preset picker.

### Removed
- Dependency on `SplashArt.Shared.Editor` assembly and `SharedStyles.cs`.
- Custom `SplashArtBase.uss` dark-theme stylesheet (replaced by Unity defaults).

## [1.0.0] — 2026-04-16

### Added
- Initial release of Pivot Adjuster.
- `PivotLogic` static class with `AdjustPivot`, `CalculateBounds`,
  `CalculateAnchorPosition`, `IsSafeToModify`, `IsSceneObject`,
  `HasPivotWrapper`, `HasVisibleRenderers`, `IsUIElement`, `IsFinite`.
- `PivotAdjusterWindow` (EditorWindow) with UI Toolkit layout.
- 9-point bounding-box preset grid for quick pivot placement.
- Manual world-space Vector3 entry field.
- Interactive Scene View position handle with bounds wireframe.
- Full Undo/Redo support (single collapsed undo step per operation).
- Prefab instance detection with one-click Unpack button.
- Play mode guard — all controls disabled during Play mode.
- Existing wrapper detection — re-adjustments reposition instead of nesting.
- `BoundsAnchor` enum (TopLeft through BottomRight).
- NUnit editor tests in `PivotLogicTests`.
- Demo scene (`Samples/RePivotDemo.unity`).
- Comprehensive offline documentation (`Documentation/README.md`).
- CHANGELOG.md, LICENSE, Third Party Notices.
