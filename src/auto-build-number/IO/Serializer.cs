using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Wckdrzr.AutomaticVersionUpdate.IO
{
    public class Serializer
    {
        public T Deserialize<T>(string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using StringReader sr = new StringReader(input);
            return (T)ser.Deserialize(sr);
        }

        public string Serialize<T>(T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(objectToSerialize.GetType());

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using var stream = new StringWriter();
            using var writer = XmlWriter.Create(stream, settings);
            xmlSerializer.Serialize(writer, objectToSerialize, ns);
            return stream.ToString();
        }
    }
} 