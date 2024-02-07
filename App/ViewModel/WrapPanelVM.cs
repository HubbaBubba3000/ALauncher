using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using ALauncher.Data;
using ALauncher.View;
using ALauncher.Model;
using System.ComponentModel;
using System.Linq;

namespace ALauncher.ViewModel;

public class WrapPanelVM : BaseVM {
    private Folder _current;
    public Folder CurrentFolder {
        get {
            return _current;
        } 
        set {
            if (value == null) return;
            _current = value;
            if (_current.Items != null)
                Items = new(_current.Items);
        }
    }

    private FolderManager folderManager;
    private ObservableCollection<Item> _items;
    public ObservableCollection<Item> Items {
        get {
            return _items ;
        }
        set {
            _items = value;
            OnPropertyChanged("Items");
        }
    }

    public ICommand AddItem {
        get {
            return new RelayCommand((_obj) => {
                using (AddictionItems ai = new AddictionItems()) {
                    ai.Closing += new CancelEventHandler((obj, e) => {
                    if (!ai.IsAdd) return;
                    Items.Add(ai.GetItem);
                    folderManager.folders.Single(f => f == CurrentFolder).Items = Items.ToList();
                    folderManager.UpdateFolders();
                    });
                    ai.Show();
                }
            });
        } 
    }
     public ICommand DeleteItem {
        get {
            return new RelayCommand((obj) => {
                    var item = Items.Single(i => i.AppName == (string)obj);
                    Items.Remove(item);
                    folderManager.folders.Single(f => f == CurrentFolder).Items = Items.ToList();
                    folderManager.UpdateFolders();
            });
        }
    } 
    public ICommand Run {
        get {
            return new RelayCommand((obj) => {
                folderManager.RunItem((string)obj);
            });
        }
    }

    public WrapPanelVM(FolderManager b) {
        folderManager = b;
        _items = new();
    }
}