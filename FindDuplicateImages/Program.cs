using System;
using System.Collections.Generic;
using DoudSystems;

namespace FindDuplicateImages {
    class Program {


        void Run() {
            var rootDir = @"C:\Users\gpdou\OneDrive\Pictures\";
            var filesQueue = new Queue<string>();
            FindImageFiles(rootDir, filesQueue);
            // build dictionary of signatures with files
            var dict = BuildDictionary(filesQueue);
            // remove keys where list contains only 1 (no dups)
            foreach(var sig in dict.Keys) {
                if(dict[sig].Count == 1) {
                    continue;
                }
                Log(sig, dict[sig]);
            }

        }

        void Log(string sig, List<string> list) {
            System.Diagnostics.Debug.WriteLine($"Sig: {sig}");
            list.ForEach(f => System.Diagnostics.Debug.WriteLine($"--File: {f}"));
            System.Diagnostics.Debug.WriteLine("------------------");
        }

        Dictionary<string, List<string>> BuildDictionary(Queue<string> filesQueue) {
            var dict = new Dictionary<string, List<string>>();
            while(filesQueue.Count > 0) {
                var file = filesQueue.Dequeue();
                var signature = DSI32.GenerateSignature(file);
                if(!dict.ContainsKey(signature)) {
                    dict.Add(signature, new List<string>() { file });
                    continue;
                }
                // else
                dict[signature].Add(file);
            }
            return dict;
        }
        void FindImageFiles(string directory, Queue<string> filesQueue) {
            var dirs = System.IO.Directory.GetDirectories(directory);
            foreach(var dir in dirs) {
                FindImageFiles(dir, filesQueue);
            }
            foreach(var ext in new string[] { "*.jpg", "*.png" }) {
                var files = System.IO.Directory.GetFiles(directory, ext);
                foreach(var file in files) {
                    filesQueue.Enqueue(file);
                }
            }
        }
        static void Main(string[] args) {
            (new Program()).Run();
        }
    }
}
