using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetMaterialDesignTemplate.Models {
    public class LoginParameter : NotificationObject,ILoginParameter {

        private string _Id;

        public string Id {
            get {
                return _Id;
            }
            set {
                if (_Id == value) {
                    return;
                }
                _Id = value;
                RaisePropertyChanged();
            }
        }

        private string _Password;

        public string Password {
            get {
                return _Password;
            }
            set {
                if (_Password == value) {
                    return;
                }
                _Password = value;
                RaisePropertyChanged();
            }
        }
        public void Validate() {
            return;
            if(string.IsNullOrEmpty(this.Id)) {
                throw new ArgumentException("Id is null.");
            }
            if (string.IsNullOrEmpty(this.Password)) {
                throw new ArgumentException("Password is null.");
            }
        }

    }
}
