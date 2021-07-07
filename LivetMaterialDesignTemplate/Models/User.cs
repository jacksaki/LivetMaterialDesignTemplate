using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetMaterialDesignTemplate.Models {
    public class User:IUser {
        public static User Create(ILoginParameter parameter) {
            var user = new User() {
            };
            return user;
        }
        public string Id {
            get;
            private set;
        }
        public string Name {
            get;
            private set;
        }

    }
}
