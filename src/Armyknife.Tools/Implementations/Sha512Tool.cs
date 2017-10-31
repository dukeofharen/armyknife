using Armyknife.Business.Interfaces;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Armyknife.Tools.Implementations
{
    internal class Sha512Tool : ISynchronousTool
    {
        private const string OutputTypeKey = "outputType";
        private const string HmacKey = "hmac";

        public string Name => "sha512";

        public string Description => ToolResources.Sha512Description;

        public string Category => CategoryResources.HashingCategory;

        public string HelpText => ToolResources.Sha512Help;

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
                using (var sha512 = SHA512.Create())
                {
                    hash = sha512.ComputeHash(inputBytes);
                }
            }
            else
            {
                var hmacBytes = Encoding.UTF8.GetBytes(hmac);
                using (var sha512 = new HMACSHA512(hmacBytes))
                {
                    hash = sha512.ComputeHash(inputBytes);
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
                    throw new ArmyknifeException(string.Format(ExceptionResources.Sha512OutputTypeNotSupported, outputType));
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
