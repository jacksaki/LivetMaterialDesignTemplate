using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetMaterialDesignTemplate.Models {
    public class LoginUser {
        private static LoginUser _singleton = null;
        public static LoginUser GetInstance() {
            if(_singleton == null) {
                _singleton = new LoginUser();
            }
            return _singleton;
        }
        public void Login(ILoginParameter parameter) {
            var user = User.Create(parameter);
            if (user != null) {
                this.User = user;
            }
        }
        public User User {
            get;
            private set;
        }
    }
}
