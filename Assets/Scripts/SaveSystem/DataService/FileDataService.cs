using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Project.SaveSystem
{
    public class BinaryFileDataService : IDataService
    {
        const string FILE_EXT = ".sav";
        const string BAKUP_EXT = ".bak";
        const string TEMP_EXT = ".tmp";
        readonly IBinarySerializer serializer;
        readonly string rootPath;

        public BinaryFileDataService(IBinarySerializer serializer, string rootPath)
        {
            this.serializer = serializer;
            this.rootPath = rootPath;
        }

        private void Combine(ref string path) => path = Path.Combine(rootPath, string.Concat(path, FILE_EXT));

        public void Save<TObject>(string fileName, TObject data)
        {
            SaveAsync(fileName, data).RunSynchronously();
        }

        public async Task<bool> SaveAsync<TObject>(string fileName, TObject data, CancellationToken cancellationToken = default)
        {

            Combine(ref fileName);
            string tempFile = string.Concat(fileName, TEMP_EXT);

            using FileStream stream = new(tempFile, FileMode.Create, FileAccess.Write);

            try
            {
                await serializer.SerializeAsync<TObject>(data, stream);
                //byte[] bytes = serializer.Serialize<TObject>(data);
                //await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
                await stream.FlushAsync();
                stream.Close();
                // replace to original

                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();
                }
                string bakFile = string.Concat(fileName, BAKUP_EXT);
                File.Replace(tempFile, fileName, bakFile);
            }
            catch (OperationCanceledException e)
            {
                // for now, just ignore
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(e.Message);
#endif
                return false;
            }
            return true;
        }

        public TObject Load<TObject>(string fileName)
        {
            return LoadAsync<TObject>(fileName).GetAwaiter().GetResult();
        }

        public async Task<TObject> LoadAsync<TObject>(string fileName, CancellationToken cancellationToken = default)
        {
            Combine(ref fileName);

            try
            {
                // byte[] data = await File.ReadAllBytesAsync(fileName, cancellationToken);
                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                TObject result = await this.serializer.Deserialize<TObject>(stream);
                await stream.DisposeAsync();
                
                return result;
            }
            catch (OperationCanceledException e)
            {
                // for now, just ignore
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(e.Message);
#endif
                return default(TObject);
            }
        }

        // static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        // {
        //     byte[] encrypted;

        //     using (Aes aesAlg = Aes.Create())
        //     {
        //         aesAlg.Key = Key;
        //         aesAlg.IV = IV;

        //         ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //         using (MemoryStream msEncrypt = new MemoryStream())
        //         {
        //             using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //             {
        //                 using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                 {
        //                     swEncrypt.Write(plainText);
        //                 }
        //                 encrypted = msEncrypt.ToArray();
        //             }
        //         }
        //     }

        //     return encrypted;
        // }

        // static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        // {
        //     string plaintext = null;

        //     using (Aes aesAlg = Aes.Create())
        //     {
        //         aesAlg.Key = Key;
        //         aesAlg.IV = IV;

        //         ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        //         using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        //         {
        //             using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //             {
        //                 using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                 {
        //                     plaintext = srDecrypt.ReadToEnd();
        //                 }
        //             }
        //         }
        //     }

        //     return plaintext;
        // }
    }
}