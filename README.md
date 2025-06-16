Improve your sleep and increase late night scrolling comfort by matching your display brightness to your body's circadian rythm hassle free, just set and forget!
ScreenDimmer is a Windows application that allows you to dim your display beyond the limits of your monitor's settings.

Unlike alternatives however, ScreenDimmer supports transitioning between different dimming levels according to time through either a manual schedule or current sun position.
If you live further from the equator and don't want to constantly change the dimming schedule in winter then this last feature is just for you!

ScreenDimmer has only been verified to work on Windows 10 though other windows versions should be supported too.

![alt text](https://github.com/user-attachments/assets/76087897-7345-4c30-948c-f84b0c6226eb)
![alt text](https://private-user-images.githubusercontent.com/43272341/455744300-89c17326-8a83-4538-ac4e-f4f380e7fbec.PNG?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NTAxMTQ2NDMsIm5iZiI6MTc1MDExNDM0MywicGF0aCI6Ii80MzI3MjM0MS80NTU3NDQzMDAtODljMTczMjYtOGE4My00NTM4LWFjNGUtZjRmMzgwZTdmYmVjLlBORz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNTA2MTYlMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjUwNjE2VDIyNTIyM1omWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPTAwOTRjODc4ODE2ZGNlMjNmZDE3OGZlMTgyZmMxNzIyOWI1MTVhOGEyZTA2YTdhZTFmMzg4YzA0MjI1NjY4YTUmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0In0.mg4VjYPrILXCftSS-w3xP_ZF0nP3e8RdaLn7cH_vC8M)

By default ScreenDimmer is unable to lower the brightness of Windows UI elements such as the start menu and alt+tab window preview for security reasons.

**Only do this if you understand the risks!** Always check for malicious code before building and signing the executable. It is also important to verify the safety procedure explained in the link below, never run commands from the internet without double checking them! 

If you want to ScreenDimmer to draw on top of Windows UI elements then build the program using ```<requestedExecutionLevel level="requireAdministrator" uiAccess="true" />``` in app.manifest and sign the executable using a self-signed certificate as explained [here].




[here]: https://stackoverflow.com/questions/84847/how-do-i-create-a-self-signed-certificate-for-code-signing-on-windows "self-signed cert tutorial"
