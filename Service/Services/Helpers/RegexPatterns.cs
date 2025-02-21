using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Helpers
{
    public static class RegexPatterns
    {
        public static readonly Dictionary<string, string> Patterns = new()
        {
           { "FullName",
             @"(?i)(?:\bAd\b|\bSoyad\b|\bFirst Name\b|\bLast Name\b|\bFull Name\b|\bİsim\b|\bAd və soyad\b)[\s\.:,;\-]*([\p{L}\p{M}'\-]+(?:\s+[\p{L}\p{M}'\-]+)*)(?=\s*(?:Email|E-mail|Phone|Mobil|Tel|$))" },

           { "Email", @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}" },

           { "Description", @"(?i)(?:\bHaqqında\b|\bAbout\b|\bBio\b|\bDescription\b|\bMən\b|\bÖzüm\b|\bTəcrübə\b|\bİxtisas\b|\bPeşəkar inkişaf\b|\bÖz haqqımda\b)[\s.:,-]*(.+?)(?=\b(Education|Skills|Experience|Work History|Bacarıqlar)\b|$)" },

           { "PhoneNumbers", @"(?i)(?:Phone|Mobil|Tel|Telefon|Əlaqə|Əlaqə nömrəsi)[^\n:]*[:\-]?\s*((?:\+994[\s\-]?\(?\d{2,3}\)?[\s\-]?\d{3}[\s\-]?\d{2}[\s\-]?\d{2})|(?:\b0(10|50|51|55|70|77|99|90|60|80|81|95|96|97|98|99)\b[\s\-]?\d{3}[\s\-]?\d{2}[\s\-]?\d{2}))" },

           { "Birthday", @"(?i)(?:Birth Date|Date of Birth|Doğum tarixi|Tarix)[^\n:]*[:\-]?\s*(\d{1,2}[-./]\d{1,2}[-./]\d{2,4})" },

           { "BirthPlace", @"(?i)(?:Birth place|Doğum yeri|Məkan|City|Country)[^\n:]*[:\-]?\s*([\p{L}\s\-]+)(?!\s*Email)" },

           { "Gender", @"(?i)(?:Gender|Cins|Sex)[^\n:]*[:\-]?\s*(Male|Female|Kişi|Qadın)" },

           { "MaritalStatus", @"(?i)(?:Marital Status|Ailə vəziyyəti|Evli|Subay|Boşanmış|Dul)[^\n:]*[:\-]?\s*(Married|Single|Evli|Subay|Divorced|Widowed)" },

           { "DriverLicense", @"(?i)(?:Driver License|Sürücülük vəsiqəsi|Kateqoriya)[^\n:]*[:\-]?\s*(A|B|C|D|BE|CE|AB|ABC|None)" },

           { "Education", @"(?i)(?:Education|Təhsil|University|School|Məktəb|İxtisas|Degree)[^\n:]*[:\-]?\s*(.+?)(?=\b(Skills|Experience|Certificates|Languages)\b|$)" },

           { "Skills",
             @"(?is)(?:\bSkills\b|\bBacarıqlar\b|\bBacarıq\b|\bİxtisas\b|\bPeşə\b|\bTexniki bacarıqlar\b)[\s\.:,;\-]*(.+?)(?=\b(Certificates|Experience|Languages|Education|Work\s?Experience)\b|$)" },

           { "Languages",
             @"(?i)(?:\bLanguages\b|\bDillər\b|\bDil bilikləri\b|\bLanguage skills\b|\bLinguistic skills\b|\bXarici dillər\b)[^\n:]*[:\-]?\s*([\p{L}\s,;]+?)(?=\s*\bAbout\b\s*\bMe\b|$|\r?\n)" },

           { "WorkExperience",
             @"(?is)(?:\bWork Experience\b|\bExperience\b|\bİş təcrübəsi\b|\bWork history\b|\bTəcrübə\b|\bİş tarixi\b)[\s:,-]*(.+?)(?=\b(Certificates|Skills|Languages|Education)\b|$)" },

           { "JobTitle", @"(?i)(?:Position|Job Title|Vəzifə|Peşə|İş adı|İş mövqeyi)[^\n:]*[:\-]?\s*([\p{L},\s\-]+)" },

           { "Certificates", @"(?i)(?:Certificates|Sertifikatlar|Diplomlar|Courses|Təlimlər|Peşə sertifikatları)[^\n:]*[:\-]?\s*(.+?)(?=\b(Skills|Experience|Languages)\b|$)" }

        };
    }
}
