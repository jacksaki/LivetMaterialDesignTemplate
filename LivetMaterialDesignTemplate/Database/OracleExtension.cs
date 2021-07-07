using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace LivetMaterialDesignTemplate {
    public enum OraDataType {
        [EnumText("BFile")]
        BFile,
        [EnumText("BinaryDouble")]
        BinaryDouble,
        [EnumText("BinaryFloat")]
        BinaryFloat,
        [EnumText("Blob")]
        Blob,
        [EnumText("Boolean")]
        Boolean,
        [EnumText("Byte")]
        Byte,
        [EnumText("Char")]
        Char,
        [EnumText("Clob")]
        Clob,
        [EnumText("Date")]
        Date,
        [EnumText("Decimal")]
        Decimal,
        [EnumText("Double")]
        Double,
        [EnumText("Int16")]
        Int16,
        [EnumText("Int32")]
        Int32,
        [EnumText("Int64")]
        Int64,
        [EnumText("IntervalDS")]
        IntervalDS,
        [EnumText("IntervalYM")]
        IntervalYM,
        [EnumText("Long")]
        Long,
        [EnumText("LongRaw")]
        LongRaw,
        [EnumText("NChar")]
        NChar,
        [EnumText("NClob")]
        NClob,
        [EnumText("NVarchar2")]
        NVarchar2,
        [EnumText("Raw")]
        Raw,
        [EnumText("RefCursor")]
        RefCursor,
        [EnumText("Single")]
        Single,
        [EnumText("TimeStamp")]
        TimeStamp,
        [EnumText("TimeStampLTZ")]
        TimeStampLTZ,
        [EnumText("TimeStampTZ")]
        TimeStampTZ,
        [EnumText("Varchar2")]
        Varchar2,
        [EnumText("XmlType")]
        XmlType,
    }

    public static class OracleExtension {
        public static OracleDbType ToOracleDbType(this OraDataType t) {
            switch (t) {
                case OraDataType.BFile:
                    return OracleDbType.BFile;
                case OraDataType.BinaryDouble:
                    return OracleDbType.BinaryDouble;
                case OraDataType.BinaryFloat:
                    return OracleDbType.BinaryFloat;
                case OraDataType.Blob:
                    return OracleDbType.Blob;
                case OraDataType.Boolean:
                    return OracleDbType.Boolean;
                case OraDataType.Byte:
                    return OracleDbType.Byte;
                case OraDataType.Char:
                    return OracleDbType.Char;
                case OraDataType.Clob:
                    return OracleDbType.Clob;
                case OraDataType.Date:
                    return OracleDbType.Date;
                case OraDataType.Decimal:
                    return OracleDbType.Decimal;
                case OraDataType.Double:
                    return OracleDbType.Double;
                case OraDataType.Int16:
                    return OracleDbType.Int16;
                case OraDataType.Int32:
                    return OracleDbType.Int32;
                case OraDataType.Int64:
                    return OracleDbType.Int64;
                case OraDataType.IntervalDS:
                    return OracleDbType.IntervalDS;
                case OraDataType.IntervalYM:
                    return OracleDbType.IntervalYM;
                case OraDataType.Long:
                    return OracleDbType.Long;
                case OraDataType.LongRaw:
                    return OracleDbType.LongRaw;
                case OraDataType.NChar:
                    return OracleDbType.NChar;
                case OraDataType.NClob:
                    return OracleDbType.NClob;
                case OraDataType.NVarchar2:
                    return OracleDbType.NVarchar2;
                case OraDataType.Raw:
                    return OracleDbType.Raw;
                case OraDataType.RefCursor:
                    return OracleDbType.RefCursor;
                case OraDataType.Single:
                    return OracleDbType.Single;
                case OraDataType.TimeStamp:
                    return OracleDbType.TimeStamp;
                case OraDataType.TimeStampLTZ:
                    return OracleDbType.TimeStampLTZ;
                case OraDataType.TimeStampTZ:
                    return OracleDbType.TimeStampTZ;
                case OraDataType.Varchar2:
                    return OracleDbType.Varchar2;
                case OraDataType.XmlType:
                    return OracleDbType.XmlType;
                default:
                    throw new ArgumentException($"Invalid OraDataType:{t}.");
            };
        }
    }
}