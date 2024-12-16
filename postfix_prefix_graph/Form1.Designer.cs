using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq.Expressions;
using System;
using System.Web.UI.DataVisualization.Charting;


namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.cmbConversion = new System.Windows.Forms.ComboBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblCalculationResult = new System.Windows.Forms.Label();
            this.txtExpression2 = new System.Windows.Forms.TextBox();
            this.txtMinX = new System.Windows.Forms.TextBox();
            this.txtMaxX = new System.Windows.Forms.TextBox();
            this.btnPlot = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtExpression
            // 
            this.txtExpression.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpression.Location = new System.Drawing.Point(183, 12);
            this.txtExpression.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(367, 28);
            this.txtExpression.TabIndex = 0;
            // 
            // cmbConversion
            // 
            this.cmbConversion.FormattingEnabled = true;
            this.cmbConversion.Location = new System.Drawing.Point(372, 48);
            this.cmbConversion.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.cmbConversion.Name = "cmbConversion";
            this.cmbConversion.Size = new System.Drawing.Size(178, 26);
            this.cmbConversion.TabIndex = 1;
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.Yellow;
            this.btnConvert.Location = new System.Drawing.Point(222, 83);
            this.btnConvert.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(114, 29);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(12, 127);
            this.lblResult.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 18);
            this.lblResult.TabIndex = 3;
            this.lblResult.Click += new System.EventHandler(this.lblResult_Click);
            // 
            // lblCalculationResult
            // 
            this.lblCalculationResult.AutoSize = true;
            this.lblCalculationResult.Location = new System.Drawing.Point(12, 153);
            this.lblCalculationResult.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCalculationResult.Name = "lblCalculationResult";
            this.lblCalculationResult.Size = new System.Drawing.Size(0, 18);
            this.lblCalculationResult.TabIndex = 4;
            // 
            // txtExpression2
            // 
            this.txtExpression2.Location = new System.Drawing.Point(167, 192);
            this.txtExpression2.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtExpression2.Name = "txtExpression2";
            this.txtExpression2.Size = new System.Drawing.Size(382, 26);
            this.txtExpression2.TabIndex = 5;
            // 
            // txtMinX
            // 
            this.txtMinX.Location = new System.Drawing.Point(188, 225);
            this.txtMinX.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Size = new System.Drawing.Size(148, 26);
            this.txtMinX.TabIndex = 6;
            // 
            // txtMaxX
            // 
            this.txtMaxX.Location = new System.Drawing.Point(402, 225);
            this.txtMaxX.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(148, 26);
            this.txtMaxX.TabIndex = 7;
            // 
            // btnPlot
            // 
            this.btnPlot.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnPlot.ForeColor = System.Drawing.Color.Black;
            this.btnPlot.Location = new System.Drawing.Point(222, 260);
            this.btnPlot.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(114, 32);
            this.btnPlot.TabIndex = 8;
            this.btnPlot.Text = "Draw Graph";
            this.btnPlot.UseVisualStyleBackColor = false;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Gainsboro;
            this.chart1.BorderlineColor = System.Drawing.Color.Gainsboro;
            this.chart1.BorderlineWidth = 3;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.LabelStyle.TruncatedLabels = true;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Brown;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.PeachPuff;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.LawnGreen;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Blue;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Magenta;
            chartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.SkyBlue;
            chartArea1.AxisX.Title = "x";
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Navy;
            chartArea1.AxisX.ToolTip = "x";
            chartArea1.AxisX2.LineColor = System.Drawing.Color.DarkMagenta;
            chartArea1.AxisX2.TitleForeColor = System.Drawing.Color.IndianRed;
            chartArea1.AxisY.LineColor = System.Drawing.Color.MediumOrchid;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.Title = "f(x)";
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Purple;
            chartArea1.AxisY.ToolTip = "y";
            chartArea1.AxisY2.LineColor = System.Drawing.Color.Green;
            chartArea1.AxisY2.TitleForeColor = System.Drawing.Color.LightCoral;
            chartArea1.BackColor = System.Drawing.Color.Silver;
            chartArea1.BorderColor = System.Drawing.Color.DarkCyan;
            chartArea1.CursorX.AutoScroll = false;
            chartArea1.CursorX.LineColor = System.Drawing.Color.Navy;
            chartArea1.CursorX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.CursorY.LineColor = System.Drawing.Color.Purple;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(20, 302);
            this.chart1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            this.chart1.Size = new System.Drawing.Size(530, 424);
            this.chart1.TabIndex = 9;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "Enter the expression : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(341, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "In what form should the expression be written?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "Enter the function :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "Enter the range:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "from:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(367, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "to:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 739);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.btnPlot);
            this.Controls.Add(this.txtMaxX);
            this.Controls.Add(this.txtMinX);
            this.Controls.Add(this.txtExpression2);
            this.Controls.Add(this.lblCalculationResult);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.cmbConversion);
            this.Controls.Add(this.txtExpression);
            this.Font = new System.Drawing.Font("Century", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Infix to Prefix/Postfix Converter";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.ComboBox cmbConversion;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblCalculationResult;
        private System.Windows.Forms.TextBox txtExpression2;
        private System.Windows.Forms.TextBox txtMinX;
        private System.Windows.Forms.TextBox txtMaxX;
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
