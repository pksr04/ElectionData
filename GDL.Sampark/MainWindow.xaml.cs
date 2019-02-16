using DapperDemo;
using GDL.Sampark.Models;
using GDL.Sampark.Service;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GDL.Sampark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Members> list = new List<Members>();
        private List<Members> searchlist = new List<Members>();

        private int PageIndex = 1;
        private int numberOfRecPerPage;
        private int FillType = -1;

        //To check the paging direction according to use selection.
        private enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };

        public MainWindow()
        {
            InitializeComponent();
            CbNumberOfRecords.Items.Add("10");
            CbNumberOfRecords.Items.Add("20");
            CbNumberOfRecords.Items.Add("30");
            CbNumberOfRecords.Items.Add("50");
            CbNumberOfRecords.Items.Add("100");
            CbNumberOfRecords.SelectedItem = 10;
            // Load();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        #region Pagination

        private void BtnFirst_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.First);
        }

        private void BtnNext_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Next);
        }

        private void BtnPrev_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Previous);
        }

        private void BtnLast_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Last);
        }

        private void CbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate((int)PagingMode.PageCountChange);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            searchlist = GetTypeOfFill(FillType, list);
            SearchDataGridByParameter(txbxPartFrom.Text.ToString(), txbxPartTo.Text.ToString(), txbxHNo.Text.ToString().Trim(), txbxName.Text.ToString(), txbxSrNo.Text.ToString(), txbxRelative.Text.ToString(), txbxEpicId.Text.ToString(), searchlist);
        }

        private void SearchDataGridByParameter(string partFrom, string partTo, string houseNo, string name, string serailNo, string relativeName, string epicId, List<Members> slist)
        {
            if (partFrom != string.Empty || partTo != string.Empty || name != string.Empty || houseNo != string.Empty || serailNo != string.Empty || relativeName != string.Empty || epicId != string.Empty)
            {
                epicId = epicId.ToUpperInvariant();
                FillType = 10;

                var isSlno = int.TryParse(serailNo, out int k);
                if (partFrom != string.Empty && partTo != string.Empty)
                {
                    int i = int.Parse(partFrom);
                    int j = int.Parse(partTo);

                    if (isSlno)
                    {
                        k = int.Parse(serailNo);
                        MemberDatagridview.ItemsSource  = slist.Where(x => x.PART_NO >= i && x.PART_NO <= j).Where(x => x.HOUSE_NO.Contains(houseNo)).Where(x => x.FM_NAME.Contains(name)).Where(x => x.RLN_FM_NM.Contains(relativeName)).Where(x => x.IDCARD_NO.Contains(epicId)).Where(x => x.Slno == k).ToList();
                    }
                    else
                    {
                        MemberDatagridview.ItemsSource  = slist.Where(x => x.PART_NO >= i && x.PART_NO <= j).Where(x => x.HOUSE_NO.Contains(houseNo)).Where(x => x.FM_NAME.Contains(name)).Where(x => x.RLN_FM_NM.Contains(relativeName)).Where(x => x.IDCARD_NO.Contains(epicId)).ToList();
                    }
                }
                else if (partFrom != string.Empty)
                {
                    if (isSlno)
                    {
                        int i = int.Parse(partFrom);
                        k = int.Parse(serailNo);
                        MemberDatagridview.ItemsSource  = slist.Where(x => x.PART_NO == i).Where(x => x.HOUSE_NO.Contains(houseNo)).Where(x => x.FM_NAME.Contains(name)).Where(x => x.RLN_FM_NM.Contains(relativeName)).Where(x => x.IDCARD_NO.Contains(epicId)).Where(x => x.Slno == k).ToList();
                    }
                    else
                    {
                        int i = int.Parse(partFrom);
                        MemberDatagridview.ItemsSource  = slist.Where(x => x.PART_NO == i).Where(x => x.HOUSE_NO.Contains(houseNo)).Where(x => x.FM_NAME.Contains(name)).Where(x => x.RLN_FM_NM.Contains(relativeName)).Where(x => x.IDCARD_NO.Contains(epicId)).ToList();
                    }
                }
                else
                {
                    if (isSlno)
                    {
                        k = int.Parse(serailNo);
                        MemberDatagridview.ItemsSource  = slist.Where(x => x.HOUSE_NO.Contains(houseNo)).Where(x => x.FM_NAME.Contains(name)).Where(x => x.RLN_FM_NM.Contains(relativeName)).Where(x => x.IDCARD_NO.Contains(epicId)).Where(x => x.Slno == k).ToList();
                    }
                    else
                    {
                        MemberDatagridview.ItemsSource  = slist.Where(x => x.HOUSE_NO.Contains(houseNo)).Where(x => x.FM_NAME.Contains(name)).Where(x => x.RLN_FM_NM.Contains(relativeName)).Where(x => x.IDCARD_NO.Contains(epicId)).ToList();
                    }
                }
            }
        }

        private void BtnConvertToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDatagridview.Items.Count != 0 && MemberDatagridview.Items != null)
            {
                List<Members> plist = new List<Members>();
                DataTable dataTable = new DataTable();
                list = GetTypeOfFill(FillType, list);

                string fileName = @"D:\Sampark" + DateTime.Now.Ticks + ".pdf";

                if (list.Count() != 0)
                {
                    dataTable = ToDataTable<Members>(list);

                    ExportDataTableToPdf(dataTable, fileName);
                }
                else
                {
                    MessageBox.Show("Load Data");
                }
            }
            else
            {
                MessageBox.Show("Data is not loaded");
            }
        }

        private static void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath)
        {
            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);

            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, iTextSharp.text.BaseColor.WHITE);
            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }

            //table Data
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {
                    table.AddCell(dtblTable.Rows[i][j].ToString());
                }
            }

            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in items)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }

            dataTable.Columns["PART_NO"].ColumnName = "Part No";
            dataTable.Columns["Slno"].ColumnName = "Sr No";
            dataTable.Columns["HOUSE_NO"].ColumnName = "House No";
            dataTable.Columns["FM_NAME"].ColumnName = "Name";
            dataTable.Columns["RLN_TYPE"].ColumnName = "Relative Type";
            dataTable.Columns["RLN_FM_NM"].ColumnName = "Relative";
            dataTable.Columns["IDCARD_NO"].ColumnName = "Card No";
            dataTable.Columns["AREAID"].ColumnName = "Address";
            dataTable.Columns.Remove("SlnInParts");
            return dataTable;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            txbxName.Clear();
            txbxHNo.Clear();
            txbxSrNo.Clear();
            txbxPartFrom.Clear();
            txbxPartTo.Clear();
            txbxEpicId.Clear();
            txbxRelative.Clear();

            MemberDatagridview.ItemsSource = null;

            list = GetTypeOfFill(FillType, list);
            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            lblPageNumber.Content = count + " of " + list.Count;
        }

        private void Navigate(int mode)
        {
            int count = 0;
            switch (mode)
            {
                case (int)PagingMode.Next:
                    BtnPrevious.IsEnabled = true;
                    BtnFirst.IsEnabled = true;

                    list = GetTypeOfFill(FillType, list);

                    if (list.Count >= (PageIndex * numberOfRecPerPage))
                    {
                        if (list.Skip(PageIndex * numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            MemberDatagridview.ItemsSource = null;
                            MemberDatagridview.ItemsSource = list.Skip((PageIndex * numberOfRecPerPage) - numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (PageIndex * numberOfRecPerPage) + (list.Skip(PageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                        }
                        else
                        {
                            MemberDatagridview.ItemsSource = null;
                            MemberDatagridview.ItemsSource = list.Skip(PageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (PageIndex * numberOfRecPerPage) + (list.Skip(PageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                            PageIndex++;
                        }

                        lblPageNumber.Content = count + " of " + list.Count;
                    }
                    else
                    {
                        BtnNext.IsEnabled = false;
                        BtnLast.IsEnabled = false;
                    }

                    break;

                case (int)PagingMode.Previous:
                    BtnNext.IsEnabled = true;
                    BtnLast.IsEnabled = true;
                    if (PageIndex > 1)
                    {
                        PageIndex -= 1;
                        MemberDatagridview.ItemsSource = null;
                        if (PageIndex == 1)
                        {
                            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
                            count = list.Take(numberOfRecPerPage).Count();
                            lblPageNumber.Content = count + " of " + list.Count;
                        }
                        else
                        {
                            MemberDatagridview.ItemsSource = list.Skip(PageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = Math.Min(PageIndex * numberOfRecPerPage, list.Count);
                            lblPageNumber.Content = count + " of " + list.Count;
                        }
                    }
                    else
                    {
                        BtnPrevious.IsEnabled = false;
                        BtnFirst.IsEnabled = false;
                    }
                    break;

                case (int)PagingMode.First:
                    PageIndex = 2;
                    Navigate((int)PagingMode.Previous);
                    break;

                case (int)PagingMode.Last:
                    PageIndex = (list.Count / numberOfRecPerPage);
                    Navigate((int)PagingMode.Next);
                    break;

                case (int)PagingMode.PageCountChange:
                    PageIndex = 1;
                    numberOfRecPerPage = Convert.ToInt32(CbNumberOfRecords.SelectedItem);
                    MemberDatagridview.ItemsSource = null;
                    MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
                    count = (list.Take(numberOfRecPerPage)).Count();
                    lblPageNumber.Content = count + " of " + list.Count;
                    BtnNext.IsEnabled = true;
                    BtnLast.IsEnabled = true;
                    BtnPrevious.IsEnabled = true;
                    BtnFirst.IsEnabled = true;
                    break;
            }
        }

        #endregion Pagination

        private void MemberDatagridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentIndex = MemberDatagridview.Items.IndexOf(MemberDatagridview.SelectedItems);

            if (MemberDatagridview.SelectedItem == null)
            {
                return;
            }
            else
            {
                var houseNo = (MemberDatagridview.SelectedItem as Members).HOUSE_NO;
                MemberDetails memberDetails = new MemberDetails(houseNo);

                memberDetails.Show();
            }
        }

        private async void BtnFillGrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
     
            list = await members.GetAllMembers();
            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 0;
        }

        private async void BtnAlphabeticalWiseFillGrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
            list = await members.GetMembersByAlphabeticalWise();

            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 1;
        }

        private async void BtnAgeWiseFillGrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
            list = await members.GetMembersByAgeWise();

            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 2;
        }

        private async void BtnFamilyFillFrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
            list = await members.GetMembersByHouseWise();

            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 3;
        }

        private async void BtnHeadOfFamilyFillFrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
            list = await members.GetMembersByHeadOfTheFamily();

            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 4;
        }

        private async void BtnMarriedWomenFillFrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
            list = await members.GetMembersByMarriedWomenWise();

            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            // totalNoPeople.NoMales = list.OrderBy(x => x.).Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F" && x.STATUSTYPE == "M").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 5;
        }

        private async void BtnAddressFillFrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
            list = await members.GetMembersByAddressWise();

            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 6;
        }

        private async void BtnDoubleNamesFillFrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
            list = await members.GetMembersDoubleName();

            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 7;
        }

        private async void BtnSurNameFillFrid_Click(object sender, RoutedEventArgs e)
        {
            MembersService members = new MembersService();
            list = await members.GetMembersSurName();

            MemberDatagridview.ItemsSource = list.Take(numberOfRecPerPage);
            int count = list.Take(numberOfRecPerPage).Count();
            lblPageNumber.Content = count + " of " + list.Count;
            TotalNoPeople totalNoPeople = new TotalNoPeople();
            totalNoPeople.NoMales = list.Where(x => x.Sex == "M").Count().ToString();
            totalNoPeople.NoFemales = list.Where(x => x.Sex == "F").Count().ToString();
            this.DataContext = totalNoPeople;
            FillType = 8;
        }

        public List<Members> GetTypeOfFill(int fillType, List<Members> members)
        {
            List<Members> fillList = new List<Members>();
            if (FillType == 1)
            {
                fillList = members.OrderBy(x => x.FM_NAME).ToList();
            }
            else if (FillType == 2)
            {
                fillList = members.OrderBy(x => x.Age).ToList();
            }
            else if (FillType == 3)
            {
                fillList = members.OrderBy(x => x.Age).ToList();
            }
            else if (FillType == 4)
            {
                fillList = members.OrderBy(x => x.HOUSE_NO).ToList();
            }
            else if (FillType == 5)
            {
                fillList = members.GroupBy(x => x.HOUSE_NO).Select(r => r.OrderByDescending(x => x.Age).First()).ToList();
            }
            else if (FillType == 6)
            {
                fillList = members.Where(x => x.Sex == "F" && x.STATUSTYPE == "M").ToList();
            }
            else if (FillType == 7)
            {
                fillList = members.OrderBy(x => x.AREAID).ToList();
            }
            else if (FillType == 8)
            {
                fillList = members.OrderBy(x => new { x.FM_NAME }).ToList();
            }
            else
            {
                fillList = list;
            }

            return fillList;
        }

        private void BtnGeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            PdfCreateWindow pdfCreateWindow = new PdfCreateWindow();
            pdfCreateWindow.Show();
        }
    }
}