using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ServerConfigTool
{
    class Program
    {
        static int Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Program>();

            Func<string,string,string> valueextractor = (key, input) =>
            {
                return input.Substring(key.Length, input.Length - key.Length);
            };

            Func<string, string, string, string, bool> setvalue =
                (string json, string key, string value, string filepath) =>
                {
                    var jsonobject = JsonSerializer.Deserialize<Dictionary<string, object>>(Encoding.UTF8.GetBytes(json));
                    {
                        if (jsonobject.ContainsKey(key))
                        {
                            jsonobject[key] = value;
                            JsonSerializer.Serialize(jsonobject);
                            using (var stream = File.Create(filepath))
                            {
                                JsonSerializerOptions options = new JsonSerializerOptions();
                                options.WriteIndented = true;
                                stream.Write(JsonSerializer.SerializeToUtf8Bytes(jsonobject, options));
                                stream.Flush();
                                stream.Close();
                                return true;
                            }
                        }
                        logger.LogError("Key "+ key + " not found ");
                        return false;
                    }
                };

            Func<string, string, string, bool> setsqlitepathvalue =
                (string json, string value, string filepath) =>
                {
                    var jsonobject = JsonSerializer.Deserialize<Dictionary<string, object>>(Encoding.UTF8.GetBytes(json));
                    {
                        string key = "Database";
                        string keylevel2 = "Sqlite";
                        string keylevel3 = "FilePath";
                        if (jsonobject.ContainsKey(key))
                        {
                            Dictionary<string, object> level2 =
                                JsonSerializer.Deserialize<Dictionary<string, object>>(
                                    Encoding.UTF8.GetBytes(((JsonElement) jsonobject[key]).GetRawText()));
                            if (level2.ContainsKey(keylevel2))
                            {
                                Dictionary<string, object> level3 =
                                    JsonSerializer.Deserialize<Dictionary<string, object>>(
                                        Encoding.UTF8.GetBytes(((JsonElement)level2[keylevel2]).GetRawText()));
                                if (level3.ContainsKey(keylevel3))
                                {
                                    level3[keylevel3] = value;
                                    level2[keylevel2] = level3;
                                    jsonobject[key] = level2;

                                    using (var stream = File.Create(filepath))
                                    {
                                        JsonSerializerOptions options = new JsonSerializerOptions();
                                        options.WriteIndented = true;
                                        stream.Write(JsonSerializer.SerializeToUtf8Bytes(jsonobject, options));
                                        stream.Flush();
                                        stream.Close();
                                        return true;
                                    }
                                }
                            }
                        }
                        logger.LogError("Key " + key + " not found ");
                        return false;
                    }
                };

            if (args.Length > 1)
            {
                const string filepathkey = "FILEPATH=";
                const string portkey = "URLS=";
                const string certthumbprintkey = "CERTTHUMBPRINT=";
                const string sqlitefilepathkey = "SQLITEFILEPATH=";

                if (args[0].ToUpper().IndexOf(filepathkey) == 0)
                {
                    string file = valueextractor(filepathkey, args[0]);
                    if (File.Exists(file))
                    {
                        string json = File.ReadAllText(file);
                        if (args[1].ToUpper().IndexOf(portkey) == 0)
                        {
                            string value = valueextractor(portkey, args[1]);
                            return setvalue(json, "Urls", value, file) ? 0 : 1;
                        }
                        else
                        {
                            if (args[1].ToUpper().IndexOf(certthumbprintkey) == 0)
                            {
                                string value = valueextractor(certthumbprintkey, args[1]);
                                return setvalue(json, "ServerCertificateFingerPrint", value, file) ? 0 : 1;
                            }
                            else
                            {
                                if (args[1].ToUpper().IndexOf(sqlitefilepathkey) == 0)
                                {
                                    string value = valueextractor(sqlitefilepathkey, args[1]);
                                    return setsqlitepathvalue(json, value, file) ? 0 : 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.LogError("File " + file + " not found");
                    }
                }
            }

            logger.LogError("invalid argument");
            Console.WriteLine("usa ONE of the following:");
            Console.WriteLine("filepath=PathToFile urls=serverurl(Example: https://*:5001)");
            Console.WriteLine("filepath=PathToFile certthumbprint=thumbprintFromServerCert");
            Console.WriteLine(@"filepath=PathToFile sqlitefilepath=pathtosqlitefile(Example:C:\temp\db.sqlite)");
            return 1;
        }
    }
}
