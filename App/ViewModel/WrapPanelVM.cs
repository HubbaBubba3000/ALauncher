using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using ALauncher.Data;
using ALauncher.View;
using ALauncher.Model;
using System.ComponentModel;
using System.Linq;
using System.Collections.Specialized;

namespace ALauncher.ViewModel;

public class WrapPanelVM : BaseVM {
    public Item SelectedItem { get;set; }
    private Folder _current;
    public Folder CurrentFolder {
        get {
            return _current;
        } 
        set {
            if (value == null) return;
            _current = value;
            Items = new(_current.Items);
        }
    }

    private Base BaseModel;
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
                    BaseModel.folders.Single(f => f == CurrentFolder).Items = Items.ToList();
                    BaseModel.UpdateFolers(null,null);
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
                    BaseModel.folders.Single(f => f == CurrentFolder).Items = Items.ToList();
                    BaseModel.UpdateFolers(null,null);
            });
        }
    } 
    public ICommand Run {
        get {
            return new RelayCommand((obj) => {
                BaseModel.RunItem((string)obj);
            });
        }
    }

    public WrapPanelVM(Base b) {
        BaseModel = b;
        _items = new();
    }
}