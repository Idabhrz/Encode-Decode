using System;
using System.IO;

public interface ICoder
{
    string Decode(string message, int shift);
}

public class MessageCoder : ICoder
{
    private static readonly char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

    public string Decode(string message, int shift)
    {
        if (message == null) return string.Empty;

        char[] decodemessage = new char[message.Length];
        for (int i = 0; i < message.Length; i++)
        {
            char c = message[i];
            if (char.IsLetter(c))
            {
                char X = char.IsUpper(c) ? 'A' : 'a';
                decodemessage[i] = (char)(((c - X - shift + 52) % 26) + X);
            }
            else
            {
                decodemessage[i] = c;
            }
        }
        return new string(decodemessage);
    }

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
        return (total1 * total2) / (total1 + total2) % Alphabet.Length;
    }
}

class DecodingMethod
{
    public static readonly string Sender = "Ida";
    public static readonly string Receiver = "SpongBob";

    static void Main(string[] args)
    {
        int shift = CalculateShift(Sender, Receiver);

        Console.WriteLine("Enter the encoded message to decode:");
        string encodedInput = Console.ReadLine();

        ICoder coder = new MessageCoder();
        string decodedMessage = coder.Decode(encodedInput, shift);
        Console.WriteLine("Decoded message: " + decodedMessage);
    }
    //The Formula
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
}