using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ostgui
{
    public class Emo
    {
        readonly int[] codes;

        public Emo(int[] codes)
        {
            this.codes = codes;
        }

        public Emo(int code)
        {
            codes = new int[] { code };
        }

        public override string ToString()
        {
            if (codes == null)
            {
                return string.Empty;
            }
            var sb = new StringBuilder(codes.Length);
            foreach (var code in codes)
            {
                sb.Append(Char.ConvertFromUtf32(code));
            }
            return sb.ToString();
        }
    }
}
