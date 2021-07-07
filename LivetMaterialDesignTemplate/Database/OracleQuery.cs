using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Diagnostics;

namespace LivetMaterialDesignTemplate {
    public class OracleQuery : DbQueryBase {
        private static string _connectionString = null;
        public OracleQuery() : base(_connectionString) {
        }
        public OracleQuery(string connectionString) : base(connectionString) {
        }
        public OracleConnection OracleConnection {
            get {
                return (OracleConnection)Connection;
            }
        }
        public static void SetConnectionString(string connectionString) {
            using(var conn = new OracleConnection(connectionString)) {
                conn.Open();
                _connectionString = connectionString;
            }
        }
        protected override IDbConnection GenerateConnection(string connectionString) {
            return new OracleConnection(connectionString);
        }

        protected override IDbDataParameter CreateParameter(string name, object value) {
            return new OracleParameter(name, value);
        }


        public static string GetCurrentUserName() {
            return new OracleQuery().GetFirstRow("SELECT SYS_CONTEXT('USERENV','CURRENT_USER') FROM DUAL", null)[0].ToString();
        }
        protected override IDbCommand GenerateCommand(string sql, IDictionary<string, object> param) {
            var cmd = (OracleCommand)this.Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.InitialLONGFetchSize = 32767;
            cmd.BindByName = true;
            if (param != null) {
                cmd.Parameters.AddRange(param.Select(p => new OracleParameter(p.Key, p.Value)).ToArray());
            }
            return cmd;
        }
        public override DataTable GetDataTable(string sql, IDictionary<string, object> param, int? fetchSize) {
            using (var cmd = (OracleCommand)GenerateCommand(sql, param))
            using (var dr = cmd.ExecuteReader()) {
                if (fetchSize.HasValue) {
                    var columnCount = dr.FieldCount;
                    dr.FetchSize = fetchSize.Value * columnCount;
                }
                var dt = new System.Data.DataTable();
                dt.Load(dr);
                return dt;
            }
        }
        public static IEnumerable<string> GetDataSources() {
            return new OracleDataSourceEnumerator().GetDataSources().Rows.Cast<System.Data.DataRow>().Select(x => x["InstanceName"].ToString());
        }

        public override SqlResult GetSqlResult(string sql, IDictionary<string, object> param, int? fetchSize) {
            using (var cmd = (OracleCommand)GenerateCommand(sql, param))
            using (var dr = cmd.ExecuteReader()) {
                if (fetchSize.HasValue) {
                    var columnCount = dr.FieldCount;
                    dr.FetchSize = fetchSize.Value * columnCount;
                }
                return new SqlResult(dr);
            }
        }

        public System.Data.DataTable GetRefCursor(string sql, IDictionary<string, object> inParameters, IEnumerable<OraParamValue> outParameters) {
            if (outParameters == null || !outParameters.Any()) {
                return null;
            }
            using (var cmd = this.GenerateCommand(sql, inParameters)) {
                foreach (var p in outParameters) {
                    switch (p.DataType) {
                        case OraDataType.Char:
                        case OraDataType.NChar:
                        case OraDataType.NVarchar2:
                        case OraDataType.Varchar2:
                            cmd.Parameters.Add(new OracleParameter(p.Name, p.DataType.ToOracleDbType(), 4000, p.Value, p.Direction));
                            break;
                        default:
                            cmd.Parameters.Add(new OracleParameter(p.Name, p.DataType.ToOracleDbType(), p.Value, p.Direction));
                            break;
                    }
                }
                cmd.ExecuteNonQuery();
                var result = new Dictionary<string, object>();
                foreach (var p in cmd.Parameters.Cast<OracleParameter>().Where(x =>
                     (x.Direction == ParameterDirection.Output || x.Direction == ParameterDirection.InputOutput)
                     && x.OracleDbType != OracleDbType.RefCursor)) {
                    result.Add(p.ParameterName, p.Value);
                }

                foreach (var p in cmd.Parameters.Cast<OracleParameter>().Where(x =>
                    (x.Direction == ParameterDirection.Output || x.Direction == ParameterDirection.InputOutput)
                    && x.OracleDbType == OracleDbType.RefCursor)) {
                    var cursor = p.Value as OracleRefCursor;
                    using (var dr = cursor.GetDataReader()) {
                        var dt = new System.Data.DataTable();
                        dt.Load(dr);
                        return dt;
                    }
                }
            }
            return null;
        }

    }
}
