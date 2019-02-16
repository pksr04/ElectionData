using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;


namespace DapperDemo.Helpers
{
    public class ITextPageBase : PdfPageEventHelper
    {
        #region Privates

        protected const string NewLine = "\n";
        protected Document document;
        protected BaseFont fontTimes;
        protected PdfTemplate footerTemplate;
        protected PdfContentByte cb;
        protected string PartyType;

        protected Font fontFooter;
        protected iTextSharp.text.Font fontGeneralText;
        protected Font NormalKannadaText;
        protected Font BoldKannadaText;
        protected Font LargeBoldKannadaText;
        protected Font CellHeadKannadaText;
        protected Font CellKannadaText;
        protected Font CellLargeHeadKannadaText;

        protected PdfWriter writer;

        #endregion Privates

        #region Properties

        #region PDFStream

        private MemoryStream _PDFStream = new MemoryStream();

        public MemoryStream PDFStream
        {
            get { return _PDFStream; }
            set
            {
                if (_PDFStream == value)
                    return;
                _PDFStream = value;
            }
        }

        #endregion PDFStream

        public byte[] DocumentBytes;

        #region HouseNo

        private string _houseNumber = "ಮನೆ ನಂ: ";

        public string HouseNumber
        {
            get { return _houseNumber; }
            set
            {
                if (_houseNumber == value)
                    return;
                _houseNumber = value;
            }
        }

        #endregion HouseNo

        #region RelationType

        private string _relationType = "ತಂದೆ/ತಾಯಿ/ಗಂಡ ಹೆಸರು:";

        public string RelationType
        {
            get { return _relationType; }
            set
            {
                if (_relationType == value)
                    return;
                _relationType = value;
            }
        }

        #endregion RelationType

        #region SerialNumber

