using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GaussJordanHelper
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("1: Pivot\n2: ZJ\nEnter: ");

            var k = Console.ReadLine();

            Clipboard.SetText(k == "1" ? Pivot() : CalcularZJ());
        }

        static string CalcularZJ()
        {
            var matrix = new List<List<double>>();
            var answer = new StringBuilder();

            foreach(var row in Clipboard.GetText().Split('\n'))
            {
                var temp = new List<double>();
                var arr = row.Split('\t');
                for (int i = 0; i < arr.Length; i++)
                {
                    if(i!=1) temp.Add(double.Parse(arr[i]));
                }

                matrix.Add(temp);
            }

            for (int i = 1; i < matrix[0].Count; i++)
            {
                double n = 0;
                for (int j = 0; j < matrix.Count; j++)
                {
                    n += matrix[j][i] * matrix[j][0];
                }
                answer.Append(n.ToString() + '\t');
            }

            return answer.ToString();
        }

        static string Pivot()
        {
            var matrix = new List<List<double>>();
            var answer = new StringBuilder();
            int pCol, pRow;

            foreach (var r in Clipboard.GetText().Split('\n'))
            {
                var temp = new List<double>();
                foreach (var k in r.Split('\t'))
                {
                    temp.Add(double.Parse(k));
                }
                matrix.Add(temp);
            }

            Console.WriteLine("Enter pivot position: ");
            var s = Console.ReadLine().Split(' ');
            pRow = int.Parse(s[0]); //Row of pivot
            pCol = int.Parse(s[1]); //Column of pivot

            var pivot = matrix.ElementAt(pRow).ElementAt(pCol); //Obtains pivot number            

            //Iterate through rest of matrix
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (i != pRow && j != pCol)
                    {
                        matrix[i][j] = ((pivot * matrix[i][j]) - (matrix[pRow][j] * matrix[i][pCol])) / pivot;
                    }
                }
            }

            //Iterates through row of pivot and divides by pivot
            var pivotRow = matrix.ElementAt(pRow);
            for (int i = 0; i < pivotRow.Count; i++)
            {
                pivotRow[i] /= pivot;
            }

            //Column of pivot (except row of pivot) is set to 0
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (j == pCol && i != pRow)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }

            foreach (var row in matrix)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    answer.Append(row[i].ToString());
                    answer.Append(i == row.Count - 1 ? "\n" : "\t");
                }
            }

            return answer.ToString();
        }
    }
}
