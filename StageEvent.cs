using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageEvent2Json
{
    public class StageEvent
    {
        public uint Field04 { get; set; }
        public uint NumEvents { get; set; }
        public uint Field0C { get; set; }
        public List<DanceEvent> DanceEvents { get; set; } = new List<DanceEvent>();

        public StageEvent() { }

        public StageEvent(BinaryReader reader)
        {
            Field04 = reader.ReadUInt32();
            NumEvents = reader.ReadUInt32();
            Field0C = reader.ReadUInt32();

            for (int i = 0; i < NumEvents; i++)
            {
                DanceEvents.Add(new DanceEvent(reader));
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Field04);
            writer.Write(NumEvents);
            writer.Write(Field0C);

            foreach (var danceEvent in DanceEvents)
            {
                danceEvent.Write(writer);
            }
        }
    }
}
