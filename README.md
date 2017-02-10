#Overview
This repository includes a Unity plugin and sample scene for stereoscopic rendering on ODG glasses.  Head tracking is supported, in which the rotation data is read from the device’s sensors.  

The camera prefab is a stereo-camera setup, where each camera splits the viewport and sets the interpupillary distance at runtime.  The other remaining camera settings can be configured.

#Motivation
The plugin can be used for stereoscopic 3D and/or 360 degree rendering.  Typical applications include 360 degree video players, virtual reality (VR), or augmented reality (AR) using 3DOF (degrees of freedom).

#Installation
For the Android project:


1. Install the plugin by either: a) Importing the ``ODGPlugin.unitypackage`` into an existing project b) Cloning the repository and using it as a starting point for your project
2. Drag and drop the ``ODGCamera.prefab`` into the current scene at desired 3D position
3. Make sure the ODGCamera prefab is the main camera used by Unity
4. If desired, toggle "Head tracking" on the OdgStereoCameraPlugin component of the ODGCamera prefab

Note: On our glasses, black renders as transparent.  This is useful for AR applications on our see-through displays.  The LeftEye and RightEye of the ODGCamera has a default background color of black to achieve this effect.

#Tests
The plugin has been tested on Unity Editor 5.5 for MacOS and Windows platform.  The plugin has been tested for R6 and R7.

#License
Copyright 2017 Osterhout Design Group

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

