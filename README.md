# Easy buttons for the Unity default inspector
These tiny scripts add the ability to quickly show a button in the inspector for any zero-argument method.

## How to use
1. Clone the repo or download the latest release
2. Add the EasyButtons folder to your Unity project or import the .unitypackage
3. Add the Button attribute to a method

   ![Code example](/Images/example.png)
4. You should now see a button at the top of the component with the same name as the method

   ![Button in the inspector](/Images/inspector.png)

   ![Result](/Images/console.png)

## Notes
- As mentioned in Issue [#4](https://github.com/madsbangh/EasyButtons/issues/4) custom editors don't play well with EasyButtons. As a workaround you can Inherit from ObjectEditor instead of Editor, or manually draw the buttons using extension `DrawEasyButtons` method.
