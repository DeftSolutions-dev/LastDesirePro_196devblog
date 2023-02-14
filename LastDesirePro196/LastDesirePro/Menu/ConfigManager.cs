using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LastDesirePro.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace LastDesirePro.Menu {
  public class ConfigManager {
    public static string temp = Environment.ExpandEnvironmentVariables("%AppData%");
    public static string ConfigPath = temp + "\\Se6Wnmsu1wYD.log";
    public static string ConfigVersion = "1.0.1";
    public static void Init() => LoadConfig(GetConfig());
    public static Dictionary < string, object > Config() {
      Dictionary < string, object > ConfigFields = new Dictionary < string, object > {
        {
          "Version",
          ConfigVersion
        }
      };
      Type[] Types = Assembly.GetExecutingAssembly().GetTypes().Where(T => T.IsClass).ToArray();
      for (int i = 0; i < Types.Length; i++) {
        Type Type = Types[i];
        FieldInfo[] Fields = Type.GetFields().Where(F => F.IsDefined(typeof (SaveAttribute), false)).ToArray();
        for (int o = 0; o < Fields.Length; o++) {
          FieldInfo Field = Fields[o];
          ConfigFields.Add(Type.Name + "." + Field.Name, Field.GetValue(null));
        }
      }
      return ConfigFields;
    }
    public static Dictionary < string, object > GetConfig() {
      if (!File.Exists(ConfigPath))
        SaveConfig(Config());
      Dictionary < string, object > ConfigDict = new Dictionary < string, object > ();
      try {
        ConfigDict = JsonConvert.DeserializeObject < Dictionary < string, object >> (File.ReadAllText(ConfigPath),
          new JsonSerializerSettings {
            Formatting = Formatting.Indented
          });
      } catch {
        ConfigDict = Config();
        SaveConfig(ConfigDict);
      }
      return ConfigDict;
    }
    public static void SaveConfig(Dictionary < string, object > Config) =>
      File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config, Formatting.Indented));
    public static void LoadConfig(Dictionary < string, object > Config) {
      foreach(var AssemblyType in Assembly.GetExecutingAssembly().GetTypes()) {
        foreach(var FInfo in AssemblyType.GetFields()
          .Where(f => Attribute.IsDefined(f, typeof (SaveAttribute)))) {
          string Name = $ "{AssemblyType.Name}.{FInfo.Name}";
          Type FIType = FInfo.FieldType;
          object DefaultInfo = FInfo.GetValue(null);
          if (!Config.ContainsKey(Name))
            Config.Add(Name, DefaultInfo);
          try {
            if (Config[Name].GetType() == typeof (JArray))
              Config[Name] = ((JArray) Config[Name]).ToObject(FInfo.FieldType);
            if (Config[Name].GetType() == typeof (JObject))
              Config[Name] = ((JObject) Config[Name]).ToObject(FInfo.FieldType);
            FInfo.SetValue(null,
              FInfo.FieldType.IsEnum ?
              Enum.ToObject(FInfo.FieldType, Config[Name]) :
              Convert.ChangeType(Config[Name], FInfo.FieldType));
          } catch {
            Config[Name] = DefaultInfo;
          }
        }
      }
      SaveConfig(Config);
    }
  }
}
