using Newtonsoft.Json;


namespace AppTest1.Utlities
{
    public static class SerializeExtension
    {
        public static string ObjectSerialize<T>(this T toSerialize)
        {

            var resultSerializeObject = JsonConvert.SerializeObject(toSerialize);

            return resultSerializeObject;
        }

        public static T ObjectDeserialize<T>(this string toDeserialize)
        {
            if (toDeserialize.IsNullOrEmpty())
                throw new Exception("Invalid json format ");

            try
            {
                return JsonConvert.DeserializeObject<T>(toDeserialize);

            }
            catch
            {
                throw new Exception("Invalid Json Format");
            }


        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static T DeepClone<T>(this T sourceObject) where T : new()
        {
            var objData = sourceObject.ObjectSerialize();

            return ObjectDeserialize<T>(objData);
        }
    }
}
