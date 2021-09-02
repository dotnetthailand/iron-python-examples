# How to run the project locally
- This works on Windows only and I can't get compiled Python DLL to target .NET 5.

## Prerequisite
- Installed Visual Studio 2019
- Installed [.NET Framwork 4.8](https://go.microsoft.com/fwlink/?linkid=2088517)
- Installed [IronPython for Windows](https://github.com/IronLanguages/ironpython3/releases/download/v3.4.0-alpha1/IronPython-3.4.0a1.msi)
- A sytem path variable contains `C:\Program Files\IronPython 3.4` folder.

## Prepare source code and environment
- Clone the project.
```
git clone git@github.com:dotnetthailand/iron-python-examples.git
```
- CD to `iron-python-examples` folder.
```
cd iron-python-examples
```
- Double click `IronPythonExamples.sln` to open the project with Visual Studio.
- If you are using Linux, open `IronPythonExamples.csproj`, change `copy /Y Math.dll $(OutDir)` command to `cp Math.dll $(OutDir)` and save it.

The current project file/folder structure.
```
iron-python-examples/
├── IronPythonClassLibraryTest.cs (the main test class)
├── IronPythonExamples.csproj
├── IronPythonExamples.sln
├── Math.py (Python code which will be compiled to Math.dll by ipyc.exe)
```

## Build the project and run unit test
- In the Solution Explorer window, select project `IronPythonExamples` node.
- Press `Ctrl+Shift+B` to build the project.
- Open `IronPythonClassLibraryTest.cs`, and click somewhere inside `Sum_ValidInput_ReturnCorectValue` test case.
- Press `Ctrl+R, T` to run the test case.
- The Test Explorer window will be automaticall opened and it will show a status of the running test case.
- You should find a succeful test result as the screenshot below.

![](images/testing-result.png)


# Useful resource
- https://www.py4u.net/discuss/736777
- https://www.codemag.com/Article/2009101/Interactive-Unit-Testing-with-.NET-Core-and-VS-Code
