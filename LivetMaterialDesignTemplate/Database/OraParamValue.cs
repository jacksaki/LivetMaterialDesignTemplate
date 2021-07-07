using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetMaterialDesignTemplate {
    /// <summary>
    /// Oracle パラメーター
    /// </summary>
    public class OraParamValue {
        private OraParamValue() {

        }
        /// <summary>
        /// コンストラクタ(OUT)
        /// </summary>
        /// <param name="name">バインド変数名</param>
        /// <param name="t">型</param>
        public OraParamValue(string name, OraDataType t) {
            this.Name = name;
            this.DataType = t;
            this.Value = null;
            this.Direction = System.Data.ParameterDirection.Output;
        }
        /// <summary>
        /// コンストラクタ(IN/OUT)
        /// </summary>
        /// <param name="name">バインド変数名</param>
        /// <param name="t">型</param>
        /// <param name="value">値</param>
        public OraParamValue(string name, OraDataType t, object value) {
            this.Name = name;
            this.DataType = t;
            this.Value = value;
            this.Direction = System.Data.ParameterDirection.InputOutput;
        }
        internal System.Data.ParameterDirection Direction {
            get;
            private set;
        }
        /// <summary>
        /// バインド変数名を取得します
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// バインド変数の型を取得します
        /// </summary>
        public OraDataType DataType {
            get;
            private set;
        }
        /// <summary>
        /// バインド変数の値を取得します。
        /// </summary>
        public object Value {
            get;
            internal set;
        }
    }
}
