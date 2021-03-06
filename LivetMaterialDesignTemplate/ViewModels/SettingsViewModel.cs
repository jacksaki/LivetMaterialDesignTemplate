using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LivetMaterialDesignTemplate.Models;

namespace LivetMaterialDesignTemplate.ViewModels {
    public class SettingsViewModel : MenuItemViewModelBase {
        public SettingsViewModel(MainWindowViewModel parent) : base(parent) {
        }
    }
}
