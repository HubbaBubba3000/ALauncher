using ALauncher.Data;
using System.Collections.Generic;

namespace ALauncher.ViewModel {
    public class ControlPanel : BaseVM {
        List<string> _FolderList;

        public ControlPanel() {
            _FolderList = new List<string>(10);
            FolderList = _FolderList;
        }
        private void setFolder() {
           var list = JsonReader.Readfolders("C:/Users/WinRAR/Documents/_projects/ALauncher/Content.json");
           foreach (var folder in list) 
                _FolderList.Add(folder.Name);
           
        }
        public List<string> FolderList {
            get {
                return _FolderList;
            }
            set {
                setFolder();
               OnPropertyChanged("ItemsSource");
            }
        }

    }
}