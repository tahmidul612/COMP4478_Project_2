# COMP-4478_Project_2

|                GROUP 2 | MEMBERS        |
| ---------------------: | :------------- |
| Abed Alhalim Ezz Aldin | Jacob Lavoie   |
|           Adam Giddens | Justin Jacko   |
|            Austin Hart | Tahmidul Islam |

---

## How to Play

| Platform | Link | Notes |
| ---: | :---: | --- |
| Windows | [![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/tahmidul612/COMP4478_Project_2?include_prereleases&style=for-the-badge)](https://github.com/tahmidul612/COMP4478_Project_2/releases) [![GitHub commits since latest release (by SemVer including pre-releases)](https://img.shields.io/github/commits-since/tahmidul612/COMP4478_Project_2/latest/main?include_prereleases&sort=semver&style=for-the-badge)]() | Download StandaloneWindows64.zip asset from the latest release, extract the zip and run `build\StandaloneWindows64\COMP4478_Project_2.exe` | 
| Others[^1] | [![Website](https://img.shields.io/website?label=Play%20Online&style=for-the-badge&url=https%3A%2F%2Ftahmidul612.github.io%2FCOMP4478_Project_2%2F)](https://tahmidul612.github.io/COMP4478_Project_2) [![GitHub last commit (branch)](https://img.shields.io/github/last-commit/tahmidul612/COMP4478_Project_2/webgl?label=Updated&style=for-the-badge)]() | Link to the webpage hosted in github pages, renders the game with WebGl |
| Source Code[^2][^3] | [![GitHub last commit (branch)](https://img.shields.io/github/last-commit/tahmidul612/COMP4478_Project_2/main?label=Updated&style=for-the-badge)](https://nightly.link/tahmidul612/COMP4478_Project_2/workflows/source/main/Source%20code.zip) | Link to download the source code. Download the latest artifact from [actions/Source Code](https://github.com/tahmidul612/COMP4478_Project_2/actions/workflows/source.yml) if link does not work. |

## Development Tools
The master branch is now protected from commits. Please do your work in a separate branch and merge with pull request when done.
https://blog.oddbit.com/post/2019-06-14-git-etiquette-commit-messages/
### Required
- Unity Editor 2021.3.21f1 (Download from Unity Hub)
- Git (https://git-scm.com/download)
- Git LFS (https://docs.github.com/en/repositories/working-with-files/managing-large-files/installing-git-large-file-storage) [^2]
- Unity Smart Merge (https://docs.unity3d.com/Manual/SmartMerge.html)
### Optional
- Fork Git Client (https://git-fork.com)
  - Ignore the pricing, the personal "evaluation" is perpetual
  - Setup Unity Smart Merge (Preferences > Integration > Merge Tool)
- GitHub Desktop (https://desktop.github.com)

[^1]: Manual resolution selection from the settings menu not available in WebGL

[^2]: The repository uses Github LFS to store large files (e.g. images, assets). As a result, downloading the source code directly from the repo page normally or from releases does not work. This Github action "[actions/Source Code](https://github.com/tahmidul612/COMP4478_Project_2/actions/workflows/source.yml)" parses LFS file pointers and downloads the actual files, then uploads the zip as an artifact.

[^3]: To clone the repository with git cli and make changes, it is required to install and initialize [git lfs](https://docs.github.com/en/repositories/working-with-files/managing-large-files/installing-git-large-file-storage)
