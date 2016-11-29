using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
       
      static void ReadSudo(int[,] Sudo)
        {
            StreamReader f = new StreamReader("Input.txt");

            for (int i = 0; i < 9; i++)
            {
                string s = f.ReadLine();
                for (int j = 0; j < 9; j++)
                {
                    if (s == null) { s = f.ReadLine(); }
                    //Sudo[i, j] = Convert.ToInt32(s[j]);
                    int.TryParse(s[j].ToString(), out Sudo[i, j]);
                }

            }
        }
       static void WriteSudo(int[,] Sudo)
        {
                StreamWriter g = new StreamWriter("Output.txt");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    g.Write(Sudo[i,j]);
                    if ((j+1) % 3 == 0) {
                        g.Write(' ');
                    }
                }
                g.Write(g.NewLine);
                if ((i + 1) % 3 == 0)
                {
                    g.Write(g.NewLine);
                }

            }
            g.Close();
        }
      static bool CheckForSudo(int[,] Sudo)
       {
           bool[] cifr= new bool[9];
           //строки
           for (int i = 0; i < 9; i++)
           {
               cifr = new bool[9];
               for (int j = 0; j < 9; j++)
               { if ((Sudo[i,j]-1)>=0)
                   {
                   if (cifr[Sudo[i, j]-1]) { return false; }
                   else { cifr[Sudo[i, j]-1] = true; }
                   }
               }
           }
           //столбцы
           for (int j = 0; j < 9; j++)
           {
               cifr = new bool[9];
               for (int i = 0; i < 9; i++)
               {
                   if ((Sudo[i, j] - 1) >= 0)
                   {
                       if (cifr[Sudo[i, j] - 1]) { return false; }
                       else { cifr[Sudo[i, j] - 1] = true; }
                   }
               }
           }
      /*   //диагональ главная
           cifr = new bool[9];
           for (int i = 0; i < 9; i++)
           {
               if ((Sudo[i, i] - 1) >= 0)
               {
                   if (cifr[Sudo[i, i] - 1]) { return false; }
                   else { cifr[Sudo[i, i] - 1] = true; }
               }
           }
           //диагональ побочная
           cifr = new bool[9];
           for (int i = 0; i < 9; i++)
           {
               if ((Sudo[i, 9-i-1] - 1) >= 0)
               {
                   if (cifr[Sudo[i, 9 - i - 1] - 1]) { return false; }
                   else { cifr[Sudo[i, 9 - i - 1] - 1] = true; }
               }
           }*/
           //квадраты
           for (int i = 0; i < 3;i++ )
               for (int j = 0; j < 3; j++)
               {
                   if (!CheckForSquare(Sudo, i * 3, j * 3)) { return false; } 
               }


               return true;
       }
      static bool CheckForSquare(int[,] Sudo, int numi, int numj)
       {
           bool[] cifr = new bool[9];
           for (int i = 0; i < 3; i++)
           {
               if ((Sudo[numi,numj + i] - 1) >= 0)
               {
                   if (cifr[Sudo[numi, numj + i] - 1]) { return false; }
                   else { cifr[Sudo[numi, numj + i] - 1] = true; }
               }
               if ((Sudo[numi+1, numj + i] - 1) >= 0)
               {
                   if (cifr[Sudo[numi+1, numj + i] - 1]) { return false; }
                   else { cifr[Sudo[numi+1, numj + i] - 1] = true; }
               }
               if ((Sudo[numi+2, numj + i] - 1) >= 0)
               {
                   if (cifr[Sudo[numi+2, numj + i] - 1]) { return false; }
                   else { cifr[Sudo[numi+2, numj + i] - 1] = true; }
               }

           }
           return true;
       }
      static void Fill(int[,] Beg, int[,] Sudo)
      {int k=0;
      int i = 0;
      int j = 0;
      int dop = 0;
      while (i < 9)
      {
          j = 0;
          while (j < 9)
          {

              if (Beg[i, j] == 0)
              {
                  if (Sudo[i, j] == 0)
                  {
                      Sudo[i, j] = 0;
                  }
                  if (Sudo[i, j] != 9)
                  {
                      while (Sudo[i, j] < 9)
                      {
                          Sudo[i, j]++;
                          if (CheckForSudo(Sudo)) { break; }

                      }
                  }
                  else Sudo[i, j]=0; 

                  if ((!(CheckForSudo(Sudo)))||(Sudo[i, j]==0))
                  {
                      Sudo[i, j] = 0;
                      int kol = (i) * 9 + j;
                      kol--;
                      i = (kol / 9);
                      j = kol % 9;
                      while (kol >= 0)
                      {
                          if (Beg[i, j] == 0) { break; }
                          kol--;
                          i = (kol / 9);
                          j = kol % 9;
                      }
                      if (kol < 0) { throw new Exception("Нет ответа"); }

                  }
                  else {
                      j++; }
              }
              else { j++; }

 
              
          }
              i++;
      }

 
      }
        static void Main(string[] args)
       {
           int[,] Sudo = new int[9, 9];
           ReadSudo(Sudo);
           int[,] Beg = new int[9, 9];
           for (int i = 0; i < 9; i++)
               for (int j = 0; j < 9; j++)
                   Beg[i, j] = Sudo[i, j];
               try
               {
                   Fill(Beg, Sudo);
               }
               catch (SystemException e)
               {
                   Console.WriteLine(e);
               }
           if (CheckForSudo(Sudo))
           {
               WriteSudo(Sudo);
           }

        }
        
       
    }
}
