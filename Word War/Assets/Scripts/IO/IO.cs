using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class IO
{
    public static List<String> ReadText(string name)
    {
        List<string> words = new List<string>();

        StreamReader reader;
        reader = new StreamReader(Application.persistentDataPath + "/Words/" + name, Encoding.GetEncoding("iso-8859-1"));

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            words.Add(line);
        }

        reader.Close();
        return words;
    }

    public static List<String> ReadText(string name, int de)
    {
        List<string> words = new List<string>();

        StreamReader reader;
        reader = new StreamReader(Application.persistentDataPath + name, Encoding.GetEncoding("iso-8859-1"));

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            words.Add(line);
        }

        reader.Close();
        return words;
    }

    public static List<String> ReadText(string name, bool onlyImpair)
    {
        List<string> words = new List<string>();

        StreamReader reader;
        reader = new StreamReader(Application.persistentDataPath + "/Words/" + name, Encoding.GetEncoding("iso-8859-1"));

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();

            if (onlyImpair == false)
            {
                if (line.Length % 2 == 0)
                {
                    words.Add(line);
                }
            }
            else
            {
                if (line.Length % 2 != 0)
                {
                    words.Add(line);
                }
            }

        }

        reader.Close();
        return words;
    }

    public static List<String> ReadText(string name, int minLength, int maxLength, bool onlyImpair)
    {
        List<string> words = new List<string>();

        StreamReader reader;
        reader = new StreamReader(Application.persistentDataPath + "/Words/" + name, Encoding.Default);


        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line.Length >= minLength && line.Length <= maxLength)
            {
                if (onlyImpair == false)
                {
                    if (line.Length % 2 == 0)
                    {
                        words.Add(line);
                    }
                }
                else
                {
                    if (line.Length % 2 != 0)
                    {
                        words.Add(line);
                    }
                }
            }
        }

        reader.Close();
        return words;
    }

    public static void WriteBinary(List<string> words, string name)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream;

        stream = new FileStream(Application.persistentDataPath + "/Words/" + name, FileMode.Create, FileAccess.ReadWrite);

        WordsInFile listOfWords = new WordsInFile(words);

        bf.Serialize(stream, listOfWords);

        stream.Close();
    }

    public static void WriteBinary(List<string> words, string name, int de = 0)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream;

        stream = new FileStream(Application.persistentDataPath +"/"+ name, FileMode.Create);

        WordsInFile listOfWords = new WordsInFile(words);

        bf.Serialize(stream, listOfWords);

        stream.Close();
    }

    public static WordsInFile ReadBinary(string name)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream;
        stream = new FileStream(Application.persistentDataPath + "/Words/" + name, FileMode.Create);


        WordsInFile listOfWords = bf.Deserialize(stream) as WordsInFile;

        stream.Close();
        return listOfWords;
    }

    public static WordsInFile ReadBinary(string name, int de)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream;

        stream = new FileStream(Application.persistentDataPath + "/" + name, FileMode.Open, FileAccess.ReadWrite);

        WordsInFile listOfWords = bf.Deserialize(stream) as WordsInFile;

        stream.Close();
        return listOfWords;
    }

    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/" + path))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + path);
        }
    }
}

[Serializable]
public class WordsInFile
{
    public List<string> words;

    public WordsInFile(List<string> words)
    {
        this.words = words;
    }
}
