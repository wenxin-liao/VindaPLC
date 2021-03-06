﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PLCDeviceMonitorGUI.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int LogicalStationNum {
            get {
                return ((int)(this["LogicalStationNum"]));
            }
            set {
                this["LogicalStationNum"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("200")]
        public int MoniterInterval {
            get {
                return ((int)(this["MoniterInterval"]));
            }
            set {
                this["MoniterInterval"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("log/PLC-Log")]
        public string LogFilename {
            get {
                return ((string)(this["LogFilename"]));
            }
            set {
                this["LogFilename"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%logger - %date - %level - T[%thread] - [ %message ] %exception%newline")]
        public string LogFormatter {
            get {
                return ((string)(this["LogFormatter"]));
            }
            set {
                this["LogFormatter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("bk/PLC-Data")]
        public string BackupFilename {
            get {
                return ((string)(this["BackupFilename"]));
            }
            set {
                this["BackupFilename"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Records.xml")]
        public string RecordConfigFilename {
            get {
                return ((string)(this["RecordConfigFilename"]));
            }
            set {
                this["RecordConfigFilename"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("STACK-REP^%PalletizerName^%PlateCode^%BoxCode&Amount^N")]
        public string MsgFormatter {
            get {
                return ((string)(this["MsgFormatter"]));
            }
            set {
                this["MsgFormatter"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.101.2" +
            "42)(PORT=1528)))(CONNECT_DATA=(SERVICE_NAME = UAT2)));User Id=cuxwms;Password=cu" +
            "xwms;")]
        public string OracleDB {
            get {
                return ((string)(this["OracleDB"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("PLC设备监控")]
        public string Title {
            get {
                return ((string)(this["Title"]));
            }
            set {
                this["Title"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2000")]
        public int DBRetryInterval {
            get {
                return ((int)(this["DBRetryInterval"]));
            }
            set {
                this["DBRetryInterval"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int DBRetryTimes {
            get {
                return ((int)(this["DBRetryTimes"]));
            }
            set {
                this["DBRetryTimes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ValidatePlateCode {
            get {
                return ((bool)(this["ValidatePlateCode"]));
            }
            set {
                this["ValidatePlateCode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ValidateBoxCode {
            get {
                return ((bool)(this["ValidateBoxCode"]));
            }
            set {
                this["ValidateBoxCode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ValidateRecordNum {
            get {
                return ((bool)(this["ValidateRecordNum"]));
            }
            set {
                this["ValidateRecordNum"] = value;
            }
        }
    }
}
