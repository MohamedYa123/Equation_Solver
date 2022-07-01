using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Equations_solver
{
    class Number
    {
        public double Realpart;
        public double Impart;
        public Number()
        {
            Realpart = 1;
            Impart = 0;
        }
        public static Number operator *(double n1,Number n2)
        {
            Number n = new Number();
            n.Realpart = n2.Realpart * n1;
            n.Impart = n2.Impart * n1;
            return n;
        }
        public static Number operator*(Number n1,Number n2)
        {
            Number n = new Number();
            n.Realpart = n1.Realpart * n2.Realpart - n1.Impart * n2.Impart;
            n.Impart = n1.Realpart * n2.Impart + n1.Impart * n2.Realpart;
            return n;
        }
        public static Number operator^(Number n2,int n1)
        {
            Number n = new Number();
            for(int i = 0; i< n1; i++)
            {
                n = n * n2;
            }
            return n;
        }
        public static Number operator+(Number n1,Number n2)
        {
            Number n = new Number();
            n.Realpart = n1.Realpart + n2.Realpart;
            n.Impart = n1.Impart + n2.Impart;
            return n;
        }
        public static Number operator -(Number n1, Number n2)
        {
            Number n = new Number();
            n.Realpart = n1.Realpart - n2.Realpart;
            n.Impart = n1.Impart - n2.Impart;
            return n;
        }
        public static string operator+(string s,Number n1)
        {
            return s + n1.ToString();
        }
        public static string operator +( Number n1, string s)
        {
            return  n1.ToString()+s;
        }
        public override string ToString()
        {
            return Realpart.ToString() + "+" + Impart + "i";
        }
    }
}
