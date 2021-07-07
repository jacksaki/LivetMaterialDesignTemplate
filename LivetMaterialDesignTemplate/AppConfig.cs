using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LivetMaterialDesignTemplate {
    public enum DatabaseType {
        [EnumText("Oracle")]
        Oracle,
        [EnumText("SQLite")]
        SQLite,
    }
    public class AppConfig {

        private static AppConfig _singleton = null;
        private AppConfig() {

        }
        public static AppConfig GetInstance() {
            if (_singleton == null) {
                _singleton = new AppConfig();
                _singleton.Load();
            }
            return _singleton;
        }
        public string ConfigPath {
            get {
                return System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
            }
        }
        public void Load() {

            var doc = XDocument.Load(this.ConfigPath);
            var root = doc.Element("Configurations");
            var db = root.Element("Databases");
            this.ConnectionString = new Dictionary<DatabaseType, string>();
            foreach (var t in EnumsNET.Enums.GetValues<DatabaseType>()) {
                this.ConnectionString.Add(t, db.Elements("Database").Where(x => t.GetEnumText().Equals(x.Attribute("Type").Value)).FirstOrDefault()?.Attribute("ConnectionString")?.Value);
            }
        }
        private Dictionary<DatabaseType, string> ConnectionString {
            get;
            set;
        }
        public void ConnectDatabase() {
            foreach (var t in EnumsNET.Enums.GetValues<DatabaseType>().Where(x => !string.IsNullOrEmpty(this.ConnectionString[x]))) {
                switch (t) {
                    case DatabaseType.Oracle:
                        OracleQuery.SetConnectionString(this.ConnectionString[t]);
                        break;
                    case DatabaseType.SQLite:
                        SQLiteQuery.SetConnectionString(this.ConnectionString[t]);
                        break;
                }
            }
        }

    }
}
