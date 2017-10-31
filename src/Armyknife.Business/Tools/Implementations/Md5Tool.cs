using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;

namespace Armyknife.Business.Tools.Implementations
{
    internal class Md5Tool : ISynchronousTool
    {
        private const string OutputTypeKey = "outputType";
        private const string HmacKey = "hmac";

        public string Name => "md5";

        public string Description => ToolResources.Md5Description;

        public string Category => CategoryResources.HashingCategory;

        public string HelpText => ToolResources.Md5Help;

        public string Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                throw new ArmyknifeException(ExceptionResources.NoInput);
            }

            string input = args[Constants.InputKey];
            var inputBytes = Encoding.UTF8.GetBytes(input);
            string hmac = null;
            if (args.ContainsKey(HmacKey))
            {
                hmac = args[HmacKey];
            }

            byte[] hash;
            if (string.IsNullOrWhiteSpace(hmac))
            {
                using (var md5 = MD5.Create())
                {
                    hash = md5.ComputeHash(inputBytes);
                }
            }
            else
            {
                var hmacBytes = Encoding.UTF8.GetBytes(hmac);
                using (var md5 = new HMACMD5(hmacBytes))
                {
                    hash = md5.ComputeHash(inputBytes);
                }
            }

            
            string result;
            string outputType = GetOutputType(args);
            switch (outputType)
            {
                case "hex":
                    result = BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
                    break;
                case "base64":
                    result = Convert.ToBase64String(hash);
                    break;
                default:
                    throw new ArmyknifeException(string.Format(ExceptionResources.Md5OutputTypeNotSupported, outputType));
            }

            return result;
        }

        private string GetOutputType(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(OutputTypeKey))
            {
                return "hex";
            }

            return args[OutputTypeKey];
        }
    }
}
