# SteamScreenshotSorter
## Automatically sort steam screenshots into subfolders by game name
Steam provides the option to store an uncompressed copy of any screenshots you take in-game, but these screenshots all end up in the same folder.

When executed, this tool will separate your screenshots based on the game they were taken in.

### Before:

![Before](/screenshots/before.png)

### After:

![After](/screenshots/after.png)

## Features:

- [x] Split Steam screenshot files based on game
- [x] Uses Steam store API to fetch game name using App ID from screenshot name
- [x] Remove App ID prefix of screenshot names when moving them
- [ ] UI with options and progress bar
- [ ] Fetch screenshots from steam directory directly

### Example usage:

`SteamScreenshotSorter.exe "C:\Users\dj-99\Pictures\Steam Screenshots"`