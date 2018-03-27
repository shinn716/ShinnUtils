# ShinnUtils

Shaderlab
http://www.shaderslab.com/index.html
1. http://www.shaderslab.com/demo-87---dissolve-3d.html
2. http://www.shaderslab.com/demo-90---flat-shading.html
3. http://www.shaderslab.com/demo-50---grayscale-depending-zbuffer.html
4. http://www.shaderslab.com/demo-99---pencil-effect-1.html
5. http://www.shaderslab.com/demo-04---snow-effect.html
6. http://www.shaderslab.com/demo-43---misc.-rim-effect.html
7. http://www.shaderslab.com/demo-29---soccer-lawn-field.html
8. http://www.shaderslab.com/demo-28---glass-with-skybox-reflection.html
9. http://www.shaderslab.com/demo-44---burning-paper.html
10. http://www.shaderslab.com/demo-60---fur-shader.html
12. http://www.shaderslab.com/demo-27---moss-on-rock.html
13. http://www.shaderslab.com/demo-34---cosmic-effect.html


SteamVR Tracking without an HMD

Reference.
1. http://help.triadsemi.com/steamvr-tracking/steamvr-tracking-without-an-hmd
2. http://www.pencilsquaregames.com/getting-steamvr-tracking-data-in-unity-without-a-hmd/

------------------------------------------------------------

StreanVR (WIN 10) - OpenVR
Step 1: 路徑 C:\Program Files (x86)\Steam\logs\vrserver.txt
找出 vrsettings 位置
[Settings] ... C:\Program Files (x86)\Steam\steamapps\common\SteamVR\resources\settings\default.vrsettings
[Settings] ... C:\Program Files (x86)\Steam\steamapps\common\SteamVR\drivers\htc\resources\settings\default.vrsettings
[Settings] ... C:\Program Files (x86)\Steam\steamapps\common\SteamVR\drivers\lighthouse\resources\settings\default.vrsettings
[Settings] ... C:\Program Files (x86)\Steam\steamapps\common\SteamVR\drivers\null\resources\settings\default.vrsettings
[Settings] ... C:\Program Files (x86)\Steam\config\steamvr.vrsettings

Step2: 修改 resources\settings\default.vrsetting
將內容修改成

"requireHmd" : false
"forcedDriver" : "null"
"activateMultipleDrivers" : true


Step3: 修改 null\resources\settings\default.vrsettings
將內容修改成

{
	"driver_null" : {
		"enable" : true,
		"serialNumber" : "Null Serial Number", 
		"modelNumber" : "Null Model Number",
		"windowX" : 0,
		"windowY" : 0,
		"windowWidth" : 2160,
		"windowHeight" : 1200,
		"renderWidth" : 1512,
		"renderHeight" : 1680,
		"secondsFromVsyncToPhotons" : 0.01111111,
		"displayFrequency" : 90.0
	}
}
完成後 SteamVR圖示會變更.

Step4: Unity OpenVR 調整
開啟 Unity, 將 SteamVR Camera 的Target eye 改為 none,
[CameraRig] 的 Left, Right, 改為 Tracker1, Tracker2
Tracker1, Tracker2 的 index 改為 1 跟 2. 

VIVE 頭盔 跟 手把 兩個皆可不使用.
只使用Tracker.
