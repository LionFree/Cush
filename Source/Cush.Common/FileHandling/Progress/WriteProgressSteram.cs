using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Cush.Common.Progress
{
    [XmlSchemaProvider("MySchema")]
    public class WriteProgressStream : IXmlSerializable
    {
        private const string Ns = "http://demos.Contoso.com/webservices";
        private readonly string _filePath;

        //private int _lastProgress = 0;

        public WriteProgressStream() { }
        public WriteProgressStream(string filePath)
        {
            _filePath = filePath;
        }

        // This is the method named by the XmlSchemaProviderAttribute applied to the type.
        /*
                public static XmlQualifiedName MySchema(XmlSchemaSet xs)
                {
                    // This method is called by the framework to get the schema for this type.
                    // We return an existing schema from disk.

                    //var schemaSerializer = new XmlSerializer(typeof(XmlSchema));


                    //string xsdPath = null;
                    //// NOTE: replace the string with your own path.
                    //xsdPath = 
                        //System.Web.HttpContext.Current.Server.MapPath("SongStream.xsd");
                    //XmlSchema s = (XmlSchema)schemaSerializer.Deserialize(
                    //    new XmlTextReader(xsdPath), null);
                    //xs.XmlResolver = new XmlUrlResolver();
                    //xs.Add(s);

                    return new XmlQualifiedName("songStream", ns);
                }
        */

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            // This is the chunking code.
            // ASP.NET buffering must be turned off for this to work.

            const int bufferSize = 4096;
            var songBytes = new char[bufferSize];
            var inFile = File.Open(_filePath, FileMode.Open, FileAccess.Read);

            var length = inFile.Length;

            // Write the file name.
            writer.WriteElementString("fileName", Ns, Path.GetFileNameWithoutExtension(_filePath));

            // Write the size.
            writer.WriteElementString("size", Ns, length.ToString(CultureInfo.InvariantCulture));

            // Write the song bytes.
            writer.WriteStartElement("song", Ns);

            var sr = new StreamReader(inFile, true);
            var readLen = sr.Read(songBytes, 0, bufferSize);

            while (readLen > 0)
            {
                writer.WriteStartElement("chunk", Ns);
                writer.WriteChars(songBytes, 0, readLen);
                writer.WriteEndElement();

                writer.Flush();
                readLen = sr.Read(songBytes, 0, bufferSize);
            }

            writer.WriteEndElement();
            inFile.Close();

        }


        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            throw new System.NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();

        }



    }
}
