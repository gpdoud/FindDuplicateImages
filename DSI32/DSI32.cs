using System;
using System.Collections.Generic;
using System.Text;

namespace DoudSystems {
    
    public class DSI32 {

        public static string GenerateSignature(string filepath) {
            var fn = filepath;
            var f = new System.IO.FileInfo(fn);
            var fs = f.OpenRead();
            var sig = new byte[] {
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
            };
            var buffer = new byte[sig.Length];
            var span = new Span<byte>(buffer);
            var cnt = 0;
            while((cnt = fs.Read(span)) != 0) {
                for(var idx = 0; idx < cnt; idx++) {
                    sig[idx] ^= span[idx];
                }
            }
            fs.Close();
            var sb = new StringBuilder();
            for(var idx = 0; idx < sig.Length; idx++) {
                sb.Append(sig[idx].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
