Improve your sleep and increase late night web scrolling comfort by matching your display brightness to your body's circadian rythm hassle free, just set and forget!
ScreenDimmer is a Windows application that allows you to dim your display beyond the limits of your monitor's settings.

Unlike alternatives however, ScreenDimmer supports transitioning between different dimming levels according to time through either a manual schedule or current sun position.
If you live further from the equator and don't want to constantly change the dimming schedule in winter then this last feature is just for you!

ScreenDimmer has only been verified to work on Windows 10 though other windows versions should be supported too.

![alt text](https://github.com/user-attachments/assets/76087897-7345-4c30-948c-f84b0c6226eb)
![alt text](https://github.com/user-attachments/assets/e4630a99-eb24-4db6-9740-1e9e5d469c21)

By default ScreenDimmer is unable to lower the brightness of Windows UI elements such as the start menu and alt+tab window preview for security reasons.

**Only do this if you understand the risks!** Always check for malicious code before building and signing the executable. It is also important to verify the safety procedure explained in the link below, never run commands from the internet without double checking them! 

If you want to ScreenDimmer to draw on top of Windows UI elements then build the program using ```<requestedExecutionLevel level="requireAdministrator" uiAccess="true" />``` in app.manifest and sign the executable using a self-signed certificate as explained [here].




[here]: https://stackoverflow.com/questions/84847/how-do-i-create-a-self-signed-certificate-for-code-signing-on-windows "self-signed cert tutorial"
