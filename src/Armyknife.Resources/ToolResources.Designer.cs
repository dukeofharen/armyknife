﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Armyknife.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ToolResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ToolResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Armyknife.Resources.ToolResources", typeof(ToolResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Decodes a piece of Base64 encoded text..
        /// </summary>
        public static string Base64DecodeDescription {
            get {
                return ResourceManager.GetString("Base64DecodeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- base64decode ZGVjb2RlIHRoaXM=
        ///- base64decode --input ZGVjb2RlIHRoaXM=.
        /// </summary>
        public static string Base64DecodeHelp {
            get {
                return ResourceManager.GetString("Base64DecodeHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encodes a piece of text or a file to a Base64 string..
        /// </summary>
        public static string Base64EncodeDescription {
            get {
                return ResourceManager.GetString("Base64EncodeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- base64encode encode this
        ///- base64encode --input encode this.
        /// </summary>
        public static string Base64EncodeHelp {
            get {
                return ResourceManager.GetString("Base64EncodeHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lets you fill in a file extension and looks up the corresponding information of that file extension..
        /// </summary>
        public static string FilextDescription {
            get {
                return ResourceManager.GetString("FilextDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- filext xlsx.
        /// </summary>
        public static string FilextHelp {
            get {
                return ResourceManager.GetString("FilextHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Extension: {0}
        ///Description: {1}
        ///Used by: {2}.
        /// </summary>
        public static string FilextResult {
            get {
                return ResourceManager.GetString("FilextResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checks a given MIME type and finds all corresponding file extensions..
        /// </summary>
        public static string FromMimeDescription {
            get {
                return ResourceManager.GetString("FromMimeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- frommime text/plain.
        /// </summary>
        public static string FromMimeHelp {
            get {
                return ResourceManager.GetString("FromMimeHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A simple to to HTML decode a given string..
        /// </summary>
        public static string HtmldecodeDescription {
            get {
                return ResourceManager.GetString("HtmldecodeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- htmldecode &amp;lt;html&amp;gt;&amp;lt;/html&amp;gt;.
        /// </summary>
        public static string HtmldecodeHelp {
            get {
                return ResourceManager.GetString("HtmldecodeHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A simple to to HTML encode a given string..
        /// </summary>
        public static string HtmlencodeDescription {
            get {
                return ResourceManager.GetString("HtmlencodeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///You can&apos;t use characters like &apos;&lt;&apos; or &apos;&gt;&apos; as input, so you need to provide the input via piping..
        /// </summary>
        public static string HtmlencodeHelp {
            get {
                return ResourceManager.GetString("HtmlencodeHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A handy tool to prettify a JSON string..
        /// </summary>
        public static string JsonprettifyDescription {
            get {
                return ResourceManager.GetString("JsonprettifyDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- jsonprettify {&quot;key&quot;: &quot;value&quot;}
        ///- jsonprettify --input {&quot;key&quot;: &quot;value&quot;} --character space --tabsize 3
        ///
        ///Optional parameters:
        ///- character: whether the indentation character should be either &quot;tab&quot; or &quot;space&quot; (default space)
        ///- tabsize: the indentation size (default 3).
        /// </summary>
        public static string JsonprettifyHelp {
            get {
                return ResourceManager.GetString("JsonprettifyHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Generates a number of Lorem Ipsum paragraphs..
        /// </summary>
        public static string LipsumDescription {
            get {
                return ResourceManager.GetString("LipsumDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- lipsum
        ///- lipsum --paragraphs 20
        ///
        ///Optional properties:
        ///- paragraphs: the number of Lorem Ipsum paragrahps (5 default)..
        /// </summary>
        public static string LipsumHelp {
            get {
                return ResourceManager.GetString("LipsumHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A handy tool to lengthen URLs from several (not all) URL shortening services..
        /// </summary>
        public static string LongurlDescription {
            get {
                return ResourceManager.GetString("LongurlDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- longurl http://tinyurl.com/123
        ///
        ///Works with (at least):
        ///- bit.ly
        ///- goo.gl
        ///- is.gd
        ///- tinyurl.com
        ///- t.co
        ///- fb.me.
        /// </summary>
        public static string LongurlHelp {
            get {
                return ResourceManager.GetString("LongurlHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encodes a piece of text to an MD5 hash..
        /// </summary>
        public static string Md5Description {
            get {
                return ResourceManager.GetString("Md5Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- md5 hash this
        ///- md5 --input hash thi
        ///- md5 --input hash this --hmac secret-key
        ///
        ///Optional properties:
        ///- hmac: the signature key for the MD5 hash
        ///- outputType: how the string should be generated (should be &apos;hex&apos; or &apos;base64&apos;).
        /// </summary>
        public static string Md5Help {
            get {
                return ResourceManager.GetString("Md5Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encodes a piece of text to a SHA1 hash..
        /// </summary>
        public static string Sha1Description {
            get {
                return ResourceManager.GetString("Sha1Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- sha1 hash this
        ///- sha1 --input hash this
        ///- sha1 --input hash this --hmac secret-key
        ///
        ///Optional properties:
        ///- hmac: the signature key for the MD5 hash
        ///- outputType: how the string should be generated (should be &apos;hex&apos; or &apos;base64&apos;).
        /// </summary>
        public static string Sha1Help {
            get {
                return ResourceManager.GetString("Sha1Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encodes a piece of text to a SHA256 hash..
        /// </summary>
        public static string Sha256Description {
            get {
                return ResourceManager.GetString("Sha256Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- sha256 hash this
        ///- sha256 --input hash this
        ///- sha256 --input hash this --hmac secret-key
        ///
        ///Optional properties:
        ///- hmac: the signature key for the SHA256 hash
        ///- outputType: how the string should be generated (should be &apos;hex&apos; or &apos;base64&apos;).
        /// </summary>
        public static string Sha256Help {
            get {
                return ResourceManager.GetString("Sha256Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encodes a piece of text to a SHA384 hash..
        /// </summary>
        public static string Sha384Description {
            get {
                return ResourceManager.GetString("Sha384Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- sha384 hash this
        ///- sha384 --input hash this
        ///- sha384 --input hash this --hmac secret-key
        ///
        ///Optional properties:
        ///- hmac: the signature key for the SHA384 hash
        ///- outputType: how the string should be generated (should be &apos;hex&apos; or &apos;base64&apos;).
        /// </summary>
        public static string Sha384Help {
            get {
                return ResourceManager.GetString("Sha384Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encodes a piece of text to a SHA512 hash..
        /// </summary>
        public static string Sha512Description {
            get {
                return ResourceManager.GetString("Sha512Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- sha512 hash this
        ///- sha512 --input hash this
        ///- sha512 --input hash this --hmac secret-key
        ///
        ///Optional properties:
        ///- hmac: the signature key for the SHA512 hash
        ///- outputType: how the string should be generated (should be &apos;hex&apos; or &apos;base64&apos;).
        /// </summary>
        public static string Sha512Help {
            get {
                return ResourceManager.GetString("Sha512Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This tool lets you reverse a string..
        /// </summary>
        public static string StringreverseDescription {
            get {
                return ResourceManager.GetString("StringreverseDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- stringreverse Reverse this string my man..
        /// </summary>
        public static string StringreverseHelp {
            get {
                return ResourceManager.GetString("StringreverseHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A handy tool to generate shortened URLs at tinyurl.com..
        /// </summary>
        public static string TinyUrlDescription {
            get {
                return ResourceManager.GetString("TinyUrlDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- tinyurl https://google.com.
        /// </summary>
        public static string TinyUrlHelp {
            get {
                return ResourceManager.GetString("TinyUrlHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Converts a given file extension or file name to the corresponding MIME type..
        /// </summary>
        public static string ToMimeDescription {
            get {
                return ResourceManager.GetString("ToMimeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- tomime txt
        ///- tomime filename.txt.
        /// </summary>
        public static string ToMimeHelp {
            get {
                return ResourceManager.GetString("ToMimeHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A simple tool to URL decode a given string..
        /// </summary>
        public static string UrldecodeDescription {
            get {
                return ResourceManager.GetString("UrldecodeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- urldecode https%3a%2f%2fgoogle.com.
        /// </summary>
        public static string UrldecodeHelp {
            get {
                return ResourceManager.GetString("UrldecodeHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A simple tool to URL encode a given string..
        /// </summary>
        public static string UrlencodeDescription {
            get {
                return ResourceManager.GetString("UrlencodeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- urlencode https://www.google.com.
        /// </summary>
        public static string UrlencodeHelp {
            get {
                return ResourceManager.GetString("UrlencodeHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A handy tool to generate a (or many) UUIDs..
        /// </summary>
        public static string UuidDescription {
            get {
                return ResourceManager.GetString("UuidDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- uuid
        ///- uuid --brackets true --uppercase true --hyphens true --howmany 1
        ///
        ///Optional properties:
        ///- brackets: whether the GUIDs should be surrounded by brackets (true or false, default false)
        ///- uppercase: whether the GUIDs should be uppercase (true or false, default false)
        ///- hyphens: whether the GUID parts should be separated by hyphens (true or false, default true)
        ///- howmany: how many GUIDs should be generated (default 1).
        /// </summary>
        public static string UuidHelp {
            get {
                return ResourceManager.GetString("UuidHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A tool which shows the current week number..
        /// </summary>
        public static string WeeknumberDescription {
            get {
                return ResourceManager.GetString("WeeknumberDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- weeknumber.
        /// </summary>
        public static string WeeknumberHelp {
            get {
                return ResourceManager.GetString("WeeknumberHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A handy tool to generate QR codes..
        /// </summary>
        public static string WriteQrDescription {
            get {
                return ResourceManager.GetString("WriteQrDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:
        ///- writeqr --input this is the QR content --outputFile C:\temp\qr.png --width 250 --height 250
        ///
        ///Optional properties:
        ///- width: the PNG file width in pixels (default 250)
        ///- height: the PNG height in pixels (default 250)
        ///- openFile: whether the file should be opened after saving (true or false, default false).
        /// </summary>
        public static string WriteQrHelp {
            get {
                return ResourceManager.GetString("WriteQrHelp", resourceCulture);
            }
        }
    }
}
