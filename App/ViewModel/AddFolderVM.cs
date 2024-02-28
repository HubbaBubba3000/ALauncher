
using System.Windows;
using System.Windows.Input;
using ALauncher.Data;

namespace ALauncher.ViewModel;

    public sealed class AddFolderVM : BaseVM {
        public Folder GetFolder {get;private set;}
        public string FolderName {
            get => GetFolder.Name;
            set {
                GetFolder.Name = value;
                OnPropertyChanged("FolderName");
            }
        }
        public AddFolderVM(Folder? folder) {
            this.GetFolder = folder ?? new Folder() {Name = "New Folder"};
        }
}