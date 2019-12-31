## SteamScreenshotSorter
#Sort steam screenshots into subfolders by game names
Steam provides the option to store an uncompressed copy of any screenshots you take in-game, but these screenshots all end up in the same folder.
When executed, this tool will separate your screenshots based on the game they were taken in.

Before | After
-------|-------
![Before](/screenshots/before.png) | ![After](/screenshots/after.png)

Example usage:
`>SteamScreenshotSorter.exe C:\Users\dj-99\Pictures\Steam Screenshots`

Features:

- [x] Split Steam screenshot files based on game
- [x] Uses Steam store API to fetch game name using App ID from screenshot name
- [x] Remove App ID prefix of screenshot names when moving them

Planned/potential features:

- [ ] UI with options and progress bar
- [ ] Fetch screenshots from steam directory directly