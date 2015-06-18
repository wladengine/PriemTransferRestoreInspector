using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Data;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PriemForeignInspector
{
    class PDFUtils
    {
        //перевод (AbitTypeId = 3)
       /* public static byte[] GetApplicationPDFTransfer(Guid appId, string dirPath)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abit = (from x in context.Application
                            where x.Id == appId
                            select new
                            {
                                x.PersonId,
                                x.Barcode,
                                Faculty = x.C_Entry.FacultyName,
                                Profession = x.C_Entry.LicenseProgramName,
                                ProfessionCode = x.C_Entry.LicenseProgramCode,
                                ObrazProgram = x.C_Entry.ObrazProgramName,
                                x.C_Entry.ObrazProgramCrypt,
                                Specialization = x.C_Entry.ProfileName,
                                StudyFormId = x.C_Entry.StudyFormId,
                                StudyFormName = x.C_Entry.StudyFormName,
                                x.C_Entry.StudyBasisId,
                                x.EntryType,
                                x.HostelEduc,
                                SemesterName = x.C_Entry.Semester.Name,
                                EducYear = x.C_Entry.Semester.EducYear,
                                x.C_Entry.StudyLevelId
                            }).FirstOrDefault();
                string email = context.User.Where(x => x.Id == abit.PersonId).Select(x => x.Email).FirstOrDefault();

                var person = context.Person.Where(x => x.Id == abit.PersonId).FirstOrDefault();

                var PersonContacts = person.PersonContacts;
                if (PersonContacts == null)
                    PersonContacts = new PersonContacts();

                MemoryStream ms = new MemoryStream();
                string dotName = "ApplicationTransfer.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (3000000 + abit.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);

                img.SetAbsolutePosition(280, 780);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abit.ProfessionCode + " " + abit.Profession);
                acrFlds.SetField("Specialization", abit.Specialization);
                acrFlds.SetField("Faculty", abit.Faculty);
                acrFlds.SetField("ObrazProgram", (abit.ObrazProgramCrypt + " " ?? "") + abit.ObrazProgram);
                acrFlds.SetField("StudyForm" + abit.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abit.StudyBasisId, "1");

                acrFlds.SetField("Course", abit.EducYear.ToString());
                acrFlds.SetField("Semester", abit.SemesterName);

                switch (abit.StudyLevelId)
                {
                    case 16: { acrFlds.SetField("chbBak", "1"); break; }
                    case 17: { acrFlds.SetField("chbMag", "1"); break; }
                    case 18: { acrFlds.SetField("chbSpec", "1"); break; }
                }

                if (abit.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                if (person.PersonAddInfo.HostelAbit ?? false)
                    acrFlds.SetField("HostelAbitYes", "1");
                else
                    acrFlds.SetField("HostelAbitNo", "1");

                if (person.Sex ?? false)
                    acrFlds.SetField("Male", "1");
                else
                    acrFlds.SetField("Female", "1");

                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("BirthPlace", person.BirthPlace);
                acrFlds.SetField("Nationality", person.Nationality.Name);
                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                string Address = (PersonContacts.Code ?? "") + " " +
                    (PersonContacts.City ?? "") + " " +
                    (PersonContacts.Street ?? "") + " " +
                    (PersonContacts.House ?? "") + " " +
                    (PersonContacts.Korpus ?? "") + " " +
                    (PersonContacts.Flat ?? "");

                string[] strSplit = GetSplittedStrings(Address, 50, 70, 2);

                for (int i = 1; i < 3; i++)
                    acrFlds.SetField("Address" + i.ToString(), strSplit[i - 1]);

                string phones = (PersonContacts.Phone ?? "") + ", e-mail: " + email + ", " + (PersonContacts.Mobiles ?? "");

                strSplit = GetSplittedStrings(phones, 30, 70, 2);
                for (int i = 1; i < 3; i++)
                    acrFlds.SetField("Phone" + i.ToString(), strSplit[i - 1]);

                var PersonEducationDocument = person.PersonEducationDocument;
                if (PersonEducationDocument == null)
                    PersonEducationDocument = new PersonEducationDocument();

                var PersonCurrentEducation = person.PersonCurrentEducation;
                if (PersonCurrentEducation == null)
                    PersonCurrentEducation = new PersonCurrentEducation();

                var PersonAddInfo = person.PersonAddInfo;
                if (PersonAddInfo == null)
                    PersonAddInfo = new PersonAddInfo();

                strSplit = GetSplittedStrings(PersonAddInfo.Parents, 50, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Parents" + i.ToString(), strSplit[i - 1]);

                acrFlds.SetField("CurrentEducationName", PersonCurrentEducation.EducName);
                acrFlds.SetField((PersonCurrentEducation.HasAccreditation ?? false) ? "HasAccred" : "NoAccred", "1");
                string AccredInfo = (PersonCurrentEducation.AccreditationNumber ?? "") +
                    (PersonCurrentEducation.AccreditationDate.HasValue ? " от " + PersonCurrentEducation.AccreditationDate.Value.ToShortDateString() : "");
                acrFlds.SetField("EducationAccreditationNumber", (PersonCurrentEducation.HasAccreditation ?? false) ? AccredInfo : "");
                if (PersonCurrentEducation.Semester != null)
                    acrFlds.SetField("CurrentCourse", PersonCurrentEducation.Semester.EducYear.ToString());

                switch (PersonCurrentEducation.StudyLevelId ?? 16)
                {
                    case 16: { acrFlds.SetField("CurrentBak", "1"); break; }
                    case 17: { acrFlds.SetField("CurrentMag", "1"); break; }
                    case 18: { acrFlds.SetField("CurrentSpec", "1"); break; }
                }

                acrFlds.SetField("ExitYear", PersonEducationDocument.SchoolExitYear ?? "");
                acrFlds.SetField("School", PersonEducationDocument.SchoolName ?? "");
                acrFlds.SetField("EducationDocument", (PersonEducationDocument.Series ?? "") + " " + (PersonEducationDocument.Number ?? ""));
                if (PersonCurrentEducation.HasScholarship ?? false)
                    acrFlds.SetField("HasScholarship", "1");
                else
                    acrFlds.SetField("NoScholarship", "1");

                acrFlds.SetField("Extra", PersonAddInfo.AddInfo ?? "");
                acrFlds.SetField("Copy", "1");

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        }*/
        public static byte[] GetApplicationPDFTransfer(Guid appId, string dirPath, Guid PersonId)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abitList = (from x in context.Application
                                join Commit in context.ApplicationCommit on x.CommitId equals Commit.Id
                                join Entry in context.Entry on x.EntryId equals Entry.Id
                                join Semester in context.Semester on Entry.SemesterId equals Semester.Id
                                where x.Id == appId
                                select new
                                {
                                    x.Id,
                                    x.PersonId,
                                    x.Barcode,
                                    Faculty = Entry.FacultyName,
                                    Profession = Entry.LicenseProgramName,
                                    ProfessionCode = Entry.LicenseProgramCode,
                                    ObrazProgram = Entry.ObrazProgramCrypt + " " + Entry.ObrazProgramName,
                                    Specialization = Entry.ProfileName,
                                    Entry.StudyFormId,
                                    Entry.StudyFormName,
                                    Entry.StudyBasisId,
                                    EntryType = (Entry.StudyLevelId == 17 ? 2 : 1),
                                    Entry.StudyLevelId,
                                    CommitIntNumber = Commit.IntNumber,
                                    x.Priority,
                                    x.IsGosLine,
                                    Entry.ComissionId,
                                    ComissionAddress = Entry.Address,
                                    SemesterName = Semester.Name,
                                    EducYear = Semester.EducYear
                                }).OrderBy(x => x.Priority).FirstOrDefault();

                /* var abitProfileList = (from x in context.Application
                                        join Ad in context.extApplicationDetails on x.Id equals Ad.ApplicationId
                                        where x.CommitId == appId
                                        select new ShortAppcationDetails()
                                        {
                                            ApplicationId = x.Id,
                                            CurrVersion = Ad.CurrVersion,
                                            CurrDate = Ad.CurrDate,
                                            ObrazProgramInEntryPriority = Ad.ObrazProgramInEntryPriority,
                                            ObrazProgramName = ((Ad.ObrazProgramCrypt + " ") ?? "") + Ad.ObrazProgramName,
                                            ProfileInObrazProgramInEntryPriority = Ad.ProfileInObrazProgramInEntryPriority,
                                            ProfileName = Ad.ProfileName
                                        }).ToList();
                 */
                string query = "SELECT Email, IsForeign FROM [User] WHERE Id=@Id";
                DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", PersonId } }); 
                string email = tbl.Rows[0].Field<string>("Email");
                var person = (from x in context.Person
                              join PersonCurrentEducation in context.PersonCurrentEducation on PersonId equals PersonCurrentEducation.PersonId
                              join Semester in context.Semester on PersonCurrentEducation.SemesterId equals Semester.Id
                              where x.Id == PersonId
                              select new
                              {
                                  x.Surname,
                                  x.Name,
                                  x.SecondName,
                                  x.Barcode,
                                  x.PersonAddInfo.HostelAbit,
                                  x.BirthDate,
                                  BirthPlace = x.BirthPlace ?? "",
                                  Sex = x.Sex,
                                  Nationality = x.Nationality.Name,
                                  Country = x.PersonContacts.Country.Name,
                                  PassportType = x.PassportType.Name,
                                  x.PassportSeries,
                                  x.PassportNumber,
                                  x.PassportAuthor,
                                  x.PassportDate,
                                  x.PersonContacts.City,
                                  Region = x.PersonContacts.Region.Name,
                                  x.PersonEducationDocument.FirstOrDefault().PersonHighEducationInfo.ProgramName,
                                  x.PersonContacts.Code,
                                  x.PersonContacts.Street,
                                  x.PersonContacts.House,
                                  x.PersonContacts.Korpus,
                                  x.PersonContacts.Flat,
                                  x.PersonContacts.Phone,
                                  x.PersonContacts.Mobiles,
                                  x.PersonAddInfo.HostelEduc,
                                  x.PersonContacts.Country.IsRussia,
                                  x.PersonEducationDocument.FirstOrDefault().SchoolName,
                                  x.PersonCurrentEducation.HasAccreditation,
                                  x.PersonCurrentEducation.AccreditationDate,
                                  x.PersonCurrentEducation.AccreditationNumber,
                                  x.PersonCurrentEducation.HasScholarship,
                                  x.PersonAddInfo.Parents,
                                  x.PersonAddInfo.AddInfo,
                                  x.PersonEducationDocument.FirstOrDefault().SchoolExitYear,
                                  x.PersonEducationDocument.FirstOrDefault().Number,
                                  x.PersonEducationDocument.FirstOrDefault().Series,
                                  x.PersonCurrentEducation.StudyLevelId,

                                  Semester.EducYear
                              }).FirstOrDefault();

                MemoryStream ms = new MemoryStream();
                string dotName = "ApplicationTransfer.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (3000000 + abitList.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);

                img.SetAbsolutePosition(280, 780);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abitList.ProfessionCode + " " + abitList.Profession);
                acrFlds.SetField("Specialization", abitList.Specialization);
                acrFlds.SetField("Faculty", abitList.Faculty);
                acrFlds.SetField("ObrazProgram", abitList.ObrazProgram);
                acrFlds.SetField("StudyForm" + abitList.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abitList.StudyBasisId, "1");

                acrFlds.SetField("Course", abitList.EducYear.ToString());
                acrFlds.SetField("Semester", abitList.SemesterName);

                switch (abitList.StudyLevelId)
                {
                    case 16: { acrFlds.SetField("chbBak", "1"); break; }
                    case 17: { acrFlds.SetField("chbMag", "1"); break; }
                    case 18: { acrFlds.SetField("chbSpec", "1"); break; }
                }

                if (person.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                acrFlds.SetField("HostelAbitYes", (person.HostelAbit ?? false) ? "1" : "0");
                acrFlds.SetField("HostelAbitNo", (person.HostelAbit ?? false) ? "0" : "1");

                acrFlds.SetField("Male", person.Sex ? "1" : "0");
                acrFlds.SetField("Female", person.Sex ? "0" : "1");

                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("BirthPlace", person.BirthPlace);
                acrFlds.SetField("Nationality", person.Nationality);
                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                string Address = string.Format("{0} {1}{2},", (person.Code) ?? "", (person.IsRussia ? (person.Region + ", ") ?? "" : person.Country + ", "), (person.City + ", ") ?? "") +
                      string.Format("{0} {1} {2} {3}", person.Street ?? "", person.House == string.Empty ? "" : "дом " + person.House,
                      person.Korpus == string.Empty ? "" : "корп. " + person.Korpus,
                      person.Flat == string.Empty ? "" : "кв. " + person.Flat);
                string[] splitStr, strSplit;
                splitStr = GetSplittedStrings(Address, 50, 70, 3);
                for (int i = 1; i <= 3; i++)
                    acrFlds.SetField("Address" + i, splitStr[i - 1]);

                string phones = (person.Phone ?? "") + ", e-mail: " + email + ", " + (person.Mobiles ?? "");

                strSplit = GetSplittedStrings(phones, 30, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Phone" + i.ToString(), strSplit[i - 1]);

                strSplit = GetSplittedStrings(person.Parents, 50, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Parents" + i.ToString(), strSplit[i - 1]);

                acrFlds.SetField("CurrentEducationName", person.SchoolName);
                acrFlds.SetField(person.HasAccreditation ? "HasAccred" : "NoAccred", "1");
                string AccredInfo = (person.AccreditationNumber ?? "") +
                    (person.AccreditationDate.HasValue ? " от " + person.AccreditationDate.Value.ToShortDateString() : "");
                acrFlds.SetField("EducationAccreditationNumber", person.HasAccreditation ? AccredInfo : "");
                if (abitList.SemesterName != null)
                    acrFlds.SetField("CurrentCourse", person.EducYear.ToString());

                switch (person.StudyLevelId)
                {
                    case 16: { acrFlds.SetField("CurrentBak", "1"); break; }
                    case 17: { acrFlds.SetField("CurrentMag", "1"); break; }
                    case 18: { acrFlds.SetField("CurrentSpec", "1"); break; }
                }

                acrFlds.SetField("ExitYear", "");//person.SchoolExitYear ?? "");
                acrFlds.SetField("School", "");//person.SchoolName ?? "");
                acrFlds.SetField("EducationDocument", ""); //(person.Series ?? "") + " " + (person.Number ?? ""));
                if (person.HasScholarship)
                    acrFlds.SetField("HasScholarship", "1");
                else
                    acrFlds.SetField("NoScholarship", "1");

                //acrFlds.SetField("Extra", person.AddInfo ?? "");
                //acrFlds.SetField("Copy", "1");

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        } 
        
      //перевод иностранцев (AbitTypeId = 4)
      public static byte[] GetApplicationPDFTransferForeign(Guid appId, string dirPath, Guid PersonId)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abitList = (from x in context.Application
                                join Commit in context.ApplicationCommit on x.CommitId equals Commit.Id
                                join Entry in context.Entry on x.EntryId equals Entry.Id
                                join Semester in context.Semester on Entry.SemesterId equals Semester.Id
                                where x.Id == appId
                                select new
                                {
                                    x.Id,
                                    x.PersonId,
                                    x.Barcode,
                                    Faculty = Entry.FacultyName,
                                    Profession = Entry.LicenseProgramName,
                                    ProfessionCode = Entry.LicenseProgramCode,
                                    ObrazProgram = Entry.ObrazProgramCrypt + " " + Entry.ObrazProgramName,
                                    Specialization = Entry.ProfileName,
                                    Entry.StudyFormId,
                                    Entry.StudyFormName,
                                    Entry.StudyBasisId,
                                    EntryType = (Entry.StudyLevelId == 17 ? 2 : 1),
                                    Entry.StudyLevelId,
                                    CommitIntNumber = Commit.IntNumber,
                                    x.Priority,
                                    x.IsGosLine,
                                    Entry.ComissionId,
                                    ComissionAddress = Entry.Address,
                                    SemesterName = Semester.Name,
                                    EducYear = Semester.EducYear
                                }).OrderBy(x => x.Priority).FirstOrDefault();
 
                string query = "SELECT Email, IsForeign FROM [User] WHERE Id=@Id";
                DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", PersonId } });
                string email = tbl.Rows[0].Field<string>("Email");
                var person = (from x in context.Person
                              join PersonCurrentEducation in context.PersonCurrentEducation on PersonId equals PersonCurrentEducation.PersonId
                              join Semester in context.Semester on PersonCurrentEducation.SemesterId equals Semester.Id
                              where x.Id == PersonId
                              select new
                              {
                                  x.Surname,
                                  x.Name,
                                  x.SecondName,
                                  x.Barcode,
                                  x.PersonAddInfo.HostelAbit,
                                  x.BirthDate,
                                  BirthPlace = x.BirthPlace ?? "",
                                  Sex = x.Sex,
                                  Nationality = x.Nationality.Name,
                                  Country = x.PersonContacts.Country.Name,
                                  PassportType = x.PassportType.Name,
                                  x.PassportSeries,
                                  x.PassportNumber,
                                  x.PassportAuthor,
                                  x.PassportDate,
                                  x.PersonContacts.City,
                                  Region = x.PersonContacts.Region.Name,
                                  x.PersonEducationDocument.FirstOrDefault().PersonHighEducationInfo.ProgramName,
                                  x.PersonContacts.Code,
                                  x.PersonContacts.Street,
                                  x.PersonContacts.House,
                                  x.PersonContacts.Korpus,
                                  x.PersonContacts.Flat,
                                  x.PersonContacts.Phone,
                                  x.PersonContacts.Mobiles,
                                  x.PersonAddInfo.HostelEduc,
                                  x.PersonContacts.Country.IsRussia,
                                  x.PersonEducationDocument.FirstOrDefault().SchoolName,
                                  x.PersonCurrentEducation.HasAccreditation,
                                  x.PersonCurrentEducation.AccreditationDate,
                                  x.PersonCurrentEducation.AccreditationNumber,
                                  x.PersonCurrentEducation.HasScholarship,
                                  x.PersonAddInfo.Parents,
                                  x.PersonAddInfo.AddInfo,
                                  x.PersonEducationDocument.FirstOrDefault().SchoolExitYear,
                                  x.PersonEducationDocument.FirstOrDefault().Number,
                                  x.PersonEducationDocument.FirstOrDefault().Series,
                                  x.PersonCurrentEducation.StudyLevelId,
                                  SemesterName = Semester.Name,
                                  Semester.EducYear,
                                  x.PersonEducationDocument.FirstOrDefault().CountryEducId,
                                  x.PersonEducationDocument.FirstOrDefault().EqualDocumentNumber,
                                  x.PersonEducationDocument.FirstOrDefault().IsEqual
                              }).FirstOrDefault();

                MemoryStream ms = new MemoryStream();
                string dotName = "ApplicationTransferForeign.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (4000000 + abitList.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);

                img.SetAbsolutePosition(280, 780);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abitList.ProfessionCode + " " + abitList.Profession);
                acrFlds.SetField("Specialization", abitList.Specialization);
                acrFlds.SetField("Faculty", abitList.Faculty);
                acrFlds.SetField("ObrazProgram", abitList.ObrazProgram);
                acrFlds.SetField("StudyForm" + abitList.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abitList.StudyBasisId, "1");

                acrFlds.SetField("Course", abitList.EducYear.ToString());
                acrFlds.SetField("Semester", abitList.SemesterName);

                switch (abitList.StudyLevelId)
                {
                    case 16: { acrFlds.SetField("chbBak", "1"); break; }
                    case 17: { acrFlds.SetField("chbMag", "1"); break; }
                    case 18: { acrFlds.SetField("chbSpec", "1"); break; }
                }

                if (person.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                acrFlds.SetField("HostelAbitYes", (person.HostelAbit ?? false) ? "1" : "0");
                acrFlds.SetField("HostelAbitNo", (person.HostelAbit ?? false) ? "0" : "1");

                acrFlds.SetField("Male", person.Sex ? "1" : "0");
                acrFlds.SetField("Female", person.Sex ? "0" : "1");

                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("BirthPlace", person.BirthPlace);
                acrFlds.SetField("Nationality", person.Nationality);
                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                string Address = string.Format("{0} {1}{2},", (person.Code) ?? "", (person.IsRussia ? (person.Region + ", ") ?? "" : person.Country + ", "), (person.City + ", ") ?? "") +
                      string.Format("{0} {1} {2} {3}", person.Street ?? "", person.House == string.Empty ? "" : "дом " + person.House,
                      person.Korpus == string.Empty ? "" : "корп. " + person.Korpus,
                      person.Flat == string.Empty ? "" : "кв. " + person.Flat);
                string[] splitStr, strSplit;
                splitStr = GetSplittedStrings(Address, 50, 70, 3);
                for (int i = 1; i <= 3; i++)
                    acrFlds.SetField("Address" + i, splitStr[i - 1]);

                string phones = (person.Phone ?? "") + ", e-mail: " + email + ", " + (person.Mobiles ?? "");

                strSplit = GetSplittedStrings(phones, 30, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Phone" + i.ToString(), strSplit[i - 1]);

                strSplit = GetSplittedStrings(phones, 70, 70, 2);
                for (int i = 1; i < 3; i++)
                    acrFlds.SetField("Phone" + i.ToString(), strSplit[i - 1]);

                strSplit = GetSplittedStrings(person.Parents, 50, 70, 3);
                for (int i = 1; i <= 3; i++)
                    acrFlds.SetField("Parents" + i.ToString(), strSplit[i - 1]);

                /*strSplit = GetSplittedStrings(person.SocialStatus, 50, 70, 2);*/
                for (int i = 1; i < 3; i++)
                    acrFlds.SetField("SocialStatus" + i.ToString(), "");
                acrFlds.SetField("MaritalStatus", "");

                acrFlds.SetField("CurrentEducationName", person.SchoolName);
                acrFlds.SetField(person.HasAccreditation ? "HasAccred" : "NoAccred", "1");
                string AccredInfo = (person.AccreditationNumber ?? "") +
                    (person.AccreditationDate.HasValue ? person.AccreditationDate.Value.ToShortDateString() : "");
                acrFlds.SetField("EducationAccreditationNumber", person.HasAccreditation ? AccredInfo : "");
                if (abitList.SemesterName != null)
                    acrFlds.SetField("CurrentSemester", person.EducYear.ToString() + " курс, " + person.SemesterName);

                switch (person.StudyLevelId)
                {
                    case 16: { acrFlds.SetField("CurrentBak", "1"); break; }
                    case 17: { acrFlds.SetField("CurrentMag", "1"); break; }
                    case 18: { acrFlds.SetField("CurrentSpec", "1"); break; }
                }

                acrFlds.SetField("ExitYear", "");//person.SchoolExitYear ?? "");
                acrFlds.SetField("School", "");//person.SchoolName ?? "");
                acrFlds.SetField("SchoolName", "");//person.SchoolName ?? "");
                acrFlds.SetField("EducationDocument", "");//(person.Series ?? "") + (person.Number ?? ""));
                acrFlds.SetField("CountryEduc", "");//person.CountryEducId.HasValue ? person.CountryEduc.Name : "");
                /*
                if (person.HasScholarship ?? false)
                    acrFlds.SetField("HasScholarship", "1");
                else
                    acrFlds.SetField("NoScholarship", "1");*/
                acrFlds.SetField("Extra", person.AddInfo ?? "");

                strSplit = GetSplittedStrings(person.Parents, 30, 70, 3);
                for (int i = 1; i < 4; i++)
                    acrFlds.SetField("Parents", strSplit[i - 1]);

                if (person.IsEqual && person.CountryEducId != 193)
                {
                    acrFlds.SetField("HasEqual", "1");
                    acrFlds.SetField("EqualNumber", person.EqualDocumentNumber);
                }
                else
                {
                    acrFlds.SetField("NoEqual", "1");
                }
                //acrFlds.SetField("Copy", "1");

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        }

        //восстановление (AbitTypeId = 5)
   /*     public static byte[] GetApplicationPDFRecover(Guid appId, string dirPath)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abit = (from x in context.Application
                            where x.Id == appId
                            select new
                            {
                                x.PersonId,
                                x.Barcode,
                                Faculty = x.C_Entry.FacultyName,
                                Profession = x.C_Entry.LicenseProgramName,
                                ProfessionCode = x.C_Entry.LicenseProgramCode,
                                x.C_Entry.ObrazProgramCrypt,
                                ObrazProgram = x.C_Entry.ObrazProgramName,
                                Specialization = x.C_Entry.ProfileName,
                                StudyFormId = x.C_Entry.StudyFormId,
                                StudyFormName = x.C_Entry.StudyFormName,
                                x.C_Entry.StudyBasisId,
                                x.EntryType,
                                x.HostelEduc,
                                SemesterName = x.C_Entry.Semester.Name,
                                EducYear = x.C_Entry.Semester.EducYear
                            }).FirstOrDefault();
                string email = context.User.Where(x => x.Id == abit.PersonId).Select(x => x.Email).FirstOrDefault();

                var person = context.Person.Where(x => x.Id == abit.PersonId).FirstOrDefault();

                var PersonContacts = person.PersonContacts;
                if (PersonContacts == null)
                    PersonContacts = new PersonContacts();

                MemoryStream ms = new MemoryStream();
                string dotName = "ApplicationRecover.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (5000000 + abit.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);

                img.SetAbsolutePosition(280, 780);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abit.ProfessionCode + " " + abit.Profession);
                acrFlds.SetField("Specialization", abit.Specialization);
                acrFlds.SetField("Faculty", abit.Faculty);
                acrFlds.SetField("ObrazProgram", abit.ObrazProgramCrypt + " " + abit.ObrazProgram);
                acrFlds.SetField("StudyForm" + abit.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abit.StudyBasisId, "1");

                acrFlds.SetField("Course", abit.EducYear.ToString());
                acrFlds.SetField("Semester", abit.SemesterName);

                if (abit.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                if (person.PersonAddInfo.HostelAbit ?? false)
                    acrFlds.SetField("HostelAbitYes", "1");
                else
                    acrFlds.SetField("HostelAbitNo", "1");

                if (person.Sex ?? false)
                    acrFlds.SetField("Male", "1");
                else
                    acrFlds.SetField("Female", "1");

                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("BirthPlace", person.BirthPlace);
                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                string Address = (PersonContacts.Code ?? "") + " " +
                    (PersonContacts.City ?? "") + " " +
                    (PersonContacts.Street ?? "") + " " +
                    (PersonContacts.House ?? "") + " " +
                    (PersonContacts.Korpus ?? "") + " " +
                    (PersonContacts.Flat ?? "");

                string[] strSplit = GetSplittedStrings(Address, 50, 70, 2);

                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Address" + i.ToString(), strSplit[i - 1]);

                string phones = (PersonContacts.Phone ?? "") + ", e-mail: " + email + ",  + (person.Mobiles ?? "");

                strSplit = GetSplittedStrings(phones, 30, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Phone" + i.ToString(), strSplit[i - 1]);

                var PersonDisorderInfo = person.PersonDisorderInfo;
                if (PersonDisorderInfo == null)
                    PersonDisorderInfo = new PersonDisorderInfo();

                acrFlds.SetField("DisorderYear", PersonDisorderInfo.YearOfDisorder);
                acrFlds.SetField("DisorderProgram1", PersonDisorderInfo.EducationProgramName ?? "");

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        } */
        public static byte[] GetApplicationPDFRecover(Guid appId, string dirPath, Guid PersonId)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abitList = (from x in context.Application
                                join Commit in context.ApplicationCommit on x.CommitId equals Commit.Id
                                join Entry in context.Entry on x.EntryId equals Entry.Id
                                join Semester in context.Semester on Entry.SemesterId equals Semester.Id
                                where x.Id == appId
                                select new
                                {
                                    x.Id,
                                    x.PersonId,
                                    x.Barcode,
                                    Faculty = Entry.FacultyName,
                                    Profession = Entry.LicenseProgramName,
                                    ProfessionCode = Entry.LicenseProgramCode,
                                    ObrazProgram = Entry.ObrazProgramCrypt + " " + Entry.ObrazProgramName,
                                    Specialization = Entry.ProfileName,
                                    Entry.StudyFormId,
                                    Entry.StudyFormName,
                                    Entry.StudyBasisId,
                                    EntryType = (Entry.StudyLevelId == 17 ? 2 : 1),
                                    Entry.StudyLevelId,
                                    CommitIntNumber = Commit.IntNumber,
                                    x.Priority,
                                    x.IsGosLine,
                                    Entry.ComissionId,
                                    ComissionAddress = Entry.Address,
                                    SemesterName = Semester.Name,
                                    EducYear = Semester.EducYear
                                }).OrderBy(x => x.Priority).FirstOrDefault();
 
                string query = "SELECT Email, IsForeign FROM [User] WHERE Id=@Id";
                DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", PersonId } });
                string email = tbl.Rows[0].Field<string>("Email");
                var person = (from x in context.Person 

                              join PersonDisorderInfo in context.PersonDisorderInfo on PersonId equals PersonDisorderInfo.PersonId into Sem2
                              from Sem in Sem2.DefaultIfEmpty()

                              where x.Id == PersonId
                              select new
                              {
                                  x.Surname,
                                  x.Name,
                                  x.SecondName,
                                  x.Barcode,
                                  x.PersonAddInfo.HostelAbit,
                                  x.BirthDate,
                                  BirthPlace = x.BirthPlace ?? "",
                                  Sex = x.Sex,
                                  Nationality = x.Nationality.Name,
                                  Country = x.PersonContacts.Country.Name,
                                  PassportType = x.PassportType.Name,
                                  x.PassportSeries,
                                  x.PassportNumber,
                                  x.PassportAuthor,
                                  x.PassportDate,
                                  x.PersonContacts.City,
                                  Region = x.PersonContacts.Region.Name,
                                  x.PersonEducationDocument.FirstOrDefault().PersonHighEducationInfo.ProgramName,
                                  x.PersonContacts.Code,
                                  x.PersonContacts.Street,
                                  x.PersonContacts.House,
                                  x.PersonContacts.Korpus,
                                  x.PersonContacts.Flat,
                                  x.PersonContacts.Phone,
                                  x.PersonContacts.Mobiles,
                                  x.PersonEducationDocument.FirstOrDefault().SchoolExitYear,
                                  x.PersonEducationDocument.FirstOrDefault().SchoolName,
                                  x.PersonAddInfo.StartEnglish,
                                  x.PersonAddInfo.EnglishMark,
                                  x.PersonEducationDocument.FirstOrDefault().IsEqual,
                                  x.PersonEducationDocument.FirstOrDefault().EqualDocumentNumber,
                                  CountryEduc = x.PersonEducationDocument.FirstOrDefault().CountryEduc != null ? x.PersonEducationDocument.FirstOrDefault().CountryEduc.Name : "",
                                  x.PersonEducationDocument.FirstOrDefault().CountryEducId,
                                  Qualification = x.PersonEducationDocument.FirstOrDefault().PersonHighEducationInfo.Qualification != null ? x.PersonEducationDocument.FirstOrDefault().PersonHighEducationInfo.Qualification.Name : "",
                                  x.PersonEducationDocument.FirstOrDefault().SchoolTypeId,
                                  EducationDocumentSeries = x.PersonEducationDocument.FirstOrDefault().Series,
                                  EducationDocumentNumber = x.PersonEducationDocument.FirstOrDefault().Number,
                                  x.PersonAddInfo.HostelEduc,
                                  x.PersonContacts.Country.IsRussia,
                                  x.PersonDisorderInfo.YearOfDisorder,
                                  x.PersonDisorderInfo.EducationProgramName
                              }).FirstOrDefault();

                MemoryStream ms = new MemoryStream();
                string dotName = "ApplicationRecover.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (5000000 + abitList.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);

                img.SetAbsolutePosition(280, 780);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abitList.ProfessionCode + " " + abitList.Profession);
                acrFlds.SetField("Specialization", abitList.Specialization);
                acrFlds.SetField("Faculty", abitList.Faculty);
                acrFlds.SetField("ObrazProgram", abitList.ObrazProgram);
                acrFlds.SetField("StudyForm" + abitList.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abitList.StudyBasisId, "1");

                acrFlds.SetField("Course", abitList.EducYear.ToString());
                acrFlds.SetField("Semester", abitList.SemesterName);

                if (person.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                acrFlds.SetField("HostelAbitYes", (person.HostelAbit ?? false) ? "1" : "0");
                acrFlds.SetField("HostelAbitNo", (person.HostelAbit ?? false) ? "0" : "1");

                acrFlds.SetField("Male", person.Sex ? "1" : "0");
                acrFlds.SetField("Female", person.Sex ? "0" : "1");

                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("BirthPlace", person.BirthPlace);
                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                string Address = string.Format("{0} {1}{2},", (person.Code) ?? "", (person.IsRussia ? (person.Region + ", ") ?? "" : person.Country + ", "), (person.City + ", ") ?? "") +
                    string.Format("{0} {1} {2} {3}", person.Street ?? "", person.House == string.Empty ? "" : "дом " + person.House,
                    person.Korpus == string.Empty ? "" : "корп. " + person.Korpus,
                    person.Flat == string.Empty ? "" : "кв. " + person.Flat);
                string[] splitStr, strSplit;
                splitStr = GetSplittedStrings(Address, 50, 70, 3);
                for (int i = 1; i <= 3; i++)
                    acrFlds.SetField("Address" + i, splitStr[i - 1]);

                string phones = (person.Phone ?? "") + ", e-mail: " + email + ", " + (person.Mobiles ?? "") ;

                strSplit = GetSplittedStrings(phones, 30, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Phone" + i.ToString(), strSplit[i - 1]);

                acrFlds.SetField("DisorderYear", person.YearOfDisorder);

                strSplit = GetSplittedStrings(person.EducationProgramName ?? "", 80, 80, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("DisorderProgram" + i.ToString(), strSplit[i - 1]);

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        }

        //перевод на бюджет (AbitTypeId = 6)
       /* public static byte[] GetApplicationPDFChangeStudyBasis(Guid appId, string dirPath)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abit = (from x in context.Application
                            where x.Id == appId
                            select new
                            {
                                x.PersonId,
                                x.Barcode,
                                Faculty = x.C_Entry.FacultyName,
                                Profession = x.C_Entry.LicenseProgramName,
                                ProfessionCode = x.C_Entry.LicenseProgramCode,
                                ObrazProgram = x.C_Entry.ObrazProgramName,
                                Specialization = x.C_Entry.ProfileName,
                                StudyFormId = x.C_Entry.StudyFormId,
                                StudyFormName = x.C_Entry.StudyFormName,
                                x.C_Entry.StudyBasisId,
                                x.EntryType,
                                x.HostelEduc,
                            }).FirstOrDefault();
                string email = context.User.Where(x => x.Id == abit.PersonId).Select(x => x.Email).FirstOrDefault();

                var person = context.Person.Where(x => x.Id == abit.PersonId).FirstOrDefault();

                var PersonContacts = person.PersonContacts;
                if (PersonContacts == null)
                    PersonContacts = new PersonContacts();

                MemoryStream ms = new MemoryStream();
                string dotName = "ApplicationChangeStudyBasis.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (6000000 + abit.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);
                if (abit.EntryType == 2)
                    img.SetAbsolutePosition(420, 720);
                else
                    img.SetAbsolutePosition(440, 740);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "")+ " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abit.ProfessionCode + " " + abit.Profession);
                //if (abit.EntryType != 2)
                acrFlds.SetField("Specialization", abit.Specialization);
                acrFlds.SetField("Faculty", abit.Faculty);
                acrFlds.SetField("ObrazProgram", abit.ObrazProgram);
                acrFlds.SetField("StudyForm" + abit.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abit.StudyBasisId, "1");

                if (abit.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                if (person.PersonAddInfo.HostelAbit ?? false)
                    acrFlds.SetField("HostelAbitYes", "1");
                else
                    acrFlds.SetField("HostelAbitNo", "1");

                if (person.Sex ?? false)
                    acrFlds.SetField("Male", "1");
                else
                    acrFlds.SetField("Female", "1");

                string Reason = "";
                if (person.PersonChangeStudyFormReason != null)
                    Reason = person.PersonChangeStudyFormReason.Reason;

                string[] ss = GetSplittedStrings(Reason, 60, 60, 3);
                for (int i = 1; i <= 3; i++)
                {
                    acrFlds.SetField("Reason" + i, ss[i - 1]);
                }

                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("BirthPlace", person.BirthPlace);
                acrFlds.SetField("Nationality", person.Nationality.Name);
                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                //acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                //acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                string Address = (PersonContacts.Code ?? "") + " " +
                    (PersonContacts.City ?? "") + " " +
                    (PersonContacts.Street ?? "") + " " +
                    (PersonContacts.House ?? "") + " " +
                    (PersonContacts.Korpus ?? "") + " " +
                    (PersonContacts.Flat ?? "");

                string[] splitted = GetSplittedStrings(Address, 50, 70, 2);
                for (int i = 1; i < 2; i++)
                {
                    acrFlds.SetField("Address" + i.ToString(), splitted[i - 1]);
                }

                string phones = (PersonContacts.Phone ?? "") + ", e-mail: " + email + ", " + (person.Mobiles ?? "");
                splitted = GetSplittedStrings(phones, 30, 70, 2);
                for (int i = 1; i < 2; i++)
                {
                    acrFlds.SetField("Phone" + i.ToString(), splitted[i - 1]);
                }

                var PersonEducationDocument = person.PersonEducationDocument;
                if (PersonEducationDocument == null)
                    PersonEducationDocument = new PersonEducationDocument();

                var PersonAddInfo = person.PersonAddInfo;
                if (PersonAddInfo == null)
                    PersonAddInfo = new PersonAddInfo();

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        }*/
        public static byte[] GetApplicationPDFChangeStudyBasis(Guid appId, string dirPath, Guid PersonId)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abitList = (from x in context.Application
                                join Commit in context.ApplicationCommit on x.CommitId equals Commit.Id
                                join Entry in context.Entry on x.EntryId equals Entry.Id
                                join Semester in context.Semester on Entry.SemesterId equals Semester.Id
                                where x.Id == appId
                                select new
                                {
                                    x.Id,
                                    x.PersonId,
                                    x.Barcode,
                                    Faculty = Entry.FacultyName,
                                    Profession = Entry.LicenseProgramName,
                                    ProfessionCode = Entry.LicenseProgramCode,
                                    ObrazProgram = Entry.ObrazProgramCrypt + " " + Entry.ObrazProgramName,
                                    Specialization = Entry.ProfileName,
                                    Entry.StudyFormId,
                                    Entry.StudyFormName,
                                    Entry.StudyBasisId,
                                    EntryType = (Entry.StudyLevelId == 17 ? 2 : 1),
                                    Entry.StudyLevelId,
                                    CommitIntNumber = Commit.IntNumber,
                                    x.Priority,
                                    x.IsGosLine,
                                    Entry.ComissionId,
                                    ComissionAddress = Entry.Address,
                                    SemesterName = Semester.Name,
                                    EducYear = Semester.EducYear
                                }).OrderBy(x => x.Priority).FirstOrDefault();
 
                string query = "SELECT Email, IsForeign FROM [User] WHERE Id=@Id";
                DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", PersonId } });
                string email = tbl.Rows[0].Field<string>("Email");
                var person = (from x in context.Person
                              //join PersonChangeStudyFormReason in context.PersonChangeStudyFormReason on PersonId equals PersonChangeStudyFormReason.PersonId
                              where x.Id == PersonId
                              select new
                              {
                                  x.Surname,
                                  x.Name,
                                  x.SecondName,
                                  x.Barcode,
                                  x.PersonAddInfo.HostelAbit,
                                  x.BirthDate,
                                  BirthPlace = x.BirthPlace ?? "",
                                  Sex = x.Sex,
                                  Nationality = x.Nationality.Name,
                                  Country = x.PersonContacts.Country.Name,
                                  PassportType = x.PassportType.Name,
                                  x.PassportSeries,
                                  x.PassportNumber,
                                  x.PassportAuthor,
                                  x.PassportDate,
                                  x.PersonContacts.City,
                                  Region = x.PersonContacts.Region.Name,
                                  x.PersonEducationDocument.FirstOrDefault().PersonHighEducationInfo.ProgramName,
                                  x.PersonContacts.Code,
                                  x.PersonContacts.Street,
                                  x.PersonContacts.House,
                                  x.PersonContacts.Korpus,
                                  x.PersonContacts.Flat,
                                  x.PersonContacts.Phone,
                                  x.PersonContacts.Mobiles,
                                  x.PersonAddInfo.HostelEduc,
                                  x.PersonContacts.Country.IsRussia,
                                  x.PersonChangeStudyFormReason.Reason,
                                  x.PersonCurrentEducation.ProfileName
                              }).FirstOrDefault();

                MemoryStream ms = new MemoryStream();
                string dotName = "ApplicationChangeStudyBasis.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (6000000 + abitList.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);
                if (abitList.EntryType == 2)
                    img.SetAbsolutePosition(420, 720);
                else
                    img.SetAbsolutePosition(440, 740);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abitList.ProfessionCode + " " + abitList.Profession);
                acrFlds.SetField("Specialization", person.ProfileName);
                acrFlds.SetField("Faculty", abitList.Faculty);
                acrFlds.SetField("ObrazProgram", abitList.ObrazProgram);
                acrFlds.SetField("StudyForm" + abitList.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abitList.StudyBasisId, "1");
                acrFlds.SetField("Course", abitList.EducYear.ToString());
                acrFlds.SetField("Semester", abitList.SemesterName);
                if (person.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                acrFlds.SetField("HostelAbitYes", (person.HostelAbit ?? false) ? "1" : "0");
                acrFlds.SetField("HostelAbitNo", (person.HostelAbit ?? false) ? "0" : "1");

                acrFlds.SetField("Male", person.Sex ? "1" : "0");
                acrFlds.SetField("Female", person.Sex ? "0" : "1");

                string Reason = "";
                if (!String.IsNullOrEmpty(person.Reason))
                    Reason = person.Reason;

                string[] ss = GetSplittedStrings(Reason, 95, 95, 3);
                for (int i = 1; i <= 3; i++)
                {
                    acrFlds.SetField("Reason" + i, ss[i - 1]);
                }

                acrFlds.SetField("BirthPlace", person.BirthPlace);
                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("Nationality", person.Nationality);
                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                string Address = string.Format("{0} {1}{2},", (person.Code) ?? "", (person.IsRussia ? (person.Region + ", ") ?? "" : person.Country + ", "), (person.City + ", ") ?? "") +
                    string.Format("{0} {1} {2} {3}", person.Street ?? "", person.House == string.Empty ? "" : "дом " + person.House,
                    person.Korpus == string.Empty ? "" : "корп. " + person.Korpus,
                    person.Flat == string.Empty ? "" : "кв. " + person.Flat);
                string[] splitStr, strSplit;
                splitStr = GetSplittedStrings(Address, 50, 70, 3);
                for (int i = 1; i <= 3; i++)
                    acrFlds.SetField("Address" + i, splitStr[i - 1]);

                string phones = (person.Phone ?? "") + ", e-mail: " + email + ", " + (person.Mobiles ?? "");

                strSplit = GetSplittedStrings(phones, 30, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Phone" + i.ToString(), strSplit[i - 1]);

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        }

        //смена обр. программы (AbitTypeId = 7)
        public static byte[] GetApplicationPDFChangeObrazProgram(Guid appId, string dirPath, Guid PersonId)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abitList = (from x in context.Application
                                join Commit in context.ApplicationCommit on x.CommitId equals Commit.Id
                                join Entry in context.Entry on x.EntryId equals Entry.Id
                                join Semester in context.Semester on Entry.SemesterId equals Semester.Id
                                where x.Id == appId
                                select new
                                {
                                    x.Id,
                                    x.PersonId,
                                    x.Barcode,
                                    Faculty = Entry.FacultyName,
                                    Profession = Entry.LicenseProgramName,
                                    ProfessionCode = Entry.LicenseProgramCode,
                                    ObrazProgram = Entry.ObrazProgramCrypt + " " + Entry.ObrazProgramName,
                                    Specialization = Entry.ProfileName,
                                    Entry.StudyFormId,
                                    Entry.StudyFormName,
                                    Entry.StudyBasisId,
                                    EntryType = (Entry.StudyLevelId == 17 ? 2 : 1),
                                    Entry.StudyLevelId,
                                    CommitIntNumber = Commit.IntNumber,
                                    x.Priority,
                                    x.IsGosLine,
                                    Entry.ComissionId,
                                    ComissionAddress = Entry.Address,
                                    SemesterName = Semester.Name,
                                    EducYear = Semester.EducYear
                                }).OrderBy(x => x.Priority).FirstOrDefault();

                string query = "SELECT Email, IsForeign FROM [User] WHERE Id=@Id";
                DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", PersonId } });
                string email = tbl.Rows[0].Field<string>("Email");
                var person = (from x in context.Person
                              join Sem in context.Semester on x.PersonCurrentEducation.SemesterId equals Sem.Id into Sem2
                              from Sem in Sem2.DefaultIfEmpty()  
                              where x.Id == PersonId
                              select new
                              {
                                  x.Surname,
                                  x.Name,
                                  x.SecondName,
                                  x.Barcode,
                                  x.PersonAddInfo.HostelAbit,
                                  x.BirthDate,
                                  BirthPlace = x.BirthPlace ?? "",
                                  Sex = x.Sex,
                                  Nationality = x.Nationality.Name,
                                  Country = x.PersonContacts.Country.Name,
                                  PassportType = x.PassportType.Name,
                                  x.PassportSeries,
                                  x.PassportNumber,
                                  x.PassportAuthor,
                                  x.PassportDate,
                                  x.PersonContacts.City,
                                  Region = x.PersonContacts.Region.Name,
                                  x.PersonEducationDocument.FirstOrDefault().PersonHighEducationInfo.ProgramName,
                                  x.PersonContacts.Code,
                                  x.PersonContacts.Street,
                                  x.PersonContacts.House,
                                  x.PersonContacts.Korpus,
                                  x.PersonContacts.Flat,
                                  x.PersonContacts.Phone,
                                  x.PersonContacts.Mobiles,
                                  x.PersonAddInfo.HostelEduc,
                                  x.PersonContacts.Country.IsRussia,
                                  x.PersonCurrentEducation.LicenseProgramId,
                                  x.PersonCurrentEducation.SemesterId,
                                  Sem.EducYear
                              }).FirstOrDefault();

                MemoryStream ms = new MemoryStream();
                string dotName = "ApplicationChangeObrazProgram.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (7000000 + abitList.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);
                if (abitList.EntryType == 2)
                    img.SetAbsolutePosition(420, 720);
                else
                    img.SetAbsolutePosition(440, 740);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abitList.ProfessionCode + " " + abitList.Profession);
                acrFlds.SetField("ObrazProgram", abitList.ObrazProgram);
                //if (abit.EntryType != 2)
                acrFlds.SetField("Specialization", abitList.Specialization);
                acrFlds.SetField("Faculty", abitList.Faculty);
                acrFlds.SetField("ObrazProgram", abitList.ObrazProgram);
                acrFlds.SetField("StudyForm" + abitList.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abitList.StudyBasisId, "1");

                if (person.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                acrFlds.SetField("HostelAbitYes", (person.HostelAbit ?? false) ? "1" : "0");
                acrFlds.SetField("HostelAbitNo", (person.HostelAbit ?? false) ? "0" : "1");

                acrFlds.SetField("Male", person.Sex ? "1" : "0");
                acrFlds.SetField("Female", person.Sex ? "0" : "1");

                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("BirthPlace", person.BirthPlace);

                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                acrFlds.SetField("Nationality", person.Nationality);

                string Address = string.Format("{0} {1}{2},", (person.Code) ?? "", (person.IsRussia ? (person.Region + ", ") ?? "" : person.Country + ", "), (person.City + ", ") ?? "") +
                                    string.Format("{0} {1} {2} {3}", person.Street ?? "", person.House == string.Empty ? "" : "дом " + person.House,
                                    person.Korpus == string.Empty ? "" : "корп. " + person.Korpus,
                                    person.Flat == string.Empty ? "" : "кв. " + person.Flat);
                string[] splitStr, strSplit;
                splitStr = GetSplittedStrings(Address, 50, 70, 3);
                for (int i = 1; i <= 3; i++)
                    acrFlds.SetField("Address" + i, splitStr[i - 1]);

                string phones = (person.Phone ?? "") + ", e-mail: " + email + ", " + (person.Mobiles ?? "");

                strSplit = GetSplittedStrings(phones, 70, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Phone" + i.ToString(), strSplit[i - 1]);

                acrFlds.SetField("CurrentProfession", context.Entry.Where(x => x.LicenseProgramId == person.LicenseProgramId)
                    .Select(x => x.LicenseProgramName).FirstOrDefault());
                acrFlds.SetField("CurrentCourse", person.EducYear.HasValue ? person.EducYear.ToString() : "-");
                //acrFlds.SetField("Original", "0");
                //acrFlds.SetField("Copy", "0");
                //acrFlds.SetField("Language", PersonEducationDocument.Language.Name ?? "");
                //acrFlds.SetField("Extra", PersonAddInfo.AddInfo ?? "");

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        }

        public static string[] GetSplittedStrings(string sourceStr, int firstStrLen, int strLen, int numOfStrings)
        {
            sourceStr = sourceStr ?? "";
            string[] retStr = new string[numOfStrings];
            int index = 0, startindex = 0;
            for (int i = 0; i < numOfStrings; i++)
            {
                if (sourceStr.Length > startindex && startindex >= 0)
                {
                    int rowLength = firstStrLen;//длина первой строки
                    if (i > 1) //длина остальных строк одинакова
                        rowLength = strLen;
                    index = startindex + rowLength;
                    if (index < sourceStr.Length)
                    {
                        index = sourceStr.IndexOf(" ", index);
                        string val = index > 0 ? sourceStr.Substring(startindex, index - startindex) : sourceStr.Substring(startindex);
                        retStr[i] = val;
                    }
                    else
                        retStr[i] = sourceStr.Substring(startindex);
                }
                startindex = index;
            }

            return retStr;
        }

    }
}
