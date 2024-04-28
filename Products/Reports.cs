using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Office.Core;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using DataTable = System.Data.DataTable;

namespace Products
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
            LoadOrderData();
            guna2Button5.Hide();
            guna2Button6.Hide();
            guna2Button1.Show();
        }

        private void LoadProductData()
        {
            try
            {
                using (MySqlConnection conn = MySQL_Connection.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT p.ProductID, p.ProductName, o.CategoryName, p.Price 
                                    FROM products p
                                    JOIN categories o ON p.CategoryID = o.CategoryID
                                    ORDER BY p.ProductID";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            // Create a DataTable to hold the data
                            DataTable dataTable1 = new DataTable();

                            // Fill the DataTable with the results from the query
                            adapter.Fill(dataTable1);

                            // Clear existing columns in the DataGridView
                            dataGridView1.Columns.Clear();

                            // Set AutoGenerateColumns to false
                            dataGridView1.AutoGenerateColumns = false;

                            // Add columns to the DataGridView with specified widths

                            dataGridView1.Columns.Add("ProductID", "Product ID");
                            dataGridView1.Columns["ProductID"].DataPropertyName = "ProductID";
                            dataGridView1.Columns["ProductID"].Width = 85;


                            dataGridView1.Columns.Add("ProductName", "Product Name");
                            dataGridView1.Columns["ProductName"].DataPropertyName = "ProductName";
                            dataGridView1.Columns["ProductName"].Width = 220;

                            dataGridView1.Columns.Add("CategoryName", "Category Name");
                            dataGridView1.Columns["CategoryName"].DataPropertyName = "CategoryName";
                            dataGridView1.Columns["CategoryName"].Width = 220;

                            dataGridView1.Columns.Add("Price", "Price");
                            dataGridView1.Columns["Price"].DataPropertyName = "Price";
                            dataGridView1.Columns["Price"].Width = 118;
                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable1;
                        }
                    }

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void LoadOrderData()
        {
            try
            {
                using (MySqlConnection conn = MySQL_Connection.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT oi.OrderItemID, o.OrderDate, o.CustomerName, p.ProductName, p.Price,  oi.Quantity, oi.Total
                                    FROM orderitems oi
                                    JOIN products p ON oi.ProductID = p.ProductID
                                    JOIN orders o ON oi.OrderID = o.OrderID
                                    ORDER BY oi.OrderItemID";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            // Create a DataTable to hold the data
                            DataTable dataTable = new DataTable();

                            // Fill the DataTable with the results from the query
                            adapter.Fill(dataTable);

                            // Clear existing columns in the DataGridView
                            dataGridView1.Columns.Clear();

                            // Set AutoGenerateColumns to false
                            dataGridView1.AutoGenerateColumns = false;

                            // Add columns to the DataGridView with specified widths

                            dataGridView1.Columns.Add("OrderItemID", "ID");
                            dataGridView1.Columns["OrderItemID"].DataPropertyName = "OrderItemID";
                            dataGridView1.Columns["OrderItemID"].Width = 40;

                            dataGridView1.Columns.Add("OrderDate", "Date");
                            dataGridView1.Columns["OrderDate"].DataPropertyName = "OrderDate";
                            dataGridView1.Columns["OrderDate"].Width = 90;

                            dataGridView1.Columns.Add("CustomerName", "Customer Name");
                            dataGridView1.Columns["CustomerName"].DataPropertyName = "CustomerName";
                            dataGridView1.Columns["CustomerName"].Width = 110; // Set the width to 100 pixels

                            dataGridView1.Columns.Add("ProductName", "ProductName");
                            dataGridView1.Columns["ProductName"].DataPropertyName = "ProductName";
                            dataGridView1.Columns["ProductName"].Width = 143;

                            dataGridView1.Columns.Add("Price", "Price");
                            dataGridView1.Columns["Price"].DataPropertyName = "Price";
                            dataGridView1.Columns["Price"].Width = 90;

                            dataGridView1.Columns.Add("Quantity", "Quantity");
                            dataGridView1.Columns["Quantity"].DataPropertyName = "Quantity";
                            dataGridView1.Columns["Quantity"].Width = 80;

                            dataGridView1.Columns.Add("Total", "Total");
                            dataGridView1.Columns["Total"].DataPropertyName = "Total";
                            dataGridView1.Columns["Total"].Width = 90;

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable;
                        }
                    }

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void LoadRevenueData()
        {
            try
            {
                using (MySqlConnection conn = MySQL_Connection.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT oi.OrderItemID, o.OrderDate, p.ProductName, p.Price,  oi.Quantity, oi.Total,
                                    ROUND((p.Price * oi.Quantity * 0.15), 2) AS ProfitPerProduct
                             FROM orderitems oi
                             JOIN products p ON oi.ProductID = p.ProductID
                             JOIN orders o ON oi.OrderID = o.OrderID
                             ORDER BY oi.OrderItemID";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            // Create a DataTable to hold the data
                            DataTable dataTable2 = new DataTable();

                            // Fill the DataTable with the results from the query
                            adapter.Fill(dataTable2);

                            // Clear existing columns in the DataGridView
                            dataGridView1.Columns.Clear();

                            // Set AutoGenerateColumns to false
                            dataGridView1.AutoGenerateColumns = false;

                            // Add columns to the DataGridView with specified widths

                            dataGridView1.Columns.Add("OrderItemID", "ID");
                            dataGridView1.Columns["OrderItemID"].DataPropertyName = "OrderItemID";
                            dataGridView1.Columns["OrderItemID"].Width = 40;

                            dataGridView1.Columns.Add("OrderDate", "Date");
                            dataGridView1.Columns["OrderDate"].DataPropertyName = "OrderDate";
                            dataGridView1.Columns["OrderDate"].Width = 85;

                            /*dataGridView1.Columns.Add("CustomerName", "Customer Name");
                            dataGridView1.Columns["CustomerName"].DataPropertyName = "CustomerName";
                            dataGridView1.Columns["CustomerName"].Width = 110; // Set the width to 100 pixels*/

                            dataGridView1.Columns.Add("ProductName", "ProductName");
                            dataGridView1.Columns["ProductName"].DataPropertyName = "ProductName";
                            dataGridView1.Columns["ProductName"].Width = 150;

                            dataGridView1.Columns.Add("Price", "Price");
                            dataGridView1.Columns["Price"].DataPropertyName = "Price";
                            dataGridView1.Columns["Price"].Width = 90;

                            dataGridView1.Columns.Add("Quantity", "Quantity");
                            dataGridView1.Columns["Quantity"].DataPropertyName = "Quantity";
                            dataGridView1.Columns["Quantity"].Width = 80;

                            dataGridView1.Columns.Add("Revenue", "Revenue");
                            dataGridView1.Columns["Revenue"].DataPropertyName = "Total";
                            dataGridView1.Columns["Revenue"].Width = 99;

                            dataGridView1.Columns.Add("ProfitPerProduct", "Profit Per Product");
                            dataGridView1.Columns["ProfitPerProduct"].DataPropertyName = "ProfitPerProduct";
                            dataGridView1.Columns["ProfitPerProduct"].Width = 99;

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable2;
                        }
                    }

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet chartSheet;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add());
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                // Activate oSheet
                oSheet.Activate();

                // Add donation data to oSheet
                PopulateOrderData(oSheet);

                // Format cells
                Excel.Range range = oSheet.Range["A14:H25"];
                range.Columns.AutoFit();

                // Create a new sheet for the chart
                chartSheet = (Excel._Worksheet)oWB.Worksheets.Add(Type.Missing, oSheet);
                chartSheet.Name = "Chart";

                // Populate chart data in the new sheet
                PopulateChartData(chartSheet);

                // Activate oSheet
                oSheet.Activate();

                // Protect the sheet again
                /*oSheet.Protect();*/

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }




        private void PopulateOrderData(Excel._Worksheet oSheet)
        {
            try
            {
                // Merge cells A1 to H7
                oSheet.get_Range("A1", "G10").Merge();

                // Merge cells A10 to H11
                Excel.Range titleRange = oSheet.get_Range("A11", "G13");
                titleRange.Merge();

                // Set text to "Order Report"
                oSheet.Cells[11, 1] = "Order Items Report";

                // Format the text
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.Font.Color = System.Drawing.Color.FromArgb(0, 120, 215);
                titleRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                
                // Construct the relative path to the image file
                string relativeImagePath = @"..\..\Resources\LOGO_NO_BG.png";
                string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string imagePath = Path.Combine(directory, relativeImagePath);
                var logoTextbox = oSheet.Shapes.AddPicture(imagePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 20, 20, 100, 100);

                var HomeTextbox = oSheet.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 20, 10, 100, 100);
                var OrderTextFrame = HomeTextbox.TextFrame2;
                // Remove the outline of the text box
                HomeTextbox.Line.Visible = MsoTriState.msoFalse;

                // Set text for the "School Supply" text box
                OrderTextFrame.TextRange.Text = "Alvain School Supply";

                // Apply formatting to the "FurEver Home" text box
                OrderTextFrame.TextRange.Font.Bold = MsoTriState.msoTrue; // Use MsoTriState.msoTrue for true
                OrderTextFrame.TextRange.Font.Size = 28;
                var color = System.Drawing.Color.FromArgb(0, 120, 215); //56,182,255
                OrderTextFrame.TextRange.Font.Fill.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(color); // Same color as the address, contact, and email

                // Resize the text box to fit the text content
                HomeTextbox.Width = OrderTextFrame.TextRange.BoundWidth + 120; // Add some padding
                HomeTextbox.Height = 35; // Add some padding

                // Position the "School Supply" text box above the address, contact, and email
                HomeTextbox.Top = 40;
                HomeTextbox.Left = logoTextbox.Left + logoTextbox.Width; // Add some spacing

                // Remove the outline of the text box
                HomeTextbox.Line.Visible = MsoTriState.msoFalse;

                // Add text box for address, contact, and email below the "School Supply" text box
                var headerTextbox = oSheet.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal,
                                                              HomeTextbox.Left, HomeTextbox.Top + HomeTextbox.Height + 0, 120, 215);
                var textFrame = headerTextbox.TextFrame2;

                // Set text for the entire text frame
                textFrame.TextRange.Text = "Address: Legazpi City, Albay\nContact: +63 912 3456 789\nEmail: alvain.2024@gmail.com";

                // Apply formatting to the entire text frame
                textFrame.TextRange.Font.Bold = MsoTriState.msoTrue; // Use MsoTriState.msoTrue for true
                textFrame.TextRange.Font.Size = 12;
                //textFrame.TextRange.Font.Fill.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(color);

                // Resize the text box to fit the text content
                headerTextbox.Width = textFrame.TextRange.BoundWidth + 30; // Add some padding
                headerTextbox.Height = textFrame.TextRange.BoundHeight - 30; // Add some padding

                // Remove the outline of the text box
                headerTextbox.Line.Visible = MsoTriState.msoFalse;

                // Lock the header cells
                oSheet.get_Range("A1", "I10").Locked = true;

                // Add original header
                oSheet.Cells[14, 1] = "OrderItem ID";
                oSheet.Cells[14, 2] = "Order Date";
                oSheet.Cells[14, 3] = "Customer Name";
                oSheet.Cells[14, 4] = "Product Name";
                oSheet.Cells[14, 5] = "Product Price";
                oSheet.Cells[14, 6] = "Quantity";
                oSheet.Cells[14, 7] = "Order Total";

                // Add styles to original header
                Excel.Range headerRange = oSheet.get_Range("A14", "G14");
                headerRange.Font.Bold = true;
                headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                headerRange.Font.Color = System.Drawing.Color.White;
                headerRange.Interior.Color = System.Drawing.Color.FromArgb(56, 182, 255);

                // Unprotect the sheet to allow modifications
                oSheet.Unprotect();

                // Get the data source from the DataGridView
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                double orderTotalSum = 0;

                // Define starting row for data insertion
                int startRow = 15; // Start from row 15 as headers are in rows 14

                // Loop through each row in the DataGridView
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    // Loop through each column in the current row
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        // Write data to Excel cell
                        if (j == 1) // Check if it's the second column (Order Date)
                        {
                            // Format as date
                            oSheet.Cells[startRow + i, j + 1].NumberFormat = "MM/dd/yyyy";
                            oSheet.Cells[startRow + i, j + 1] = Convert.ToDateTime(dataTable.Rows[i][j]);
                        }
                        else if (j == 4 || j == 6) // Check if it's the fifth or seventh column (Product Price or Order Total)
                        {
                            // Format as float with two decimal places
                            oSheet.Cells[startRow + i, j + 1].NumberFormat = "0.00";
                            oSheet.Cells[startRow + i, j + 1] = Convert.ToDouble(dataTable.Rows[i][j]);

                        }
                        else
                        {
                            // Just write the data as it is
                            oSheet.Cells[startRow + i, j + 1] = dataTable.Rows[i][j].ToString();
                        }
                    }
                }

                // Calculate the total value from the last row of the DataGridView
                foreach (DataRow row in dataTable.Rows)
                {
                    orderTotalSum += Convert.ToDouble(row["Total"]);
                }

                // Insert the total value into the correct cell to the right of the data
                oSheet.Cells[startRow + dataTable.Rows.Count + 0, dataTable.Columns.Count - 1].Value = "Total:";
                oSheet.Cells[startRow + dataTable.Rows.Count + 0, dataTable.Columns.Count - 1].Font.Bold = true; // Make "Total:" bold

                oSheet.Cells[startRow + dataTable.Rows.Count + 0, dataTable.Columns.Count + 0].Value = orderTotalSum;
                oSheet.Cells[startRow + dataTable.Rows.Count + 0, dataTable.Columns.Count + 0].Font.Bold = true; // Make the total value bold


                // Merge cells A-G after the DataGridView
                Excel.Range mergeRange = oSheet.Range["A" + (startRow + dataTable.Rows.Count + 1), "G" + (startRow + dataTable.Rows.Count + 1)];
                mergeRange.Merge();



                // Add signature and printed name to the left part
                Excel.Range signatureRange = oSheet.Range["A" + (startRow + dataTable.Rows.Count + 2), "C" + (startRow + dataTable.Rows.Count + 7)];
                signatureRange.Merge();
                signatureRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                signatureRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                signatureRange.Value = (UserSession.Fname + " " + UserSession.Mname + ". " + UserSession.Lname).ToUpper() + "\n––––––––––––––––––––––––––––––––\nSIGNATURE OVER PRINTED NAME";

                // Add exported date and time with AM/PM to the right part
                string exportedDateTime = DateTime.Now.ToString("MMMM dd, yyyy / hh:mm tt"); // "Month 00, 0000  time 00:00 AM/PM"
                Excel.Range dateTimeRange = oSheet.Range["D" + (startRow + dataTable.Rows.Count + 2), "G" + (startRow + dataTable.Rows.Count + 7)];
                dateTimeRange.Merge();
                dateTimeRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                dateTimeRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                dateTimeRange.Value = "Exported Date and Time: " + exportedDateTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }


        private void PopulateChartData(Excel._Worksheet chartSheet)
        {
            // Get age distribution data
            Dictionary<string, int> quantityDistribution = GetProductQuantityDistribution();

            // Populate age distribution data into Sheet2
            int startRow = 1;
            int currentRow = startRow;
            foreach (var kvp in quantityDistribution)
            {
                chartSheet.Cells[currentRow, 1] = kvp.Key; // product name
                chartSheet.Cells[currentRow, 2] = kvp.Value; // count
                currentRow++;
            }

            // Generate pie chart for age distribution on Sheet2
            GeneratePieChart(chartSheet, quantityDistribution);

        }

        private Dictionary<string, int> GetProductQuantityDistribution()
        {
            Dictionary<string, int> productQuantityDistribution = new Dictionary<string, int>();

            using (MySqlConnection con = MySQL_Connection.GetConnection())
            {
                con.Open();
                string quantityQuery = "SELECT p.ProductName, SUM(oi.Quantity) AS TotalQuantity " +
                                       "FROM orderitems oi " +
                                       "JOIN products p ON oi.ProductID = p.ProductID " +
                                       "GROUP BY p.ProductName;";
                using (MySqlCommand quantityCmd = new MySqlCommand(quantityQuery, con))
                {
                    using (MySqlDataReader quantityReader = quantityCmd.ExecuteReader())
                    {
                        if (quantityReader.HasRows)
                        {
                            while (quantityReader.Read())
                            {
                                string productName = quantityReader.GetString(0);
                                int totalQuantity = quantityReader.GetInt32(1);
                                productQuantityDistribution.Add(productName, totalQuantity);
                            }
                        }
                    }
                }
            }

            return productQuantityDistribution;
        }

        private void GeneratePieChart(Excel._Worksheet chartSheet, Dictionary<string, int> quantityDistribution)
        {
            Excel.ChartObjects chartObjects = (Excel.ChartObjects)chartSheet.ChartObjects(System.Type.Missing);
            Excel.ChartObject chartObject = chartObjects.Add(100, 100, 300, 300) as Microsoft.Office.Interop.Excel.ChartObject;

            Excel.Chart chart = chartObject.Chart;

            // Prepare data for the chart
            Excel.Range chartRange = chartSheet.Range["A1", "B" + quantityDistribution.Count] as Microsoft.Office.Interop.Excel.Range;

            chart.SetSourceData(chartRange, Excel.XlRowCol.xlColumns);

            // Set chart type to Pie
            chart.ChartType = Excel.XlChartType.xlPie;

            // Add data labels
            chart.ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowLabel, false, true, true, false, false, true, false, true, true);
        }


        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2Button3.FillColor = Color.FromArgb(0, 120, 215);
            guna2Button3.ForeColor = Color.White;
            guna2Button2.FillColor = Color.White;
            guna2Button2.ForeColor = Color.FromArgb(0, 120, 215);
            guna2Button4.FillColor = Color.White;
            guna2Button4.ForeColor = Color.FromArgb(0, 120, 215);
            label7.Text = "Products Report";
            LoadProductData();
            guna2Button1.Hide();
            guna2Button5.Hide();
            guna2Button5.Show();

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2Button4.FillColor = Color.FromArgb(0, 120, 215);
            guna2Button4.ForeColor = Color.White;
            guna2Button2.FillColor = Color.White;
            guna2Button2.ForeColor = Color.FromArgb(0, 120, 215);
            guna2Button3.FillColor = Color.White;
            guna2Button3.ForeColor = Color.FromArgb(0, 120, 215);
            label7.Text = "Revenue Report";
            LoadRevenueData();
            guna2Button5.Hide();
            guna2Button1.Hide();
            guna2Button6.Show();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2Button2.FillColor = Color.FromArgb(0, 120, 215);
            guna2Button2.ForeColor = Color.White;
            guna2Button4.FillColor = Color.White;
            guna2Button4.ForeColor = Color.FromArgb(0, 120, 215);
            guna2Button3.FillColor = Color.White;
            guna2Button3.ForeColor = Color.FromArgb(0, 120, 215);
            label7.Text = "Orders Report";
            LoadOrderData();
            guna2Button5.Hide();
            guna2Button6.Hide();
            guna2Button1.Show();


        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet chartSheet;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add());
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                // Activate oSheet
                oSheet.Activate();

                // Add donation data to oSheet
                PopulateProductData(oSheet);

                // Format cells
                Excel.Range range = oSheet.Range["A14:H25"];
                range.Columns.AutoFit();

                // Create a new sheet for the chart
                chartSheet = (Excel._Worksheet)oWB.Worksheets.Add(Type.Missing, oSheet);
                chartSheet.Name = "Chart";

                // Populate chart data in the new sheet
                PopulateChartData(chartSheet);

                // Activate oSheet
                oSheet.Activate();

                // Protect the sheet again
                /*oSheet.Protect();*/

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private void PopulateProductData(Excel._Worksheet oSheet)
        {
            try
            {
                // Merge cells A1 to H7
                oSheet.get_Range("A1", "G10").Merge();

                // Merge cells A10 to H11
                Excel.Range titleRange = oSheet.get_Range("A11", "G13");
                titleRange.Merge();

                // Set text to "Order Report"
                oSheet.Cells[11, 1] = "Products Report";

                // Format the text
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.Font.Color = System.Drawing.Color.FromArgb(0, 120, 215);
                titleRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                
                // Construct the relative path to the image file
                string relativeImagePath = @"..\..\Resources\LOGO_NO_BG.png";
                string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string imagePath = Path.Combine(directory, relativeImagePath);
                var logoTextbox = oSheet.Shapes.AddPicture(imagePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 20, 20, 100, 100);

                // Add text box for "School Supply" above the address, contact, and email
                var HomeTextbox = oSheet.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 20, 10, 100, 100);
                var OrderTextFrame = HomeTextbox.TextFrame2;
                // Remove the outline of the text box
                HomeTextbox.Line.Visible = MsoTriState.msoFalse;

                // Set text for the "School Supply" text box
                OrderTextFrame.TextRange.Text = "Alvain School Supply";

                // Apply formatting to the "FurEver Home" text box
                OrderTextFrame.TextRange.Font.Bold = MsoTriState.msoTrue; // Use MsoTriState.msoTrue for true
                OrderTextFrame.TextRange.Font.Size = 28;
                var color = System.Drawing.Color.FromArgb(0, 120, 215); //56,182,255
                OrderTextFrame.TextRange.Font.Fill.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(color); // Same color as the address, contact, and email

                // Resize the text box to fit the text content
                HomeTextbox.Width = OrderTextFrame.TextRange.BoundWidth + 120; // Add some padding
                HomeTextbox.Height = 35; // Add some padding

                // Position the "School Supply" text box above the address, contact, and email
                HomeTextbox.Top = 40;
                HomeTextbox.Left = logoTextbox.Left + logoTextbox.Width; // Add some spacing

                // Remove the outline of the text box
                HomeTextbox.Line.Visible = MsoTriState.msoFalse;

                // Add text box for address, contact, and email below the "School Supply" text box
                var headerTextbox = oSheet.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal,
                                                              HomeTextbox.Left, HomeTextbox.Top + HomeTextbox.Height + 0, 120, 215);
                var textFrame = headerTextbox.TextFrame2;

                // Set text for the entire text frame
                textFrame.TextRange.Text = "Address: Legazpi City, Albay\nContact: +63 912 3456 789\nEmail: alvain.2024@gmail.com";

                // Apply formatting to the entire text frame
                textFrame.TextRange.Font.Bold = MsoTriState.msoTrue; // Use MsoTriState.msoTrue for true
                textFrame.TextRange.Font.Size = 12;
                //textFrame.TextRange.Font.Fill.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(color);

                // Resize the text box to fit the text content
                headerTextbox.Width = textFrame.TextRange.BoundWidth + 30; // Add some padding
                headerTextbox.Height = textFrame.TextRange.BoundHeight - 30; // Add some padding

                // Remove the outline of the text box
                headerTextbox.Line.Visible = MsoTriState.msoFalse;

                // Lock the header cells
                oSheet.get_Range("A1", "I10").Locked = true;

                // Add original header
                oSheet.Cells[14, 1] = "Product ID";
                oSheet.Cells[14, 2] = "";
                oSheet.Cells[14, 3] = "Product Name";
                oSheet.Cells[14, 4] = "";
                oSheet.Cells[14, 5] = "Category Name";
                oSheet.Cells[14, 6] = "";
                oSheet.Cells[14, 7] = "Product Price";

                // Add styles to original header
                Excel.Range headerRange = oSheet.get_Range("A14", "G14");
                headerRange.Font.Bold = true;
                headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                headerRange.Font.Color = System.Drawing.Color.White;
                headerRange.Interior.Color = System.Drawing.Color.FromArgb(56, 182, 255);

                // Unprotect the sheet to allow modifications
                oSheet.Unprotect();

                // Get the data source from the DataGridView
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                

                // Define starting row for data insertion
                int startRow = 15; // Start from row 15 as headers are in rows 14

                // Loop through each row in the DataGridView
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    // Loop through each column in the current row
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        if (j == 0) 
                        {
                            // Just write the data as it is
                            oSheet.Cells[startRow + i, j + 1] = dataTable.Rows[i][j].ToString();
                            

                        }
                        else if (j == 3)
                        {
                            // Format as float with two decimal places
                            oSheet.Cells[startRow + i, j + 4].NumberFormat = "0.00";
                            oSheet.Cells[startRow + i, j + 4] = Convert.ToDecimal(dataTable.Rows[i][j]);

                        }
                        else if (j == 2)
                        {
                            oSheet.Cells[startRow + i, j + 3] = dataTable.Rows[i][j].ToString();

                        }
                        else
                        {
                            // Just write the data as it is
                            oSheet.Cells[startRow + i, j + 2] = dataTable.Rows[i][j].ToString();
                        }
                    }
                }



                // Merge cells A-G after the DataGridView
                Excel.Range mergeRange = oSheet.Range["A" + (startRow + dataTable.Rows.Count + 1), "G" + (startRow + dataTable.Rows.Count + 1)];
                mergeRange.Merge();



                // Add signature and printed name to the left part
                Excel.Range signatureRange = oSheet.Range["A" + (startRow + dataTable.Rows.Count + 2), "C" + (startRow + dataTable.Rows.Count + 7)];
                signatureRange.Merge();
                signatureRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                signatureRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                signatureRange.Value = (UserSession.Fname + " " + UserSession.Mname + ". " + UserSession.Lname).ToUpper() + "\n––––––––––––––––––––––––––––––––\nSIGNATURE OVER PRINTED NAME";

                // Add exported date and time with AM/PM to the right part
                string exportedDateTime = DateTime.Now.ToString("MMMM dd, yyyy / hh:mm tt"); // "Month 00, 0000  time 00:00 AM/PM"
                Excel.Range dateTimeRange = oSheet.Range["D" + (startRow + dataTable.Rows.Count + 2), "G" + (startRow + dataTable.Rows.Count + 7)];
                dateTimeRange.Merge();
                dateTimeRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                dateTimeRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                dateTimeRange.Value = "Exported Date and Time: " + exportedDateTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet chartSheet;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add());
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
               

                // Activate oSheet
                oSheet.Activate();

                // Add donation data to oSheet
                PopulateRevenueData(oSheet);

                // Format cells
                Excel.Range range = oSheet.Range["A14:H25"];
                range.Columns.AutoFit();

                // Create a new sheet for the chart
                chartSheet = (Excel._Worksheet)oWB.Worksheets.Add(Type.Missing, oSheet);
                chartSheet.Name = "Chart";

                // Populate chart data in the new sheet
                PopulateProfitChartData(chartSheet);

                // Activate oSheet
                oSheet.Activate();

                // Protect the sheet again
                /*oSheet.Protect();*/

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private void PopulateRevenueData(Excel._Worksheet oSheet)
        {
            try
            {
                // Merge cells A1 to H7
                oSheet.get_Range("A1", "G10").Merge();

                // Merge cells A10 to H11
                Excel.Range titleRange = oSheet.get_Range("A11", "G13");
                titleRange.Merge();

                // Set text to "Order Report"
                oSheet.Cells[11, 1] = "Revenue Report";

                // Format the text
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.Font.Color = System.Drawing.Color.FromArgb(0, 120, 215);
                titleRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                
                // Construct the relative path to the image file
                string relativeImagePath = @"..\..\Resources\LOGO_NO_BG.png";
                string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string imagePath = Path.Combine(directory, relativeImagePath);
                // Use the imagePath variable in your code
                var logoTextbox = oSheet.Shapes.AddPicture(imagePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 20, 20, 100, 100);

                // Add text box for "School Supply" above the address, contact, and email
                var HomeTextbox = oSheet.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 20, 10, 100, 100);
                var OrderTextFrame = HomeTextbox.TextFrame2;
                // Remove the outline of the text box
                HomeTextbox.Line.Visible = MsoTriState.msoFalse;

                // Set text for the "School Supply" text box
                OrderTextFrame.TextRange.Text = "Alvain School Supply";

                // Apply formatting to the "FurEver Home" text box
                OrderTextFrame.TextRange.Font.Bold = MsoTriState.msoTrue; // Use MsoTriState.msoTrue for true
                OrderTextFrame.TextRange.Font.Size = 28;
                var color = System.Drawing.Color.FromArgb(0, 120, 215); //56,182,255
                OrderTextFrame.TextRange.Font.Fill.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(color); // Same color as the address, contact, and email

                // Resize the text box to fit the text content
                HomeTextbox.Width = OrderTextFrame.TextRange.BoundWidth + 120; // Add some padding
                HomeTextbox.Height = 35; // Add some padding

                // Position the "School Supply" text box above the address, contact, and email
                HomeTextbox.Top = 40;
                HomeTextbox.Left = logoTextbox.Left + logoTextbox.Width; // Add some spacing

                // Remove the outline of the text box
                HomeTextbox.Line.Visible = MsoTriState.msoFalse;

                // Add text box for address, contact, and email below the "School Supply" text box
                var headerTextbox = oSheet.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal,
                                                              HomeTextbox.Left, HomeTextbox.Top + HomeTextbox.Height + 0, 120, 215);
                var textFrame = headerTextbox.TextFrame2;

                // Set text for the entire text frame
                textFrame.TextRange.Text = "Address: Legazpi City, Albay\nContact: +63 912 3456 789\nEmail: alvain.2024@gmail.com";

                // Apply formatting to the entire text frame
                textFrame.TextRange.Font.Bold = MsoTriState.msoTrue; // Use MsoTriState.msoTrue for true
                textFrame.TextRange.Font.Size = 12;
                //textFrame.TextRange.Font.Fill.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(color);

                // Resize the text box to fit the text content
                headerTextbox.Width = textFrame.TextRange.BoundWidth + 30; // Add some padding
                headerTextbox.Height = textFrame.TextRange.BoundHeight - 30; // Add some padding

                // Remove the outline of the text box
                headerTextbox.Line.Visible = MsoTriState.msoFalse;

                // Lock the header cells
                oSheet.get_Range("A1", "I10").Locked = true;

                // Add original header
                oSheet.Cells[14, 1] = "Order ID";
                oSheet.Cells[14, 2] = "Order Date";
                oSheet.Cells[14, 3] = "Product Name";
                oSheet.Cells[14, 4] = "Product Price";
                oSheet.Cells[14, 5] = "Quantity";
                oSheet.Cells[14, 6] = "Revenue";
                oSheet.Cells[14, 7] = "Profit Product";

                // Add styles to original header
                Excel.Range headerRange = oSheet.get_Range("A14", "G14");
                headerRange.Font.Bold = true;
                headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                headerRange.Font.Color = System.Drawing.Color.White;
                headerRange.Interior.Color = System.Drawing.Color.FromArgb(56, 182, 255);

                // Unprotect the sheet to allow modifications
                oSheet.Unprotect();

                // Get the data source from the DataGridView
                DataTable dataTable2 = (DataTable)dataGridView1.DataSource;
                double orderTotalSum = 0;
                double profitTotalSum = 0;

                // Define starting row for data insertion
                int startRow = 15; // Start from row 15 as headers are in rows 14

                // Loop through each row in the DataGridView
                for (int i = 0; i < dataTable2.Rows.Count; i++)
                {
                    // Loop through each column in the current row
                    for (int j = 0; j < dataTable2.Columns.Count; j++)
                    {
                        // Write data to Excel cell
                        if (j == 1) // Check if it's the second column (Order Date)
                        {
                            // Format as date
                            oSheet.Cells[startRow + i, j + 1].NumberFormat = "MM/dd/yyyy";
                            oSheet.Cells[startRow + i, j + 1] = Convert.ToDateTime(dataTable2.Rows[i][j]);
                        }
                        else if (j == 3 || j == 5 || j == 6) // Check if it's the fifth or seventh column (Product Price or Order Total)
                        {
                            // Format as float with two decimal places
                            oSheet.Cells[startRow + i, j + 1].NumberFormat = "0.00";
                            oSheet.Cells[startRow + i, j + 1] = Convert.ToDecimal(dataTable2.Rows[i][j]);

                        }
                        else
                        {
                            // Just write the data as it is
                            oSheet.Cells[startRow + i, j + 1] = dataTable2.Rows[i][j].ToString();
                        }
                    }
                }

                // Calculate the total value from the last row of the DataGridView
                foreach (DataRow row in dataTable2.Rows)
                {
                    orderTotalSum += Convert.ToDouble(row["Total"]);
                }

                foreach (DataRow row in dataTable2.Rows)
                {
                    profitTotalSum += Convert.ToDouble(row["ProfitPerProduct"]);
                }

                // Insert the total value into the correct cell to the right of the data
                oSheet.Cells[startRow + dataTable2.Rows.Count + 0, dataTable2.Columns.Count - 2].Value = "Total:";
                oSheet.Cells[startRow + dataTable2.Rows.Count + 0, dataTable2.Columns.Count - 2].Font.Bold = true; // Make "Total:" bold

                oSheet.Cells[startRow + dataTable2.Rows.Count + 0, dataTable2.Columns.Count - 1].Value = orderTotalSum;
                oSheet.Cells[startRow + dataTable2.Rows.Count + 0, dataTable2.Columns.Count - 1].Font.Bold = true; // Make the total value bold

                oSheet.Cells[startRow + dataTable2.Rows.Count + 0, dataTable2.Columns.Count + 0].Value = profitTotalSum;
                oSheet.Cells[startRow + dataTable2.Rows.Count + 0, dataTable2.Columns.Count + 0].Font.Bold = true; // Make the total value bold


                // Merge cells A-G after the DataGridView
                Excel.Range mergeRange = oSheet.Range["A" + (startRow + dataTable2.Rows.Count + 1), "G" + (startRow + dataTable2.Rows.Count + 1)];
                mergeRange.Merge();



                // Add signature and printed name to the left part
                Excel.Range signatureRange = oSheet.Range["A" + (startRow + dataTable2.Rows.Count + 2), "C" + (startRow + dataTable2.Rows.Count + 7)];
                signatureRange.Merge();
                signatureRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                signatureRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                signatureRange.Value = (UserSession.Fname + " " + UserSession.Mname + ". " + UserSession.Lname).ToUpper() + "\n––––––––––––––––––––––––––––––––\nSIGNATURE OVER PRINTED NAME";

                // Add exported date and time with AM/PM to the right part
                string exportedDateTime = DateTime.Now.ToString("MMMM dd, yyyy / hh:mm tt"); // "Month 00, 0000  time 00:00 AM/PM"
                Excel.Range dateTimeRange = oSheet.Range["D" + (startRow + dataTable2.Rows.Count + 2), "G" + (startRow + dataTable2.Rows.Count + 7)];
                dateTimeRange.Merge();
                dateTimeRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                dateTimeRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                dateTimeRange.Value = "Exported Date and Time: " + exportedDateTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private void PopulateProfitChartData(Excel._Worksheet chartSheet)
        {
            // Get profit per product data
            Dictionary<string, double> profitPerProduct = GetProfitPerProduct();

            // Populate profit per product data into the worksheet
            int startRow = 1;
            int currentRow = startRow;
            foreach (var kvp in profitPerProduct)
            {
                chartSheet.Cells[currentRow, 1] = kvp.Key; // product name
                chartSheet.Cells[currentRow, 2] = kvp.Value; // profit
                currentRow++;
            }

            // Generate bar chart for profit per product
            GenerateBarChart(chartSheet, profitPerProduct);
        }

        private Dictionary<string, double> GetProfitPerProduct()
        {
            Dictionary<string, double> profitPerProduct = new Dictionary<string, double>();

            using (MySqlConnection con = MySQL_Connection.GetConnection())
            {
                con.Open();
                string profitQuery = @"SELECT p.ProductName, SUM(p.Price * oi.Quantity * 0.15) AS TotalProfit
                               FROM orderitems oi
                               JOIN products p ON oi.ProductID = p.ProductID
                               GROUP BY p.ProductName";
                using (MySqlCommand profitCmd = new MySqlCommand(profitQuery, con))
                {
                    using (MySqlDataReader profitReader = profitCmd.ExecuteReader())
                    {
                        if (profitReader.HasRows)
                        {
                            while (profitReader.Read())
                            {
                                string productName = profitReader.GetString(0);
                                double totalProfit = profitReader.GetDouble(1);
                                profitPerProduct.Add(productName, totalProfit);
                            }
                        }
                    }
                }
            }

            return profitPerProduct;
        }

        private void GenerateBarChart(Excel._Worksheet chartSheet, Dictionary<string, double> profitPerProduct)
        {
            Excel.ChartObjects chartObjects = (Excel.ChartObjects)chartSheet.ChartObjects(System.Type.Missing);
            Excel.ChartObject chartObject = chartObjects.Add(100, 100, 600, 500) as Microsoft.Office.Interop.Excel.ChartObject; // Adjust the width and height as needed

            Excel.Chart chart = chartObject.Chart;

            // Prepare data for the chart
            Excel.Range chartRange = chartSheet.Range["A1", "B" + profitPerProduct.Count] as Microsoft.Office.Interop.Excel.Range;

            chart.SetSourceData(chartRange, Excel.XlRowCol.xlColumns);

            // Set chart type to Bar
            chart.ChartType = Excel.XlChartType.xlColumnClustered;

            // Set chart title
            chart.HasTitle = true;
            chart.ChartTitle.Text = "Profit per Product";

            // Set axis titles
            chart.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary).HasTitle = true;
            chart.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = "Product";
            chart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).HasTitle = true;
            chart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = "Profit";

            // Adjust the number of tick marks on the Y-axis
            chart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).MajorUnit = 200; // Adjust the major tick marks interval as needed
            chart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).MinorUnit = 50; // Adjust the minor tick marks interval as needed
        }





    }
}

