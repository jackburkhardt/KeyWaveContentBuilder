using System;
using SFB;
using UnityEngine;

namespace DefaultNamespace
{
    public class FilePicker : MonoBehaviour
    {

        public void SelectFile()
        {
            StandaloneFileBrowser.OpenFilePanelAsync("Select .json file(s)...", "", "json", true, 
                LoadFile);
        }
        
        public void SelectFolder()
        {
            StandaloneFileBrowser.OpenFolderPanelAsync("Select GameData folder...", "", false,
                strings => { LoadFolder(strings[0]);});
        }

        private void LoadFolder(string path)
        {
            Debug.Log(path);
        }

        private void LoadFile(string[] paths)
        {
            
        }
    }
}