using System;
using System.Collections;
using System.IO;
using System.Text;

namespace GameOfLifePatternParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: GameOfLifePatternParser /path/to/input aliveChar varName outputValue");
                Console.WriteLine("You gave: " + args.Length);
                for (int i = 0; i < args.Length; i++)
                    Console.WriteLine($"{i}: {args[i]}");
                Environment.Exit(-1);
            }

            IEnumerable lines;

            var list = new ArrayList();
            try
            {
                lines = File.ReadLines(args[0]);

                foreach (string line in lines)
                {
                    list.Add(line);
                }
            } catch (IOException e)
            {
                Console.WriteLine("Error: Unable to read file");
                Console.WriteLine(e);
                Environment.Exit(-1);
            }     
            
            if (list.Count == 0)
            {
                Console.WriteLine("File was empty. Cannot create pattern code");
                Environment.Exit(-1);
            }
            else
            {
                var linelength = ((string)list[0]).Length;

                for (int i = 0; i < list.Count; i++)
                {
                    if (((string)list[i]).Length != linelength)
                    {
                        Console.WriteLine("Error: Unable to process file. File contains lines that are not equal length.");
                        Environment.Exit(-1);
                    }
                }

                var code = GenerateCode(list, args[1], args[2], args[3]);

                var name = $"code_{args[0]}";

                File.WriteAllText(name, code);

                Console.WriteLine($"Output file was generate at: {name}");
                Environment.Exit(0);
            }
        }

        private static string GenerateCode(ArrayList list, string aliveChar, string var, string output)
        {
            var cells = ListToArray(list);
            var code = new StringBuilder();
            code.AppendLine($"height: {cells.GetLength(0)}");
            code.AppendLine($"width: {cells.GetLength(1)}\n");
            code.AppendLine("// Generated with GameOfLifePatternParser");
            code.AppendLine("// See project: https://github.com/beebopbrown/GameOfLifePatternParser");

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if (cells[i, j].ToString() == aliveChar)
                    {
                        code.AppendLine($"{var}[{i}, {j}] = {output};");
                    }
                }
            }

            return code.ToString();
        }

        private static string[,] ListToArray(ArrayList list)
        {
            var cells = new string[list.Count, ((string)list[0]).Length];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < ((string)list[0]).Length; j++)
                {
                    cells[i, j] = ((string)list[i]).ToCharArray()[j].ToString();
                }
            }

            return cells;
        }
    }
}
