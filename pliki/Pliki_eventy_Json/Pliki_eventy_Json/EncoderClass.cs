using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pliki_eventy_Json
{
    internal class EncoderClass
    {
        public EncoderClass()
        {

        }
        public byte[] EncodeByEncoding(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);

            bytes = new byte[Encoding.UTF8.GetByteCount(data)];

            for(var i = 0; i < Encoding.UTF8.GetByteCount(data); i++)
            {
                bytes[i] = Encoding.UTF8.GetBytes(data, i, 1).First();
            }




            return bytes;

        }
        public byte[] EncodeByEncoder(string data)
        {

            Encoder encoder = Encoding.UTF8.GetEncoder();
            byte[] bytArr = new byte[Encoding.UTF8.GetByteCount(data)];
            encoder.GetBytes
            (
               data.ToCharArray(),
               0,
               bytArr.Length,
               bytArr, 
               0,
               true
            
            );

           


            return bytArr;

        }

        public byte[] ConvertChars(string data)
        {
            var t = data.ToCharArray();
            var e = Encoding.UTF8.GetEncoder();
            var b = Encoding.UTF8.GetByteCount(t);
            byte[] converted = new byte[b];

            e.Convert(
                t,
                0,
                t.Count(),
                converted,
                0,
                converted.Count(),
                true,
                out int charsUsed,
                out int bytesUsed,
                out bool completed

             );


            return null;
        }



    }
}
