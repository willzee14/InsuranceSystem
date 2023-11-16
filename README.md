# InsuranceSystem

To run the code pls use this console app to encrypt and decrypt request and response

class Program
    {
        static void Main(string[] args)
        {
            var Key = "zMdRgUkXp2s5v8y/B?O(H+MbPeShZxCe";
            var text = "{\r\n  \"NationalIDOfPolicyHolder\":\"367890324134526782988245454996\",\r\n  \"ExpenseId\": 2,\r\n  \"Amount\": 200000,\r\n  \"DateOfExpense\":\"16/11/2023\"\r\n}";
            var resp = "4PmHXBsDCCnN/q0W2sXaoHRYZGXZzK8OpNKVdT4QwImSeONkbxb977N0mQkp9qQu7uDeFPCNEvEJeapzlu8QiKE5hLa93wlX3gtjLm9jnQiNmARO/0fvIzqpQhHWbxWbYE4QBageacX2+5lXCs/9Z18okXohpJt8eDu5WDIc2dHOcx1K2TNGDUaFQ03O2e8DWXBBJdsgIMrqV07wruflebLWBi6lr1PnN0jXDID74HyEM/DXQN7O9M8jpEc5pIaEFBtK88LzXx86LR64u5vpuqjj0yUCyFpxxg3VAWaL9CJqh8h15dfZ5ZA/FnKKcRzhHd+iz/FJreH/m0u5cIlYf/31KGlykP0X5FlocuJUn6m+0aPVm8ByF3MFxby6eWuj39exW3dK4FYsUXUSBWAFozobfo/EES4LRsCMyF1+m/26xJkVOuExZT+cJsEr0ijxfkvqNis6zDoB5UAYg3yisYDRlXXE5ShS241q87T1uubH7dLnm4HRsMXbhR7BiGTkYcEDgHcTCtrRO2mff12PC1MQaVDlwED0UGy8hzV/9TUc45N3haujZfF+JyfGuxS74J4SJ3Gnaco8wgOUl3rY0/lpmgJQvfHj6zo/DyOsZlnCDbn8KEl8l1kgoHLQ1TFUbBRkA9WGuf7ZcbD3ma84ANlooA9G+S7WFCX6W3+gIpW8n0BeZwkWjGc60plcVzDk";


            // Authorisation header
            var authTokenCombo = "InsuranceSystem_API" + ":&" + "InsuranceSystem_API!@";

            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                             .GetBytes(authTokenCombo));

            var Authorization = "Basic" + encoded;

            var encrypt = EncryptString(Key, text);
            var decrypt = DecryptString(Key, resp);
            Console.WriteLine("Hello World!");
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            Console.WriteLine("IV is:", iv);
            byte[] array;
            Console.WriteLine(Convert.ToBase64String(iv));

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                            streamWriter.Flush();
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
	
	Ensure to use postman to test the api
	For Header authentication use the Basic Auth on postman client using the credentials below:
	"Username": "InsuranceSystem_API",
    "Password": "InsuranceSystem_API!@"