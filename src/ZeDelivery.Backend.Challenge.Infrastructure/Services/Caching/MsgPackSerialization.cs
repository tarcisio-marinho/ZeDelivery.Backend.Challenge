using MsgPack.Serialization;
using System;
using System.IO;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Services.Caching
{
    public static class MsgPackSerialization 
    {
        public static string Serialize<T>(T @object)
        {
            var context = new SerializationContext { SerializationMethod = SerializationMethod.Map };

            var serializer = MessagePackSerializer.Get<T>(context);

            using (var byteStream = new MemoryStream())
            {
                serializer.Pack(byteStream, @object);
                var byteArray = byteStream.ToArray();
                return Convert.ToBase64String(byteArray);
            }
        }

        public static T Deserialize<T>(string packedData)
        {
            var context = new SerializationContext { SerializationMethod = SerializationMethod.Map };

            var serializer = MessagePackSerializer.Get<T>(context);

            var bytes = Convert.FromBase64String(packedData);

            using (var byteStream = new MemoryStream(bytes))
            {
                return serializer.Unpack(byteStream);
            }
        }
    }
}
