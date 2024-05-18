using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageEvent2Json
{
    public class TblFile
    {
        public string Magic { get; set; }
        public StageEvent StageEvent { get; set; }

        public TblFile() { }

        public TblFile(BinaryReader reader)
        {
            Magic = new string(reader.ReadChars(4));

            if (Magic == "DEFT")
            {
                StageEvent = new StageEvent(reader);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Magic.ToCharArray());

            if (Magic == "DEFT")
            {
                StageEvent.Write(writer);
            }
        }
    }
}
