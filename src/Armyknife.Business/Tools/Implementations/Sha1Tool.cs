using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Armyknife.Business.Tools.Implementations
{
    internal class Sha1Tool : ITool
    {
        private const string OutputTypeKey = "outputType";
        private const string HmacKey = "hmac";

        public string Name => "sha1";

        public string Description => ToolResources.Sha1Description;

        public string Category => CategoryResources.TextCategory;

        public string HelpText => ToolResources.Sha1Description;

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
                using (var sha1 = SHA1.Create())
                {
                    hash = sha1.ComputeHash(inputBytes);
                }
            }
            else
            {
                var hmacBytes = Encoding.UTF8.GetBytes(hmac);
                using (var md5 = new HMACSHA1(hmacBytes))
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
