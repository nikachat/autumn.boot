using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public class DbFirstTemplate
    {
        #region Template
        public static string ClassTemplate =   "{using}\r\n" +
                                               "namespace {Namespace}\r\n" +
                                               "{\r\n" +
                                                ClassDescriptionSpace + "{ClassDescription}{DbLiteTable}\r\n" +
                                                ClassSpace+ "public partial class {ClassName}\r\n" +
                                                ClassSpace + "{\r\n" +
                                                PropertySpace + "public {ClassName}(){\r\n\r\n" +
                                                "{Constructor}\r\n" +
                                                PropertySpace + "}\r\n" +
                                                "{PropertyName}\r\n" +
                                                 ClassSpace + "}\r\n" +
                                                "}\r\n";
        public static string ClassDescriptionTemplate =
                                                ClassSpace + "///<summary>\r\n" +
                                                ClassDescriptionSpace + "///{ClassDescription}" +
                                                ClassDescriptionSpace + "///</summary>";

        public static string PropertyTemplate = PropertySpace + "{DbLiteColumn}" +
                                                PropertySpace + "public {PropertyType} {PropertyName} {get;set;}";

        public static string PropertyDescriptionTemplate =
                                                PropertySpace + "///<summary>\r\n" +
                                                PropertySpace + "///{PropertyDescription}\r\n" +
                                                PropertySpace + "///</summary>";

        public static string ConstructorTemplate = PropertySpace + " this.{PropertyName} ={DefaultValue};\r\n";

        public static string UsingTemplate =   "//-----------------------------------------------------------------------------\r\n" +
                                               "//此代码由D4模板自动生成 By Jim\r\n" +
                                               "//生成时间 " + System.DateTime.Now +"\r\n" +
                                               "//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。\r\n" +
                                               "//-----------------------------------------------------------------------------\r\n" +
                                               "using System;\r\n" +
                                               "using System.Linq;\r\n" +
                                               "using System.Text;" + "\r\n";
        #endregion

        #region Replace Key
        public const string KeyUsing = "{using}";
        public const string KeyNamespace = "{Namespace}";
        public const string KeyClassName = "{ClassName}";
        public const string KeyIsNullable = "{IsNullable}";
        public const string KeyDbLiteTable = "{DbLiteTable}";
        public const string KeyConstructor = "{Constructor}";
        public const string KeyDbLiteColumn = "{DbLiteColumn}";
        public const string KeyPropertyType = "{PropertyType}";
        public const string KeyPropertyName = "{PropertyName}";
        public const string KeyDefaultValue = "{DefaultValue}";
        public const string KeyClassDescription = "{ClassDescription}";
        public const string KeyPropertyDescription = "{PropertyDescription}";
        #endregion

        #region Replace Value
        public const string ValueDbLiteTable = "\r\n" + ClassDescriptionSpace + "[DbLiteTable(\"{0}\")]";
        public const string ValueDbLiteCoulmn =   "[DbLiteColumn({0})]";
        //public const string ValueDbLiteTable = ClassSpace + "[DbLiteTable(\"{0}\")]";
        //public const string ValueDbLiteCoulmn = PropertySpace + "[DbLiteColumn({0})]";
        #endregion

        #region Space
        //public const string PropertySpace = "           ";
        //public const string ClassSpace = "    "; 
        public const string PropertySpace = "        ";
        public const string ClassSpace = "";
        public const string ClassDescriptionSpace = "    ";
        #endregion
    }
}
