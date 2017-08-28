using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Armyknife.Business.Tools.Implementations
{
    internal class Sha256Tool : ITool
    {
        private const string OutputTypeKey = "outputType";
        private const string HmacKey = "hmac";

        public string Name => "sha256";

        public string Description => ToolResources.Sha256Description;

        public string Category => CategoryResources.TextCategory;

        public string HelpText => ToolResources.Sha256Help;

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
                using (var sha256 = SHA256.Create())
                {
                    hash = sha256.ComputeHash(inputBytes);
                }
            }
            else
            {
                var hmacBytes = Encoding.UTF8.GetBytes(hmac);
                using (var sha256 = new HMACSHA256(hmacBytes))
                {
                    hash = sha256.ComputeHash(inputBytes);
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
                    throw new ArmyknifeException(string.Format(ExceptionResources.Sha256OutputTypeNotSupported, outputType));
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
