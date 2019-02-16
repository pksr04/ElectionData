using DapperDemo.Helpers;
using GDL.Sampark.Service;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo.Service
{
    public class PdfCreation: ITextPageBase
    {
        public PdfCreation()
        {

        }

        public async void GeneratePage(string partyType)
        {
            MembersService members = new MembersService();
            var membersList = await members.GetAllMembers();
            var list = membersList.Take(10);
            

           GeneratePageBase(partyType);

            document.Add(HeaderLogo(partyType));
            var spacerParagraph = HeaderSapce();

            document.Add(spacerParagraph);
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 50.0f;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.TotalWidth = 100;
            var houseMembers = list.GroupBy(x => x.HOUSE_NO).Select(x => x.OrderByDescending(y => y.Age)).First().ToList();
            var headMember = houseMembers.First();

            table.AddCell(CellHeadMembercolSpanData(headMember.FM_NAME));
            table.AddCell(CellHeadMemberData(RelationType));
            table.AddCell(CellHeadMemberData(headMember.RLN_FM_NM));
            table.AddCell(CellMemberData(HouseNumber));
            table.AddCell(CellMemberColSpanData(headMember.HOUSE_NO));
            table.AddCell(CellMemberColSpanData(headMember.AREAID));

            document.Add(table);

            var contentSpace = NewParagraph();
            document.Add(contentSpace);
            document.Add(SalutationLine());
            string text = string.Format(@"ತಮ್ಮ ಕುಟುಂಬದಲ್ಲಿ " + houseMembers.Count() + " ಮತಾದರರಿದ್ದು" + headMember.PART_NO + " ರಲ್ಲಿ ಇರುತ್ತಾರೆ ತಮ್ಮ ಪಕ್ಷಕ್ಕೆ" + Environment.NewLine + " ಇರಬೇಕಂದು ವಿನಂತಿಸುತ್ತೇವೆ");
            document.Add(NewKannadaParagraph(text));

            int count = 0;
            foreach (var member in houseMembers.OrderBy(x => x.Slno).SkipWhile(x => x.Slno == headMember.Slno))
            {
                PdfPTable tablea = new PdfPTable(3);
                tablea.WidthPercentage = 200.0f;
                tablea.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths = new float[] { 1.5f, 0.5f, 0.2f };
                tablea.SetWidths(widths);
                tablea.TotalWidth = 300;
                tablea.LockedWidth = true;

                tablea.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                //1st row
                tablea.AddCell(CellSerialNoMemberData(string.Format(SerialNumber + ":" + member.Slno)));
                tablea.AddCell(CellPartNoMemberData(string.Format(PartNumber + ":" + member.PART_NO)));
                //2nd row
                tablea.AddCell(CellMemberNameMemberData(string.Format(Name + ":" + member.FM_NAME)));
                tablea.AddCell(CellMemberSexData(member.Sex));
                tablea.AddCell(CellAgeNameMemberData(member.Age.ToString()));

                //3rd row
                tablea.AddCell(CellRelativeNameMemberData(string.Format(RelationType + ":" + member.RLN_FM_NM)));


                //++count;
                //if (count % 2 == 0)
                //{
                //    table1.WriteSelectedRows(0, -1, d1.Left, d1.Top, wr.DirectContent);
                //}
                //else
                //{

                //    table1.WriteSelectedRows(0, -1, d1.Left + 200, d1.Top-1800, wr.DirectContent);

                //}

                document.Add(tablea);
                document.Add(contentSpace);
            }


            document.Close();
            DocumentBytes = PDFStream.GetBuffer();
        }
    }
}
