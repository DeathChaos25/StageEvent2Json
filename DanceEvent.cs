using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.Tracing;

public class DanceEvent
{
    public float EvtTiming1 { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public EvtCommand EvtCommand1 { get; set; }

    public CharID? TargetID { get; set; }
    public ushort? EvtSubCommand1 { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public PartnerType? PartnerID { get; set; }
    public uint? EvtCommand2 { get; set; }
    public float EvtTiming2 { get; set; }

    public DanceEvent() { }

    public DanceEvent(BinaryReader reader)
    {
        EvtTiming1 = reader.ReadSingle();
        EvtCommand1 = (EvtCommand)reader.ReadUInt16();

        if (EvtCommand1 == EvtCommand.CharacterAppear || EvtCommand1 == EvtCommand.CharacterDisappear)
        {
            TargetID = (CharID)reader.ReadUInt16();
        }
        else
        {
            EvtSubCommand1 = reader.ReadUInt16();
        }

        if (EvtCommand1 == EvtCommand.DancePartnerAppear)
        {
            PartnerID = (PartnerType)reader.ReadUInt32();
        }
        else
        {
            EvtCommand2 = reader.ReadUInt32();
        }

        EvtTiming2 = reader.ReadSingle();
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(EvtTiming1);
        writer.Write((ushort)EvtCommand1);

        if (EvtCommand1 == EvtCommand.CharacterAppear || EvtCommand1 == EvtCommand.CharacterDisappear)
        {
            writer.Write((ushort)TargetID.GetValueOrDefault());
        }
        else
        {
            writer.Write(EvtSubCommand1.GetValueOrDefault());
        }

        if (EvtCommand1 == EvtCommand.DancePartnerAppear)
        {
            writer.Write((uint)PartnerID.GetValueOrDefault());
        }
        else
        {
            writer.Write(EvtCommand2.GetValueOrDefault());
        }

        writer.Write(EvtTiming2);
    }
}

public enum EvtCommand : ushort
{
    CharacterAppear = 0x2,
    DancePartnerAppear = 0x4,
    CharacterDisappear = 0x102,
}

public enum PartnerType : uint
{
    FirstPartner = 0,
    SecondPartner = 1,
}

public enum CharID : ushort
{
    MainDancer = 0,
    Yu = 1,
    Yosuke = 2,
    Chie = 3,
    Yukiko = 4,
    Rise = 5,
    Kanji = 6,
    Naoto = 7,
    Teddie = 8,
    Kanami = 9,
    Nanako = 10,
    Marie = 12,
    Adachi = 13,
    Miku = 14,
    MakotoYuki = 101,
    Yukari = 102,
    Junpei = 103,
    Akihiko = 104,
    Mitsuru = 105,
    Fuuka = 106,
    Aigis = 107,
    Ken = 108,
    KoromaruDummy = 109,
    Shinji = 110,
    Elizabeth = 111,
    Theodore = 112,
    Labrys = 113,
    MikuP3D = 114,
    Sho = 115,
    Margaret = 116,
    Ren = 201,
    Ryuji = 202,
    Morgana = 203,
    Ann = 204,
    Yusuke = 205,
    Makoto = 206,
    Haru = 207,
    Futaba = 208,
    Akechi = 209,
    Justine = 210,
    Caroline = 211,
    Lavenza = 212,
}