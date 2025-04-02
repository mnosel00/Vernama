// See https://aka.ms/new-console-template for more information
using System.Text;


Console.WriteLine("Hello, World!");

var text = "NoselMateusz";

var keyLength = text.Length;

var keyFile = "key.bin";

byte[] bytes;

bytes = BBSKey(keyLength);
File.WriteAllBytes(keyFile, bytes);



byte[] encrypted = Vernam(Encoding.UTF8.GetBytes(text), bytes);
string encryptedText = Convert.ToBase64String(encrypted);

byte[] decrypted = Vernam(encrypted, bytes);
string decryptedText = Encoding.UTF8.GetString(decrypted);

Console.WriteLine("Oryginalny tekst: " + text);
Console.WriteLine("Zaszyfrowany (Base64): " + encryptedText);
Console.WriteLine("Odszyfrowany tekst: " + decryptedText);
Console.WriteLine();
Console.WriteLine(BytesToBinaryString(encrypted));
Console.WriteLine();
Console.WriteLine(BytesToBinaryString(bytes));
Console.WriteLine();
Console.WriteLine(BytesToBinaryString(decrypted));



static string BytesToBinaryString(byte[] data)
{
    StringBuilder sb = new StringBuilder();
    foreach (byte b in data)
    {
        sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
        sb.Append(' '); 
    }
    return sb.ToString().Trim();
}


static byte[] Vernam(byte[] text, byte[] bytes)
{
    byte[] result = new byte[text.Length];
    for (int i = 0; i < text.Length; i++)
    {
        result[i] = (byte)(text[i] ^ bytes[i]);
    }
    return result;
}

static byte[] BBSKey(int length)
{
    var p = 11;
    var q = 19;
    var n = p * q;
    var seed = 3;

    var result = new byte[length];
    for (int i = 0; i < length; i++)
    {
        seed = (seed * seed) % n;
        result[i] = (byte)(seed % 256);
    }

    if (result.Length > length)
    {
        Array.Resize(ref result, length);
    }

    return result;
}
