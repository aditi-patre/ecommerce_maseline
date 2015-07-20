using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Misc
/// </summary>
public class Misc
{
	public Misc()
	{
		
	}
    public static string GenerateEmailTemplate(string path, Hashtable hashTable)
    {
        StreamReader streamreader = new StreamReader(path);
        string text = "", stream;
        try
        {
            text = "";
            stream = streamreader.ReadLine();
            while (stream != null)
            {
                stream = streamreader.ReadLine();
                IDictionaryEnumerator en = hashTable.GetEnumerator();
                while (en.MoveNext())
                {
                    if (stream.Contains(en.Key.ToString()))
                    {
                        string str = en.Value.ToString();
                        stream = stream.Replace(en.Key.ToString(), en.Value.ToString());
                        hashTable.Remove(en.Key);
                        break;
                    }
                }
                text = text + stream;
            }
            streamreader.Close();
        }
        catch (Exception ex)
        {
            streamreader.Close();
        }
        return text;
    }
}