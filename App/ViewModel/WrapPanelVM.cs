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
    public Folder CurrentFolder {
        get {
            return controlPanelVM.CurrentFolder;
        } 
        set {
            controlPanelVM.CurrentFolder = value;
            
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
            CurrentFolder.Items = _items.ToList();
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
                    BaseModel.UpdateFolers();
                    });
                    ai.Show();
                }
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

    public WrapPanelVM(Base b, ControlPanelVM cp) {
        BaseModel = b;
        controlPanelVM = cp;
        _items = new(CurrentFolder.Items);
    }
}