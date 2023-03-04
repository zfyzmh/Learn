using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace algorithm
{
    public class 自定义JsonConverter
    {
        [Test]
        public void Test1()
        {

            var model = new Person();
            model.state = 1;
            var json = JsonConvert.SerializeObject(model);//由于ID值为1，得到json为{"ID":ture}
            Console.WriteLine(json);
            var newModel = JsonConvert.DeserializeObject<Person>(json);//序列化得到的newModel对象ID值为1
            Console.WriteLine(newModel);
        }

        public class Person
        {
            [JsonConverter(typeof(IntToStringntConvert))]
            public int state { get; set; }
        }

        public class IntToStringntConvert : JsonConverter
        {
            public IntToStringntConvert()
            {
            }

            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                int vsNum;

                bool isNum;

                Int32.TryParse(reader.Value.ToString(), out vsNum);

                //return isNum;

                return vsNum;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString());
            }
        }
    }
}