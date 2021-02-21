using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSE
{
    public class MetadataReader
    {
        private readonly string _endpoint;

        private MetadataReader(string endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task PrintMetadataSections()
        {
             // TODO: Build SOAP message
            string content = @"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope"" xmlns:a=""http://www.w3.org/2005/08/addressing"">
                <s:Header>
                    <a:Action s:mustUnderstand=""1"">http://schemas.xmlsoap.org/ws/2004/09/transfer/Get</a:Action>
                    <a:MessageID>urn:uuid:1ea1b446-d5a9-4217-b099-c6bb5b8bc7af</a:MessageID>
                    <a:ReplyTo><a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address></a:ReplyTo>
                    <a:To s:mustUnderstand=""1"">http://pauline.informatik.tu-chemnitz.de/WcfAddService/Service1.svc/mex</a:To>
                </s:Header>
                <s:Body/>
            </s:Envelope>";

            // TODO: Parse response   
            Dictionary<string, string> para = new Dictionary<string, string>();
            para["content-type"] = "application/soap+xml; charset=utf-8";
            var result = await HttpRequest.Post(_endpoint, content, para);
            
            // TODO: Print Metadata
            var ns = "http://schemas.xmlsoap.org/ws/2004/09/mex";
            var xmlRes = XElement.Parse(result.Content);
            var sections = xmlRes.Descendants(XNamespace.Get(ns) + "MetadataSection");
            foreach (var section in sections)
            {
                Console.WriteLine(section.ToString());
            }
            Console.WriteLine();
        }  
        
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                MetadataReader client = new MetadataReader(@"http://pauline.informatik.tu-chemnitz.de/WcfAddService/Service1.svc/mex");

                await client.PrintMetadataSections();
                Console.ReadLine();
            }).Wait();
        }
    }
}