        private string _serialNumber = "ಕ್ರಮ ಸಂ";

        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (_serialNumber == value)
                    return;
                _serialNumber = value;
            }
        }

        #endregion SerialNumber

        #region PartNumber

        private string _partNumber = "ಭಾಗದ ಸಂಖ್ಯೆ";

        public string PartNumber
        {
            get { return _partNumber; }
            set
            {
                if (_partNumber == value)
                    return;
                _partNumber = value;
            }
        }

        #endregion PartNumber

        #region Name

        private string _name = "ಹೆಸರು";

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
            }
        }

        #endregion Name

        #region CTOR

        public ITextPageBase()
        {
            document = new Document(PageSize.A4);
            PDFStream = new MemoryStream();
            buildFonts();
        }

        #endregion CTOR

        #endregion Properties

        #region buildFonts

        /// <summary>
        /// defines standard fonts used in this class
        /// </summary>
        private void buildFonts()
        {
            // add more font types. These may be reused through the document creation

            fontTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            fontFooter = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.ITALIC, BaseColor.DARK_GRAY);
            fontGeneralText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            string kannadaFontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\Kedage-n.ttf";
            BaseFont kannadaFont = BaseFont.CreateFont(kannadaFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            NormalKannadaText = new Font(kannadaFont, 12f, Font.NORMAL, BaseColor.BLACK);
            BoldKannadaText = new Font(kannadaFont, 14f, Font.BOLD, BaseColor.BLACK);
            LargeBoldKannadaText = new Font(kannadaFont, 16f, Font.BOLD, BaseColor.BLACK);
            CellHeadKannadaText = new Font(kannadaFont, 12f, Font.BOLD, BaseColor.BLACK);
            CellLargeHeadKannadaText = new Font(kannadaFont, 14f, Font.BOLD, BaseColor.BLACK);
            CellKannadaText = new Font(kannadaFont, 10f, Font.NORMAL, BaseColor.BLACK);
        }

        #endregion buildFonts

        //TODO: Change the default properties

        #region Salutation

        private string _Salutation = "ಮಾನ್ಯರೇ ";

        public string Salutation
        {
            get { return _Salutation; }
            set
            {
                if (_Salutation == value)
                    return;
                _Salutation = value;
            }
        }

        #endregion Salutation

        #region NewParagraph

        /// <summary>
        /// Creates a new paragraph using the general font,30f space before and 15f space after.
        /// </summary>
        /// <returns>paragraph object</returns>
        public Paragraph NewParagraph()
        {
            Paragraph p = new Paragraph();
            p.Font = fontGeneralText;
            p.Leading = 12f;
            p.SpacingBefore = 30f;
            p.SpacingAfter = 10f;
            return p;
        }

        #endregion NewParagraph

        #region HeaderSapce

        /// <summary>
        /// Creates a new header paragraph using the general font,10f  spacing before and 200f space after.
        /// </summary>
        /// <returns>paragraph object</returns>
        public Paragraph HeaderSapce()
        {
            Paragraph p = new Paragraph();
            p.Leading = 12f;
            p.SpacingBefore = 10f;
            p.SpacingAfter = 200f;
            return p;
        }

        #endregion HeaderSapce

        #region NewKannadaParagraph

        /// <summary>
        /// Adds a simple paragraph with a single line of text.
        /// </summary>
        /// <param name="text">Content of paragraph</param>
        /// <returns></returns>
        public Paragraph NewKannadaParagraph(string text)
        {
            Paragraph p = new Paragraph(text, NormalKannadaText);
            p.SpacingAfter = 10f;
            //p.Leading = p.Font.Size + 1f;
            return p;
        }

        #endregion NewKannadaParagraph

        #region Second Page Footer

        /// <summary>
        /// Page footer for 2nd and remaining pages.  Only prints page number.
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <returns></returns>
        public PdfTemplate AddPageFooter(int PageNumber)
        {
            PdfTemplate footerTemplate = cb.CreateTemplate(500, 55);

            footerTemplate.BeginText();
            BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            footerTemplate.SetFontAndSize(bf2, 11);
            footerTemplate.SetColorStroke(BaseColor.DARK_GRAY);
            footerTemplate.SetColorFill(BaseColor.GRAY);
            int al = -200;
            footerTemplate.SetLineWidth(3);
            //footerTemplate.LineTo(500, footerTemplate.YTLM);

            string texttoadd = "Page: " + PageNumber.ToString();
            float widthoftext = 500.0f - bf2.GetWidthPoint(texttoadd, 11);
            footerTemplate.ShowTextAligned(al, texttoadd, widthoftext, 30, 0);
            footerTemplate.EndText();

            return footerTemplate;
        }

        #endregion Second Page Footer

        #region HeaderLogo

        /// <summary>
        /// Adds the header logo from the resource file.
        /// </summary>
        /// <returns>(IElement) Image</returns>
        public Image HeaderLogo(string partyType)
        {
            PartyType = partyType;
            Image imageHeader = null;
            if (PartyType == "BJP")
            {
                imageHeader = Image.GetInstance(new Uri(@"D:\WPF\ElectionPDFItextSharpDemo\Itextsharp5x\Images\bjp.JPG"));
                imageHeader.ScaleToFit(document.PageSize.Width, document.Top);
                imageHeader.SetAbsolutePosition(-1, 600f); // set the position to bottom left corner of pdf

               
            }
            else if(PartyType == "CONGRESS")
            {
                imageHeader = Image.GetInstance(new Uri(@"D:\WPF\ElectionPDFItextSharpDemo\Itextsharp5x\Images\congress.JPG"));
                imageHeader.ScaleToFit(document.PageSize.Width, document.Top);
                imageHeader.SetAbsolutePosition(-1, 600f); // set the position to bottom left corner of pdf

            }

            return imageHeader;

        }

        #endregion HeaderLogo

        #region CellHeadMemberDate

        /// <summary>
        /// creates a PdfpCell Cell Head Member Date.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellHeadMemberData(string text)
        {
            Paragraph headMember = new Paragraph(text, CellHeadKannadaText);
            PdfPCell x = new PdfPCell(headMember);
            headMember.Alignment = Element.ALIGN_LEFT;
            x.Border = 0;
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            return x;
        }

        #region CellLeftAlignmentMemberData
        /// <summary>
        /// creates a PdfpCell to a Left Alignment.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellSerialNoMemberData(string text)
        {
            Paragraph row = new Paragraph(text, CellKannadaText);
            row.Alignment = Element.ALIGN_LEFT;
            PdfPCell x = new PdfPCell(row);
            x.Border = 0;
            x.BorderWidthLeft = 1f;
            x.BorderWidthTop = 1f;

            //x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            return x;
        }
        #endregion


        #region CellMemberData
        /// <summary>
        /// creates a PdfpCell with a border.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellMemberData(string text)
        {
            Paragraph curphrase = new Paragraph(text, CellKannadaText);
            PdfPCell x = new PdfPCell(curphrase);
            x.Border = 0;
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            return x;
        }
        #endregion

        #endregion CellHeadMemberDate

        #region CellHeadMemberColSpanData

        /// <summary>
        /// creates a PdfpCell with a border.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellHeadMembercolSpanData(string text)
        {
            Paragraph headMember = new Paragraph(text, CellLargeHeadKannadaText);
            PdfPCell x = new PdfPCell(headMember);

            headMember.Alignment = Element.ALIGN_LEFT;
            x.Border = 0;
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            x.Colspan = 2;
            return x;
        }

        #endregion CellHeadMemberColSpanData

        #region CellMemberColSpanData

        /// <summary>
        /// creates a PdfpCell with a border.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellMemberColSpanData(string text)
        {
            Paragraph curphrase = new Paragraph(text, CellKannadaText);
            PdfPCell x = new PdfPCell(curphrase);
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            x.Border = 0;

            x.Colspan = 2;
            return x;
        }

        #endregion CellMemberColSpanData

        #region CellPartNoMemberData

        /// <summary>
        /// creates a Cell PartNo Member Data.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellPartNoMemberData(string text)
        {
            Paragraph curphrase = new Paragraph(text, CellKannadaText);
            PdfPCell x = new PdfPCell(curphrase);
            curphrase.Alignment = Element.ALIGN_CENTER;
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            x.Border = 0;
            x.BorderWidthRight = 1f;
            x.BorderWidthTop = 1f;
            x.Colspan = 2;
            return x;
        }

        #endregion CellPartNoMemberData

        #region CellMemberNameMemberData

        /// <summary>
        /// creates a CellMember Name
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellMemberNameMemberData(string text)
        {
            Paragraph curphrase = new Paragraph(text, CellKannadaText);
            PdfPCell x = new PdfPCell(curphrase);
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            x.Border = 0;
            x.BorderWidthLeft = 1f;
            return x;
        }

        #endregion CellMemberNameMemberData

        #region CellAgeNameMemberData

        /// <summary>
        /// creates a PdfpCell to a Relative Name Member Data.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellAgeNameMemberData(string text)
        {
            Paragraph row = new Paragraph(text, CellKannadaText);
            row.Alignment = Element.ALIGN_LEFT;
            PdfPCell x = new PdfPCell(row);
            x.Border = 0;
            x.BorderWidthRight = 1f;

            //x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            return x;
        }

        #endregion CellAgeNameMemberData

        #region CellRelativeNameMemberData

        /// <summary>
        /// creates a PdfpCell to a Relative Name Member Data.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellRelativeNameMemberData(string text)
        {
            Paragraph row = new Paragraph(text, CellKannadaText);
            row.Alignment = Element.ALIGN_LEFT;
            PdfPCell x = new PdfPCell(row);
            x.Border = 0;
            x.BorderWidthLeft = 1f;
            x.BorderWidthBottom = 1f;
            x.BorderWidthRight = 1f;
            x.Colspan = 3;

            //x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            return x;
        }

        #endregion CellRelativeNameMemberData

        #region CellMemberSexData

        /// <summary>
        /// creates a Cell Member SexData
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellMemberSexData(string text)
        {
            Paragraph row = new Paragraph(text, fontGeneralText);
            row.Alignment = Element.ALIGN_RIGHT;
            PdfPCell x = new PdfPCell(row);
            x.Border = 0;

            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            return x;
        }

        #endregion CellMemberSexData

        #region Salutation

        public Paragraph SalutationLine()
        {
            Paragraph p = new Paragraph();
            p.SpacingAfter = 10.0f;
            p.Add(new Phrase(Salutation, NormalKannadaText));

            return p;
        }

        #endregion Salutation

        #region GeneratePageBase

        /// <summary>
        /// This creates the base letter objects
        /// </summary>
        public void GeneratePageBase(string partyType)
        {
            PartyType = partyType;
            writer = PdfWriter.GetInstance(document, PDFStream);
            document.Open();
            cb = writer.DirectContent;
            writer.PageEvent = this;
            document.SetMargins(HeaderLogo(PartyType).Height + document.LeftMargin,
                document.RightMargin,
                document.TopMargin,
                document.BottomMargin);
        }

        #endregion GeneratePageBase

        #region Events

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            Debug.WriteLine("On Page Start Called");
        }

        public override void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
        {
            base.OnParagraph(writer, document, paragraphPosition);
            Debug.WriteLine("On Paragraph.....");
        }

        /// <summary>
        /// OnEndPage - This sets up the page footer. Will use the footer block for first
        /// page and page number for subsequent page
        /// </summary>
        /// <param name="writer">Default parameter</param>
        /// <param name="document">Default parameter</param>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            if (document.PageNumber > 1)
            {
                cb.AddTemplate(AddPageFooter(document.PageNumber), 50, 50);
                cb.AddImage(HeaderLogo(PartyType));
            }
            //else
            //    cb.AddTemplate(AddAddressFooter(), 50, 50);
        }

        #endregion Events
    }
}