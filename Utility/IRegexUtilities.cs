using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bilog.Utility
{
    public interface IRegexUtilities
    {
        Task<bool> IsValidEmail(string strIn);
        string DomainMapper(Match match);
    }
}