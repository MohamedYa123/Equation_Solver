using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Equations_solver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            p = x0;
            breaklimit = 0.00000001;
        }
        Panel p;
        private void guna2TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( !Char.IsDigit(Convert.ToChar(e.KeyChar.ToString())) && e.KeyChar.ToString()!="."&& e.KeyChar.ToString() != "-")
            {
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            first = true;
            rates1 = new double[Controls.Count, 2];
            rates2 = new double[Controls.Count, 2];
            try
            {
                int i = 0;
                foreach (Control c in Controls)
                {
                    rates1[i, 0] = Convert.ToDouble(c.Width) / Convert.ToDouble(Width);
                    rates1[i, 1] = Convert.ToDouble(c.Height) / Convert.ToDouble(Height);
                    rates2[i, 0] = Convert.ToDouble(c.Top) / Convert.ToDouble(Height);
                    rates2[i, 1] = Convert.ToDouble(c.Left) / Convert.ToDouble(Width);
                    i += 1;
                }
                first = true;
            }
            catch
            {

            }
        }
        double[,] rates1;
        double[,] rates2;
        bool first;
        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(Convert.ToChar(e.KeyChar.ToString())))
            {
                e.Handled = true;
            }
        }
        int degree;
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            main.Controls.Clear();
            main.Controls.Add(p);
            degree= Convert.ToInt32(guna2TextBox1.Text);
            for (int i = 1; i <= degree; i++)
            {
                Guna.UI2.WinForms.Guna2Panel pn = new Guna.UI2.WinForms.Guna2Panel();
                //pn.BackColor = Color.Red;
                
                Guna.UI2.WinForms.Guna2TextBox g = new Guna.UI2.WinForms.Guna2TextBox();
                Label g2 = new Label();
              //  pn.Top = p.Top + p.Height*i;
                pn.Width = p.Width;
                pn.Height = p.Height;
                g2.Top = label1.Top;
                g2.Left = label1.Left;
                g2.Text = "X^" + i;
                g.Width = Q0.Width;
               // g.BackColor = guna2TextBox2.BackColor;
                g.Height = Q0.Height;
                g.Name = "g" + i;
                g.Text = "0";
                g.Top = Q0.Top;
                g.Left = Q0.Left;
                pn.Top = p.Top + p.Height*i;
                pn.Left = p.Left;
                g.BorderRadius = Q0.BorderRadius;
                g.KeyPress += delegate (object o, KeyPressEventArgs e2)
                {
                    guna2TextBox2_KeyPress(o, e2);
                };
                g.Click += delegate(object o,EventArgs e2)
                {
                    g.SelectionStart = 0;
                    g.SelectionLength = ((Guna.UI2.WinForms.Guna2TextBox)o).Text.Length;
                };
                pn.Controls.Add(g);
                pn.Controls.Add(g2);
                main.Controls.Add(pn);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                if (first)
                {
                    foreach (Control c in Controls)
                    {
                        c.Width = Convert.ToInt32(rates1[i, 0] * Convert.ToDouble(Width));
                        c.Height = Convert.ToInt32(rates1[i, 1] * Convert.ToDouble(Height));
                        c.Top = Convert.ToInt32(rates2[i, 0] * Convert.ToDouble(Height));
                        c.Left = Convert.ToInt32(rates2[i, 1] * Convert.ToDouble(Width));
                        i += 1;
                    }
                }
            }
            catch
            {

            }
        }

        private void main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void x0_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var r=Q0.Text;
            // terms.Clear();
            //solutions.Clear();
            terms = new List<double>();
            List<double> terms2 = new List<double>();
            solutions = new List<double>();
            for (int i= 0; i <= degree;i++)
            {
                terms.Add(0);
                terms2.Add(0);
                if (i > 0)
                {
                    solutions.Add(0);
                }
            }
            foreach(Control c in main.Controls)
            {
                if (c.GetType() == main.GetType())
                {
                    Guna.UI2.WinForms.Guna2Panel g = new Guna.UI2.WinForms.Guna2Panel();
                    g = (Guna.UI2.WinForms.Guna2Panel)c;
                    foreach(Control cc in g.Controls)
                    {
                        if (cc.GetType() == guna2TextBox1.GetType())
                        {
                            int y = Convert.ToInt32(cc.Name.Substring(1));
                            Guna.UI2.WinForms.Guna2TextBox t = (Guna.UI2.WinForms.Guna2TextBox)cc; 
                            terms[y] = Convert.ToDouble(t.Text);
                            terms2[y] = Convert.ToDouble(t.Text);
                        }
                    }
                }
            }
            //double ans = solver(1000);

            solutionfiller();
            //var guna2TextBox2 = textBox1;
            guna2TextBox2.Text = "Solutionfor : ";
            int ii = 0;
            foreach(double rr  in terms2)
            {

                guna2TextBox2.Text += rr + "X" + "^" + ii;
                if (ii < terms2.Count - 1)
                {
                    guna2TextBox2.Text += "+";
                }
                ii++;
            }
            guna2TextBox2.Text += "=0\r\n";
            foreach(double rr in solutions)
            {
                if (Math.Abs(rr) > 30000)
                {
                    guna2TextBox2.Text += "X=" + "Nan" + "\r\n";
                }
                else
                {
                    guna2TextBox2.Text += "X=" + Math.Round(rr, 6) + "\r\n";
                }
            }
        }
        List<double> solutions;
        List<double> terms;
        double breaklimit;
        private void solutionfiller()
        {
            for(int i = 0; i < solutions.Count; i++)
            {
                solutions[i] = solver(10000);
                terms = longdivision(solutions[i]);
            }
        }
        
        private List<double> longdivision(double value)
        {
            double[] nterms = new double[terms.Count];
            double[] nterms2 = nterms.Take(nterms.Length - 1).ToArray();
            terms.CopyTo(nterms);
            while (nterms.Length > 1)
            {
                double[] nnt = nterms.Take(nterms.Length-1).ToArray();
               // for (int i = nterms.Length - 2; i >= 0; i--)
                //{
                    int i = nterms.Length - 2;
                    nnt[i] = nterms[nterms.Length - 2] + nterms[nterms.Length - 1]*value;
                nterms2[i] = nterms[nterms.Length - 1];
                nterms = nnt;
                //}
            }
            List<double> g = new List<double>();
            foreach(var s in nterms2)
            {
                g.Add(s);
            }
            return g;
        }
        private double solver(int maxsteps)
        {
            double part1 = 1;
            double part2 = -1;

            double apart1 = calc(part1);
            double apart2 = calc(part2);
            int max2 = 25;
            while (max2 > 0)
            {
                if ((apart1 > 0) != (apart2 > 0))
                {
                    break;
                }
                else
                {
                    part1 *= 2;
                    part2 *= 2;
                    apart1 = calc(part1);
                    apart2 = calc(part2);
                }
                max2 -= 1;
            }
            double avg = 0;
            double aavg = 0;
            while (maxsteps > 0)
            {
                apart1 = calc(part1);
                apart2 = calc(part2);
                if (Math.Abs(apart1 - apart2) <= breaklimit)
                {
                    break;
                }
                avg = (part1 + part2) / 2;
                aavg=calc(avg);
                if ((apart1 > 0) != (aavg > 0))
                {
                    part2 = avg;
                }
                else
                {
                    part1 = avg;
                }
                maxsteps -= 1;
             }
            return avg;
        }

        double calc(double x)
        {
            double ans = 0;
            for (int i = 0; i < terms.Count; i++)
            {
                ans += terms[i] * Math.Pow(x, i);
            }
            return ans;
        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            guna2TextBox1.SelectionStart = 0;
            guna2TextBox1.SelectionLength = guna2TextBox1.Text.Length;
        }

        private void Q0_Click(object sender, EventArgs e)
        {
            Q0.SelectionStart = 0;
            Q0.SelectionLength = Q0.Text.Length;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://www.youtube.com/channel/UCyK_zVDWCutbm8Ji_m3QDWQ");
        }
    }
}
