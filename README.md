# Easy buttons for the Unity default inspector
These tiny scripts add the ability to quickly show a button in the inspector for any zero-argument method.

## How to use
### Install via Git URL
Project supports Unity Package Manager. To install the project as a Git package do the following:

1. Close the Unity project and open the `Packages/manifest.json` file.
2. Update dependencies to have `com.madsbangh.easybuttons` package:
   ```json
   {
     "dependencies": {
       "com.madsbangh.easybuttons": "https://github.com/madsbangh/EasyButtons.git#upm"
     }
   }
   ```
3. Open Unity project.

### Add to project Assets
Alternatively you can add the code directly to the project:

1. Clone the repo or download the latest release
2. Add the EasyButtons folder to your Unity project or import the .unitypackage

### Use the Button attribute
1. Add the Button attribute to a method

   ![Code example](/Images/example.png)
2. You should now see a button at the top of the component with the same name as the method

   ![Button in the inspector](/Images/inspector.png)

   ![Result](/Images/console.png)

## Notes
- As mentioned in Issue [#4](https://github.com/madsbangh/EasyButtons/issues/4) custom editors don't play well with EasyButtons. As a workaround you can Inherit from ObjectEditor instead of Editor, or manually draw the buttons using extension `DrawEasyButtons` method.
- Older versions of Unity might not understand the reference to EasyButtons runtime in tbe EasyButtons editor assembly definition. If you experience issues, you can re-add the reference, or remove the asmdefs completely.
