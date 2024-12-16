using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Web.UI.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;
using SeriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType;
using ChartArea = System.Windows.Forms.DataVisualization.Charting.ChartArea;
using System.Text.RegularExpressions;
using Antlr.Runtime;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            InitializeChart();

            cmbConversion.Items.Add("Prefix");
            cmbConversion.Items.Add("Postfix");
            cmbConversion.SelectedIndex = 0;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            string infixExpression = txtExpression.Text.Trim();

            if (string.IsNullOrWhiteSpace(infixExpression))
            {
                MessageBox.Show("Please enter a valid expression.");
                return;
            }

            string selectedConversion = cmbConversion.SelectedItem.ToString();
            string convertedExpression = string.Empty;
            double result = 0;

            try
            {
                if (selectedConversion == "Prefix")
                {
                    convertedExpression = InfixToPrefix(infixExpression);
                    result = EvaluatePrefix(convertedExpression);
                }
                else if (selectedConversion == "Postfix")
                {
                    convertedExpression = InfixToPostfix(infixExpression);
                    result = EvaluatePostfix(convertedExpression);
                }

                lblResult.Text = $"Converted Expression: {convertedExpression}";
                lblCalculationResult.Text = $"Result: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in conversion or evaluation: " + ex.Message);
            }
        }

        private int Precedence(char op)
        {
            switch (op)
            {
                case '+':
                case '-': return 1;
                case '*':
                case '/': return 2;
                case '^': return 3;
            }
            return 0;
        }

        private bool IsOperator(char c) => c == '+' || c == '-' || c == '*' || c == '/' || c == '^';

        private string InfixToPostfix(string infix)
        {
            StringBuilder postfix = new StringBuilder();
            Stack<char> stack = new Stack<char>();
            StringBuilder numberBuilder = new StringBuilder();

            for (int i = 0; i < infix.Length; i++)
            {
                char c = infix[i];

                if (char.IsWhiteSpace(c)) continue;

                if (char.IsDigit(c) || (c == '-' && (i == 0 || IsOperator(infix[i - 1]) || infix[i - 1] == '(')))
                {
                    numberBuilder.Append(c); 
                }
                else
                {
                    if (numberBuilder.Length > 0)
                    {
                        postfix.Append(numberBuilder.ToString() + " ");
                        numberBuilder.Clear();
                    }

                    if (c == '(')
                    {
                        stack.Push(c);
                    }
                    else if (c == ')')
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                            postfix.Append(stack.Pop() + " ");
                        stack.Pop();
                    }
                    else if (IsOperator(c))
                    {
                        while (stack.Count > 0 && Precedence(stack.Peek()) >= Precedence(c))
                            postfix.Append(stack.Pop() + " ");
                        stack.Push(c);
                    }
                }
            }

            if (numberBuilder.Length > 0)
            {
                postfix.Append(numberBuilder.ToString() + " ");
            }

            while (stack.Count > 0)
                postfix.Append(stack.Pop() + " ");

            return postfix.ToString().Trim();
        }

        private string InfixToPrefix(string infix)
        {
            char[] infixArray = infix.ToCharArray();
            Array.Reverse(infixArray);
            string reversedInfix = new string(infixArray);

            StringBuilder modifiedInfix = new StringBuilder();

            foreach (char c in reversedInfix)
            {
                if (c == '(') modifiedInfix.Append(')');
                else if (c == ')') modifiedInfix.Append('(');
                else modifiedInfix.Append(c);
            }

            string postfix = InfixToPostfix(modifiedInfix.ToString());

            char[] postfixArray = postfix.ToCharArray();
            Array.Reverse(postfixArray);

            return new string(postfixArray);
        }

        private double EvaluatePostfix(string postfix)
        {
            Stack<double> stack = new Stack<double>();
            string[] tokens = postfix.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else if (IsOperator(token[0]))
                {
                    double right = stack.Pop();
                    double left = stack.Pop();
                    switch (token[0])
                    {
                        case '+': stack.Push(left + right); break;
                        case '-': stack.Push(left - right); break;
                        case '*': stack.Push(left * right); break;
                        case '/':
                            if (right == 0) throw new InvalidOperationException("Division by zero.");
                            stack.Push(left / right); break;
                        case '^': stack.Push(Math.Pow(left, right)); break;
                    }
                }
            }

            return stack.Pop();
        }

        private double EvaluatePrefix(string prefix)
        {
            Stack<double> stack = new Stack<double>();
            string[] tokens = prefix.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];

                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else if (IsOperator(token[0])) 
                {
                    if (stack.Count < 2)
                    {
                        throw new InvalidOperationException("Not enough operands for the operator.");
                    }

                    double left = stack.Pop();
                    double right = stack.Pop();

                    switch (token[0])
                    {
                        case '+': stack.Push(left + right); break;
                        case '-': stack.Push(left - right); break;
                        case '*': stack.Push(left * right); break;
                        case '/':
                            if (right == 0) throw new InvalidOperationException("Division by zero.");
                            stack.Push(left / right); break;
                        case '^': stack.Push(Math.Pow(left, right)); break;
                    }
                }
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid expression.");
            }

            return stack.Pop();
        }

        private void InitializeChart()
        {
            chart1.ChartAreas[0].AxisX.Minimum = -100;
            chart1.ChartAreas[0].AxisX.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = -100;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisX.Title = "Y Axis";
            chart1.ChartAreas[0].AxisY.Title = "X Axis";

            chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;

            DrawAxes();
        }

        private void DrawAxes()
        {
            var xAxisSeries = new System.Windows.Forms.DataVisualization.Charting.Series("XAxis")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                Color = System.Drawing.Color.Purple,
                BorderWidth = 2
            };

            var yAxisSeries = new System.Windows.Forms.DataVisualization.Charting.Series("YAxis")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                Color = System.Drawing.Color.Navy,
                BorderWidth = 2
            };

            xAxisSeries.Points.AddXY(-100, 0);
            xAxisSeries.Points.AddXY(100, 0);

            yAxisSeries.Points.AddXY(0, -100);
            yAxisSeries.Points.AddXY(0, 100);

            chart1.Series.Add(xAxisSeries);
            chart1.Series.Add(yAxisSeries);
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear(); 
            DrawAxes();


            var series = new System.Windows.Forms.DataVisualization.Charting.Series("Function")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                Color = System.Drawing.Color.Crimson,
                BorderWidth = 2
            };
            chart1.Series.Add(series);

            double minX = double.Parse(txtMinX.Text);
            double maxX = double.Parse(txtMaxX.Text);

            double step = 0.1; 

            for (double x = minX; x <= maxX; x += step)
            {
                double y = EvaluateFunction(ConvertToProductForm(txtExpression2.Text), x);
                series.Points.AddXY(x, y);
            }
        }


        private string ConvertToProductForm(string expression)
        {
         
            string pattern = @"x\^(\d+)";

            if (Regex.IsMatch(expression, pattern))
            {
                string result = Regex.Replace(expression, pattern, match =>
                {
                    int n = int.Parse(match.Groups[1].Value); 
                    return string.Join("*", Enumerable.Repeat("x", n)); 
                });

                return result;
            }

            return expression;

        }

        private double EvaluateFunction(string function, double x)
        {
            function = function.Replace("x", x.ToString());
            DataTable table = new DataTable();
            var result = table.Compute(function, string.Empty);
            return Convert.ToDouble(result);
        }
    }
}













