# Homework-Challenge

How to Play


After clone the repository you gonna see the sample scene like this:

<img width="1900" height="955" alt="image" src="https://github.com/user-attachments/assets/889e5192-20d7-4e08-b2c5-03a06404da95" />

You can click on Play to see four game objects instansiated in the Scene, three of them locally and one from server (Google Drive for testing purposes)

The "Artist should"

1) Drag and drop the spawner prefab in scene
2) Create a new GameObject "The Artist" would like to spawn and maka a prefab from it.
3) Assign a new AssetBundle name to it
   <img width="331" height="203" alt="image" src="https://github.com/user-attachments/assets/8267da58-ec60-4fc9-bd07-eb5e90bb206b" />
4) Create a new ObjectToSpawnReference
<img width="1242" height="636" alt="image" src="https://github.com/user-attachments/assets/33a6b522-a03b-4423-a858-3c1315fa520e" />
5) Drag a new prefab "The Artist" would like to spawn in the field Prefab Reference
<img width="407" height="267" alt="image" src="https://github.com/user-attachments/assets/8e107e2d-2d3d-45e1-9bb8-4da3bbddb4d7" />
6) Automacally the others field gonna be completed, but also there is a manual way to updated with the Update Data Reference button in the context menu
<img width="395" height="211" alt="image" src="https://github.com/user-attachments/assets/24888802-ae7b-478d-adf7-f4f0b3703da7" />
7) Assign the ObjectToSpawnReference to the new spawner in the scene
8) Go to the AmberTool menu click on AssetBundle -> CreateAssetBundle
9) In the AssetBundleManager GameObject from the scene set the name of the bundle you want to load
    <img width="604" height="124" alt="image" src="https://github.com/user-attachments/assets/4c99341a-055d-473f-b342-1688759288af" />
10) In the same GameObject leave Local the SourceLocation
    Note: I made an implementation with Server but GoogleDrive send finish the transaction even before to really finished.
    Just for server example a leave an humble solution in AssetBundleDownload with the url to the drive where a load the assetbundle files.
