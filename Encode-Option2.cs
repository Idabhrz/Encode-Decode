using System;
using System.IO;

public interface ICoder
{
    string Encode(string message, int shift);
}

public class MessageCoder : ICoder
{
    private static readonly char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

    public string Encode(string message, int shift)
    {
        if (message == null) return string.Empty;

        char[] encodemessage = new char[message.Length];
        for (int i = 0; i < message.Length; i++)
        {
            char c = message[i];
            if (char.IsLetter(c))
            {
                char X = char.IsUpper(c) ? 'A' : 'a';
                encodemessage[i] = (char)(((c - X + shift + 52) % 26) + X);
            }
            else
            {
                encodemessage[i] = c;
            }
        }
        return new string(encodemessage);
    }
    //The Formula
    public int NameToNumber(string sender, string receiver)
    {
        int total1 = 0;
        int total2 = 0;
        foreach (char c in sender)
        {
            total1 += Array.IndexOf(Alphabet, c);
        }
        foreach (char c in receiver)
        {
            total2 += Array.IndexOf(Alphabet, c);
        }
        return (total1 * total2)/ (total1 + total2) % Alphabet.Length;
    }
}

class EncodingMethod
{
    public static readonly string Sender = "Ida";
    public static readonly string Receiver = "SpongBob";

    static void Main(string[] args)
    {
        int shift = CalculateShift(Sender, Receiver);

        Console.WriteLine("Enter your secret message (It will be safe here, don't worry):");
        string message = Console.ReadLine();

        ICoder coder = new MessageCoder();
        string encodedMessage = coder.Encode(message, shift);

        SaveToFile("EncodedMessage.txt", encodedMessage);
        SaveToFile("EncodedMessage.ini", encodedMessage);

        Console.WriteLine("Your message was saved. Go find it!");
    }

    private static int CalculateShift(string sender, string receiver)
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        int senderValue = NameToNumber(sender, alphabet);
        int receiverValue = NameToNumber(receiver, alphabet);
        return (senderValue * receiverValue) / (senderValue + receiverValue) % alphabet.Length;
    }

    private static int NameToNumber(string name, char[] alphabet)
    {
        int total = 0;
        foreach (char c in name)
        {
            int index = Array.IndexOf(alphabet, c);
            if (index != -1)
            {
                total += index;
            }
        }
        return total;
    }
    //Save
    private static void SaveToFile(string filename, string message)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(message);
        }
    }
}
