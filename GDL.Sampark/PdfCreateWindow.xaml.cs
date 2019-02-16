using CrystalDecisions.CrystalReports.Engine;
using GDL.Sampark.Models;
using GDL.Sampark.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows;

namespace DapperDemo
{
    /// <summary>
    /// Interaction logic for PdfCreateWindow.xaml
    /// </summary>
    public partial class PdfCreateWindow : Window
    {
        
        public PdfCreateWindow()
        {
            InitializeComponent();
        }

        private List<Members> mlist = new List<Members>();
        



        private async void BtnCreatePdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MembersService members = new MembersService();
                List<Members> headmemberlist = await members.GetMembersByHeadOfTheFamily();
                List<Members> memberlist = await members.GetMembersByHouseWise();

                DataTable table = new DataTable();
                table = ToDataTable<Members>(memberlist);

                ReportGenerator.CrystalReport.crptMainPage crpt = new ReportGenerator.CrystalReport.crptMainPage();
                //crpt.Subreports[0].SetDataSource(table);
                crpt.Database.Tables["HeadOfTheHouse"].SetDataSource(headmemberlist);
                crpt.Database.Tables["HouseMembers"].SetDataSource(memberlist);


                crvReport.ViewerCore.ReportSource = crpt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }









            //crvReport.ViewerCore.RefreshReport();

            //  string partyType = null;
            //  if (btnBjp.IsChecked==true)
            //  {
            //      PdfCreation pdf = new PdfCreation();
            //      partyType = "BJP";
            //      pdf.GeneratePage(partyType);
            //  }
            //else  if (btnCongress.IsChecked==true)
            //  {
            //      PdfCreation pdf = new PdfCreation();
            //      partyType = "CONGRESS";
            //      pdf.GeneratePage(partyType);
            //  }
            //  else{
            //      MessageBox.Show("Please select Party Type");
            //  }
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
    }
}