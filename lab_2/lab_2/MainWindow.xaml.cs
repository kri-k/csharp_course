using System;
using System.Windows;
using System.Windows.Controls;

namespace lab_2
{
    public partial class MainWindow : Window
    {
        bool showError = false;
        System.Data.DataTable dataTable = new System.Data.DataTable();

        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += ButtonClick;
                }
            }
        }

        private double Eval(string expression)
        {
            return Convert.ToDouble(dataTable.Compute(expression, string.Empty));
        }

        private void Clear()
        {
            display.Text = "";
            showError = false;
        }

        private void Backspace()
        {
            if (display.Text.Length > 0)
            {
                display.Text = display.Text.Remove(
                    display.Text.Length - 1);
            }
        }

        private void Calc()
        {
            string result;
            try
            {
                var doubleRes = Eval(display.Text);
                if (double.IsInfinity(doubleRes) || double.IsNaN(doubleRes))
                {
                    throw new Exception();
                }
                result = Math.Round(doubleRes, 3).ToString().Replace(',', '.');
            }
            catch (Exception) 
            {
                showError = true;
                display.Text = "ERROR";
                return;
            }

            history.Text += string.Format(
                "\n{0} = {1}", display.Text, result);
            display.Text = result;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (showError)
            {
                showError = false;
                display.Text = "";
            }

            string s = (string)((Button)e.OriginalSource).Content;

            if (s == "CLEAR")
            {
                Clear();
            }
            else if (s == "backspace")
            {
                Backspace();
            }
            else if (s == "=")
            {
                Calc();
            }
            else
            {
                display.Text += s;
            }
        }
    }
}