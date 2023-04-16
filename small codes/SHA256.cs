using System.Security.Cryptography;

Func<string, bool> validate = (id) =>
{
   foreach (var c in id)
      if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '+' || c == '/' || c == '='))
         return false;
   return true;
};
Func<string, string> pad = (id) =>
{
   int mod4 = id.Length % 4;
   if (mod4 != 0)
      id += new string('=', 4 - mod4);
   return id;
};

SHA256 sHA256 = SHA256.Create();
//string elementIdCharSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_$";
string origId = "0G_O1aOmP5c8v8C8CPp_wM";//"0GxO1aOmP5c8v8C8CPp_wM";
//"E1356379-FD38-443F-9841-B3ED1D6DB893";
string base64Id = origId.Replace('_', '/').Replace('$', '=');

base64Id = pad(base64Id);

byte[] IdBytes = Convert.FromBase64String(base64Id);
byte[] guidBytes = new byte[16];
guidBytes[0] = IdBytes[3];
guidBytes[1] = IdBytes[2];
guidBytes[2] = IdBytes[1];
guidBytes[3] = IdBytes[0];
guidBytes[4] = IdBytes[5];
guidBytes[5] = IdBytes[4];
guidBytes[6] = IdBytes[7];
guidBytes[7] = IdBytes[6];
Array.Copy(IdBytes, 8, guidBytes, 8, 8);
//foreach(char c in origId)
//   var k = elementIdCharSet.IndexOf(c);
StringBuilder newIdBuilder = new();
for (int i = 0; i < guidBytes.Length; ++i)
{
   newIdBuilder.Append(String.Format("{0:X2}", guidBytes[i]));
   if (i == 3 | i == 5 | i == 7 | i == 9)
      newIdBuilder.Append('-');
}
Guid guid1 = Guid.Parse(newIdBuilder.ToString());
string guidFormats = "DBNPX";
foreach (char c in guidFormats)
{
   Console.WriteLine("GUID in {0} format is {1}",
      c, guid1.ToString(c.ToString()));
}
sHA256.ComputeHash(guid1.ToByteArray());
for (int i = 0; i < 16; ++i)
   Console.Write("{0:X}", sHA256.Hash[i]);
Console.WriteLine();
sHA256.ComputeHash(guidBytes);
for (int i = 0; i < 16; ++i)
   Console.Write("{0:X}", sHA256.Hash[i]);