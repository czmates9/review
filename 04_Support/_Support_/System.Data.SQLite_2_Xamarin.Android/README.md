# system.data.sqlite
howto build and use System.Data.SQLite with Xamarin.Android  
and build on OS Windows 10 (Professional)  

**Why**  
We have solution for wince devices, ASP.NET on IIS which uses System.Data.SQLite library and components which depends on this.  
We can use other (new) libraries, but it will be more work to transform logic from existing components. So it will be nice to use existing components and port System.Data.SQLite for Xamarin.Android (VS 2019, .NET 4.xx, .NET Standard[Mono]).  

**Prerequisities**   

[System.Data.SQLite](http://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki)
- downloadable sources for sqlite
- installers for windows, winces
- but not for android

To be able to work with, we need to compile specific version for Android. To do that we need Android.ndk to build sources.  \
[Android NDK](https://developer.android.com/ndk/downloads)  

**Procedure**
- download Android NDK and uzip - [Android NDK windows](https://developer.android.com/ndk/downloads) for windows due to my purpose 
- download System.Data.SQLite and unzip - [sqlite-netFx-source-1.0.113.0.zip](http://system.data.sqlite.org/downloads/1.0.113.0/sqlite-netFx-source-1.0.113.0.zip)

*build native library for android*  
- create Android ndk build scripts  
- point scripts to correct directories
- modify some source files `*.c` in SQLite.Interop/src, respectively I copied src files into directory `build` in Android ndk build scripts. extensions are #including headers from incorrect path: `#include "sqlite3ext.h"` => `#include "../core/sqlite3ext.h"` 
- run build.bat at Android ndk build scripts directory 
- if run correctly it creates libraries at libs directory for each supported platform with names `libsqlite3.so`. 

*build SQLite.InterOP*  
- navigate to System.Data.SQLite Setup directory : ...\sqlite-netFx-full-source-1.0.113.0\Setup\  
- we want to build wrapper for android library based on .NET Standard 2.0 or 2.1 (we used 2.1) 
- but there is problem with resolvin library name on xamarin x android x java x kernel : it's maybe on not impleneted components at xamarin android, but who knows. So I made a hack at `\sqlite-netFx-source-1.0.113.0\System.Data.SQLite\UnsafeNativeMethods.cs` where I added `#define ANDROID` and changed on lines 3368-3373 from:
```c
#elif USE_INTEROP_DLL
    //
    // NOTE: Otherwise, if the native SQLite interop assembly is enabled,
    //       use it.
    //
    internal const string SQLITE_DLL = "SQLite.Interop.dll";
#else
```
TO
```c
#elif USE_INTEROP_DLL && !ANDROID
        //
        // NOTE: Otherwise, if the native SQLite interop assembly is enabled,
        //       use it.
        //
        internal const string SQLITE_DLL = "SQLite.Interop.dll";
#elif USE_INTEROP_DLL && ANDROID
        // HACK for android
        internal const string SQLITE_DLL = "libsqlite3.so";   # here may be any name, but have to have extension '.so', because it is native library and on instalation is distributed to system/project libs by Android installer
#else
```
- run command `.\build_net_standard_21.bat`  

*linking into VS2019*  
- add link in project to compiled libs directory, respective copy to the project and include sources into project (lib direcory)
- each `libsqlite3.so` have to be tagged as `Build Action = AndroidNativeLibrary`  
- add reference to compiled System.Data.SQLite from NetStandard2.1 directory, and `Copy Lokacal = True`
- add reference to 'System.Data.SQLite.dll.config' and set `Build Action = Content`
- edit 'System.Data.SQLite.dll.config' add this tags: `<add key="PreLoadSQLite_LibraryFileNameOnly" value="libsqlite3.so" />` and `<add key="PreLoadSQLite_UseAssemblyDirectory" value="1" />`  

*Compile, build, deploy and test*
For testing, we modified tests from System.Data.SQLite sources at `sqlite-netFx-source-1.0.113.0\test\` directory  
Code is located at ... 

*finaly build at release and test*

:) Have fun (:
