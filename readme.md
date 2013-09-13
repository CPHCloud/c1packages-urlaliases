# Url Aliases Composite C1 Package

## Intro
This package adds URL Aliases to Composite C1. A usefull feature when you want to make nice short urls for pages deep in the site page hierarchy, or temporarily redirect users to other pages than Composite would normally serve. Read more about the package at http://packages.cphcloud.com or grab a stable build from http://www.composite.net/Add-on-Market/Packages/CphCloud.Packages.UrlAlias

## Technical information
This solution has one C# project that contains the actual source for the package. It has a post build script that uses 7Zip to create a packaged zip file, that can be installed on a Composite instance. Find this under /bin/debug or /bin/release depending on your build.

Also, the build script copies the files defined as output in the C# project to `/Website`. If you like, you can place a Composite instance here for easier testing during development. No need to uninstall/install the package all the time.
